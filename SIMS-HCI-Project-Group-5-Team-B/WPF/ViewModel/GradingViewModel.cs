using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class GradingViewModel
    {
        public ObservableCollection<Reservation> ReservationsForGrading { get; set; }
        public ObservableCollection<OwnerAccommodationGrade> OwnerAccommodationGradesForShowing { get; set; }
        public OwnerAccommodationGrade SelectedOwnerAccommodationGrade { get; set; }
        public Reservation SelectedReservation { get; set; }

        public RelayCommand GuestGradingWindowCommand { get; }
        public RelayCommand OwnerAccommodationGradeDetailsWindowCommand { get; }

        public ReservationService reservationService;
        public OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        public OwnerGuestGradeService ownerGuestGradeService;
        public Owner owner;
        public GradingViewModel(ReservationService reservationService, OwnerAccommodationGradeSevice ownerAccommodationGradeService, OwnerGuestGradeService ownerGuestGradeService, Owner owner)
        {
            this.reservationService = reservationService;
            this.ownerAccommodationGradeService = ownerAccommodationGradeService;
            this.ownerGuestGradeService = ownerGuestGradeService;
            this.owner = owner;
            ReservationsForGrading = new ObservableCollection<Reservation>(reservationService.GetReservationsForGrading(owner));
            OwnerAccommodationGradesForShowing = new ObservableCollection<OwnerAccommodationGrade>(ownerAccommodationGradeService.GetOwnerAccommodationGradesForShowing(owner));
            OwnerAccommodationGradeDetailsWindowCommand = new RelayCommand(OwnerAccommodationGradeDetailsWindow_Execute, CanExecute);
            GuestGradingWindowCommand = new RelayCommand(GuestGradingWindow_Execute, CanExecute);

        }

        public bool CanExecute()
        {
            return true;
        }

        public void OwnerAccommodationGradeDetailsWindow_Execute()
        {
            if (SelectedOwnerAccommodationGrade != null)
            {
                OwnerAccommodationGradeDetailsWindow ownerAccommodationGradeDetailsWindow = new OwnerAccommodationGradeDetailsWindow(SelectedOwnerAccommodationGrade);
                ownerAccommodationGradeDetailsWindow.Show();
            }
        }

        public void GuestGradingWindow_Execute()
        {
            if (SelectedReservation != null)
            {
                GradingGuestWindow gradingGuestWindow = new GradingGuestWindow(ownerGuestGradeService, ownerAccommodationGradeService, reservationService, SelectedReservation, ReservationsForGrading, OwnerAccommodationGradesForShowing);
                gradingGuestWindow.Show();
            }
        }

    }
}
