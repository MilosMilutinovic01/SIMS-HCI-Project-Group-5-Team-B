using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using System.Collections.ObjectModel;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for AccommodationForm.xaml
    /// </summary>
    public partial class AccommodationForm : Window,IDataErrorInfo, INotifyPropertyChanged
    {

        private AccommodationService accommodationService;
        private LocationController locationController;
        private OwnerService ownerService;
        
        public Accommodation Accommodation { get; set; }
        public Location Location { get; set; }
        

        public ObservableCollection<Accommodation> AccomodationsOfLogedInOwner { get; set; }

        private Owner owner;


        public List<string> states { get; set; }
        public List<string> cities;

       

        public AccommodationForm(ObservableCollection<Accommodation> AccomodationsOfLogedInOwner, Owner owner)
        {
            
            Accommodation = new Accommodation();
            InitializeComponent();
            this.DataContext = this;
            locationController = new LocationController();
            ownerService = new OwnerService();
            Location = new Location();
            accommodationService = new AccommodationService(locationController, ownerService);
            states = locationController.GetStates();
            this.AccomodationsOfLogedInOwner = AccomodationsOfLogedInOwner;
            this.owner = owner;

        }

        

        private void Create_Accommodation_Click(object sender, RoutedEventArgs e)
        {
            if (Accommodation.IsValid)
            {
                
                Location existingLocation = locationController.GetLocation(Location);
                Accommodation.OwnerId = owner.Id;
                if (existingLocation != null)
                {
                    Accommodation.LocationId = existingLocation.Id;
                    
                    accommodationService.Save(Accommodation);
                    AccomodationsOfLogedInOwner.Add(Accommodation);
                }
                else
                {
                    Accommodation.LocationId = locationController.makeId();
                    locationController.Save(Location);
                }


                Close();
            }
            else
            {
                MessageBox.Show("Accommodation can't be created, because fileds are not valid");
            }
        }

        private void TypeChanged_ComboBox(object sender,RoutedEventArgs e)
        {
            ComboBox cBox = (ComboBox)sender;
            ComboBoxItem cbItem = (ComboBoxItem)cBox.SelectedItem;
            string newType = (string)cbItem.Content;
            if(newType == "Apartment")
            {
                Accommodation.Type = "Apartment";
            }
            else if(newType == "House")
            {
                Accommodation.Type = "House";
            }
            else if(newType == "Cottage")
            {
                Accommodation.Type = "Cottage";
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Regex locationRegex = new Regex("[A-Z].{0,20},[A-Z].{0,20}");
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Location.City")
                {
                    if (string.IsNullOrEmpty(Location.City))
                        return "Filed must be filled";
                }
                else if(columnName == "Location.State")
                {
                    if (string.IsNullOrEmpty(Location.State))
                        return "Filed must be filled";
                }
                return null;
            }

        }
        private readonly string[] _validatedProperties = { "Location.City" , "Location.State"};
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cities = locationController.GetCityByState(ComboBoxStates.SelectedItem.ToString());
            ComboBoxCities.ItemsSource = cities;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
