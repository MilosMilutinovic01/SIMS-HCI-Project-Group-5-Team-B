using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ReservationChangeRequestViewModel : INotifyPropertyChanged
    {
        public ReservationChangeRequest NewReservationRequest { get; set; }
        public string Header { get; set; } = "";
        public string LocationHeader { get; set; } = "";
        private ReservationChangeRequestService reservationChangeRequestService;
        private ReservationService reservationService;
        private SingleReservationViewModel selectedReservationView;
        private ObservableCollection<ReservationChangeRequest> ReservaitionChangeRequests;
        private ReservationChangeRequestForm window;
        public RelayCommand SendCommand { get;}   
        public RelayCommand CloseCommand { get;}
        public event PropertyChangedEventHandler? PropertyChanged;
        private IRenovationService renovationService;

        public ReservationChangeRequestViewModel(ObservableCollection<ReservationChangeRequest> ReservaitionChangeRequests,SingleReservationViewModel selectedReservationView, ReservationChangeRequestService reservationChangeRequestService, ReservationService reservationService, ReservationChangeRequestForm window)
        {
            this.reservationChangeRequestService = reservationChangeRequestService;
            this.reservationService = reservationService;
            this.selectedReservationView = selectedReservationView;
            NewReservationRequest = new ReservationChangeRequest(selectedReservationView.Reservation);
            this.ReservaitionChangeRequests = ReservaitionChangeRequests;
            this.renovationService = Injector.CreateInstance<IRenovationService>();

            SetHeader();
            this.window = window;

            //commands
            SendCommand = new RelayCommand(CreateReservationChangeRequest, CanExecute);
            CloseCommand = new RelayCommand(Close, CanExecute);

        }

        private void SetHeader()
        {
            Header = "   " + NewReservationRequest.Reservation.Accommodation.Name + "`s Change Request\n";
            LocationHeader = "       " + NewReservationRequest.Reservation.Accommodation.Location.ToString();
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Close()
        {
            window.Close();
        }
        public void CreateReservationChangeRequest()
        {
            if(NewReservationRequest.IsValid)
            {
                if(reservationService.IsAccomodationAvailableForChangingReservationDates(NewReservationRequest.Reservation, NewReservationRequest.Start, NewReservationRequest.End)
                    && renovationService.IsAccomodationNotInRenovation(NewReservationRequest.Reservation.Accommodation, NewReservationRequest.Start, NewReservationRequest.End))
                {
                    NewReservationRequest.IsAvailable = "Yes";
                }
                else
                {
                    NewReservationRequest.IsAvailable = "No";
                }
                
                reservationChangeRequestService.Save(NewReservationRequest);
                //if we send change request we can not cancel or modify reservation anymore
                selectedReservationView.IsCancelable = false;
                selectedReservationView.IsModifiable = false;
                ReservaitionChangeRequests.Add(NewReservationRequest);
                MessageBox.Show("Request sent!");
                Close();
            }
            else
            {
                MessageBox.Show("Request can not be formed\nBecause data is not valid!");
            }
        }

        

    }
}
