using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for AccomodationsWindow.xaml
    /// </summary>
    public partial class AccommodationsWindow : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        private AccommodationService accommodationController;
        private LocationController locationController;
        private ReservationService reservationController;
        private OwnerService ownerController;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeController;
        private SuperOwnerService superOwnerController;
        public ObservableCollection<Accommodation> Accomodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public string SearchName { get; set; } = "";
        public string SearchLocationString { get; set; } = "";
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
            this.ownerController = ownerService;
            this.accommodationController = accommodationService;
            this.reservationController = reservationService;
            //Accomodations = new ObservableCollection<Accommodation>(accommodationController.GetAll());
            ownerAccommodationGradeController = new OwnerAccommodationGradeSevice(reservationController);
            superOwnerController = new SuperOwnerService(ownerAccommodationGradeController, accommodationController);
            Accomodations = new ObservableCollection<Accommodation>(superOwnerController.AccommodationsForShowing());
            this.ownerGuestId = ownerGuestId;
            //reservationController = new ReservationController(accommodationController);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

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
                    searchResult = accommodationController.GetSearchResult(FindLocationId(),SearchName,SearchType, Int32.Parse(SearchGuestsNumber), Int32.Parse(SearchDays));
                }
                else if (IsSearchParameter(SearchGuestsNumber) && !IsSearchParameter(SearchDays))
                {
                    searchResult = accommodationController.GetSearchResult(FindLocationId(), SearchName, SearchType, Int32.Parse(SearchGuestsNumber));
                }
                else if (!IsSearchParameter(SearchGuestsNumber) && IsSearchParameter(SearchDays))
                {
                    searchResult = accommodationController.GetSearchResult(FindLocationId(), SearchName, SearchType, 1, Int32.Parse(SearchDays));
                }
                else
                {
                    searchResult = accommodationController.GetSearchResult(FindLocationId(), SearchName, SearchType);
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
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedAccommodation != null)
            {
                AccomodationDetailsWindow accomodationDetailsWindow = new AccomodationDetailsWindow(SelectedAccommodation, reservationController, ownerGuestId);
                accomodationDetailsWindow.Show();
            }
        }
        Regex numberRegex = new Regex(@"[\d]");

        Regex locationRegex = new Regex("[A-Z].{0,20},[A-Z].{0,20}");
        public string this[string columnName]
        {
            get
            {
                if (columnName == "SearchLocationString")
                {
                    if (string.IsNullOrEmpty(SearchLocationString))
                    {
                        return null;
                    }

                    Match match = locationRegex.Match(SearchLocationString);
                    if (!match.Success)
                        return "Format: city, state";
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
        private readonly string[] _validatedProperties = { "SearchLocationString", "SearchGuestsNumber", "SearchDays" };
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
            if (string.IsNullOrEmpty(SearchLocationString))
            {
                return -1;
            }
            int count = 2;
            string delimeter = ",";
            string[] locationValues = SearchLocationString.Split(delimeter, count);
            string State = locationValues[0];
            string City = locationValues[1];

            int locationId = -2;
            foreach (Location location in locationController.GetAll())
            {
                if (location.State == State && location.City == City)
                {
                    locationId = location.Id;
                }
            }

            return locationId;
        }

        private void Reset_Button_Click(object sender, RoutedEventArgs e)
        {
            Accomodations.Clear();
            foreach (Accommodation accommodation in accommodationController.GetAll())
            {
                Accomodations.Add(accommodation);
            }
        }
    }
}
