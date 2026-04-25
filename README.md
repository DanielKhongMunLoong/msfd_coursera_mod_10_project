# msfd_coursera_mod_10_project
Source code for Graded Assignment: Data Structures and Algorithms Project

**Implementation walkthrough for source code**

https://github.com/DanielKhongMunLoong/msfd_coursera_mod_10_project/wiki

**(5 pts) Did you submit an optimized binary tree implementation?**

Yes, implemented in class BinaryTree in BinaryTree.cs file. Tested in program.cs.

Please refer to https://github.com/DanielKhongMunLoong/msfd_coursera_mod_10_project/wiki for console output of source code.

**(5 pts) Did you submit a refined task scheduling algorithm?**

Yes, implemented in class ApiRequestQueue in ApiRequestQueue.cs file. Tested in program.cs.

Please refer to https://github.com/DanielKhongMunLoong/msfd_coursera_mod_10_project/wiki for console output of source code.

**(5 pts) Did you improve a sorting algorithm from O(n²) to O(n log n)?**

Yes, implemented in class Sorter in Sorter.cs file. Tested in program.cs.

Please refer to https://github.com/DanielKhongMunLoong/msfd_coursera_mod_10_project/wiki for console output of source code.

**(5 pts) Did you submit debugged and optimized task execution code?**

Yes, implemented in class TaskExecutor in TaskExecutor.cs file. Tested in program.cs.

Please refer to https://github.com/DanielKhongMunLoong/msfd_coursera_mod_10_project/wiki for console output of source code.

**(5 pts) Did you measure and document performance improvements for each task?**

Yes, for each task, the performances were measured in program.cs in the test execution functions.

Please refer to https://github.com/DanielKhongMunLoong/msfd_coursera_mod_10_project/wiki for console output of source code.

**(5 pts) Did you clearly explain how the LLM contributed to your improvements?**

The reflection reports are as follows for each task.

**Task 1: Binary tree optimization and improvements**

[A] Analysis report

The original tree could become unbalanced, especially if values were inserted in sorted order. That could degrade search from O(log n) to O(n).

The optimized version improves this by:

Using an AVL tree to automatically rebalance after insertions.

- Adding an efficient Search() method.
- Storing Height in each node to avoid recalculating subtree heights repeatedly.
- Avoiding duplicate values with else if (value > node.Value).
- Using iterative search to reduce unnecessary recursion overhead.

[B] Reflection report

[1] How did the LLM assist in refining the code?

LLM analyzed the original binary tree implementation and identified key performance limitations, especially the risk of the tree becoming unbalanced. It refactored the structure into an AVL self-balancing tree, added an efficient search method, and improved how insertions are handled to maintain optimal performance. It also enhanced the design by storing node heights and using rotations to automatically rebalance the tree after each insertion.

[2] Were any suggestions inaccurate or unnecessary?

No major suggestions were inaccurate. However, depending on the system’s scale, implementing a full AVL tree might be slightly more complex than necessary. 
For smaller datasets, a simple binary search tree could still be sufficient. That said, for a scalable system like SwiftCollab, the balancing logic is justified.

[3] What were the most impactful improvements you implemented?

The most impactful improvements were:
- Tree balancing (AVL rotations): Prevents performance degradation and ensures consistent O(log n) operations.
- Efficient search method: Enables fast retrieval of tasks based on priority.
- Height tracking optimization: Avoids repeated calculations and improves runtime efficiency.

These changes significantly improve scalability, ensuring the system remains performant as the number of tasks grows.

**Task 2: Refined task scheduling algorithm enhancements**

[A] Reflection report

[1] How did the LLM assist in refining the code?

The LLM identified the core inefficiency in the original implementation—re-sorting the entire list on every Enqueue. It recommended replacing this with a binary min-heap, which maintains order incrementally instead of globally.

It also:
- Highlighted the inefficiency of RemoveAt(0) (which shifts all elements). 
- Introduced heap operations (HeapifyUp, HeapifyDown) to maintain ordering efficiently. 
- Suggested batch enqueue (BuildHeap) for handling large request volumes more efficiently. 
- Added thread-safety using locks, which is important in real-world API systems handling concurrent requests

Overall, the LLM helped shift the design from a simple but inefficient structure to a scalable, production-ready approach.

[2] Were any suggestions inaccurate or unnecessary?

Most suggestions were valid and aligned with best practices. However, a few points could be considered situational:

- Thread-safety using lock

  While correct, it may not always be optimal in high-performance systems. Alternatives like ConcurrentQueue + priority handling or lock-free structures could be more scalable depending on workload. 

- Batch enqueue (BuildHeap)

  Useful for bulk inserts, but if requests typically arrive one-by-one (real-time APIs), this feature might not be heavily utilized. 
  These suggestions were not incorrect, but their usefulness depends on the actual system requirements and traffic patterns.

[3] What were the most impactful improvements you implemented?

The most significant improvements were:

- Replacing List.Sort() with a Binary Min-Heap 
  - Reduced insertion complexity from O(n log n) → O(log n) 
  - Eliminated repeated full-list sorting 

- Optimizing Dequeue Operation 
  - Removed costly RemoveAt(0) (O(n)) 
  - Replaced with heap root swap + heapify (O(log n)) 

- Improved Scalability 
  - Heap structure ensures consistent performance even with large request volumes 

- Better System Design 
  - Added support for bulk operations 
  - Introduced thread-safety for concurrent environments 

These changes collectively transformed the queue from a simple but inefficient implementation into a high-performance, scalable priority queue suitable for real-world API workloads.

**Task 3: Sorting algorithm improvements**

[A] Analysis report

The original algorithm used Bubble Sort, which has a time complexity of O(n²). This is inefficient for large datasets because every element may need to be compared repeatedly with many other elements.

The optimized version replaces Bubble Sort with Quick Sort, which has an average time complexity of O(n log n). This makes it much more suitable for analytics and reporting workloads with large datasets.

The new implementation also sorts in-place, meaning it does not require a second array to store sorted results. This keeps the space complexity low, typically O(log n) due to recursive calls.

A parallel version was also added using Parallel.Invoke. This can improve performance on large datasets by sorting partitions concurrently, though it should only be used for sufficiently large arrays because parallel execution adds overhead.

[B] Reflection report

[1] How did the LLM assist in refining the code?

The LLM helped refine the algorithm by identifying that the original Bubble Sort implementation was inefficient for large datasets because of its O(n²) time complexity. It suggested replacing it with Quick Sort, which improves average performance to O(n log n) and is more suitable for analytics/reporting workloads.

[2] Were any suggestions inaccurate or unnecessary?

Some suggestions were useful but optional. For example, parallel sorting can improve performance for very large datasets, but it may be unnecessary for small or medium datasets because thread overhead can make it slower. 

Also, Quick Sort has a worst-case complexity of O(n²) if the pivot selection is poor, so using randomized or median pivot selection would make it more reliable.

[3] What were the most impactful improvements you implemented?

The most impactful improvement was replacing Bubble Sort with an in-place Quick Sort. This reduced average runtime significantly while keeping memory usage low. 

Adding a size threshold before using parallel sorting was also important because it avoids unnecessary overhead on smaller arrays.

**Task 4: Debugged and optimized task execution code**

[A] Analysis report

Main issues fixed:

1.	null tasks previously caused crashes. 
2.	Failed tasks stopped the entire queue from processing. 
3.	There was no retry logic. 
4.	Error messages were not clearly logged. 
5.	Input validation was missing from AddTask. 

Most impactful improvement: wrapping each task execution in try/catch, so one bad task no longer prevents later tasks like "Task 2" from running.

[B] Reflection report

[1] How did the LLM assist in refining the code?

The LLM helped by systematically identifying weaknesses in the original implementation and proposing structured fixes:

- Error handling gaps identified: It recognized that unhandled exceptions in ExecuteTask() would crash the entire system. 
- Input validation added: It flagged the lack of validation in AddTask() and prevented invalid tasks (e.g., null) from entering the queue. 
- Control flow improvement: It suggested isolating each task execution so failures don’t interrupt the overall processing loop. 
- Retry mechanism introduced: It proposed a simple retry strategy to handle transient failures without overcomplicating the design. 
- Logging standardization: It replaced scattered Console.WriteLine calls with structured logging methods for better traceability.

[2] Were any suggestions inaccurate or unnecessary?

Overall, the suggestions were relevant and practical, but a few points could be considered slightly unnecessary depending on system requirements:

- Double validation (in both AddTask and ExecuteTask):
  This is defensive programming, but in a controlled system, validating once at entry might be sufficient. 

- Retry logic for all failures:
  Not all failures should be retried (e.g., deterministic failures like "Fail Task"). In a real system, retries should be conditional (e.g., only for transient errors like network issues). 

- Basic logging via Console:
  While useful for demonstration, production systems would typically use structured logging frameworks (e.g., Serilog, NLog). 

These are not incorrect, but they could be refined further depending on real-world constraints.

[3] What were the most impactful improvements you implemented?

The key improvements that had the greatest impact were:

- Exception isolation using try-catch:
  Prevented a single task failure from crashing the entire processing pipeline. 

- Input validation at task entry point:
  Eliminated invalid tasks early, reducing downstream errors. 

- Retry mechanism:
  Increased robustness by allowing recovery from temporary failures. 

- Separation of concerns (logging methods):
  Improved code readability and maintainability by centralizing logging. 

- Graceful failure handling:
  Ensured failed tasks are logged and skipped instead of halting execution.
