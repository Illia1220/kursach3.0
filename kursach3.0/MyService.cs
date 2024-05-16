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
                    array[j] = temp;
                }
                gap /= 3;
            }
        }

        // Метод Шелла з послідовністю дельт Фібоначі
        public static void ShellSortFibonacci(int[] array)
        {
            int n = array.Length;
            int fib1 = 1;
            int fib2 = 0;
            int fib3 = 0;
            while (fib3 < n)
            {
                fib3 = fib1 + fib2;
                fib1 = fib2;
                fib2 = fib3;
            }
            while (fib2 > 0)
            {
                int i = fib2;
                while (i < n)
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
                fib3 = fib1;
                fib2 = fib1;
                fib1 = fib3 - fib2;
            }
        }

        // Метод Шелла з послідовністю дельт Токуда
        public static void ShellSortTokuda(int[] array)
        {
            int n = array.Length;
            int[] tokudaSequence = GenerateTokudaSequence(n);
            for (int k = tokudaSequence.Length - 1; k >= 0; k--)
            {
                int gap = tokudaSequence[k];
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
        private int[] GenerateRandomArray(int size, int minValue, int maxValue)
        {
            if (size < 100)
            {
                MessageBox.Show("Минимальный размер массива должен быть не менее 100 элементов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; // Возвращаем null в случае ошибки
            }

            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(minValue, maxValue + 1);
            }
            return array;
        }
    }
}
