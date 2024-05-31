using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace kursach3._0
{
    public class MyService
    {

        public static void ShellSortClassic(int[] array)
        {
            int n = array.Length;
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
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
        }

        // Метод Шелла з послідовністю дельт Седжвіка
        public static void ShellSortSedgewick(int[] array)
        {
            int nw = array.Length;
            int gap = 1;
            while (gap < nw / 3)
            {
                gap = 3 * gap + 1;
            }
            while (gap >= 1)
            {
                for (int iw = gap; iw < nw; iw++)
                {
                    int temp = array[iw];
                    int j;
                    for (j = iw; j >= gap && array[j - gap] > temp; j -= gap)
                    {
                        array[j] = array[j - gap];
                    }



                }
                gap /= 3;
            }

        }

        // Метод Шелла з послідовністю дельт Фібоначі
        public static void ShellSortFibonacci(int[] array)
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

            // Обновление GUI (если необходимо)

        }

        // Метод Шелла з послідовністю дельт Токуда
        public static void ShellSortTokuda(int[] array)
        {
            int n = array.Length;
            int[] tokudaSequence = GenerateTokudaSequence(n);
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
            }
            // Обновление GUI (вынесено за внутренний цикл)

        }

        // Генерування послідовності дельт Токуда
        private static int[] GenerateTokudaSequence(int n)
        {
            Random rand = new Random();
            int count = rand.Next(5, 15); // Генеруємо випадкову довжину послідовності, наприклад, від 5 до 15
            int[] sequence = new int[count];
            for (int i = 0; i < count; i++)
            {
                sequence[i] = rand.Next(1, n / 2); // Генеруємо випадкові значення для послідовності в межах від 1 до n/2
            }
            Array.Reverse(sequence); // Реверсуємо, щоб послідовність була у зростаючому порядку
            return sequence;
        }


        // Виведення масиву на екран
        public static void PrintArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        // Генерування випадкового масиву з заданими межами значень
        
    }
}
