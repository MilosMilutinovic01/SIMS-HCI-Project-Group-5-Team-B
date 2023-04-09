using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
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
        
        public event PropertyChangedEventHandler? PropertyChanged;

        public ReservationChangeRequestViewModel(Reservation selectedReservation, ReservationChangeRequestService reservationChangeRequestService)
        {
            this.reservationChangeRequestService = reservationChangeRequestService;
            NewReservationRequest = new ReservationChangeRequest(selectedReservation);
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
                
                reservationChangeRequestService.Save(NewReservationRequest);
                MessageBox.Show("Request sent!");
            }
            else
            {
                MessageBox.Show("Request can not be formed\nBecause data is not valid!");
            }
        }

    }
}
