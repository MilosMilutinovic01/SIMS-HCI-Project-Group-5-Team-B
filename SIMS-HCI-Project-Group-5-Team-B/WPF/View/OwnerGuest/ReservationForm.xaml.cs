using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for ReservationFormWindow.xaml
    /// </summary>
    public partial class ReservationForm : Window
    {
        private ReservationService reservationService;
        private SuperOwnerGuestTitleService superOwnerGuestTitleService;
        public Reservation NewReservation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Accommodation SelectedAccomodation { get; set; }
        public ObservableCollection<ReservationRecommendation> ReservationRecommendations { get; set; }
        public ReservationRecommendation SelectedDate { get; set; }
        private int ownerGuestId;
        public string Header { get; private set; } 
        public string Location { get; private set; }

        public RelayCommand ReserveCommand { get;}
        public RelayCommand CloseCommand { get;}
        public RelayCommand GuestIncreaseCommand { get;}
        public RelayCommand GuestDecreaseCommand { get;}
        public RelayCommand DaysIncreaseCommand { get;}
        public RelayCommand DaysDecreaseCommand { get;}
        public RelayCommand SearchCommand { get;}
        public ReservationForm(ReservationService reservationService, Accommodation SelectedAccomodation,int ownerGuestId)
        {
            InitializeComponent();
            this.DataContext = this;
            this.reservationService = reservationService;
            superOwnerGuestTitleService = new SuperOwnerGuestTitleService();
            this.SelectedAccomodation = SelectedAccomodation;
            NewReservation = new Reservation();
            this.ownerGuestId = ownerGuestId;
            SetReservationParameters();
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
            reservationDaysTextBox.Text = SelectedAccomodation.MinReservationDays.ToString();
            guestNumberTextBox.Text = "1";
            ReservationRecommendations = new ObservableCollection<ReservationRecommendation>();
            SelectedDate = new ReservationRecommendation(DateTime.MinValue,DateTime.MinValue);
            SetHeaders();

            CloseCommand = new RelayCommand(Cancel_Executed,CanExecute);
            ReserveCommand = new RelayCommand(Reserve_Executed,CanExecute);
            GuestIncreaseCommand = new RelayCommand(GuestNumberIncrease_Executed,CanExecute);
            GuestDecreaseCommand = new RelayCommand(GuestNumberDecrease_Executed,CanExecute);
            DaysIncreaseCommand = new RelayCommand(ReservationDaysIncrease_Execute,CanExecute);
            DaysDecreaseCommand = new RelayCommand(ReservationDaysDecrease_Execute,CanExecute);
            SearchCommand = new RelayCommand(Search_Executed, CanExecute);

        }

        public bool CanExecute()
        {
            return true;
        }

        public void ReservationDaysIncrease_Execute()
        {
            int currentValue = Int32.Parse(reservationDaysTextBox.Text);

            currentValue++;
            reservationDaysTextBox.Text = currentValue.ToString();


        }

        public void ReservationDaysDecrease_Execute()
        {
            int currentValue = Int32.Parse(reservationDaysTextBox.Text);
            if (currentValue > SelectedAccomodation.MinReservationDays)
            {
                currentValue--;
                reservationDaysTextBox.Text = currentValue.ToString();
            }
        }

        private void SetReservationParameters()
        {
            
            NewReservation.AccommodationId = SelectedAccomodation.Id;
            NewReservation.Accommodation = SelectedAccomodation;
            NewReservation.GuestsNumber = 1; // default value
            NewReservation.OwnerGuestId = this.ownerGuestId;
            StartDate = NewReservation.StartDate; //ehis could change but it can not go before today
            EndDate = NewReservation.EndDate;
        }

        public void Cancel_Executed()
        {
            Close();
        }

        public void Search_Executed()
        {
            SetReservationParameters(); 
            if (NewReservation.IsValid)
            {
                ReservationRecommendations.Clear();
                List<ReservationRecommendation> list = reservationService.GetReservationRecommendations(SelectedAccomodation, StartDate, EndDate, NewReservation.ReservationDays);
                if (list != null)
                {
                    foreach (ReservationRecommendation item in list)
                    {
                        ReservationRecommendations.Add(item);
                    }
                }


            }
            else
            {
                MessageBox.Show("Search can not be preformed because data is not valid!");
            }

        }

        public void Reserve_Executed()
        {
            if(SelectedDate == null)
            {
                return;
            }

            NewReservation.StartDate = SelectedDate.Start;
            NewReservation.EndDate = SelectedDate.End;
            if(NewReservation.IsValid) 
            {
                reservationService.Save(NewReservation);
                //check for superOwner and update points
                //if guest becomes with this reservation superGuest, discount can be applied only after this reservation
                superOwnerGuestTitleService.UpdatePoints(ownerGuestId);
                superOwnerGuestTitleService.BecomeSuperOwnerGuest();
                MessageBox.Show("Reservation was successful");
                Close();
            }
            else
            {
                MessageBox.Show("Reservation can not be made because the data is not valid!");
            }

        }

        public void GuestNumberDecrease_Executed()
        {
            
            int currentValue = Int32.Parse(guestNumberTextBox.Text);
            if (currentValue >1)
            {
                currentValue--;
                guestNumberTextBox.Text = currentValue.ToString();
            }

        }

        public void GuestNumberIncrease_Executed()
        {
            int currentValue = Int32.Parse(guestNumberTextBox.Text);
            if(currentValue < SelectedAccomodation.MaxGuests)
            {
                currentValue++;
                guestNumberTextBox.Text = currentValue.ToString();
            }
        }

        private void SetHeaders()
        {
            Header = SelectedAccomodation.Name + " Reservation";
            Location = SelectedAccomodation.Location.ToString() ;
        }
    }
}
