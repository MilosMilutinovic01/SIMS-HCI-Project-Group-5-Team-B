using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class OwnerGuestAccountViewModel
    {

        private SuperOwnerGuestTitleService superOwnerGuestTitleService;
        public OwnerGuest ActiveOwnerGuest { get; set; }
        private ReservationService reservationService;
        private ReservationChangeRequestService reservationChangeRequestService;
        private UserController userController;
        private NotificationController notificationController;
        public int LastYearReservations { get; set; }
        public int MyPoints { get; set; }
        public bool IsSuperOwnerGuest { get; set; }

        public ObservableCollection<SingleReservationViewModel> ReservationViews { get; set; }
        public SingleReservationViewModel SelectedReservationView { get; set; }
        public ObservableCollection<ReservationChangeRequest> PendingChangeRequests { get; set; }

        //commands
        public RelayCommand ModifyCommand { get; }
        public RelayCommand CancelReservationCommand { get; }
        public RelayCommand ReportCommand { get; }

        public OwnerGuestAccountViewModel(OwnerGuest ActiveOwnerGuest)
        {
            this.superOwnerGuestTitleService = new SuperOwnerGuestTitleService();
            this.ActiveOwnerGuest = ActiveOwnerGuest;
            this.reservationService = new ReservationService();
            this.reservationChangeRequestService = new ReservationChangeRequestService();
            notificationController = new NotificationController();
            userController = new UserController();
            //setting parameters
            LastYearReservations = reservationService.GetReservationsNumberInLastYear(ActiveOwnerGuest.Id);
            SetMyPoints(ActiveOwnerGuest.Id);

            ReservationViews = new ObservableCollection<SingleReservationViewModel>(GetReservationViews(ActiveOwnerGuest.Id));
            PendingChangeRequests = new ObservableCollection<ReservationChangeRequest>(reservationChangeRequestService.GetPendingRequests(ActiveOwnerGuest.Id));

            ModifyCommand = new RelayCommand(Modify_Execute, Modify_CanExecute);
            CancelReservationCommand = new RelayCommand(Cancel_Execute, Cancel_CanExecute);
            ReportCommand = new RelayCommand(OnReport);
        }

        private void SetMyPoints(int ownerGuestId)
        {
            MyPoints = 0;
            IsSuperOwnerGuest = false;
            SuperOwnerGuestTitle title = superOwnerGuestTitleService.GetActiveByOwnerGuestId(ownerGuestId);
            if (title != null)
            {
                MyPoints = title.AvailablePoints;
                IsSuperOwnerGuest = true;
            }
                
        }

        public List<SingleReservationViewModel> GetReservationViews(int ownerGuestId)
        {
            List<SingleReservationViewModel> reservationViews = new List<SingleReservationViewModel>();
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

                    reservationViews.Add(new SingleReservationViewModel(reservation, isForGrading, isModifiable, isCancelable));
                }


            }
            return reservationViews;
        }

        public bool Cancel_CanExecute()
        {
            //if (SelectedReservationView.IsCancelable)
            return true;
            //return false;
        }
        public bool Modify_CanExecute()
        {
            //if (SelectedReservationView.IsModifiable)
            return true;
            // return false;
        }

        public void Modify_Execute()
        {
            if (SelectedReservationView != null && SelectedReservationView.IsModifiable)
            {
                ReservationChangeRequestForm reservationChangeRequestForm = new ReservationChangeRequestForm(PendingChangeRequests, SelectedReservationView, reservationChangeRequestService, reservationService);
                reservationChangeRequestForm.Show();

            }

        }



        public void Cancel_Execute()
        {
            if (SelectedReservationView != null && SelectedReservationView.IsCancelable)
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

        public void OnReport()
        {
            ReportWindow window = new ReportWindow(ActiveOwnerGuest.Id);
            window.Show();
        }
    }
}
