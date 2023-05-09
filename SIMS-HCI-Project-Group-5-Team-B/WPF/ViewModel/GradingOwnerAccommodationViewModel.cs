using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class GradingOwnerAccommodationViewModel
    {
        //property for heading text
        public string Heading { get; set; }
        public Reservation SelectedReservation { get; set; }
        private SingleReservationViewModel reservationView;
        public OwnerAccommodationGrade OwnerAccommodationGrade { get; set; }
        private OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        private ReservationService reservationService;
        //private ObservableCollection<ReservationGridView> ReservationViews;


        private SuperOwnerService superOwnerService;
        private OwnerService ownerService;
        protected GradingOwnerAccommodation window;

        public RelayCommand CloseCommand { get; }
        public RelayCommand SendRequestCommand { get; }
        public RelayCommand GradeCommand { get; }

        public GradingOwnerAccommodationViewModel(OwnerAccommodationGradeSevice ownerAccommodationGradeService, ReservationService reservationService, SingleReservationViewModel reservationView, SuperOwnerService superOwnerService, OwnerService ownerService, GradingOwnerAccommodation window)
        {
            
            OwnerAccommodationGrade = new OwnerAccommodationGrade(1, 1, 1, 1, 1);
            this.reservationView = reservationView;
            this.SelectedReservation = reservationView.Reservation;
            this.ownerAccommodationGradeService = ownerAccommodationGradeService;
            this.reservationService = reservationService;
            this.superOwnerService = superOwnerService;
            this.ownerService = ownerService;
            Heading = string.Empty;
            FormHeading();
            this.window = window;

            //commands
            CloseCommand = new RelayCommand(Cancel_Exexute, CanExecute);
            SendRequestCommand = new RelayCommand(RenovationRequest_Exexute, CanExecute);
            GradeCommand = new RelayCommand(Grade_Execute, CanExecute);

        }

        private void FormHeading()
        {
            Heading = SelectedReservation.Accommodation.Name + " Grading\n"
                      + "Owner: " + SelectedReservation.Accommodation.Owner.Name + " " + SelectedReservation.Accommodation.Owner.Surname + "";
        }

        public void Cancel_Exexute()
        {
            window.Close();
        }

        public void Grade_Execute()
        {
            if (OwnerAccommodationGrade.IsValid)
            {
                //setting parameter to true
                SelectedReservation.IsGradedByGuest = true;
                reservationService.Update(SelectedReservation);
                OwnerAccommodationGrade.ReservationId = SelectedReservation.Id;
                OwnerAccommodationGrade.GradeAverage = ownerAccommodationGradeService.GetAverageGrade(OwnerAccommodationGrade);
                ownerAccommodationGradeService.Save(OwnerAccommodationGrade);
                reservationView.IsForGrading = false;
                OwnerAccommodationGrade.Reservation.Accommodation.Owner.GradeAverage = superOwnerService.CalculateGradeAverage(OwnerAccommodationGrade.Reservation.Accommodation.Owner);
                if (OwnerAccommodationGrade.Reservation.Accommodation.Owner.GradeAverage > 4.5 && superOwnerService.GetNumberOfGrades(OwnerAccommodationGrade.Reservation.Accommodation.Owner) >= 50)
                {
                    OwnerAccommodationGrade.Reservation.Accommodation.Owner.IsSuperOwner = true;
                }
                else
                {
                    OwnerAccommodationGrade.Reservation.Accommodation.Owner.IsSuperOwner = false;
                }
                ownerService.Update(OwnerAccommodationGrade.Reservation.Accommodation.Owner);
                MessageBox.Show("Grading was successful!");

                window.Close();
            }
            else
            {
                MessageBox.Show("Data is not valid!");
            }
        }

        public void RenovationRequest_Exexute()
        {
            RenovationRequestForm renovationRequestForm = new RenovationRequestForm(SelectedReservation.Id);
            renovationRequestForm.Show();
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}
