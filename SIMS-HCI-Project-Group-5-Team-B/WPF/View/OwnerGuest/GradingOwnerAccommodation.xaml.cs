using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
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
        private ReservationGridView reservationView;
        public OwnerAccommodationGrade OwnerAccommodationGrade { get; set; }
        private OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        private ReservationService reservationService;
        //private ObservableCollection<ReservationGridView> ReservationViews;

        
        private SuperOwnerService superOwnerService;
        private OwnerService ownerService;
        public GradingOwnerAccommodation(OwnerAccommodationGradeSevice ownerAccommodationGradeService, ReservationService reservationService, ReservationGridView reservationView, SuperOwnerService superOwnerService, OwnerService ownerService)
        {
            InitializeComponent();
            this.DataContext = this;
            OwnerAccommodationGrade = new OwnerAccommodationGrade();
            this.reservationView = reservationView;
            this.SelectedReservation = reservationView.Reservation;
            this.ownerAccommodationGradeService = ownerAccommodationGradeService;
            this.reservationService = reservationService;
            this.superOwnerService = superOwnerService;
            this.ownerService = ownerService;
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
                
                Close();
            }
            else
            {
                MessageBox.Show("Data is not valid!");
            }
        }
        
    }
}
