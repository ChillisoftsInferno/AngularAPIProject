// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace EmployeeManagement.ExampleCode.AsyncronousExamples;

public class AsyncAllTheWay
{
    // “Async all the way” means that you shouldn’t mix synchronous and asynchronous code without carefully considering the consequences. In particular, it’s
    // usually a bad idea to block on async code by calling Task.Wait or Task.Result. This is an especially common problem for programmers who are “dipping their
    // toes” into asynchronous programming, converting just a small part of their application and wrapping it in a synchronous API so the rest of the application
    // is isolated from the changes. Unfortunately, they run into problems with deadlocks. After answering many async-related questions on the MSDN forums, Stack
    // Overflow and e-mail, I can say this is by far the most-asked question by async newcomers once they learn the basics:
    // “Why does my partially async code deadlock?”
    
    // The following code shows a simple example where one method blocks on the result of an async method. This code will work just fine in a console application
    // but will deadlock when called from a GUI or ASP.NET context. This behavior can be confusing, especially considering that stepping through the debugger
    // implies that it’s the await that never completes. The actual cause of the deadlock is further up the call stack when Task.Wait is called.

    // A Common Deadlock Problem When Blocking on Async Code
    public static class DeadlockDemo
    {
        private static async Task DelayAsync()
        {
            await Task.Delay(1000);
        }
        // This method causes a deadlock when called in a GUI or ASP.NET context.
        public static void Test()
        {
            // Start the delay.
            var delayTask = DelayAsync();
            // Wait for the delay to complete.
            delayTask.Wait();
        }
    }
    
    // Note that console applications don’t cause this deadlock. They have a thread pool SynchronizationContext instead of a one-chunk-at-a-time
    // SynchronizationContext, so when the await completes, it schedules the remainder of the async method on a thread pool thread. The method is able to complete,
    // which completes its returned task, and there’s no deadlock. This difference in behavior can be confusing when programmers write a test console program,
    // observe the partially async code work as expected, and then move the same code into a GUI or ASP.NET application, where it deadlocks.
      
    // The best solution to this problem is to allow async code to grow naturally through the codebase. If you follow this solution, you’ll see async code expand
    // to its entry point, usually an event handler or controller action. Console applications can’t follow this solution fully because the Main method can’t be
    // async. If the Main method were async, it could return before it completed, causing the program to end. Figure 4 demonstrates this exception to the
    // guideline: The Main method for a console application is one of the few situations where code may block on an asynchronous method.
    
    // The Main Method May Call Task.Wait or Task.Result
    void Main()
    {
        MainAsync().Wait();
    }
    static async Task MainAsync()
    {
        try
        {
            // Asynchronous implementation.
            await Task.Delay(1000);
        }
        catch (Exception ex)
        {
            // Handle exceptions.
        }
    }
    
    // Every Task will store a list of exceptions. When you await a Task, the first exception is re-thrown, so you can catch the specific exception type
    // (such as InvalidOperationException). However, when you synchronously block on a Task using Task.Wait or Task.Result, all of the exceptions are wrapped in an
    // AggregateException and thrown. Refer again to Figure 4. The try/catch in MainAsync will catch a specific exception type, but if you put the try/catch in
    // Main, then it will always catch an AggregateException. Error handling is much easier to deal with when you don’t have an AggregateException, so I put the
    // “global” try/catch in MainAsync.
       
    // So far, I’ve shown two problems with blocking on async code: possible deadlocks and more-complicated error handling. There’s also a problem with using
    // blocking code within an async method. Consider this simple example:
    public static class NotFullyAsynchronousDemo
    {
        // This method synchronously blocks a thread.
        public static async Task TestNotFullyAsync()
        {
            await Task.Yield();
            Thread.Sleep(5000);
        }
        // This method isn’t fully asynchronous. It will immediately yield, returning an incomplete task, but when it resumes it will synchronously block whatever
        // thread is running. If this method is called from a GUI context, it will block the GUI thread; if it’s called from an ASP.NET request context, it will
        // block the current ASP.NET request thread. Asynchronous code works best if it doesn’t synchronously block. Figure 5 is a cheat sheet of async
        // replacements for synchronous operations.
    }
}
