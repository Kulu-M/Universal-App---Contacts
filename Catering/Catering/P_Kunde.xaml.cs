using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace Catering
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    /// 
      

    public sealed partial class P_Kunde : Page
    {
        Kunde kunde;
        ObservableCollection<Bestellung> k_bestellungen = new ObservableCollection<Bestellung>();

        public P_Kunde()
        {
            this.InitializeComponent();     
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        { 
            kunde = (Kunde)e.Parameter;

            if(kunde != null && kunde.nachname == "Eingeben")
            {
                piv_bestellungen.SelectedIndex = 1;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            piv_bestellungen.DataContext = kunde;

            var bes = (from b in App._bestellungen
                       where b.kd_id == kunde.id
                       select b).ToList();

            k_bestellungen = new ObservableCollection<Bestellung>(bes);

            //foreach (var item in bes)
            //{
            //    k_bestellungen.Add(item);
            //}


            cbx_bestellungen.ItemsSource = k_bestellungen;

            

        }

        private void bA_add_Click(object sender, RoutedEventArgs e)
        {
            var best = new Bestellung() {kd_id = kunde.id, bezeichnung = "Erfassen", bestellDatum = DateTime.Now, lieferDatum = DateTime.Now, anzPersonen = 1, hinweise = "Erfassen"};
            App._bestellungen.Add(best);
            k_bestellungen.Add(best);



        }

        private void bA_del_Click(object sender, RoutedEventArgs e)
        {
            if (cbx_bestellungen.SelectedItem == null)
            {
                return;
            }

            App._bestellungen.Remove(cbx_bestellungen.SelectedItem as Bestellung);


        }

        private void cbx_bestellungen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            piv_bestellungen.DataContext = (Bestellung)cbx_bestellungen.SelectedItem;
        }
    }
}
