// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace EmployeeManagement.ExampleCode.AsyncronousExamples;

public class AsyncVoidExceptions
{
    // Figure 2 illustrates that exceptions thrown from async void methods can’t be caught naturally.
    // Figure 2 Exceptions from an Async Void Method Can’t Be Caught with Catch
    private async void ThrowExceptionAsync()
    {
        throw new InvalidOperationException();
    }
    public void AsyncVoidExceptions_CannotBeCaughtByCatch()
    {
        try
        {
            ThrowExceptionAsync();
        }
        catch (Exception)
        {
            // The exception is never caught here!
            throw;
        }
    }
    
    // It’s clear that async void methods have several disadvantages compared to async Task methods, but they’re quite useful in one particular case: asynchronous
    // event handlers. The differences in semantics make sense for asynchronous event handlers. They raise their exceptions directly on the SynchronizationContext,
    // which is similar to how synchronous event handlers behave. Synchronous event handlers are usually private, so they can’t be composed or directly tested.
    // An approach I like to take is to minimize the code in my asynchronous event handler—for example, have it await an async Task method that contains the actual
    // logic. The following code illustrates this approach, using async void methods for event handlers without sacrificing testability:
    private async void button1_Click(object sender, EventArgs e)
    {
        await Button1ClickAsync();
    }
    public async Task Button1ClickAsync()
    {
        // Do asynchronous work.
        await Task.Delay(1000);
    }
}
