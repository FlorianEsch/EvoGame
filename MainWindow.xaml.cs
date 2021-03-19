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
using System.Windows.Threading;


namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
        }


        bool isStart = false;
        bool paint = false;
        DispatcherTimer timer = new DispatcherTimer();
        //set Zell 1200
        const int cellWidth = 40;
        const int cellHeight = 40;
        Rectangle[,] field = new Rectangle[cellWidth, cellHeight];
        private void Button_Click(object sender, RoutedEventArgs e)
        {



            for (int i = 0; i < cellWidth; i++)
            {
                for (int j = 0; j < cellHeight; j++)
                {
                    //set Cell
                    Rectangle r = new Rectangle();
                    r.Width = World.ActualWidth / cellWidth - 2.0;
                    r.Height = World.ActualHeight / cellHeight - 2.0;
                    r.Fill = Brushes.Blue;
                    World.Children.Add(r);
                    //set World 
                    Canvas.SetLeft(r, j * World.ActualWidth / cellWidth);
                    Canvas.SetTop(r, i * World.ActualHeight / cellHeight);
                    r.MouseDown += R_MouseDown;
                    r.MouseLeave += R_MouseLeave;
                    field[i, j] = r;
                    isStart = true;

                }
            }

        }


        private void R_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Click to change Color
            ((Rectangle)sender).Fill =
            (((Rectangle)sender).Fill == Brushes.Blue) ? Brushes.Red : Brushes.Blue;
        }

        private void R_MouseLeave(object sender, MouseEventArgs e)
        {
            if (paint == true)
            {
                //Brush
                ((Rectangle)sender).Fill =
                (((Rectangle)sender).Fill == Brushes.Blue) ? Brushes.Red : Brushes.Blue;
            }
        }





        private void Timer_Tick(object sender, EventArgs e)
        {
            //Which color around the cell 
            int[,] cellNeighbour = new int[cellHeight, cellWidth];
            for (int i = 0; i < cellHeight; i++)
            {
                for (int j = 0; j < cellWidth; j++)
                {
                    int neighbour = 0;
                    int iOver = i - 1;
                    if (iOver < 0)
                    {
                        iOver = cellHeight - 1;
                    }
                    int iUnder = i + 1;
                    if (iUnder >= cellHeight)
                    {
                        iUnder = 0;
                    }
                    int jRight = j + 1;
                    if (jRight >= cellWidth)
                    {
                        jRight = 0;
                    }
                    int jLeft = j - 1;
                    if (jLeft < 0)
                    {
                        jLeft = cellWidth - 1;
                    }
                    if (field[iOver, jLeft].Fill == Brushes.Red)
                    {
                        neighbour++;
                    }
                    if (field[iOver, j].Fill == Brushes.Red)
                    {
                        neighbour++;
                    }

                    if (field[iOver, jRight].Fill == Brushes.Red)
                    {
                        neighbour++;
                    }
                    if (field[i, jLeft].Fill == Brushes.Red)
                    {
                        neighbour++;
                    }

                    if (field[i, jRight].Fill == Brushes.Red)
                    {
                        neighbour++;
                    }
                    if (field[iUnder, jLeft].Fill == Brushes.Red)
                    {
                        neighbour++;
                    }

                    if (field[iUnder, j].Fill == Brushes.Red)
                    {
                        neighbour++;
                    }
                    if (field[iUnder, jRight].Fill == Brushes.Red)
                    {
                        neighbour++;
                    }
                    cellNeighbour[i, j] = neighbour;
                }
            }
            //Dead or Life 
            for (int i = 0; i < cellHeight; i++)
            {
                for (int j = 0; j < cellWidth; j++)
                {
                    if (cellNeighbour[i, j] < 2 || cellNeighbour[i, j] > 3)
                    {
                        field[i, j].Fill = Brushes.Blue;
                    }
                    else if (cellNeighbour[i, j] == 3)
                    {
                        field[i, j].Fill = Brushes.Red;
                    }


                }
            }


        }



        bool run = false;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (run == true && isStart == true)
            {
                timer.Stop();
                run = false;
                StartStop.Content = "Start";
            }
            else if (isStart == true)

            {
                timer.Start();
                run = true;
                StartStop.Content = "Stop";

            }
            else
            {
                MessageBox.Show("First Set the World!");
            }

        }






        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (paint == true)
            {
                paint = false;
                Paint.Content = "Point";
            }
            else
            {
                paint = true;
                Paint.Content = "Paint";
            }
        }
    }
}
