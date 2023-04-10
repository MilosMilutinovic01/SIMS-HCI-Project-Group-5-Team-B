using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System.Collections.ObjectModel;

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

        public ReservationsViewModel(ReservationService reservationController, OwnerAccommodationGradeSevice ownerAccommodationGradeController, SuperOwnerService superOwnerController, OwnerService ownerController, int ownerGuestId, ReservationChangeRequestService reservationChangeRequestService) 
        {
            this.reservationController = reservationController;
            this.ownerAccommodationGradeController = ownerAccommodationGradeController;
            this.superOwnerController = superOwnerController;
            this.ownerController = ownerController;
            this.reservationChangeRequestService = reservationChangeRequestService;

            //add method for checking the userId when showing reservations
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
    }
}
