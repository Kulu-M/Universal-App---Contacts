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

namespace Catering
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class P_Essen : Page
    {
        public P_Essen()
        {
            this.InitializeComponent();
        }

        private void tb_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (from s in App._speisen where 
                          s.bezeichnung.StartsWith(tb_filter.Text, StringComparison.CurrentCultureIgnoreCase)
                          select s).ToList();

            var filter2 = (from s in App._speisen
                           where s.bezeichnung.ToLower().Contains(tb_filter.Text.ToLower())
                           select s).ToList();

            filter.AddRange(filter2);
            lb_speisen.ItemsSource = filter.Distinct();
        }

        private void lb_speisen_Tapped(object sender, TappedRoutedEventArgs e)
        {
            pi_piSpeisen.SelectedIndex = 1;
        }

        private void lb_kunden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void bA_del_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bA_add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void bA_navFood_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
