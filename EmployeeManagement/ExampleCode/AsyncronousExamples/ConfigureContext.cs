// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace EmployeeManagement.ExampleCode.AsyncronousExamples;

public class MockButton()
{
    public bool Enabled { get; set; }
}

public class ConfigureContext
{
    // Earlier in this article, I briefly explained how the “context” is captured by default when an incomplete Task is awaited, and that this captured context is
    // used to resume the async method. The example in Figure 3 shows how resuming on the context clashes with synchronous blocking to cause a deadlock.
    // This context behavior can also cause another problem—one of performance. As asynchronous GUI applications grow larger, you might find many small parts of
    // async methods all using the GUI thread as their context. This can cause sluggishness as responsiveness suffers from “thousands of paper cuts.”

    //To mitigate this, await the result of ConfigureAwait whenever you can. The following code snippet illustrates the default context behavior and the use of
    //ConfigureAwait:
    async Task MyMethodAsync()
    {
        // Code here runs in the original context.
        await Task.Delay(1000);
        // Code here runs in the original context.
        await Task.Delay(1000).ConfigureAwait(
            continueOnCapturedContext: false);
        // Code here runs without the original
        // context (in this case, on the thread pool).
    }
    
    // By using ConfigureAwait, you enable a small amount of parallelism: Some asynchronous code can run in parallel with the GUI thread instead of
    // badgering it with bits of work to do.
       
    // Aside from performance, ConfigureAwait has another important aspect: It can avoid deadlocks. Consider Figure 3 again; if you add “ConfigureAwait(false)”
    // to the line of code in DelayAsync, then the deadlock is avoided. This time, when the await completes, it attempts to execute the remainder of the async
    // method within the thread pool context. The method is able to complete, which completes its returned task, and there’s no deadlock. This technique is
    // particularly useful if you need to gradually convert an application from synchronous to asynchronous.
    // ------------------------------------------------------------------------------------------------------------------------------------------------------------
    
    // If you can use ConfigureAwait at some point within a method, then I recommend you use it for every await in that method after that point. Recall that the
    // context is captured only if an incomplete Task is awaited; if the Task is already complete, then the context isn’t captured. Some tasks might complete
    // faster than expected in different hardware and network situations, and you need to graciously handle a returned task that completes before it’s awaited.
    // Figure 6 shows a modified example.
       
    // Figure 6 Handling a Returned Task that Completes Before It’s Awaited
    async Task MySecondMethodAsync()
    {
        // Code here runs in the original context.
        await Task.FromResult(1);
        // Code here runs in the original context.
        await Task.FromResult(1).ConfigureAwait(continueOnCapturedContext: false);
        // Code here runs in the original context.
        var random = new Random();
        int delay = random.Next(2); // Delay is either 0 or 1
        await Task.Delay(delay).ConfigureAwait(continueOnCapturedContext: false);
        // Code here might or might not run in the original context.
        // The same is true when you await any Task
        // that might complete very quickly.
    }
    
    // You should not use ConfigureAwait when you have code after the await in the method that needs the context. For GUI apps, this includes any code that
    // manipulates GUI elements, writes data-bound properties or depends on a GUI-specific type such as Dispatcher/CoreDispatcher. For ASP.NET apps, this
    // includes any code that uses HttpContext.Current or builds an ASP.NET response, including return statements in controller actions. Figure 7demonstrates
    // one common pattern in GUI apps—having an async event handler disable its control at the beginning of the method, perform some awaits and then re-enable
    // its control at the end of the handler; the event handler can’t give up its context because it needs to re-enable its control.
       
    // Figure 7 Having an Async Event Handler Disable and Re-Enable Its Control
    private async void button1_Click(object sender, EventArgs e)
    {
        var button1 = new MockButton();
        button1.Enabled = false;
        try
        {
            // Can't use ConfigureAwait here ...
            await Task.Delay(1000);
        }
        finally
        {
            // Because we need the context here.
            button1.Enabled = true;
        }
    }
    
    //Each async method has its own context, so if one async method calls another async method, their contexts are independent. Figure 8 shows a minor
    //modification of Figure 7.

    //Figure 8 Each Async Method Has Its Own Context
    private async Task HandleClickAsync()
    {
        // Can use ConfigureAwait here.
        await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: false);
    }
    private async void button1_Click_2(object sender, EventArgs e)
    {
        var button1 = new MockButton();
        button1.Enabled = false;
        try
        {
            // Can't use ConfigureAwait here.
            await HandleClickAsync();
        }
        finally
        {
            // We are back on the original context for this method.
            button1.Enabled = true;
        }
    }
    // Context-free code is more reusable. Try to create a barrier in your code between the context-sensitive code and context-free code, and minimize the
    // context-sensitive code. In Figure 8, I recommend putting all the core logic of the event handler within a testable and context-free async Task method,
    // leaving only the minimal code in the context-sensitive event handler. Even if you’re writing an ASP.NET application, if you have a core library that’s
    // potentially shared with desktop applications, consider using ConfigureAwait in the library code.
       
    // To summarize this third guideline, you should use Configure­Await when possible. Context-free code has better performance for GUI applications and is
    // a useful technique for avoiding deadlocks when working with a partially async codebase. The exceptions to this guideline are methods that require
    // the context.
}
