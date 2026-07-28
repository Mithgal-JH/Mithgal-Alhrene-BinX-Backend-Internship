using System.Collections;
using System.Diagnostics;



// Task 1 => 1. Write 3 async methods that each simulate a delay (Task.Delay) representing a different data source.

static async Task GetUsersAsync(CancellationToken cancellationToken = default)
{
    Console.WriteLine("Getting users...");
    await Task.Delay(2000, cancellationToken);
    Console.WriteLine("Users loaded.");
}

static async Task GetProductsAsync()
{
    Console.WriteLine("Getting products...");
    await Task.Delay(2000);
    Console.WriteLine("Products loaded.");
}

static async Task GetOrdersAsync()
{
    Console.WriteLine("Getting orders...");
    await Task.Delay(2000);
    Console.WriteLine("Orders loaded.");
}





// Sequential execution:
// Each async method is awaited before the next one starts.
// Total execution time is approximately the sum of all delays.
// Task 2 => Call all 3 sequentially with individual awaits and measure the total elapsed time.
System.Console.WriteLine("Task 2 => Call all 3 sequentially with individual awaits and measure the total elapsed time.");
var stopwatch = Stopwatch.StartNew();

await GetUsersAsync();
await GetProductsAsync();
await GetOrdersAsync();

stopwatch.Stop();

Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} ms");



// Concurrent execution:
// All tasks start immediately without waiting for each other.
// Task.WhenAll waits until every task has finished.
// Total execution time is approximately equal to the longest-running task,
// not the sum of all task durations.

// Task 3 => Rewrite the same calls using Task.WhenAll and compare the elapsed time.
System.Console.WriteLine("\n\n\nTask 3 => Rewrite the same calls using Task.WhenAll and compare the elapsed time.");
stopwatch = Stopwatch.StartNew();

// Starting the tasks immediately.
// At this point all three operations are running concurrently.
Task usersTask = GetUsersAsync();
Task productsTask = GetProductsAsync();
Task ordersTask = GetOrdersAsync();


// Wait for all running tasks to complete.
await Task.WhenAll(usersTask, productsTask, ordersTask);

stopwatch.Stop();

Console.WriteLine($"Elapsed Time: {stopwatch.ElapsedMilliseconds} ms");




// Demonstrating cancellation using CancellationToken.
// The operation is cancelled before it finishes.
// Task 4 => Add a CancellationToken parameter to one method and demonstrate cancelling it mid-operation.
System.Console.WriteLine("\n\n\nTask 4 => Add a CancellationToken parameter to one method and demonstrate cancelling it mid-operation.");


// CancellationTokenSource is responsible for creating and triggering the cancellation signal.
CancellationTokenSource cts = new();

// Pass the token so the async method can observe cancellation requests.
Task task = GetUsersAsync(cts.Token);
await Task.Delay(1000);

// Send the cancellation request.
cts.Cancel();

// The operation throws OperationCanceledException when cancellation is observed.
try
{
    await task;
}
catch (OperationCanceledException)
{
    Console.WriteLine("Operation cancelled.");
}