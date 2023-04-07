﻿using System;
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
using SIMS_HCI_Project_Group_5_Team_B.Model;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for AccommodationForm.xaml
    /// </summary>
    public partial class AccommodationForm : Window,IDataErrorInfo, INotifyPropertyChanged
    {

        private AccommodationService accommodationController;
        private LocationController locationController;
        private OwnerService ownerController;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeController;
        private ReservationService reservationController;
        private SuperOwnerService superOwnerController;
        public Accommodation Accommodation { get; set; }
        public Location Location { get; set; }
        private string locationString;


        public List<string> states { get; set; }
        public List<string> cities;

        public string LocationString
        {
            get { return locationString; }
            set
            {
                if (locationString != value)
                {
                    locationString = value;
                    OnPropertyChanged();

                }

            }
        }


        public AccommodationForm()
        {
            locationString = "";
            Accommodation = new Accommodation();
            InitializeComponent();
            this.DataContext = this;
            locationController = new LocationController();
            ownerController = new OwnerService();
            Location = new Location();
            accommodationController = new AccommodationService(locationController, ownerController);
            states = locationController.GetStates();
        }

        

        private void Create_Accommodation_Click(object sender, RoutedEventArgs e)
        {
            if (Accommodation.IsValid)
            {
                /*int count = 2;
                String delimeter = ",";
                string[] locationValues = locationString.Split(delimeter, count);
                Location = new Location(locationValues[0], locationValues[1]);

                accommodationController.AddAccommodation(Accommodation, Location);*/

                Location existingLocation = locationController.GetLocation(Location);

                if (existingLocation != null)
                {
                    Accommodation.LocationId = existingLocation.Id;
                    accommodationController.Save(Accommodation);
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


    }
}