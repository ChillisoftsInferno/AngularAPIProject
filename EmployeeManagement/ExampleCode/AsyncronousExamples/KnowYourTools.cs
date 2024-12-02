// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this

namespace EmployeeManagement.ExampleCode.AsyncronousExamples;

public class KnowYourTools
{
    // Know Your Tools
    // There’s a lot to learn about async and await, and it’s natural to get a little disoriented. Figure 9 is a quick reference of solutions to common problems.
       
    // Figure 9 Solutions to Common Async Problems
    
    // --------------------------------------------------------------------------------------------------------------------------------------
    // [                Problem	                        ]   [                               Solution                                        ]
    // --------------------------------------------------------------------------------------------------------------------------------------
    // [Create a task to execute code	                ]   [  Task.Run or TaskFactory.StartNew (not the Task constructor or Task.Start)    ]
    // [Create a task wrapper for an operation or event	]   [  TaskFactory.FromAsync or TaskCompletionSource<T>                             ]
    // [Support cancellation	                        ]   [  CancellationTokenSource and CancellationToken                                ]
    // [Report progress	                                ]   [  IProgress<T> and Progress<T>                                                 ]
    // [Handle streams of data	                        ]   [  TPL Dataflow or Reactive Extensions                                          ]
    // [Synchronize access to a shared resource	        ]   [  SemaphoreSlim                                                                ]
    // [Asynchronously initialize a resource	        ]   [  AsyncLazy<T>                                                                 ]
    // [Async-ready producer/consumer structures	    ]   [  TPL Dataflow or AsyncCollection<T>                                           ]
    // --------------------------------------------------------------------------------------------------------------------------------------
    
    // The first problem is task creation. Obviously, an async method can create a task, and that’s the easiest option. If you need to run code on the thread
    // pool, use Task.Run. If you want to create a task wrapper for an existing asynchronous operation or event, use TaskCompletionSource<T>. The next
    // common problem is how to handle cancellation and progress reporting. The base class library (BCL) includes types specifically intended to solve
    // these issues: CancellationTokenSource/CancellationToken and IProgress<T>/Progress<T>. Asynchronous code should use the Task-based Asynchronous Pattern,
    // or TAP (msdn.microsoft.com/library/hh873175), which explains task creation, cancellation and progress reporting in detail.
       
    // Another problem that comes up is how to handle streams of asynchronous data. Tasks are great, but they can only return one object and only complete once.
    // For asynchronous streams, you can use either TPL Dataflow or Reactive Extensions (Rx). TPL Dataflow creates a “mesh” that has an actor-like feel to it.
    // Rx is more powerful and efficient but has a more difficult learning curve. Both TPL Dataflow and Rx have async-ready methods and work well with
    // asynchronous code.
       
    // Just because your code is asynchronous doesn’t mean that it’s safe. Shared resources still need to be protected, and this is complicated by the fact
    // that you can’t await from inside a lock. Here’s an example of async code that can corrupt shared state if it executes twice, even if it always runs
    // on the same thread:
    
    int value;

    Task<int> GetNextValueAsync(int current)
    {
        throw new NotImplementedException();
    }

    async Task UpdateValueAsync()
    {
        value = await GetNextValueAsync(value);
    }
    
    //The problem is that the method reads the value and suspends itself at the await, and when the method resumes it assumes the value hasn’t changed.
    //To solve this problem, the SemaphoreSlim class was augmented with the async-ready WaitAsync overloads. Figure 10 demonstrates SemaphoreSlim.WaitAsync.

    //Figure 10 SemaphoreSlim Permits Asynchronous Synchronization
    SemaphoreSlim mutex = new SemaphoreSlim(1);

    int value2;

    Task<int> GetNextValueAsync2(int current)
    {
        throw new NotImplementedException();
    }

    async Task UpdateValueAsync2()
    {
        await mutex.WaitAsync().ConfigureAwait(false);
        try

        {

            value = await GetNextValueAsync(value);

        }
        finally
        {

            mutex.Release();

        }
    }
    
    // Asynchronous code is often used to initialize a resource that’s then cached and shared. There isn’t a built-in type for this, but Stephen Toub
    // developed an AsyncLazy<T> that acts like a merge of Task<T> and Lazy<T>. The original type is described on his blog (bit.ly/dEN178), and an
    // updated version is available in my AsyncEx library (nitoasyncex.codeplex.com).
       
    // Finally, some async-ready data structures are sometimes needed. TPL Dataflow provides a BufferBlock<T> that acts like an async-ready producer/consumer
    // queue. Alternatively, AsyncEx provides AsyncCollection<T>, which is an async version of BlockingCollection<T>.
       
    // I hope the guidelines and pointers in this article have been helpful. Async is a truly awesome language feature, and now is a great time to start using it!
       
    // Stephen Cleary is a husband, father and programmer living in northern Michigan. He has worked with multithreading and asynchronous programming
    // for 16 years and has used async support in the Microsoft .NET Framework since the first CTP. His home page, including his blog, is at stephencleary.com.
       
    // Thanks to the following technical expert for reviewing this article: Stephen Toub
    // Stephen Toub works on the Visual Studio team at Microsoft. He specializes in areas related to parallelism and asynchrony.
}
