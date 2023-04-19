using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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
        private ReservationGridView selectedReservationView;
        private ObservableCollection<ReservationChangeRequest> ReservaitionChangeRequests;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ReservationChangeRequestViewModel(ObservableCollection<ReservationChangeRequest> ReservaitionChangeRequests,ReservationGridView selectedReservationView, ReservationChangeRequestService reservationChangeRequestService, ReservationService reservationService)
        {
            this.reservationChangeRequestService = reservationChangeRequestService;
            this.reservationService = reservationService;
            this.selectedReservationView = selectedReservationView;
            NewReservationRequest = new ReservationChangeRequest(selectedReservationView.Reservation);
            this.ReservaitionChangeRequests = ReservaitionChangeRequests;
            SetHeader();

        }

        private void SetHeader()
        {
            Header = NewReservationRequest.Reservation.Accommodation.Name + "`s Change Request\n";
            LocationHeader = NewReservationRequest.Reservation.Accommodation.Location.ToString();
        }

        public void CreateReservationChangeRequest()
        {
            if(NewReservationRequest.IsValid)
            {
                if(reservationService.IsAccomodationAvailableForChangingReservationDates(NewReservationRequest.Reservation, NewReservationRequest.Start, NewReservationRequest.End))
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
            }
            else
            {
                MessageBox.Show("Request can not be formed\nBecause data is not valid!");
            }
        }

    }
}
