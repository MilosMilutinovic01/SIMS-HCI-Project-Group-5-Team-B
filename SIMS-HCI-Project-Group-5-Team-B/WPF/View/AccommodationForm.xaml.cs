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
using SIMS_HCI_Project_Group_5_Team_B.Utilities;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for AccommodationForm.xaml
    /// </summary>
    public partial class AccommodationForm : Window/*,IDataErrorInfo*//*, INotifyPropertyChanged*/
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

        public RelayCommand CreateAccommodationCommand { get; }
        public RelayCommand CancelCommand { get; }

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
            CreateAccommodationCommand = new RelayCommand(CreateAccommodationExecute, CanExecute);
            CancelCommand = new RelayCommand(CancelExecute, CanExecute);
        }

        public bool CanExecute()
        {
            return true;
        }

        private void CreateAccommodationExecute()
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
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("Accommodation can't be created, because fileds are not valid");
                }
                else
                {
                    MessageBox.Show("Smestaj ne moze biti kreiran, jer polja nisu validna");
                }
               
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
                //Accommodation.Type = TYPE.Apartment;
            }
            else if(newType == "House")
            {
                Accommodation.Type = "House";
                //Accommodation.Type = TYPE.House;
            }
            else if(newType == "Cottage")
            {
                Accommodation.Type = "Cottage";
                //Accommodation.Type = TYPE.Cottage;
            }

        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cities = locationController.GetCityByState(ComboBoxStates.SelectedItem.ToString());
            ComboBoxCities.ItemsSource = cities;
        }

        private void CancelExecute()
        {
            Close();
        }
    }
}
