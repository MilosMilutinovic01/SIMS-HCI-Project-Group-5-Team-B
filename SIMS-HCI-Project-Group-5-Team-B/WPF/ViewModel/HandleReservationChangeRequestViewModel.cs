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
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class HandleReservationChangeRequestViewModel : INotifyPropertyChanged
    {
        private ReservationChangeRequestService reservationChangeRequestService;
        private ReservationService reservationService;
        private UserController userController;
        private NotificationController notificationController;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ReservationChangeRequest SelectedReservationChangeRequest { get; set; }
        public ObservableCollection<ReservationChangeRequest> OwnersPendingRequests { get; set; }
        public Owner owner { get; set; }
        public RelayCommand AcceptRequestCommand { get; }
        public RelayCommand DeclineRequestCommand { get; }
        public RelayCommand AcceptingRequestCommand { get; }
        public RelayCommand DecliningRequestCommand { get; }
        public RelayCommand CloseCommand { get; }

        public HandleReservationChangeRequestViewModel(ReservationChangeRequestService reservationChangeRequestService,ReservationService reservationService, Owner owner/*,ReservationChangeRequest SelectedReservationChangeRequest*/)
        {
            this.reservationChangeRequestService = reservationChangeRequestService;
            this.reservationService = reservationService;
            userController = new UserController();
            notificationController = new NotificationController();
            this.owner = owner;
            //this.SelectedReservationChangeRequest = SelectedReservationChangeRequest;
            OwnersPendingRequests = new ObservableCollection<ReservationChangeRequest>(reservationChangeRequestService.GetOwnersPendingRequests(owner));
            AcceptRequestCommand = new RelayCommand(Accept_Execute, Accept_CanExecute);
            DeclineRequestCommand = new RelayCommand(Decline_Execute, Decline_CanExecute);
            AcceptingRequestCommand = new RelayCommand(AcceptingRequest_Execute, AcceptingRequest_CanExecute);
            DecliningRequestCommand = new RelayCommand(DecliningRequest_Execute, DecliningRequest_CanExecute);
            CloseCommand = new RelayCommand(Close, CanExecute);

        }

        public bool CanExecute()
        {
            return true;
        }

        public void Close()
        {
            App.Current.Windows[4].Close();
        }

        public bool AcceptingRequest_CanExecute()
        {

            return true;

        }

        public bool DecliningRequest_CanExecute()
        {

            return true;

        }


        public void AcceptingRequest_Execute()
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
                if(Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("Reservation was succesfully changed","Succesfull",MessageBoxButton.OK,MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Rezervacija je uspesno pomerena", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            else
            {
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("Reservation was not selected", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Close();
                }
                else
                {
                    MessageBox.Show("Rezervacija nije selektovana", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Close();
                }
            }
        }


        public void DecliningRequest_Execute()
        {
            if(SelectedReservationChangeRequest != null)
            {
                SelectedReservationChangeRequest.RequestStatus = REQUESTSTATUS.Denied;
                reservationChangeRequestService.Update(SelectedReservationChangeRequest);
                //send notification
                notificationController.Send(CreateOwnerNotification("Denied"));
                OwnersPendingRequests.Remove(SelectedReservationChangeRequest);
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("Request was succesfully denied!", "Succesfull", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Zahtev je uspesno odbijen", "Uspesno", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }

            }
            else
            {
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("Request was not selected", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Close();
                }
                else
                {
                    MessageBox.Show("Zahtev nije selektovan", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Close();
                }
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

        public bool Accept_CanExecute()
        {
           
            return true;
           
        }

        public bool Decline_CanExecute()
        {
            
            return true;
           
        }

        public void Accept_Execute()
        {
            if(SelectedReservationChangeRequest != null)
            {
                AcceptReservationChangeRequestWindow acceptReservationChangeRequestWindow = new AcceptReservationChangeRequestWindow(reservationChangeRequestService,reservationService,owner,SelectedReservationChangeRequest,OwnersPendingRequests);
                acceptReservationChangeRequestWindow.Show();
            }
        }

        public void Decline_Execute()
        {
            if(SelectedReservationChangeRequest != null)
            {
                DeclineReservationChangeRequestForm declineReservationChangeRequestForm = new DeclineReservationChangeRequestForm(reservationChangeRequestService, reservationService, owner, SelectedReservationChangeRequest,OwnersPendingRequests);
                declineReservationChangeRequestForm.Show();
            }
        }

    }
}
