using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Bomber
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            int x = 18;
            int y = 14;
            int bombsQty = 40;

            List<List<int>> field = CreateField(x, y, bombsQty);

            InitializeComponent();
            lst.ItemsSource = field;
        }

        List<List<int>> CreateField(int x, int y, int bombsQty)
        {
            //Создание пустого поля
            int[,] field = new int[x, y];
            List<List<int>> res = new List<List<int>>();
            //Заполнение поля нулями
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    field[i, j] = 0;
                }
            }
            //Создание объекта для генерации чисел
            Random rnd = new Random();
            int bombsCreated = 0;
            //Создание бомб на поле
            while (bombsCreated != bombsQty)
            {
                int xCoor = rnd.Next(0, x);
                int yCoor = rnd.Next(0, y);
                if (field[xCoor, yCoor] == 0)
                {
                    field[xCoor, yCoor] = 9;
                    bombsCreated++;
                }
                else continue;
            }
            //Заполнение поля верными значениями
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (field[i, j] == 9) continue;
                    else
                    {
                        if (i != 0) field[i, j] += field[i - 1, j];
                        if (i != x - 1) field[i, j] += field[i + 1, j];
                        if (j != 0) field[i, j] += field[i, j - 1];
                        if (j != y - 1) field[i, j] += field[i, j + 1];
                        if (i != 0 && j != 0) field[i, j] += field[i - 1, j - 1];
                        if (i != x - 1 && j != y - 1) field[i, j] += field[i + 1, j + 1];
                        if (i != 0 && j != y - 1) field[i, j] += field[i - 1, j + 1];
                        if (i != x - 1 && j != 0) field[i, j] += field[i + 1, j - 1];
                        if (field[i, j] != 0) field[i, j] /= 9;
                        else continue;
                    }
                }
            }

            for (int i = 0; i < x; i++)
            {
                res.Add(new List<int>());

                for (int j = 0; j < y; j++)
                {
                    res[i].Add(field[i, j]);
                }
            }
            return res;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).MouseRightButtonDown -= Button_MouseRightButtonDown;
            ((Button)sender).Background = new SolidColorBrush(Color.FromArgb(255, 165, 202, 251));
            switch (((Button)sender).Content.ToString())
            {
                case "1":
                    ((Button)sender).Foreground = Brushes.Green;
                    ((Button)sender).FontSize = 12;
                    break;
                case "2":
                    ((Button)sender).Foreground = Brushes.Blue;
                    ((Button)sender).FontSize = 12;
                    break;
                case "3":
                    ((Button)sender).Foreground = Brushes.Brown;
                    ((Button)sender).FontSize = 12;
                    break;
                case "4":
                    ((Button)sender).Foreground = Brushes.OrangeRed;
                    ((Button)sender).FontSize = 12;
                    break;
                case "5":
                    ((Button)sender).Foreground = Brushes.Red;
                    ((Button)sender).FontSize = 12;
                    break;
                case "6":
                    ((Button)sender).Foreground = Brushes.Red;
                    ((Button)sender).FontSize = 12;
                    break;
                case "9":
                    ((Button)sender).Background = Brushes.Black;
                    MessageBoxResult result = MessageBox.Show("Game over!\nWould you like to start new game?", "Game Over!", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                        Application.Current.Shutdown();
                    }
                    else Close();
                        break;
                case "0":
                    break;
            }
        }

        private void Button_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (((Button)sender).Background == Brushes.White)
            {
                ((Button)sender).Background = Brushes.Red;
                ((Button)sender).Click -= Button_Click;
            }
            else
            {
                ((Button)sender).Background = Brushes.White;
                ((Button)sender).Click += Button_Click;
            }
        }

    }
}
