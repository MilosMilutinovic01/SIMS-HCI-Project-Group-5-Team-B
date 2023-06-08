using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class AccommodationProposalsViewModel
    {
        public ObservableCollection<Location> PopularLocations { get; set; }
        public List<Location> UnpopularLocations { get; set; }
        public ObservableCollection<Accommodation> AccommodationsOnUnpopularLocations { get; set; }
        public AccommodationService accommodationService;
        public YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService;
        public ObservableCollection<Accommodation> AccommodationsOfLogedInOwner { get; set; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand CloseAccommodation { get; }
        public RelayCommand AddAccommodationCommand { get; }
        public Accommodation SelectedAccommodation { get; set; }
        public Location SelectedLocation { get; set; }
        public Owner Owner { get; set; }
        public AccommodationProposalsViewModel(AccommodationService accommodationService, YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService,Owner owner, ObservableCollection<Accommodation> accommodationsOfLogedInOwner)
        {
            this.accommodationService = accommodationService;
            this.yearlyAccommodationStatisticsService = yearlyAccommodationStatisticsService;
            this.Owner = owner;
            this.AccommodationsOfLogedInOwner = accommodationsOfLogedInOwner;
            PopularLocations = new ObservableCollection<Location>(GetPopularLocations());
            UnpopularLocations = new List<Location>(GetUnpopularLocations());
            AccommodationsOnUnpopularLocations = new ObservableCollection<Accommodation>(GetOwnersAccommodationsOnUnpopularLocations());
            CloseCommand = new RelayCommand(Cancel_Exexute, CanExecute);
            CloseAccommodation = new RelayCommand(DeleteAccommmodationExecute, CanExecute);
            AddAccommodationCommand = new RelayCommand(AddAccommodationOnPopularLocationExecute, CanExecute);
        }
        public void Cancel_Exexute()
        {
            App.Current.Windows[4].Close();
        }

        public bool CanExecute()
        {
            return true;
        }
        private ObservableCollection<Location> GetPopularLocations()
        {
            List<Location> locationsWithAccommodation = new List<Location>(accommodationService.GetLocationsWithAccommodation());

            Dictionary<Location,double> numberOfReservationsOnLocation = new Dictionary<Location, double>();
            Dictionary<Location, double> busynessOnLocation = new Dictionary<Location, double>();


            foreach (Location location in locationsWithAccommodation)
            {
                List<Accommodation> accommodationsOnLocation = new List<Accommodation>(accommodationService.GetAccommodationsOnLocation(location));
                var averageNumberOfReservations = yearlyAccommodationStatisticsService.CalculateAverageNumberOfReservationsForAccommodations(accommodationsOnLocation);
                var averageBusyness = yearlyAccommodationStatisticsService.CalculateAverageBusynessForAccommodations(accommodationsOnLocation);
                
                numberOfReservationsOnLocation.Add(location, averageNumberOfReservations);
                busynessOnLocation.Add(location, averageBusyness);
            }

            var SortedBusynessOnLocation = new Dictionary<Location, double>(busynessOnLocation.OrderByDescending(pair => pair.Value));
            var SortedNumberOfReservationsOnLocation = new Dictionary<Location, double>(numberOfReservationsOnLocation.OrderByDescending(pair => pair.Value));
            var topFiveBusynessLocations = SortedBusynessOnLocation.Take(5).Select(pair => pair.Key).ToList();
            var topFiveReservationLocations = SortedNumberOfReservationsOnLocation.Take(5).Select(pair => pair.Key).ToList();
            var intersectingLocations = topFiveBusynessLocations.Intersect(topFiveReservationLocations).ToList();
            var popularLocations = new ObservableCollection<Location>(intersectingLocations);
            return popularLocations;
        }

        private List<Location> GetUnpopularLocations()
        {
            List<Location> locationsWithAccommodation = new List<Location>(accommodationService.GetLocationsWithAccommodation());

            Dictionary<Location, double> numberOfReservationsOnLocation = new Dictionary<Location, double>();
            Dictionary<Location, double> busynessOnLocation = new Dictionary<Location, double>();


            foreach (Location location in locationsWithAccommodation)
            {
                List<Accommodation> accommodationsOnLocation = new List<Accommodation>(accommodationService.GetAccommodationsOnLocation(location));
                var averageNumberOfReservations = yearlyAccommodationStatisticsService.CalculateAverageNumberOfReservationsForAccommodations(accommodationsOnLocation);
                var averageBusyness = yearlyAccommodationStatisticsService.CalculateAverageBusynessForAccommodations(accommodationsOnLocation);

                numberOfReservationsOnLocation.Add(location, averageNumberOfReservations);
                busynessOnLocation.Add(location, averageBusyness);
            }

            var SortedBusynessOnLocation = new Dictionary<Location, double>(busynessOnLocation.OrderBy(pair => pair.Value));
            var SortedNumberOfReservationsOnLocation = new Dictionary<Location, double>(numberOfReservationsOnLocation.OrderBy(pair => pair.Value));
            var topFiveBusynessLocations = SortedBusynessOnLocation.Take(10).Select(pair => pair.Key).ToList();
            var topFiveReservationLocations = SortedNumberOfReservationsOnLocation.Take(10).Select(pair => pair.Key).ToList();
            var unpopularLocations = topFiveBusynessLocations.Intersect(topFiveReservationLocations).ToList();
            //var unpopularLocations = new ObservableCollection<Location>(intersectingLocations);
            return unpopularLocations;
        }

        private ObservableCollection<Accommodation> GetOwnersAccommodationsOnUnpopularLocations()
        {
            ObservableCollection<Accommodation> accommodations = new ObservableCollection<Accommodation>();
            foreach(Location location in UnpopularLocations)
            {
                foreach(Accommodation accommodation in accommodationService.GetOwnersAccommodationsOnLocation(location,Owner))
                {
                    accommodations.Add(accommodation);
                }
            }
            return accommodations;
        }


        public void AddAccommodationOnPopularLocationExecute()
        {
            AccommodationFormWithPopularLocation accommodationFormWithPopularLocation = new AccommodationFormWithPopularLocation(SelectedLocation,AccommodationsOfLogedInOwner,Owner,accommodationService);
            accommodationFormWithPopularLocation.Show();
        }


        public void DeleteAccommmodationExecute()
        {
            if (SelectedAccommodation != null)
            {
                SelectedAccommodation.IsClosed = true;
                accommodationService.Update(SelectedAccommodation);
                AccommodationsOfLogedInOwner.Remove(SelectedAccommodation);
                AccommodationsOnUnpopularLocations.Remove(SelectedAccommodation);
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("You have succesfully deleted accommodation on unpopular location!");
                }
                else
                {
                    MessageBox.Show("Uspesno ste botisali smestaj na nepopularnoj lokaciji!");
                }
            }
        }

    }
}
