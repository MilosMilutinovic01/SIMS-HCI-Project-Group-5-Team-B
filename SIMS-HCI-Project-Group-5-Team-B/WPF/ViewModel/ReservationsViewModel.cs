using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using System.Text;
using SIMS_HCI_Project_Group_5_Team_B.Controller;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ReservationsViewModel
    {
        private ReservationService reservationService;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        private OwnerService ownerService;
        private SuperOwnerService superOwnerService;
        private NotificationController notificationController;
        private UserController userController;
        private ReservationChangeRequestService reservationChangeRequestService;
        public ObservableCollection<ReservationGridView> ReservationViews { get; set; }
        public ReservationGridView SelectedReservationView { get; set; }

        public ObservableCollection<ReservationChangeRequest> ReservaitionChangeRequests { get; set; }
        private int ownerGuestId;

        public ReservationsViewModel(ReservationService reservationService, OwnerAccommodationGradeSevice ownerAccommodationGradeService, SuperOwnerService superOwnerService, OwnerService ownerService, int ownerGuestId, ReservationChangeRequestService reservationChangeRequestService)
        {
            this.reservationService = reservationService;
            this.ownerAccommodationGradeService = ownerAccommodationGradeService;
            this.superOwnerService = superOwnerService;
            this.ownerService = ownerService;
            notificationController = new NotificationController();
            userController = new UserController();
            this.reservationChangeRequestService = reservationChangeRequestService;
            this.ownerGuestId = ownerGuestId;

            ReservationViews = new ObservableCollection<ReservationGridView>(GetReservationViews(ownerGuestId));
            ReservaitionChangeRequests = new ObservableCollection<ReservationChangeRequest>(reservationChangeRequestService.GetOwnerGuestsReservationRequests(ownerGuestId));
        }

        public void Grade()
        {
            if (SelectedReservationView != null)
            {
                GradingOwnerAccommodation gradingOwnerAccommodatoinWindow = new GradingOwnerAccommodation(ownerAccommodationGradeService, reservationService, SelectedReservationView, superOwnerService, ownerService);
                gradingOwnerAccommodatoinWindow.Show();


            }
        }

        public void Modify()
        {
            if (SelectedReservationView != null)
            {
                ReservationChangeRequestForm reservationChangeRequestForm = new ReservationChangeRequestForm(ReservaitionChangeRequests, SelectedReservationView, reservationChangeRequestService, reservationService);
                reservationChangeRequestForm.Show();

            }

        }



        public void Cancel()
        {
            if (SelectedReservationView != null)
            {
                if (ConfirmReservationDeletion() == MessageBoxResult.Yes)
                {
                    //send notification
                    notificationController.Send(CreateOwnerNotification());

                    SelectedReservationView.Reservation.IsDeleted = true;
                    reservationService.Update(SelectedReservationView.Reservation);
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

        public List<ReservationGridView> GetReservationViews(int ownerGuestId)
        {
            List<ReservationGridView> reservationViews = new List<ReservationGridView>();
            foreach (Reservation reservation in reservationService.GetUndeleted())
            {
                if (reservation.OwnerGuestId == ownerGuestId)
                {
                    bool isForGrading = true;
                    if (!reservationService.IsReservationGradable(reservation))
                    {
                        isForGrading = false;
                    }

                    bool isModifiable = true;
                    if (!reservationService.IsReservationModifiable(reservation))
                    {
                        isModifiable = false;
                    }

                    bool isCancelable = true;
                    if (!reservationService.IsReservationDeletable(reservation))
                    {
                        isCancelable = false;
                    }

                    reservationViews.Add(new ReservationGridView(reservation, isForGrading, isModifiable, isCancelable));
                }


            }
            return reservationViews;
        }

        private Notification CreateOwnerNotification()
        {
            Notification notification = new Notification();
            notification.IsRead = false;
            notification.Message = GetNotificationMessage();
            notification.ReceiverId = userController.GetByUsername(SelectedReservationView.Reservation.Accommodation.Owner.Username).Id;
            return notification;
        }

        private string GetNotificationMessage()
        {
            StringBuilder sb = new StringBuilder($"{SelectedReservationView.Reservation.OwnerGuest.Name} {SelectedReservationView.Reservation.OwnerGuest.Surname} cancelled reservation in ");
            sb.Append($"{SelectedReservationView.Reservation.Accommodation.Name} From: {SelectedReservationView.Reservation.StartDate} To: {SelectedReservationView.Reservation.EndDate}");
            return sb.ToString();
        }
    }
}
