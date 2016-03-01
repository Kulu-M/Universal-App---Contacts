using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace QuizBoard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        private void cb_boardSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App._boardSize = (int) cb_boardSize.SelectedItem;

            Windows.Storage.ApplicationData.Current.RoamingSettings.Values["boardSize"] = (int)cb_boardSize.SelectedItem;
        }

        private void Settings_OnLoaded(object sender, RoutedEventArgs e)
        {
            cb_boardSize.ItemsSource = new List<int> {8, 9, 10 };

            cb_boardSize.SelectedItem = App._boardSize;
        }
    }
}
