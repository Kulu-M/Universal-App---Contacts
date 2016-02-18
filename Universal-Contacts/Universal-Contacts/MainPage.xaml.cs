using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Universal_Contacts;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Universal_Contacts
{

    public sealed partial class MainPage : Page
    {
        Random rnd = new Random();

        public  MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            lb_persons.ItemsSource = App._persons;

            //this.DataContext = this;
        }

        private void b_generate_Click(object sender, RoutedEventArgs e)
        {
            App._persons = new ObservableCollection<Person>();
            generatePersons(20);
        }

        private void generatePersons(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var person = new Person();

                person.nachname = generatePersonNachname();

                person.gender = generatePersonGender();

                person.vorname = generatePersonVorname(person.gender);  

                person.dateofbirth = generateAge(16, 40);

                person.familyStatz = generateFamilyStatus();

                person.zipcode = generateZipCode();

                App._persons.Add(person);
            }
            lb_persons.ItemsSource = App._persons;
        }

        private string generateFamilyStatus()
        {
            int caseSwitch = rnd.Next(4);
            switch (caseSwitch)
            {
                case 1:
                    return Person.familyStatus.Married.ToString();
                case 2:
                    return Person.familyStatus.Unmarried.ToString();
                case 3:
                    return Person.familyStatus.Divorced.ToString();
                case 0:
                    return Person.familyStatus.Widowed.ToString();      
            }
            return null;
        }

        private string generatePersonGender()
        {
            string gender;

            if (rnd.Next(2) == 0)
            {
                gender = Person.sex.Female.ToString();
            }
            else
            {
                gender = Person.sex.Male.ToString();
            }

            return gender;
        }

        private string generatePersonVorname(string gender)
        {
            List<string> vn_male = new List<string> { "Light", "Makarov-San", "John", "Tony", "Agent", "George", "Obi-Wan" };

            List<string> vn_female = new List<string> { "Kerrigan", "Lara", "Samus", "Faith", "Rochelle", "Katniss" };

            string vorname = "Max";

            if (gender == "Female")
            {
                vorname = vn_female[rnd.Next(vn_female.Count)];
            }
            else if(gender == "Male")
            {
                vorname = vn_male[rnd.Next(vn_male.Count)];
            }
            else
            {
                //generate Transvestit Names from both Lists
            }
            return vorname;
        }

        private string generatePersonNachname()
        {
            List<string> nn = new List<string> { "Yagami", "Wayne", "Stark", "Croft", "47", "R. R. Martin", "Kenobi", "Aran" };

            string nachname = nn[rnd.Next(nn.Count)];

            return nachname;
        }

        private int generateZipCode()
        {
            int plz = rnd.Next(01001, 99998);
            return plz;
        }

        private DateTime generateAge(int minAge, int maxAge)
        {
            int minD = minAge * 365;
            int maxD = maxAge * 365;
            int days = rnd.Next(minD, maxD);
            DateTime date = DateTime.Now.AddDays(-days);
            return date;
        }

        private void Lb_persons_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_persons.SelectedItem == null)
            {
                return;
            }
            else
            {
                //Change Pivot
                pi_person.SelectedIndex = 1;

                //Calc Age
                var now = DateTime.Today;
                var result = (now - (lb_persons.SelectedItem as Person).dateofbirth).Days / 365;

                //Configure DatePicker
                dp_dob.Date = (lb_persons.SelectedItem as Person).dateofbirth;
            }
        }

        public int ageProperty
        {
            get { return (int) GetValue(agePropertyProperty); }
            set { SetValue(agePropertyProperty, value); }
        }

        private void AppBarButton_prev_OnClick(object sender, RoutedEventArgs e)
        {
            if (lb_persons.SelectedIndex > 0)
                lb_persons.SelectedIndex--;  
        }

        private void AppBarButton_next_OnClick(object sender, RoutedEventArgs e)
        {
            if (lb_persons.SelectedIndex < App._persons.Count - 1)
            lb_persons.SelectedIndex ++;
        }

        private void tb_text_textchanged(object sender, TextChangedEventArgs e)
        {
            App._save = true;
        }

        private void AppBarButton_save_OnClick(object sender, RoutedEventArgs e)
        {
            //if (App._save)
            //{

            //}

            var x = SaveLoad.writeObjektAsync(App.savefile, App._persons);

            refreshList();
            pi_person.SelectedIndex = 0;

        }

        public static readonly DependencyProperty agePropertyProperty = DependencyProperty.Register(
            "ageProperty", typeof(int), typeof(MainPage), new PropertyMetadata(default(int)));

        private void AppBarButton_delete_OnClick(object sender, RoutedEventArgs e)
        {
            if (lb_persons.SelectedItem == null)
                return;
            App._persons.Remove(lb_persons.SelectedItem as Person);
            pi_person.SelectedIndex = 0;
        }

        private void AppBarButton_add_OnClick(object sender, RoutedEventArgs e)
        {
            var p = new Person {vorname = "Please", nachname = "change"};
            App._persons.Add(p);
            lb_persons.SelectedIndex = App._persons.Count - 1;
        }

        public void refreshList()
        {
            lb_persons.ItemsSource = null;
            lb_persons.ItemsSource = App._persons;
        }
    }
}
