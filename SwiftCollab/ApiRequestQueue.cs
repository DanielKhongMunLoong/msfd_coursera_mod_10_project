using System;
using System.Collections.Generic;

public class ApiRequest
{
    public string Endpoint { get; set; }
    public int Priority { get; set; }

    public ApiRequest(string endpoint, int priority)
    {
        Endpoint = endpoint;
        Priority = priority;
    }
}

public class ApiRequestQueue
{
    // LLM improvement:
    // Replaced sorted List with a binary min-heap.
    // The request with the lowest Priority value is kept at the root.
    private readonly List<ApiRequest> heap = new List<ApiRequest>();

    // LLM improvement:
    // Added lock object to make queue operations thread-safe.
    // This prevents race conditions when multiple threads enqueue/dequeue.
    private readonly object queueLock = new object();

    public int Count
    {
        get
        {
            lock (queueLock)
            {
                return heap.Count;
            }
        }
    }

    public void Enqueue(ApiRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        lock (queueLock)
        {
            heap.Add(request);

            // LLM improvement:
            // Instead of sorting the entire list O(n log n),
            // move only the new item upward until heap order is restored.
            // Complexity: O(log n)
            HeapifyUp(heap.Count - 1);
        }
    }

    public void EnqueueBatch(IEnumerable<ApiRequest> requests)
    {
        if (requests == null)
            throw new ArgumentNullException(nameof(requests));

        lock (queueLock)
        {
            foreach (var request in requests)
            {
                if (request == null)
                    continue;

                heap.Add(request);
            }

            // LLM improvement:
            // Bulk enqueue uses heap construction instead of sorting after every insert.
            // This is more efficient for large batches.
            // Complexity: O(n)
            BuildHeap();
        }
    }

    public ApiRequest Dequeue()
    {
        lock (queueLock)
        {
            if (heap.Count == 0)
                return null;

            ApiRequest nextRequest = heap[0];

            // Move the last item to the root, then restore heap order.
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
            {
                // LLM improvement:
                // Dequeue no longer uses RemoveAt(0), which shifts all items O(n).
                // HeapifyDown restores ordering in O(log n).
                HeapifyDown(0);
            }

            return nextRequest;
        }
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;

            if (heap[index].Priority >= heap[parentIndex].Priority)
                break;

            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void HeapifyDown(int index)
    {
        while (true)
        {
            int leftChild = index * 2 + 1;
            int rightChild = index * 2 + 2;
            int smallest = index;

            if (leftChild < heap.Count &&
                heap[leftChild].Priority < heap[smallest].Priority)
            {
                smallest = leftChild;
            }

            if (rightChild < heap.Count &&
                heap[rightChild].Priority < heap[smallest].Priority)
            {
                smallest = rightChild;
            }

            if (smallest == index)
                break;

            Swap(index, smallest);
            index = smallest;
        }
    }

    private void BuildHeap()
    {
        for (int i = heap.Count / 2 - 1; i >= 0; i--)
        {
            HeapifyDown(i);
        }
    }

    private void Swap(int first, int second)
    {
        ApiRequest temp = heap[first];
        heap[first] = heap[second];
        heap[second] = temp;
    }
}