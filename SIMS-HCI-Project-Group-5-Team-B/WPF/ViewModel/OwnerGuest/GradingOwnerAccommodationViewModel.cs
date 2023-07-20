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
        private RenovationRequestService renovationRequestService;
        //private ObservableCollection<ReservationGridView> ReservationViews;


        private SuperOwnerService superOwnerService;
        private OwnerService ownerService;
        

        public RelayCommand CloseCommand { get; }
        public RelayCommand SendRequestCommand { get; }
        public RelayCommand GradeCommand { get; }

        public GradingOwnerAccommodationViewModel(OwnerAccommodationGradeSevice ownerAccommodationGradeService, ReservationService reservationService, SingleReservationViewModel reservationView, SuperOwnerService superOwnerService, OwnerService ownerService)
        {
            
            OwnerAccommodationGrade = new OwnerAccommodationGrade(1, 1, 1, 1, 1);
            this.reservationView = reservationView;
            this.SelectedReservation = reservationView.Reservation;
            this.ownerAccommodationGradeService = ownerAccommodationGradeService;
            this.reservationService = reservationService;
            this.superOwnerService = superOwnerService;
            this.ownerService = ownerService;
            renovationRequestService = new RenovationRequestService();
            Heading = string.Empty;
            FormHeading();

            //commands
            CloseCommand = new RelayCommand(Cancel_Exexute, CanExecute);
            SendRequestCommand = new RelayCommand(RenovationRequest_Exexute, SendRequestCanExecute);
            GradeCommand = new RelayCommand(Grade_Execute, CanExecute);

        }

        private void FormHeading()
        {
            Heading = SelectedReservation.Accommodation.Name + " Grading\n"
                      + "Owner: " + SelectedReservation.Accommodation.Owner.Name + " " + SelectedReservation.Accommodation.Owner.Surname + "";
        }

        public void Cancel_Exexute()
        {
            App.Current.Windows.OfType<GradingOwnerAccommodation>().FirstOrDefault().Close();
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
                MessageBox.Show("Grading was successful!", "Grading", MessageBoxButton.OK, MessageBoxImage.Information);

                App.Current.Windows.OfType<GradingOwnerAccommodation>().FirstOrDefault().Close();
            }
            else
            {
                MessageBox.Show("Data is not valid!", "Grading", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public bool SendRequestCanExecute()
        {
            return ! renovationRequestService.Exists(SelectedReservation.Id);
        }
    }
}
