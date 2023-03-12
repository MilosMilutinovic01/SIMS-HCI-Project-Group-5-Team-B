using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for ReservationFormWindow.xaml
    /// </summary>
    public partial class ReservationFormWindow : Window
    {
        private ReservationController reservationController;
        public Reservation NewReservation { get; set; }
        public Accommodation SelectedAccomodation { get; set; } 
        public ReservationFormWindow(ReservationController reservationController, Accommodation SelectedAccomodation)
        {
            InitializeComponent();
            this.DataContext = this;
            this.reservationController = reservationController;
            this.SelectedAccomodation = SelectedAccomodation;
            SetReservationParameters();
           
            guestsNumberTextBox.Text = "1";
        }

        private void Increase_Click(object sender, RoutedEventArgs e)
        {
            int currentValue = Int32.Parse(guestsNumberTextBox.Text);
            if (currentValue < NewReservation.Accommodation.MaxGuests)
            {
                currentValue++;
                guestsNumberTextBox.Text = currentValue.ToString();
            }

        }

        private void Decrease_Click(object sender, RoutedEventArgs e)
        {
            int currentValue = Int32.Parse(guestsNumberTextBox.Text);
            if (currentValue > 1)
            {
                currentValue--;
                guestsNumberTextBox.Text = currentValue.ToString();
            }
        }

        private void SetReservationParameters()
        {
            NewReservation = new Reservation();
            NewReservation.AccommodationId = SelectedAccomodation.Id;
            NewReservation.Accommodation = SelectedAccomodation;
            NewReservation.GuestsNumber = 1; //default value
        }

        private bool IsAccomodationAvailable()
        {
            List<Reservation> accomodationReservations = GetAccomodationReservations();

            foreach(Reservation reservation in accomodationReservations)
            {
                if(NewReservation.StartDate >= reservation.StartDate && NewReservation.StartDate <= reservation.EndDate)
                {
                    return false;
                }
                else if(NewReservation.EndDate >= reservation.StartDate && NewReservation.EndDate <= reservation.EndDate)
                {
                    return false;
                }
                else if(NewReservation.StartDate <= reservation.StartDate && reservation.EndDate >= reservation.EndDate)
                {
                    return false;
                }
            }
            return true;
        }

        private List<Reservation> GetAccomodationReservations()
        {
            List<Reservation> accomodationReservations = new List<Reservation>();
            foreach (Reservation reservation in reservationController.GetAll())
            {
                if (reservation.AccommodationId == SelectedAccomodation.Id)
                {
                    accomodationReservations.Add(reservation);
                }
            }
            return accomodationReservations;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Create_CLick(object sender, RoutedEventArgs e)
        {
            TimeSpan reservationDays = NewReservation.EndDate - NewReservation.StartDate;
            NewReservation.ReservationDays = Convert.ToInt32(reservationDays.Days);
            if (NewReservation.IsValid)
            {
                if(IsAccomodationAvailable())
                {
                    reservationController.Save(NewReservation);
                    Close();
                }
                else
                {
                    MessageBox.Show("Accomodation isnt available in this span.");
                }

            }
            else
            {
                MessageBox.Show("Reservation can not be formed because data is not valid!");
            }
            
        }
    }
}
