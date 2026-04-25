using System;
using System.Threading.Tasks;

public class Sorter
{
    // LLM improvement:
    // Replaced Bubble Sort with Quick Sort.
    // Bubble Sort repeatedly compares adjacent elements, causing O(n²) time.
    // Quick Sort uses divide-and-conquer, giving average O(n log n) performance.
    public static void QuickSort(int[] arr)
    {
        if (arr == null || arr.Length <= 1)
            return;

        QuickSort(arr, 0, arr.Length - 1);
    }

    private static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);

            QuickSort(arr, low, pivotIndex - 1);
            QuickSort(arr, pivotIndex + 1, high);
        }
    }

    // LLM improvement:
    // Partitioning places smaller values before the pivot
    // and larger values after it.
    // This avoids repeated full-array passes like Bubble Sort.
    private static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] <= pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }

        Swap(arr, i + 1, high);
        return i + 1;
    }

    private static void Swap(int[] arr, int i, int j)
    {
        if (i == j) return;

        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    // Optional LLM improvement:
    // Parallel Quick Sort can improve performance for large datasets
    // by sorting left and right partitions at the same time.
    public static void ParallelQuickSort(int[] arr)
    {
        if (arr == null || arr.Length <= 1)
            return;

        ParallelQuickSort(arr, 0, arr.Length - 1);
    }

    private static void ParallelQuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);

            // Only parallelize large partitions.
            // This avoids unnecessary overhead for small arrays.
            if (high - low > 10000)
            {
                Parallel.Invoke(
                    () => ParallelQuickSort(arr, low, pivotIndex - 1),
                    () => ParallelQuickSort(arr, pivotIndex + 1, high)
                );
            }
            else
            {
                QuickSort(arr, low, pivotIndex - 1);
                QuickSort(arr, pivotIndex + 1, high);
            }
        }
    }
}