using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals.RenderableSeries;
using SciChart.Charting.Visuals;
using System.Threading;
using SciChart.Data.Model;
using SciChart.Charting.Visuals.Axes;
using System.Windows.Media.Animation;


namespace kursach3._0
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private int[] sortedArray;
        private bool isAnimating;

        public Window1()
        {
            InitializeComponent();
            StartAnimation();
        }







        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        private void Classik_Click(object sender, RoutedEventArgs e)
        {

            if (!IsValidInput())
            {
                MessageBox.Show("Введенные данные должны содержать только цифры и не должны быть пустыми.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (string.IsNullOrWhiteSpace(Size_array.Text) || string.IsNullOrWhiteSpace(Min_value.Text) || string.IsNullOrWhiteSpace(Max_value.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ContainsWhitespace(Size_array.Text) || ContainsWhitespace(Min_value.Text) || ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введенные данные не должны содержать пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int size = 0;
            if (!string.IsNullOrEmpty(Size_array.Text))
                size = int.Parse(Size_array.Text);
            if (size < 100)
            {
                MessageBox.Show("Минимальный размер массива должен быть не менее 100 элементов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Максимальное значение элемента не должно превышать 50000.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int[] array = GenerateRandomArray2(size, minValue, maxValue);
            if (array == null)
                return;

            Stopwatch stopwatch = Stopwatch.StartNew();
            ShellSortClassik(array);
            stopwatch.Stop();

            Time_taken.Text = $"{stopwatch.ElapsedTicks} ticks";


            //var totalMs = TimeSpan.FromTicks(stopwatch.ElapsedTicks).TotalMilliseconds;

        }

        private int[] GenerateRandomArray(int size, int minValue, int maxValue)
        {
            if (size < 100)
            {
                MessageBox.Show("Минимальный размер массива должен быть не менее 100 элементов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            if (maxValue > 50000)
            {
                MessageBox.Show("Максимальное значение элемента не должно превышать 50000.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(minValue, maxValue + 1);
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


            if (!IsValidInput())
            {
                MessageBox.Show("Введенные данные должны содержать только цифры и не должны быть пустыми.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (string.IsNullOrWhiteSpace(Size_array.Text) || string.IsNullOrWhiteSpace(Min_value.Text) || string.IsNullOrWhiteSpace(Max_value.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            if (ContainsWhitespace(Size_array.Text) || ContainsWhitespace(Min_value.Text) || ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введенные данные не должны содержать пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Максимальное значение элемента не должно превышать 50000.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int[] array = GenerateRandomArray2(size, minValue, maxValue);

            Stopwatch stopwatch = Stopwatch.StartNew();
            ShellSortSedgewick(array);
            stopwatch.Stop();

            Time_taken.Text = $"{stopwatch.ElapsedTicks} ticks";
        }

        private int[] GenerateRandomArray2(int size, int minValue, int maxValue)
        {
            if (maxValue > 50000)
            {
                MessageBox.Show("Максимальное значение элемента не должно превышать 50000.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(minValue, maxValue + 1);
            }
            return array;
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
                    array[j] = temp;

                    sortedArray = array;
                }
                gap /= 3;
            }



        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        private void Fibonachi_Click(object sender, RoutedEventArgs e)
        {
            if (!IsValidInput())
            {
                MessageBox.Show("Введенные данные должны содержать только цифры и не должны быть пустыми.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(Size_array.Text) || string.IsNullOrWhiteSpace(Min_value.Text) || string.IsNullOrWhiteSpace(Max_value.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            if (ContainsWhitespace(Size_array.Text) || ContainsWhitespace(Min_value.Text) || ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введенные данные не должны содержать пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Максимальное значение элемента не должно превышать 50000.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int[] array = GenerateRandomArray(size, minValue, maxValue);

            Stopwatch stopwatch = Stopwatch.StartNew();
            ShellSortSedgewick(array);
            stopwatch.Stop();

            Time_taken.Text = $"{stopwatch.ElapsedTicks} ticks";
        }

        private int[] GenerateRandomArray1(int size, int minValue, int maxValue)
        {
            Random random = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(minValue, maxValue + 1);
            }
            return array;
        }

        private void ShellSortFibonacci(int[] array)
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

                    sortedArray = array;
                    i++;
                }
                fib3 = fib1;
                fib2 = fib1;
                fib1 = fib3 - fib2;
            }
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        private void Tokuda_Click(object sender, RoutedEventArgs e)
        {


            if (!IsValidInput())
            {
                MessageBox.Show("Введенные данные должны содержать только цифры и не должны быть пустыми.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (string.IsNullOrWhiteSpace(Size_array.Text) || string.IsNullOrWhiteSpace(Min_value.Text) || string.IsNullOrWhiteSpace(Max_value.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (ContainsWhitespace(Size_array.Text) || ContainsWhitespace(Min_value.Text) || ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введенные данные не должны содержать пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Максимальное значение элемента не должно превышать 50000.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int[] array = GenerateRandomArray(size, minValue, maxValue);

            Stopwatch stopwatch = Stopwatch.StartNew();
            ShellSortSedgewick(array);
            stopwatch.Stop();

            Time_taken.Text = $"{stopwatch.ElapsedTicks} ticks";
        }

        private void ShellSortTokuda(int[] array)
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

                        sortedArray = array;

                    }
                    array[j] = temp;

                    

                }
                
            }
        }

        private  int[] GenerateTokudaSequence(int n)
        {
            Random rand = new Random();
            int count = rand.Next(5, 15);
            int[] sequence = new int[count];
            for (int i = 0; i < count; i++)
            {
                sequence[i] = rand.Next(1, n / 2);
                sortedArray = sequence;
            }
            Array.Reverse(sequence);

         
            return sequence;
        }

        public static int[] GenerateRandomArray4(int size, int minValue, int maxValue)
        {
            if (maxValue > 50000)
            {
                MessageBox.Show("Максимальное значение элемента не должно превышать 50000.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            Random rand = new Random();
            int[] array = new int[size];
            for (int i = 0; i < size; i++)
            {
                array[i] = rand.Next(minValue, maxValue + 1);
            }
            return array;
        }

        internal static object GenerateRandomArray(int size)
        {
            throw new NotImplementedException();
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        private void Sberegty_Click(object sender, RoutedEventArgs e)
        {
            if (sortedArray == null)
            {
                MessageBox.Show("Сначала выполните сортировку массива.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SaveArrayToFile((int[])sortedArray);
        }








        private async Task VisualizeSortingAnimated(int[] array, Action<int[]> sortingAlgorithm)
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

            await Task.Run(async () =>
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
            });

            sortingChart.RenderableSeries.Clear();
        }

        private async void Visual_Click(object sender, RoutedEventArgs e)
        {
            if (ContainsWhitespace(Size_array.Text) || ContainsWhitespace(Min_value.Text) || ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введенные данные не должны содержать пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Максимальное значение элемента не должно превышать 50000.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int[] array = GenerateRandomArray(size, minValue, maxValue);

            sortingChart.RenderableSeries.Clear();

            await VisualizeSortingAnimated(array, ShellSortClassik);
            await VisualizeSortingAnimated(array, ShellSortSedgewick);
            await VisualizeSortingAnimated(array, ShellSortFibonacci);
            await VisualizeSortingAnimated(array, ShellSortTokuda);
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
                MessageBox.Show("Введенные данные не должны содержать пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Min_value_TextChanged(object sender, TextChangedEventArgs e)
        {




            if (ContainsWhitespace(Min_value.Text))
            {
                MessageBox.Show("Введенные данные не должны содержать пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Max_value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ContainsWhitespace(Max_value.Text))
            {
                MessageBox.Show("Введенные данные не должны содержать пробелы.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Пожалуйста, вводите только числа.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void SaveArrayToFile(int[] array)
        {
            // Предположим, что пользователь выбирает место сохранения файла
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "SortedArray"; // Имя файла по умолчанию
            dlg.DefaultExt = ".txt"; // Расширение файла по умолчанию
            dlg.Filter = "Text documents (.txt)|*.txt"; // Фильтр файлов

            // Показываем диалог для сохранения файла
            Nullable<bool> result = dlg.ShowDialog();

            // Пользователь выбрал файл для сохранения
            if (result == true)
            {
                // Сохраняем отсортированный массив в файл
                string filePath = dlg.FileName;
                try
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                    {
                        foreach (int num in array)
                        {
                            file.WriteLine(num);
                        }
                    }
                    MessageBox.Show("Отсортированный массив успешно сохранен в файл.", "Сохранение завершено", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }

}
