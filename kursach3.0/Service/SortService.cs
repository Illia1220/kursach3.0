using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach3._0.Service
{
    public static class SortService
    {
        public static int[]  ShellSortClassik(int[] array)
        {
            int n = array.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i += 1)
                {
                    int temp = array[i];
                    int j;
                    for (j = i; j >= gap && array[j - gap] > temp; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }
                    array[j] = temp;
                }
            }
            return array;
        }
        public static int[] ShellSortSedgewick(int[] array)
        {
            int n = array.Length;
            int gap = 1;
            while (gap < n / 3)
            {
                gap = 3 * gap + 1;
            }
            while (gap >= 1)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = array[i];
                    int j;
                    for (j = i; j >= gap && array[j - gap] > temp; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }
                    array[j] = temp; // Правильне розміщення цього рядка коду
                }
                gap /= 3;
            }
            return array;
        }
        public static int[] ShellSortFibonacci(int[] array)
        {
            int nq = array.Length;
            int fib1 = 1;
            int fib2 = 0;
            int fib3 = 0;
            int i;
            while (fib3 < nq)
            {
                fib3 = fib1 + fib2;
                fib1 = fib2;
                fib2 = fib3;
            }
            fib1 = fib2 - fib1;
            fib2 -= fib1;
            while (fib2 > 0)
            {
                i = fib2;
                while (i < nq)
                {
                    int temp = array[i];
                    int j = i - fib2;
                    while (j >= 0 && array[j] > temp)
                    {
                        array[j + fib2] = array[j];
                        j -= fib2;
                    }
                    array[j + fib2] = temp;
                    i++;
                }
                int tempFib = fib2;
                fib2 = fib1;
                fib1 = tempFib - fib1;
            }

            return array;

        }
        public static int[] ShellSortTokuda(int[] array)
        {
            int n = array.Length;
            int[] tokudaSequence = GenerateService.GenerateTokudaSequence(n);
            for (int k = tokudaSequence.Length - 1; k >= 0; k--)
            {
                int gap = tokudaSequence[k];
                for (int i1 = gap; i1 < n; i1++)
                {
                    int temp = array[i1];
                    int j;
                    for (j = i1; j >= gap && array[j - gap] > temp; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }
                    array[j] = temp;
                }
                return array;
            }
            // Обновление GUI (вынесено за внутренний цикл)

            return array;
        }
    }
}

