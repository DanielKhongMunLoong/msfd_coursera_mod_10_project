using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Diagnostics;
class Program
{
    static void TestOptimizedBtree()
    {
        Console.WriteLine($"--- 1) Testing Optimized binary tree class ---");
        BinaryTree btree1 = new BinaryTree();
        List<int> testSequence = new List<int> { 50, 30, 70, 20, 40, 60, 80 };
        foreach(var num in testSequence)
        {
            Console.WriteLine($"Inserting new number to binary tree : {num}");
            btree1.Insert(num);
        }

        
        bool result = default;

        foreach(var num in testSequence)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            result = btree1.Search(num);

            stopwatch.Stop();

            if (result)
            {
                Console.WriteLine($"Searching for number: {num} inside binary tree, found: {result}, time taken (optimized): {stopwatch.ElapsedMilliseconds} ms");
            }
            else
            {
                Console.WriteLine($"Test is stopped, unable to find number: {num} inside binary tree, found: {result} ");
                return;
            }
        }

        // additional negative test cases
        List<int> testSequence_neg = new List<int> { 1, 2, 3 };
        foreach(var num in testSequence_neg)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
            result = btree1.Search(num);
            
            stopwatch.Stop();
            Console.WriteLine($"Searching for non existent number: {num} inside binary tree, found: {result}, time taken (optimized): {stopwatch.ElapsedMilliseconds} ms ");
        }
        Console.WriteLine($"Testing Optimized binary tree class is done");        
    }

    static void TestOptimizedPqueue()
    {
        Console.WriteLine($"--- 2) Testing optimized priority queue class ---");
        ApiRequestQueue queue = new ApiRequestQueue();

        queue.Enqueue(new ApiRequest("/auth", 1));
        queue.Enqueue(new ApiRequest("/data", 3));
        queue.Enqueue(new ApiRequest("/healthcheck", 2));

        queue.EnqueueBatch(new List<ApiRequest>
        {
            new ApiRequest("/admin", 1),
            new ApiRequest("/reports", 4),
            new ApiRequest("/metrics", 2)
        });

        while (queue.Count > 0)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ApiRequest request = queue.Dequeue();

            stopwatch.Stop();
            Console.WriteLine($"Processing: {request.Endpoint}, Priority: {request.Priority}, time taken (optimized): {stopwatch.ElapsedMilliseconds} ms");
        }
        Console.WriteLine($"Testing optimized priority queue is done");
    }

    static void TestQuickSort()
    {
        Console.WriteLine("--- 3) Testing sorter class ---");
        int[] testSequence = { 50, 20, 40, 10, 30 };
        string ts_before = string.Join(", ", testSequence);
        Console.WriteLine($"Before Sorting, test sequence: {ts_before}");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Sorter.QuickSort(testSequence);
        stopwatch.Stop();
        
        string ts_after = string.Join(", ", testSequence);
        Console.WriteLine($"After Sorting, test sequence: {ts_after}");
        Console.WriteLine("With optimization: Time taken: " + stopwatch.ElapsedMilliseconds + " ms");
        Console.WriteLine("Testing sorter class is done");
    }

    static void TestOptimizedTaskExecutor()
    {
        Console.WriteLine("--- 4) Testing task executor class ---");
        TaskExecutor executor = new TaskExecutor();

        executor.AddTask("Task 1");
        // Safely rejected
        executor.AddTask(null);       
        // Retried, then logged as failed
        executor.AddTask("Fail Task"); 
        executor.AddTask("Task 2");

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        executor.ProcessTasks();
        
        stopwatch.Stop();
        Console.WriteLine("With optimization: Time taken: " + stopwatch.ElapsedMilliseconds + " ms");
        
        Console.WriteLine("Testing task executor class is done");
    }

    static void Main()
    {
        // 1. Optimized Binary Tree – Final implementation with balancing techniques for efficient retrieval
        TestOptimizedBtree();
        // 2. Optimized Task Scheduling – Improved priority queue logic with better sorting and execution time
        TestOptimizedPqueue();
        // 3. Optimized Sorting Algorithm – Implementation showing improvements from O(n²) to O(n log n)
        TestQuickSort();
        // 4. Debugged Task Execution Code – Stable implementation with error handling and logging
        TestOptimizedTaskExecutor();
        // 5. Performance Report is integrated in each test function execution for 1 to 4
    }
}