using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class AccommodationAADetailsViewModel
    {

        public Accommodation SelectedAccommodation { get; set; }
        private ReservationService reservationService;
        private int ownerGuestId;

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int guestNo;
        public int days;

        public List<String> images { get; set; }

        public RelayCommand ReserveCommand { get; }
        public RelayCommand CancelCommand { get; }
        private ObservableCollection<AnywhereAnytimeReservation> suggestions;
        private AnywhereAnytimeReservation SelectedReservation;
        public AccommodationAADetailsViewModel(AnywhereAnytimeReservation SelectedReservation, ReservationService reservationService, int ownerGuestId, int guestNo, int resDays, ObservableCollection<AnywhereAnytimeReservation> suggestions)
        {

            this.SelectedAccommodation = SelectedReservation.Accommodation;
            this.Start = SelectedReservation.Start;
            this.End = SelectedReservation.End;
            this.reservationService = reservationService;
            this.ownerGuestId = ownerGuestId;
            this.guestNo = guestNo;
            this.days = resDays;
            this.suggestions = suggestions;
            this.SelectedReservation = SelectedReservation;
            //commands
            ReserveCommand = new RelayCommand(Reserve_Execute);
            CancelCommand = new RelayCommand(Cancel_Execute);
            
            images = new List<String>();
            images.AddRange(SelectedAccommodation.pictureURLs);
        }

        public void Reserve_Execute()
        {
            //need to reserve
            Reservation NewReservation = new Reservation(SelectedAccommodation.Id,Start,End,days,guestNo);

            NewReservation.Accommodation = SelectedAccommodation;
            NewReservation.OwnerGuestId = this.ownerGuestId;
            
            reservationService.Save(NewReservation);
            suggestions.Remove(SelectedReservation);
            MessageBox.Show("Reservation successfully made!");
            Cancel_Execute();
        }

        private void Cancel_Execute()
        {
            App.Current.Windows.OfType<AccommodationAADetails>().FirstOrDefault().Close();
        }
    }
}
