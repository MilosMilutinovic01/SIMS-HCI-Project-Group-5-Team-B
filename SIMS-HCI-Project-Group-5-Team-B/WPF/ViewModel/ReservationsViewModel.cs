using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ReservationsViewModel
    {
        private ReservationService reservationController;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeController;
        private OwnerService ownerController;
        private SuperOwnerService superOwnerController;
        private ReservationChangeRequestService reservationChangeRequestService;
        public ObservableCollection<ReservationGridView> ReservationViews { get; set; }
        public ReservationGridView SelectedReservationView { get; set; }

        public ObservableCollection<ReservationChangeRequest> ReservaitionChangeRequests { get; set; }
        private int ownerGuestId;

        public ReservationsViewModel(ReservationService reservationController, OwnerAccommodationGradeSevice ownerAccommodationGradeController, SuperOwnerService superOwnerController, OwnerService ownerController, int ownerGuestId, ReservationChangeRequestService reservationChangeRequestService) 
        {
            this.reservationController = reservationController;
            this.ownerAccommodationGradeController = ownerAccommodationGradeController;
            this.superOwnerController = superOwnerController;
            this.ownerController = ownerController;
            this.reservationChangeRequestService = reservationChangeRequestService;
            this.ownerGuestId = ownerGuestId;

            ReservationViews = new ObservableCollection<ReservationGridView>(reservationController.GetReservationsForGuestGrading(ownerGuestId));
            ReservaitionChangeRequests = new ObservableCollection<ReservationChangeRequest>(reservationChangeRequestService.GetOwnerGuestsReservationRequests(ownerGuestId));
        }

        public void Grade()
        {
            if (SelectedReservationView != null)
            {
                GradingOwnerAccommodation gradingOwnerAccommodatoinWindow = new GradingOwnerAccommodation(ownerAccommodationGradeController, reservationController, SelectedReservationView, superOwnerController, ownerController);
                gradingOwnerAccommodatoinWindow.Show();

            }
        }

        public void Modify()
        {
            if(SelectedReservationView != null)
            {
                ReservationChangeRequestForm reservationChangeRequestForm = new ReservationChangeRequestForm(SelectedReservationView.Reservation, reservationChangeRequestService, reservationController);
                reservationChangeRequestForm.Show();
            }
            
        }

        public void Cancel()
        {
            if (SelectedReservationView != null)
            {
                if(ConfirmReservationDeletion() == MessageBoxResult.Yes)
                {
                    SelectedReservationView.Reservation.IsDeleted = true;
                    reservationController.Update(SelectedReservationView.Reservation);
                    ReservationViews.Remove(SelectedReservationView);
                }
            }
        }

        private MessageBoxResult ConfirmReservationDeletion()
        {
            string sMessageBoxText = $"Are you sure you want to cancel this reservation?";
            string sCaption = "Confirm";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult result = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);
            return result;
        }
    }
}
