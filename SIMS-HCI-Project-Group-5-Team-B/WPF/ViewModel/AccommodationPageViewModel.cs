using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class AccommodationPageViewModel
    {
        private AccommodationService accommodationService;
        private LocationController locationController;
        private OwnerService ownerService;
        private YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService;
        public ObservableCollection<Accommodation> AccomodationsOfLogedInOwner { get; set; }
        public Owner LogedInOwner { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        private ReservationService reservationService;
        public RelayCommand CreateAccommodationCommand { get; }
        public RelayCommand GenerateReportCommand { get;  }
        public RelayCommand SuggestionsCommand { get; }
        public AccommodationPageViewModel(int ownerId, ReservationService reservationService)
        {
            locationController = new LocationController();
            ownerService = new OwnerService();
            yearlyAccommodationStatisticsService = new YearlyAccommodationStatisticsService();
            this.accommodationService = new AccommodationService(locationController, ownerService);
            AccomodationsOfLogedInOwner = new ObservableCollection<Accommodation>(accommodationService.GetAccommodationsOfLogedInOwner(ownerId));
            this.LogedInOwner = ownerService.getById(ownerId);
            this.reservationService = reservationService;
            CreateAccommodationCommand = new RelayCommand(CreateAccommodationExecute, Can_Execute);
            GenerateReportCommand = new RelayCommand(GenerateReportExecute, Can_Execute);
            SuggestionsCommand = new RelayCommand(SuggestionsExecute, Can_Execute);
        }

        private bool Can_Execute()
        {
            return true;
        }

        private void GenerateReportExecute()
        {
            ReportForm reportForm = new ReportForm(SelectedAccommodation, reservationService);
            reportForm.Show();
        }

        private void CreateAccommodationExecute()
        {
            AccommodationForm accommodationForm = new AccommodationForm(AccomodationsOfLogedInOwner, LogedInOwner);
            accommodationForm.Show();
        }

        private void SuggestionsExecute()
        {
            AccommodationLocationSuggestionsWindow accommodationLocationSuggestionsWindow = new AccommodationLocationSuggestionsWindow(accommodationService, yearlyAccommodationStatisticsService, LogedInOwner, AccomodationsOfLogedInOwner);
            accommodationLocationSuggestionsWindow.Show();
        }





    }
}
