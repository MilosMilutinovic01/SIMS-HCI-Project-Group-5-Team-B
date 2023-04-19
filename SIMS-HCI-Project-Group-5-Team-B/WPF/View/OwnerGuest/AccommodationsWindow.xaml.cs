using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for AccomodationsWindow.xaml
    /// </summary>
    public partial class AccommodationsWindow : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private AccommodationService accommodationService;
        private LocationController locationController;
        private ReservationService reservationService;
        private OwnerService ownerService;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        private SuperOwnerService superOwnerService;
        public ObservableCollection<Accommodation> Accomodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public string SearchName { get; set; } = "";
        public string City { get; set; }
        private string state;
        public string State 
        { 
            get
            {
                return state;
            }
            set
            {
                state = value;
                NotifyPropertyChanged(nameof(State));
                NotifyPropertyChanged(nameof(City));
            } 
        
        }
        public List<string> states { get; set; }
        public List<string> cities { get; set; }
        public string SearchType { get; set; } = "";
        public string SearchGuestsNumber { get; set; } = "";
        public string SearchDays { get; set; } = "";

        public string Error => null;

        private int ownerGuestId;

        public AccommodationsWindow(int ownerGuestId,LocationController locationController, OwnerService ownerService, AccommodationService accommodationService, ReservationService reservationService )
        {
            InitializeComponent();
            DataContext = this;
            this.locationController = locationController;
            this.ownerService = ownerService;
            this.accommodationService = accommodationService;
            this.reservationService = reservationService;
            //Accomodations = new ObservableCollection<Accommodation>(accommodationController.GetAll());
            ownerAccommodationGradeService = new OwnerAccommodationGradeSevice(this.reservationService);
            superOwnerService = new SuperOwnerService(ownerAccommodationGradeService, this.accommodationService);
            Accomodations = new ObservableCollection<Accommodation>(superOwnerService.GetSortedAccommodations());
            this.ownerGuestId = ownerGuestId;
            //reservationController = new ReservationController(accommodationController);
            
            states = locationController.GetStates();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            List<Accommodation> searchResult;
            if (this.IsValid)
            {
                
                if (IsSearchParameter(SearchGuestsNumber) && IsSearchParameter(SearchDays))
                {
                    searchResult = accommodationService.GetSearchResult(FindLocationId(),SearchName,SearchType, Int32.Parse(SearchGuestsNumber), Int32.Parse(SearchDays));
                }
                else if (IsSearchParameter(SearchGuestsNumber) && !IsSearchParameter(SearchDays))
                {
                    searchResult = accommodationService.GetSearchResult(FindLocationId(), SearchName, SearchType, Int32.Parse(SearchGuestsNumber));
                }
                else if (!IsSearchParameter(SearchGuestsNumber) && IsSearchParameter(SearchDays))
                {
                    searchResult = accommodationService.GetSearchResult(FindLocationId(), SearchName, SearchType, 1, Int32.Parse(SearchDays));
                }
                else
                {
                    searchResult = accommodationService.GetSearchResult(FindLocationId(), SearchName, SearchType);
                }

                if (searchResult != null)
                {
                    Accomodations.Clear();
                    foreach (Accommodation accommodation in searchResult)
                    {
                        Accomodations.Add(accommodation);
                    }
                }
            }
            else
            {
                MessageBox.Show("Search can not be preformed beacause data is not valid!");
            }
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
                AccomodationDetailsWindow accomodationDetailsWindow = new AccomodationDetailsWindow(SelectedAccommodation, reservationService, ownerGuestId);
                accomodationDetailsWindow.Show();
            }
        }
        Regex numberRegex = new Regex(@"[\d]");

        public string this[string columnName]
        {
            get
            {
                if (columnName == "State")
                {
                    if (string.IsNullOrEmpty(State))
                    {
                        return null;
                    }
                    
                }
                

                if (columnName == "City")
                {
                    if (!string.IsNullOrEmpty(State) && string.IsNullOrEmpty(City))
                    {
                        return "You must select city";
                    }
                    else
                    {
                        return null;
                    }


                }
                else if (columnName == "SearchGuestsNumber")
                {
                    if (string.IsNullOrEmpty(SearchGuestsNumber))
                    {
                        return null;
                    }
                    Match match = numberRegex.Match(SearchGuestsNumber);
                    if (!match.Success)
                        return "Invalid input";
                }
                else if (columnName == "SearchDays")
                {
                    if (string.IsNullOrEmpty(SearchDays))
                    {
                        return null;
                    }
                    Match match = numberRegex.Match(SearchDays);
                    if (!match.Success)
                        return "Invalid input";
                }
                return null;


            }
        }
        private readonly string[] _validatedProperties = { "City","State", "SearchGuestsNumber", "SearchDays" };
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

        private bool IsSearchParameter(string parameter)
        {
            return !string.IsNullOrEmpty(parameter);
        }

        private int FindLocationId()
        {
            if (string.IsNullOrEmpty(this.State) || string.IsNullOrEmpty(this.City))
            {
                return -1;
            }
            
            string State = this.State;
            string City = this.City;

            int locationId = -2;
            foreach (Location location in locationController.GetAll())
            {
                if (location.State == State && location.City == City)
                {
                    locationId = location.Id;
                    break;
                }
            }

            return locationId;
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            Accomodations.Clear();
            foreach (Accommodation accommodation in accommodationService.GetAll())
            {
                Accomodations.Add(accommodation);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cities = locationController.GetCityByState(ComboBoxStates.SelectedItem.ToString());
            ComboBoxCities.ItemsSource = cities;
        }
    }
}
