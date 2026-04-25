using System;
using System.Collections.Generic;

public class TaskExecutor
{
    private readonly Queue<string> taskQueue = new Queue<string>();

    // LLM improvement: Added max retry limit to avoid infinite retry loops.
    private const int MaxRetries = 2;

    public void AddTask(string task)
    {
        // LLM improvement: Validate task before adding it to the queue.
        // This prevents null or empty tasks from entering the system.
        if (string.IsNullOrWhiteSpace(task))
        {
            LogError("Invalid task was not added to the queue.");
            return;
        }

        taskQueue.Enqueue(task);
        LogInfo($"Task added: {task}");
    }

    public void ProcessTasks()
    {
        while (taskQueue.Count > 0)
        {
            string task = taskQueue.Dequeue();

            // LLM improvement: Process each task safely so one failed task
            // does not crash the whole program.
            bool success = ProcessTaskWithRetry(task);

            if (!success)
            {
                LogError($"Task permanently failed after retries: {task}");
            }
        }
    }

    private bool ProcessTaskWithRetry(string task)
    {
        int attempt = 0;

        while (attempt <= MaxRetries)
        {
            try
            {
                attempt++;
                LogInfo($"Processing task: {task}, attempt {attempt}");

                ExecuteTask(task);

                LogInfo($"Task completed successfully: {task}");
                return true;
            }
            catch (Exception ex)
            {
                // LLM improvement: Catch exceptions and log them instead of crashing.
                LogError($"Error processing task '{task}' on attempt {attempt}: {ex.Message}");

                if (attempt > MaxRetries)
                {
                    return false;
                }
            }
        }

        return false;
    }

    private void ExecuteTask(string task)
    {
        // LLM improvement: Defensive null check remains here as a second layer
        // of protection, even though AddTask already validates input.
        if (string.IsNullOrWhiteSpace(task))
        {
            throw new ArgumentException("Task cannot be null or empty.");
        }

        if (task.Contains("Fail"))
        {
            throw new InvalidOperationException("Task execution failed.");
        }

        Console.WriteLine($"Task {task} completed successfully.");
    }

    private void LogInfo(string message)
    {
        // LLM improvement: Centralized logging instead of scattered Console.WriteLine calls.
        Console.WriteLine($"[INFO] {DateTime.Now}: {message}");
    }

    private void LogError(string message)
    {
        // LLM improvement: Error messages are clearly marked.
        Console.WriteLine($"[ERROR] {DateTime.Now}: {message}");
    }
}