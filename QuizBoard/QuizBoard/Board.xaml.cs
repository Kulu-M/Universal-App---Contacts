using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace QuizBoard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Board : Page
    {
        public Board()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            g_board.Width = 500;
            g_board.Height = 500;

            for (int i = 1; i <= App._boardSize; i++)
            {
                g_board.ColumnDefinitions.Add(new ColumnDefinition());
                g_board.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < g_board.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < g_board.ColumnDefinitions.Count; j++)
                {
                    var rect = new Rectangle();

                    if ((i + j)%2 == 0)
                        rect.Fill = new SolidColorBrush(Colors.DarkOliveGreen);
                    else 
                        rect.Fill = new SolidColorBrush(Colors.Yellow);


                    g_board.Children.Add(rect);

                    rect.SetValue(Grid.ColumnProperty, j);
                    rect.SetValue(Grid.RowProperty, i);
                }
            }
            el_player.SetValue(Canvas.ZIndexProperty, 1000);
            el_player.SetValue(Grid.RowProperty, App._boardSize -1); 
        }

        private void b_move_Click(object sender, RoutedEventArgs e)
        {
                 
            var posR = (int)el_player.GetValue(Grid.RowProperty);

            var increment = posR%2 == 0 ? -1 : 1;

            var posC = (int)el_player.GetValue(Grid.ColumnProperty) + increment;

            if (increment == 1 && posC > App._boardSize -1 )
            {
                posC -= 1;
                posR -= 1;
            }

            if (increment == -1 && posC < 0)
            {
                posC = 0;
                posR -= 1;
            }

            el_player.SetValue(Grid.ColumnProperty, posC );
            el_player.SetValue(Grid.RowProperty, posR );
        }
    }
}
