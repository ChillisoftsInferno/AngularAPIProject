// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace EmployeeManagement.ExampleCode.AsyncronousExamples;

public class SyncVsAsync
{
    // The following code snippet illustrates a synchronous void-returning method and its asynchronous equivalent.
    void MyMethod()
    {
        // Do synchronous work.
        Thread.Sleep(1000);
    }
    async Task MyMethodAsync()
    {
        // Do asynchronous work.
        await Task.Delay(1000);
    }
}
