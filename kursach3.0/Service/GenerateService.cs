using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kursach3._0.Service
{
    public static class GenerateService
    {
        public static int[] GenerateArray(int size, int minValue, int maxValue, string arrayType)
        {
            int[] array = new int[size];

            switch (arrayType)
            {
                case "Ordered":
                    for (int i = 0; i < size; i++)
                    {
                        array[i] = minValue + i * (maxValue - minValue) / size;
                    }
                    break;

                case "Reversed":
                    for (int i = 0; i < size; i++)
                    {
                        array[i] = maxValue - i * (maxValue - minValue) / size;
                    }

                    break;
                default:
                    Random random = new Random();
                    for (int i = 0; i < size; i++)
                    {
                        array[i] = random.Next(minValue, maxValue + 1);
                    }
                    break;
            }

            return array;
        }
        public static int[] GenerateTokudaSequence(int n)
        {
            Random rand = new Random();
            int count = rand.Next(1, n);
            int[] sequence = new int[count];
            for (int i = 0; i < count; i++)
            {
                sequence[i] = rand.Next(1, n / 2);
                
            }
            Array.Reverse(sequence);


            return sequence;
        }

    }
}
