# Week 2 - Day 3: Async/Await Deep Dive & Concurrency Basics

## Overview

Today's focus was understanding asynchronous programming in C# and how modern .NET applications execute asynchronous operations efficiently.

In addition to completing the hands-on lab, I explored the internal behavior of `async`/`await`, concurrency concepts, common pitfalls, and cancellation mechanisms.

---

## Concepts Learned

### Task-Based Asynchronous Pattern (TAP)

- Understanding `Task` and `Task<T>`
- Why asynchronous programming exists
- How `await` works
- Difference between synchronous and asynchronous execution

### Async / Await

- Execution flow of async methods
- What happens after reaching an `await`
- Why `await` does **not** block the current thread
- Compiler-generated async state machine (high-level overview)

### Concurrency

- Sequential execution
- Concurrent execution
- Running independent operations with `Task.WhenAll`
- Difference between concurrency and parallelism
- Why `Task.WhenAll` improves performance for I/O-bound operations

### Threading Concepts

- Process vs Thread
- ThreadPool overview
- Why async does not create new threads automatically
- Thread behavior before and after `await`

### Common Async Pitfalls

- Async All the Way principle
- Why `.Result` and `.Wait()` should be avoided
- Blocking vs non-blocking code
- Deadlock (conceptual overview)

### Cancellation

- `CancellationToken`
- `CancellationTokenSource`
- Cooperative cancellation
- Handling `OperationCanceledException`

### Measuring Performance

- Using `Stopwatch`
- Comparing sequential and concurrent execution times

---

## Hands-on Lab

### Task 1
Created three asynchronous methods that simulate different data sources using `Task.Delay`.

### Task 2
Executed the methods sequentially and measured the execution time.

### Task 3
Executed the same methods concurrently using `Task.WhenAll` and compared the results.

### Task 4
Implemented cancellation using `CancellationToken` and demonstrated cancelling an operation before completion.

---

## Key Takeaways

- `await` frees the calling thread instead of blocking it.
- `Task.WhenAll` executes independent asynchronous operations concurrently.
- Async programming improves scalability for I/O-bound operations.
- `CancellationToken` provides a safe way to cancel long-running asynchronous work.
- Following the **Async All the Way** principle leads to cleaner and safer asynchronous code.

---

## Resources

- Microsoft Learn
- Official .NET Documentation
- YouTube: https://www.youtube.com/watch?v=_fPNcQrB1JA
- ChatGPT (used for explanations, discussions, and reviewing concepts)