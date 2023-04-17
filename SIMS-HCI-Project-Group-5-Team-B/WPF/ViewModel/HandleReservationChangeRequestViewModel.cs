using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class HandleReservationChangeRequestViewModel : INotifyPropertyChanged
    {
        private ReservationChangeRequestService reservationChangeRequestService;
        private ReservationService reservationService;
        public event PropertyChangedEventHandler? PropertyChanged;
        public ReservationChangeRequest SelectedReservationChangeRequest;
        public ObservableCollection<ReservationChangeRequest> OwnersPendingRequests { get; set; }


        public HandleReservationChangeRequestViewModel(ReservationChangeRequestService reservationChangeRequestService,ReservationService reservationService, Owner owner, ReservationChangeRequest SelectedReservationChangeRequest)
        {
            this.reservationChangeRequestService = reservationChangeRequestService;
            this.reservationService = reservationService;
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
                OwnersPendingRequests.Remove(SelectedReservationChangeRequest);
                MessageBox.Show("Request was succesfully denied!");
            }
            else
            {
                MessageBox.Show("Request was not selected!");
            }
        }

    }
}
