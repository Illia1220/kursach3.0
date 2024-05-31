using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.RenderableSeries;
using System.Windows.Media.Animation;
using System.IO;
using Microsoft.Win32;


namespace kursach3._0
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {



        private int[] _array;
        private string _mode;
        private int[] sortedArray;
        private bool isAnimating;

        private XyDataSeries<int, int> dataSeries = new XyDataSeries<int, int>();
        private Random random = new Random();

        public Window1()
        {
            InitializeComponent();
            StartAnimation();
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void DisplaySortedArray()
        {
            if (sortedArray != null)
            {
                SortedArrayDisplay.Text = string.Join(", ", sortedArray);
            }
        }





        private void Classik_Click(object sender, RoutedEventArgs e)
        {
            _mode = "classik";

            if (string.IsNullOrWhiteSpace(Size_array.Text) || string.IsNullOrWhiteSpace(Min_value.Text) || string.IsNullOrWhiteSpace(Max_value.Text))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidInput())
            {
                MessageBox.Show("Введені дані повинні містити лише цифри та не можуть бути порожніми.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ContainsWhitespace(Size_array.Text) || ContainsWhitespace(Min_value.Text) || ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введені дані не можуть містити пробіли.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int size = 0;
            if (!string.IsNullOrEmpty(Size_array.Text))
                size = int.Parse(Size_array.Text);
            if (size < 100)
            {
                MessageBox.Show("Мінімальний розмір масиву має бути не менше 100 елементів.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int minValue = 0;
            if (!string.IsNullOrEmpty(Min_value.Text))
                minValue = int.Parse(Min_value.Text);

            int maxValue = 0;
            if (!string.IsNullOrEmpty(Max_value.Text))
                maxValue = int.Parse(Max_value.Text);

            if (maxValue > 50000)
            {
                MessageBox.Show("Максимальне значення елемента не повинно перевищувати 50000.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Get the selected array type
            string arrayType = (ArrayTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();


            if (arrayType == null)
            {
                MessageBox.Show("Пожалуйста, выберите метод генерации массива в ComboBox.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            int[] array = GenerateArrayGenerateArray(size, minValue, maxValue, arrayType);
            if (array == null)
                return;

            Stopwatch stopwatch = Stopwatch.StartNew();
            ShellSortClassik(array);
            stopwatch.Stop();

            Time_taken.Text = $"{stopwatch.ElapsedTicks} ticks";
            DisplaySortedArray();
        }

        private int[] GenerateArray(int size, int minValue, int maxValue, string arrayType)
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

                case "Random":
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





        private void ShellSortClassik(int[] array)
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
            sortedArray = array;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Sedjvik_Click(object sender, RoutedEventArgs e)
        {
            _mode = "Sedjvik";

            if (string.IsNullOrWhiteSpace(Size_array.Text) || string.IsNullOrWhiteSpace(Min_value.Text) || string.IsNullOrWhiteSpace(Max_value.Text))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перевірка на коректність введення (лише цифри)
            if (!Size_array.Text.All(char.IsDigit) || !Min_value.Text.All(char.IsDigit) || !Max_value.Text.All(char.IsDigit))
            {
                MessageBox.Show("Введені дані повинні містити лише цифри.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Перевірка на пробіли
            if (Size_array.Text.Contains(" ") || Min_value.Text.Contains(" ") || Max_value.Text.Contains(" "))
            {
                MessageBox.Show("Введені дані не можуть містити пробіли.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int size = int.Parse(Size_array.Text);
            int minValue = int.Parse(Min_value.Text);
            int maxValue = int.Parse(Max_value.Text);

            if (maxValue > 50000)
            {
                MessageBox.Show("Максимальне значення елемента не повинно перевищувати 50000.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (size < 100)
            {
                MessageBox.Show("Мінімальний розмір масиву має бути не менше 100 елементів.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string arrayType = (ArrayTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();


            if (arrayType == null)
            {
                MessageBox.Show("Пожалуйста, выберите метод генерации массива в ComboBox.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            int[] array = GenerateArray(size, minValue, maxValue, arrayType);
            if (array == null)
                return;

            Stopwatch stopwatch = Stopwatch.StartNew();
            ShellSortSedgewick(array);
            stopwatch.Stop();

            Time_taken.Text = $"{stopwatch.ElapsedTicks} ticks";
            DisplaySortedArray();
        }

       

        private void ShellSortSedgewick(int[] array)
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
            sortedArray = array;
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void Fibonachi_Click(object sender, RoutedEventArgs e)
        {

            _mode = "Fibonachi";

            if (string.IsNullOrWhiteSpace(Size_array.Text) || string.IsNullOrWhiteSpace(Min_value.Text) || string.IsNullOrWhiteSpace(Max_value.Text))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidInput())
            {
                MessageBox.Show("Введені дані повинні містити лише цифри та не можуть бути порожніми.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (ContainsWhitespace(Size_array.Text) || ContainsWhitespace(Min_value.Text) || ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введені дані не можуть містити пробіли.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int size = 0;
            if (!string.IsNullOrEmpty(Size_array.Text))
                size = int.Parse(Size_array.Text);
            if (size == 0)
                return;

            int minValue = 0;
            if (!string.IsNullOrEmpty(Min_value.Text))
                minValue = int.Parse(Min_value.Text);

            int maxValue = 0;
            if (!string.IsNullOrEmpty(Max_value.Text))
                maxValue = int.Parse(Max_value.Text);

            if (maxValue > 50000)
            {
                MessageBox.Show("Максимальне значення елемента не повинно перевищувати 50000.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (size < 100)
            {
                MessageBox.Show("Мінімальний розмір масиву має бути не менше 100 елементів.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            string arrayType = (ArrayTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();


            if (arrayType == null)
            {
                MessageBox.Show("Пожалуйста, выберите метод генерации массива в ComboBox.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            int[] array = GenerateArray(size, minValue, maxValue, arrayType);
            if (array == null)
                return;

            Stopwatch stopwatch = Stopwatch.StartNew();
            ShellSortFibonacci(array);
            stopwatch.Stop();

            Time_taken.Text = $"{stopwatch.ElapsedTicks} ticks";
            DisplaySortedArray();

        }

        

        private void ShellSortFibonacci(int[] array)
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

            sortedArray = array;

        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void Tokuda_Click(object sender, RoutedEventArgs e)
        {
            _mode = "Tokuda";

            if (string.IsNullOrWhiteSpace(Size_array.Text) || string.IsNullOrWhiteSpace(Min_value.Text) || string.IsNullOrWhiteSpace(Max_value.Text))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidInput())
            {
                MessageBox.Show("Введені дані повинні містити лише цифри та не можуть бути порожніми.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (ContainsWhitespace(Size_array.Text) || ContainsWhitespace(Min_value.Text) || ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введені дані не можуть містити пробіли.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int size = 0;
            if (!string.IsNullOrEmpty(Size_array.Text))
                size = int.Parse(Size_array.Text);
            if (size == 0)
                return;

            int minValue = 0;
            if (!string.IsNullOrEmpty(Min_value.Text))
                minValue = int.Parse(Min_value.Text);

            int maxValue = 0;
            if (!string.IsNullOrEmpty(Max_value.Text))
                maxValue = int.Parse(Max_value.Text);

            if (maxValue > 50000)
            {
                MessageBox.Show("Максимальне значення елемента не повинно перевищувати 50000.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (size < 100)
            {
                MessageBox.Show("Мінімальний розмір масиву має бути не менше 100 елементів.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            string arrayType = (ArrayTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();


            if (arrayType == null)
            {
                MessageBox.Show("Пожалуйста, выберите метод генерации массива в ComboBox.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            int[] array = GenerateArray(size, minValue, maxValue, arrayType);
            if (array == null)
                return;

            Stopwatch stopwatch = Stopwatch.StartNew();
            ShellSortTokuda(array);
            stopwatch.Stop();

            Time_taken.Text = $"{stopwatch.ElapsedTicks} ticks";
            DisplaySortedArray();
        }

        private void ShellSortTokuda(int[] array)
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
                sortedArray = array;
            }
            // Обновление GUI (вынесено за внутренний цикл)


        }

        private  int[] GenerateTokudaSequence(int n)
        {
            Random rand = new Random();
            int count = rand.Next(1, n);
            int[] sequence = new int[count];
            for (int i = 0; i < count; i++)
            {
                sequence[i] = rand.Next(1, n / 2);
                sortedArray = sequence;
            }
            Array.Reverse(sequence);

         
            return sequence;
        }

      

        

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void Sberegty_Click(object sender, RoutedEventArgs e)
        {
           

            SaveArrayToFile(sortedArray);
        }




        private async Task VisualizeSortingAnimated(int[] array)
        {
            var dataSeries = new XyDataSeries<int, int>();
            for (int i = 0; i < array.Length; i++)
            {
                dataSeries.Append(i, array[i]);
            }

            var renderableSeries = new FastColumnRenderableSeries
            {
                DataSeries = dataSeries,
            };
            sortingChart.RenderableSeries.Add(renderableSeries);

            await Task.Delay(1000);

            switch (_mode)
            {
                case "classik":
                    await ClassicSort(array, dataSeries);
                    break;
                case "Tokuda":
                    await TokudaSort(array, dataSeries);
                    break;
                case "Fibonachi":
                    await FibonacciSort(array, dataSeries);
                    break;
                case "Sedjvik":
                    await SedgwickSort(array, dataSeries);
                    break;
            }
        }

        private async Task ClassicSort(int[] array, XyDataSeries<int, int> dataSeries)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[j] < array[minIndex])
                    {
                        minIndex = j;
                    }
                }

                int temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    dataSeries.Clear();
                    for (int k = 0; k < array.Length; k++)
                    {
                        dataSeries.Append(k, array[k]);
                    }
                });

                await Task.Delay(100);
            }
        }

        private async Task TokudaSort(int[] array, XyDataSeries<int, int> dataSeries)
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

                    // Обновление GUI
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        dataSeries.Clear();
                        for (int b = 0; b < array.Length; b++)
                        {
                            dataSeries.Append(b, array[b]);
                        }
                    });

                    await Task.Delay(1);
                }
                await Task.Delay(100);
            }
        }

        private async Task FibonacciSort(int[] array, XyDataSeries<int, int> dataSeries)
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

                Application.Current.Dispatcher.Invoke(() =>
                {
                    dataSeries.Clear();
                    for (int k = 0; k < array.Length; k++)
                    {
                        dataSeries.Append(k, array[k]);
                    }
                });

                await Task.Delay(1000);
            }
        }

        private async Task SedgwickSort(int[] array, XyDataSeries<int, int> dataSeries)
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

                Application.Current.Dispatcher.Invoke(() =>
                {
                    dataSeries.Clear();
                    for (int k = 0; k < array.Length; k++)
                    {
                        dataSeries.Append(k, array[k]);
                    }
                });

                await Task.Delay(1000);

                gap /= 3;
            }
        }


        private async void Visual_Click(object sender, RoutedEventArgs e)
        {

            //if (sortedArray == null)
            //{
            //    MessageBox.Show("Спочатку виконайте сортировку масива.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            if (ContainsWhitespace(Size_array.Text) || ContainsWhitespace(Min_value.Text) || ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введені дані не можуть містити пробіли.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int size = 0;
            if (!string.IsNullOrEmpty(Size_array.Text))
                size = int.Parse(Size_array.Text);
            if (size == 0)
                return;

            int minValue = 0;
            if (!string.IsNullOrEmpty(Min_value.Text))
                minValue = int.Parse(Min_value.Text);

            int maxValue = 0;
            if (!string.IsNullOrEmpty(Max_value.Text))
                maxValue = int.Parse(Max_value.Text);

            if (maxValue > 50000)
            {
                MessageBox.Show("Максимальне значення елемента не повинно перевищувати 50000.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (size < 100)
            {
                MessageBox.Show("Мінімальний розмір масиву має бути не менше 100 елементів.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            string arrayType = (ArrayTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            int[] array = GenerateArray(size, minValue, maxValue, arrayType);
            if (array == null)
                return;
            //Array.Sort(array);
            //Array.Reverse(array);
            sortingChart.RenderableSeries.Clear();

            await VisualizeSortingAnimated(array);

        }



        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            // Создаем диалоговое окно с вопросом
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите покинуть приложение?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Если пользователь нажал "Да", то завершаем работу приложения
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }




        private void Size_array_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ContainsWhitespace(Size_array.Text))
            {
                MessageBox.Show("Введені дані не можуть містити пробіли.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Min_value_TextChanged(object sender, TextChangedEventArgs e)
        {



            if (ContainsWhitespace(Min_value.Text))
            {
                MessageBox.Show("Введені дані не можуть містити пробіли.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Max_value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введені дані не можуть містити пробіли.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void Time_taken_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Ваш код обработки события здесь, если необходимо что-то делать при изменении текста в TextBox
        }


        private bool ContainsWhitespace(string text)
        {
            return text.Contains(" ");
        }

        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Проверяем, является ли введенный символ числом или десятичной точкой
            if (!char.IsDigit(e.Text, 0) && e.Text != ".")
            {
                e.Handled = true; // Отменяем ввод символа, если он не является числом или десятичной точкой
                MessageBox.Show("Будь ласка, вводіть лише числа.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        public void SaveArrayToFile(int[] array)
        {
            if (array == null || array.Length == 0)
            {
                MessageBox.Show("Массив пуст или не инициализирован.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog
            {
                FileName = "SortedArray", // Имя файла по умолчанию
                DefaultExt = ".txt", // Расширение файла по умолчанию
                Filter = "Text documents (.txt)|*.txt" // Фильтр файлов
            };

            bool result = (bool)dlg.ShowDialog();

            if (result == true)
            {
                string filePath = dlg.FileName;
                try
                {
                    using (StreamWriter file = new StreamWriter(filePath))
                    {
                        foreach (int num in array)
                        {
                            file.WriteLine(num);
                        }
                    }
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show("Відсортований масив успішно збережено у файл.", "Збереження завершено", MessageBoxButton.OK, MessageBoxImage.Information);
                    });
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        MessageBox.Show($"Помилка збереження файлу: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    });
                }
            }
        }


        private bool IsValidInput()
        {
            if (!int.TryParse(Size_array.Text, out _) || !int.TryParse(Min_value.Text, out _) || !int.TryParse(Max_value.Text, out _))
            {
                return false;
            }

            // Ensure the input contains only digits
            if (Size_array.Text.Any(c => !char.IsDigit(c)) || Min_value.Text.Any(c => !char.IsDigit(c)) || Max_value.Text.Any(c => !char.IsDigit(c)))
            {
                return false;
            }

            return true;
        }



        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Действия, выполняемые при отпускании кнопки мыши
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            // Действия, выполняемые при перемещении мыши по окну
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            // Сохранение координат и размеров текущего окна
            double left = this.Left;
            double top = this.Top;
            double width = this.Width;
            double height = this.Height;

            // Создание нового экземпляра текущего окна
            var newWindow = new Window1();

            // Применение координат и размеров к новому окну
            newWindow.Left = left;
            newWindow.Top = top;
            newWindow.Width = width;
            newWindow.Height = height;

            // Закрытие текущего окна
            this.Close();

            // Открытие нового окна
            newWindow.Show();
        }



        private void StartAnimation()
        {
            if (!isAnimating)
            {
                isAnimating = true;

                textBlock.Text = "Neo.Sort";
                ColorAnimation colorAnimation1 = new ColorAnimation();
                colorAnimation1.From = Colors.Lime;
                colorAnimation1.To = Colors.Black;
                colorAnimation1.Duration = new Duration(TimeSpan.FromSeconds(2));
                colorAnimation1.AutoReverse = true;

                ColorAnimation colorAnimation2 = new ColorAnimation();
                colorAnimation2.From = Colors.Black;
                colorAnimation2.To = Colors.Lime;
                colorAnimation2.Duration = new Duration(TimeSpan.FromSeconds(2));
                colorAnimation2.AutoReverse = true;

                textBlock.Loaded += (sender, e) =>
                {
                    Storyboard storyboard = new Storyboard();
                    storyboard.RepeatBehavior = RepeatBehavior.Forever;

                    storyboard.Children.Add(colorAnimation1);
                    Storyboard.SetTarget(colorAnimation1, textBlock);
                    Storyboard.SetTargetProperty(colorAnimation1, new PropertyPath("(TextBlock.Foreground).(SolidColorBrush.Color)"));

                    storyboard.Children.Add(colorAnimation2);
                    Storyboard.SetTarget(colorAnimation2, textBlock);
                    Storyboard.SetTargetProperty(colorAnimation2, new PropertyPath("(TextBlock.Foreground).(SolidColorBrush.Color)"));

                    storyboard.Begin();
                };

                textBlock.MouseLeftButtonDown += (sender, e) =>
                {
                    if (textBlock.Text == "Neo.Sort")
                    {
                        textBlock.Text = "IsTheBest";
                    }
                    else
                    {
                        textBlock.Text = "Neo.Sort";
                    }
                };
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 secondWindow = new Window2();
            secondWindow.Show();
        }

        private void ArrayTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
