using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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
        public Reservation NewReservation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Accommodation SelectedAccomodation { get; set; }
        public ObservableCollection<ReservationRecommendation> ReservationRecommendations { get; set; }
        public ReservationRecommendation SelectedDate { get; set; }
        private int ownerGuestId;
        public ReservationForm(ReservationService reservationService, Accommodation SelectedAccomodation,int ownerGuestId)
        {
            InitializeComponent();
            this.DataContext = this;
            this.reservationService = reservationService;
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
            SetGuestNumberParameters();

        }

        private void ReservationDaysIncrease_Button_Click(object sender, RoutedEventArgs e)
        {
            int currentValue = Int32.Parse(reservationDaysTextBox.Text);

            currentValue++;
            reservationDaysTextBox.Text = currentValue.ToString();


        }

        private void ReservationDaysDecrease_Button_Click(object sender, RoutedEventArgs e)
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

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Search_Button_CLick(object sender, RoutedEventArgs e)
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

        private void Reserve_Button_Click(object sender, RoutedEventArgs e)
        {
            NewReservation.StartDate = SelectedDate.Start;
            NewReservation.EndDate = SelectedDate.End;
            if(NewReservation.IsValid) 
            {
                reservationService.Save(NewReservation);
                Close();
            }
            else
            {
                MessageBox.Show("Reservation can not be made because the data is not valid!");
            }

        }

        private void GuestNumberDecrease_Button_Click(object sender, RoutedEventArgs e)
        {
            
            int currentValue = Int32.Parse(guestNumberTextBox.Text);
            if (currentValue >1)
            {
                currentValue--;
                guestNumberTextBox.Text = currentValue.ToString();
            }

        }

        private void GuestNumberIncrease_Button_Click(object sender, RoutedEventArgs e)
        {
            int currentValue = Int32.Parse(guestNumberTextBox.Text);
            if(currentValue < SelectedAccomodation.MaxGuests)
            {
                currentValue++;
                guestNumberTextBox.Text = currentValue.ToString();
            }
        }

        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SetGuestNumberParameters();

        }

        private void SetGuestNumberParameters()
        {
            if (SelectedDate.Start == DateTime.MinValue && SelectedDate.End == DateTime.MinValue)
            {
                //Date has not been selected
                reservationButton.IsEnabled = false;
                guestNumberDecreaseButton.IsEnabled = false;
                guestNumberIncreaseButton.IsEnabled = false;
            }
            else
            {
                guestNumberDecreaseButton.IsEnabled = true;
                guestNumberIncreaseButton.IsEnabled = true;
                reservationButton.IsEnabled = true;
            }
        }
    }
}
