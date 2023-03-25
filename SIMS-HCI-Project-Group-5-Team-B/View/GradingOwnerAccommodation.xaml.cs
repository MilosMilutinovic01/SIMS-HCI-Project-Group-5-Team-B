﻿using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for GradingOwnerAccommodation.xaml
    /// </summary>
    public partial class GradingOwnerAccommodation : Window
    {
        //property for heading text
        public string Heading { get; set; }
        public Reservation SelectedReservation { get; set; }
        private ReservationView reservationView;
        public OwnerAccommodationGrade OwnerAccommodationGrade { get; set; }
        private OwnerAccommodationGradeController ownerAccommodationGradeController;
        private ReservationController reservationController;
        private ObservableCollection<ReservationView> ReservationViews;
        public GradingOwnerAccommodation(OwnerAccommodationGradeController ownerAccommodationGradeController, ReservationController reservationController, ReservationView reservationView)
        {
            InitializeComponent();
            this.DataContext = this;
            OwnerAccommodationGrade = new OwnerAccommodationGrade();
            this.reservationView = reservationView;
            this.SelectedReservation = reservationView.Reservation;
            this.ownerAccommodationGradeController = ownerAccommodationGradeController;
            this.reservationController = reservationController;
            Heading = string.Empty;
            FormHeading();
            
        }

        private void FormHeading()
        {
            Heading = SelectedReservation.Accommodation.Name + " Grading\n"
                      + "Owner: " + SelectedReservation.Accommodation.Owner.Name + " " + SelectedReservation.Accommodation.Owner.Surname + "";
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grade_Button_Click(object sender, RoutedEventArgs e)
        {
            if(OwnerAccommodationGrade.IsValid)
            {
                //setting parameter to true
                SelectedReservation.IsGradedByGuest = true;
                reservationController.Update(SelectedReservation);
                OwnerAccommodationGrade.ReservationId = SelectedReservation.Id;
                ownerAccommodationGradeController.Save(OwnerAccommodationGrade);
                reservationView.IsForGrading = false;
                MessageBox.Show("Grading was successful!");
                
                Close();
            }
            else
            {
                MessageBox.Show("Accommodation can't be created, because fileds are not valid");
            }
        }
        
    }
}
