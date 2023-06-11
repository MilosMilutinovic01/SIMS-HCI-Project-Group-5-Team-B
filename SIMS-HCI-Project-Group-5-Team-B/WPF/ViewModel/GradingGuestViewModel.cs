using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using System.Windows;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System.ComponentModel;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class GradingGuestViewModel : INotifyPropertyChanged
    {
        public ReservationService reservationService;
        public OwnerGuestGradeService ownerGuestGradeService;
        public OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        public OwnerGuestGrade NewOwnerGuestGrade { get; set; }
        public Reservation SelectedReservation { get; set; }
        public ObservableCollection<Reservation> ReservationsForGrading { get; set; }
        public ObservableCollection<OwnerAccommodationGrade> OwnerAccommodationGradesForShowing { get; set; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand GradeCommand { get; }
        public string Heading { get; set; }

        private string selectedIsPaymentCompletedOnTime;
        private string selectedComplatintsFromGuests;
        public string SelectedIsPaymentCompletedOnTime {
            get { return selectedIsPaymentCompletedOnTime; }
            set
            {
                if (selectedIsPaymentCompletedOnTime != value)
                {
                    selectedIsPaymentCompletedOnTime = value;
                    NotifyPropertyChanged(nameof(SelectedIsPaymentCompletedOnTime));
                    //NewOwnerGuestGrade.IsPaymentCompletedOnTime = SetOwnerGuestGradeCompletedPayment(selectedIsPaymentCompletedOnTime);

                }
            }
        }
        public string SelectedComplatintsFromGuests {
            get { return selectedComplatintsFromGuests; }
            set
            {
                if (selectedComplatintsFromGuests != value)
                {
                    selectedComplatintsFromGuests = value;
                    NotifyPropertyChanged(nameof(SelectedComplatintsFromGuests));
                    //NewOwnerGuestGrade.ComplaintsFromOtherGuests = SetOwnerGuestGradeComplaintsFromGuests(selectedComplatintsFromGuests);

                }
            }
        }
        public List<string> BoolAnswers { get; set; }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public GradingGuestViewModel(OwnerGuestGradeService ownerGuestGradeService, OwnerAccommodationGradeSevice ownerAccommodationGradeService, ReservationService reservationService, Reservation SelectedReservation, ObservableCollection<Reservation> ReservationsForGrading, ObservableCollection<OwnerAccommodationGrade> OwnerAccommodationGradesForShowing)
        {
            this.ownerGuestGradeService = ownerGuestGradeService;
            this.ownerAccommodationGradeService = ownerAccommodationGradeService;
            this.reservationService = reservationService;
            this.SelectedReservation = SelectedReservation;
            this.ReservationsForGrading = ReservationsForGrading;
            this.OwnerAccommodationGradesForShowing = OwnerAccommodationGradesForShowing;
            SelectedComplatintsFromGuests = "No";
            SelectedIsPaymentCompletedOnTime = "Yes";
            SetOwnerGuestGradeParameters();
            CancelCommand = new RelayCommand(Cancel_Execute, CanExecute);
            GradeCommand = new RelayCommand(GradeGuest_Execute, CanExecute);
            BoolAnswers = new List<string>();
            FormHeading();


        }

        private void FormHeading()
        {
            if (Properties.Settings.Default.currentLanguage == "en-US")
            {
                Heading = SelectedReservation.Accommodation.Name + " Grading\n"
                      + "Guest: " + SelectedReservation.OwnerGuest.Name + " " + SelectedReservation.OwnerGuest.Surname + "";
            }
            else
            {
                Heading = SelectedReservation.Accommodation.Name + " Ocenjivanje\n"
                     + "Gost: " + SelectedReservation.OwnerGuest.Name + " " + SelectedReservation.OwnerGuest.Name + "";
            }

        }
        private void SetOwnerGuestGradeParameters()
        {
            NewOwnerGuestGrade = new OwnerGuestGrade();
            NewOwnerGuestGrade.ReservationId = SelectedReservation.Id;
            NewOwnerGuestGrade.Reservation = SelectedReservation;

        }

        

        private bool SetOwnerGuestGradeCompletedPayment(string selectedPaymentCompletedOnTime)
        {
            if(selectedPaymentCompletedOnTime.Contains("Yes"))
            {
                return true;
            }

            return false;
        }

        private bool SetOwnerGuestGradeComplaintsFromGuests(string selectedComplatintsFromGuests)
        {
            if (selectedComplatintsFromGuests.Contains("Yes"))
            {
                return true;
            }

            return false;
        }



        public bool CanExecute()
        {
            return true;
        }


        public void Cancel_Execute()
        {
            App.Current.Windows[4].Close();
        }

        private void GradeGuest_Execute()
        {
            if (NewOwnerGuestGrade.IsValid)
            {
                //SetOwnerGuestGradeBoolParameters();
                NewOwnerGuestGrade.ComplaintsFromOtherGuests = SetOwnerGuestGradeComplaintsFromGuests(SelectedComplatintsFromGuests);
                NewOwnerGuestGrade.IsPaymentCompletedOnTime = SetOwnerGuestGradeCompletedPayment(SelectedIsPaymentCompletedOnTime);
                ownerGuestGradeService.Save(NewOwnerGuestGrade);
                SelectedReservation.IsGraded = true;
                reservationService.Update(SelectedReservation);
                ReservationsForGrading.Remove(SelectedReservation);

                if (SelectedReservation.IsGradedByGuest)
                {
                    List<OwnerAccommodationGrade> ownerAccommodationGrades = ownerAccommodationGradeService.GetAll();
                    foreach (OwnerAccommodationGrade ownerAccommodationGrade in ownerAccommodationGrades)
                    {
                        if (ownerAccommodationGrade.ReservationId == SelectedReservation.Id)
                        {
                            OwnerAccommodationGradesForShowing.Add(ownerAccommodationGrade);
                        }
                    }
                }
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("Guest succesfully graded","Succesfull graded",MessageBoxButton.OK,MessageBoxImage.Information);
                    
                }
                else
                {
                    MessageBox.Show("Gost je uspesno ocenjen", "Uspesno ocenjeno", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
                Cancel_Execute();

            }
            else
            {
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("Reservation  can't be graded, because fileds are not valid", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Rezervacija ne moze biti ocenjena, jer polja nisu validna", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
        }


    }
}
