using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace Catering
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int kdNr = 0;

        public MainPage()
        {
            this.InitializeComponent();
        }

       

        public List<Kunde> myListe = new List<Kunde>();


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lb_kunden.ItemsSource = App._kunden;

            //Mockup-Data:

            /*
            var mykunde = makeKunde(kdNr);

            myListe.Add(mykunde);

            kdNr++;

            var mykunde2 = makeKunde(kdNr);

            myListe.Add(mykunde2);

            lb_kunden.ItemsSource = myListe;
    
            */


        }

        private Kunde makeKunde(int kdNr)
        {
            var kunde = new Kunde
            {
                id = string.Format("k{0:00000}", kdNr),
                nachname = "Tran",
                vorname = "Michael",
                kundeSeit = DateTime.Today
            };

            return kunde;


        }


        private void bA_add_Click(object sender, RoutedEventArgs e)
        {
            App._kdId++;
            var kunde = new Kunde { id = string.Format("k{0:00000}", App._kdId),
                                    nachname = "Eingeben",
                                    vorname = "Eingeben",
                                    kundeSeit = DateTime.Today
            };
            App._kunden.Add(kunde);
            
        }

        private void lb_kunden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var kunde = (Kunde)lb_kunden.SelectedItem;

            pi_mainPi.SelectedIndex = 1;

            pi_mainPi.DataContext = kunde;
        }

        private void tb_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filter = (from k in App._kunden
                          where k.nachname.StartsWith(tb_filter.Text, StringComparison.CurrentCultureIgnoreCase)
                          select k).ToList();

            var filter2 = (from k in App._kunden
                          where k.nachname.ToLower().Contains(tb_filter.Text.ToLower())
                          select k).ToList();


            filter.AddRange(filter2);

            lb_kunden.ItemsSource = filter.Distinct() ;
        }

        private void bA_navOrders_Click(object sender, RoutedEventArgs e)
        {
            if (lb_kunden.SelectedItem != null)
                Frame.Navigate(typeof (P_Kunde), lb_kunden.SelectedItem as Kunde);
        }

        private async void bA_del_Click(object sender, RoutedEventArgs e)
        {
            var selKunde = lb_kunden.SelectedItem as Kunde;

            if (selKunde == null)
            {
                return;
            }

            var obBestellung =
                (from b in App._bestellungen where b.kd_id == (selKunde).id select b)
                    .FirstOrDefault();

            if (obBestellung == null)
            {
                App._kunden.Remove(selKunde);
            }
            else
            {
                MessageDialog msg = new MessageDialog("Customer cannot be deleted!\nPlease delete all Orders from Customer first.");
                await msg.ShowAsync();
            }


        }

        private void pi_mainPi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pi_mainPi.SelectedIndex == 0)
            {
                bA_Orders.Visibility = Visibility.Collapsed;
                bA_add.Visibility = Visibility.Visible;
                bA_delete.Visibility = Visibility.Collapsed;
            }
            if (pi_mainPi.SelectedIndex == 1)
            {
                bA_Orders.Visibility = Visibility.Visible;
                bA_add.Visibility = Visibility.Collapsed;
                bA_delete.Visibility = Visibility.Visible;
            }
        }
    }//class
}//namespace
