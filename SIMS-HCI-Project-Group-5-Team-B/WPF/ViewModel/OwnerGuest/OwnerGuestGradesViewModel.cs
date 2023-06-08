using Microsoft.VisualBasic;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class OwnerGuestGradesViewModel
    {
        private OwnerGuestGradeService ownerGuestGradeService;
        private AccommodationService accommodationService;
        private LocationController locationController;
        private ReservationService reservationService;
        private OwnerService ownerService;
        private SuperOwnerGuestTitleService superOwnerGuestTitleService;

        public ObservableCollection<OwnerGuestGradesDTO> GradeDTOs { get; set; }
        public double AverageGrade { get; set; }
        public int GradeCount { get; set; } = 0;
        public OwnerGuestGradesDTO SelectedGrade { get; set; }
        public int MyPoints { get; private set; }

        public RelayCommand DetailsCommand { get;}
        public OwnerGuestGradesViewModel(int ownerGuestId)
        {
            locationController = new LocationController();
            ownerService = new OwnerService();
            accommodationService = new AccommodationService(locationController, ownerService);
            reservationService = new ReservationService();
            ownerGuestGradeService = new OwnerGuestGradeService(reservationService);
            superOwnerGuestTitleService = new SuperOwnerGuestTitleService();

            GradeDTOs = new ObservableCollection<OwnerGuestGradesDTO>(ownerGuestGradeService.GetOwnerGuestsGradesDTO(ownerGuestId));
            AverageGrade = Math.Round(ownerGuestGradeService.GetAverageGrade(ownerGuestId),2);
            SetMyPoints(ownerGuestId);
            GradeCount = ownerGuestGradeService.GetGradesCount(ownerGuestId);

            //commands
            DetailsCommand = new RelayCommand(Details_Execute, CanExecute);
        }

        private void SetMyPoints(int ownerGuestId)
        {
            MyPoints = 0;

            SuperOwnerGuestTitle title = superOwnerGuestTitleService.GetActiveByOwnerGuestId(ownerGuestId);
            if (title != null)
                MyPoints = title.AvailablePoints;
        }

        public void Details_Execute()
        {
            GradeDetailsWindow gradeDetailsWindow = new GradeDetailsWindow(SelectedGrade);
            gradeDetailsWindow.Show();
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}
