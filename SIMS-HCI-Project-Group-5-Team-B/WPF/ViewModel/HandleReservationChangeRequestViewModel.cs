using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class HandleReservationChangeRequestViewModel : INotifyPropertyChanged
    {
        private ReservationChangeRequestService reservationChangeRequestService;
        private ReservationService reservationService;
        private UserController userController;
        private NotificationController notificationController;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ReservationChangeRequest SelectedReservationChangeRequest;
        public ObservableCollection<ReservationChangeRequest> OwnersPendingRequests { get; set; }


        public HandleReservationChangeRequestViewModel(ReservationChangeRequestService reservationChangeRequestService,ReservationService reservationService, Owner owner, ReservationChangeRequest SelectedReservationChangeRequest)
        {
            this.reservationChangeRequestService = reservationChangeRequestService;
            this.reservationService = reservationService;
            userController = new UserController();
            notificationController = new NotificationController();
            this.SelectedReservationChangeRequest = SelectedReservationChangeRequest;
            OwnersPendingRequests = new ObservableCollection<ReservationChangeRequest>(reservationChangeRequestService.GetOwnersPendingRequests(owner));
        }


        public void AcceptReservationChangeRequest()
        {
            if(SelectedReservationChangeRequest != null)
            {
                DateTime wantedStartDate = SelectedReservationChangeRequest.Start;
                DateTime wantedEndDate = SelectedReservationChangeRequest.End;
                SelectedReservationChangeRequest.Reservation.StartDate = wantedStartDate;
                SelectedReservationChangeRequest.Reservation.EndDate = wantedEndDate;
                SelectedReservationChangeRequest.RequestStatus = REQUESTSTATUS.Confirmed;
                reservationService.Update(SelectedReservationChangeRequest.Reservation);
                reservationChangeRequestService.Update(SelectedReservationChangeRequest);
                //sending notification
                notificationController.Send(CreateOwnerNotification("Accepted"));
                OwnersPendingRequests.Remove(SelectedReservationChangeRequest);
                MessageBox.Show("Reservation was succesfully changed");
            }
            else
            {
                MessageBox.Show("Request was not selected!");
            }
        }


        public void DeclineReservationChangeRequest(ReservationChangeRequest SelectedReservationChangeRequest)
        {
            if(SelectedReservationChangeRequest != null)
            {
                SelectedReservationChangeRequest.RequestStatus = REQUESTSTATUS.Denied;
                reservationChangeRequestService.Update(SelectedReservationChangeRequest);
                //send notification
                notificationController.Send(CreateOwnerNotification("Denied"));
                OwnersPendingRequests.Remove(SelectedReservationChangeRequest);
                MessageBox.Show("Request was succesfully denied!");
            }
            else
            {
                MessageBox.Show("Request was not selected!");
            }
        }

        private Notification CreateOwnerNotification(string ownerAction)
        {
            Notification notification = new Notification();
            notification.IsRead = false;
            notification.Message = GetNotificationMessage(ownerAction);
            notification.ReceiverId = userController.GetByUsername(SelectedReservationChangeRequest.Reservation.OwnerGuest.Username).Id;
            return notification;
        }

        private string GetNotificationMessage(string ownerAction)
        {
            StringBuilder sb = new StringBuilder($"{SelectedReservationChangeRequest.Reservation.Accommodation.Owner.Name} {SelectedReservationChangeRequest.Reservation.Accommodation.Owner.Surname} changed reservation request status to '{ownerAction}'. ");
            sb.Append($"Reservation: {SelectedReservationChangeRequest.Reservation.Accommodation.Name} From: {SelectedReservationChangeRequest.Reservation.StartDate} To: {SelectedReservationChangeRequest.Reservation.EndDate}");
            return sb.ToString();
        }

    }
}
