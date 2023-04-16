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
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using System.Collections.ObjectModel;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        LocationController locationService;
        AccommodationService accommodationService;
        ReservationService reservationService;
        OwnerService ownerService;
        public List<Reservation> reservationsForGrading;
        OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        OwnerGuestGradeService ownerGuestGradeService;
        SuperOwnerService superOwnerService;
        public Owner LogedInOwner;
        public ObservableCollection<Accommodation> AccomodationsOfLogedInOwner { get; set; }
        public ObservableCollection<Reservation> ReservationsForGrading { get; set; }
        public ObservableCollection<OwnerAccommodationGrade> OwnerAccommodationGradesForShowing { get; set; }
        public ObservableCollection<ReservationChangeRequest> OwnersPendingRequests { get; set; }
        public Reservation SelectedReservation { get; set; }
        public OwnerAccommodationGrade SelectedOwnerAccommodationGrade { get; set; }
        public ReservationChangeRequest SelectedReservationChangeRequest   { get; set; }

        //Added for dependency injection


        private readonly AcceptingAndDecliningReservationChangeRequestViewModel _viewModel;
        private ReservationChangeRequestService reservationChangeRequestService;

        //private DateTime lastDisplayed;
        public OwnerWindow(string username)
        {
            InitializeComponent();

            DataContext = this;
            

            locationService = new LocationController();
            ownerService = new OwnerService();
            accommodationService = new AccommodationService(locationService, ownerService);
            reservationService = new ReservationService(accommodationService);
            ownerAccommodationGradeService = new OwnerAccommodationGradeSevice(reservationService);
            ownerGuestGradeService = new OwnerGuestGradeService(reservationService);
            superOwnerService = new SuperOwnerService(reservationService, ownerAccommodationGradeService, ownerService, accommodationService);

            reservationsForGrading = new List<Reservation>();
            LogedInOwner = ownerService.GetByUsername(username);
            LogedInOwner.GradeAverage = superOwnerService.CalculateGradeAverage(LogedInOwner);
            ownerService.Update(LogedInOwner);
            //lastDisplayed = Properties.Settings.Default.LastShownDate;
            AccomodationsOfLogedInOwner = new ObservableCollection<Accommodation>(accommodationService.GetAccommodationsOfLogedInOwner(LogedInOwner));
            ReservationsForGrading = new ObservableCollection<Reservation>(reservationService.GetReservationsForGrading(LogedInOwner));
            OwnerAccommodationGradesForShowing = new ObservableCollection<OwnerAccommodationGrade>(ownerAccommodationGradeService.GetOwnerAccommodationGradesForShowing(LogedInOwner));

            reservationChangeRequestService = new ReservationChangeRequestService();
            _viewModel = new AcceptingAndDecliningReservationChangeRequestViewModel(reservationChangeRequestService,reservationService, LogedInOwner,SelectedReservationChangeRequest);
            OwnersPendingRequests = new ObservableCollection<ReservationChangeRequest>(_viewModel.OwnersPendingRequests);

            

        }

        private void NotifyOwnerToGradeGuests(object sender, RoutedEventArgs e)
        {
            reservationsForGrading = reservationService.GetReservationsForGrading(LogedInOwner);
            if (reservationsForGrading.Count != 0 /*&& DateTime.Today != lastDisplayed*/)
            {
                MessageBox.Show("You have guests to grade!!!");
                //lastDisplayed = DateTime.Today;
                //Properties.Settings.Default.LastShownDate = lastDisplayed;
                //Properties.Settings.Default.Save();
            }
        }

        private void Create_Accommodation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationForm accommodationForm = new AccommodationForm(AccomodationsOfLogedInOwner, LogedInOwner);
            accommodationForm.Show();
        }

        
        

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Grade_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                GradingGuestWindow gradingGuestWindow = new GradingGuestWindow(ownerGuestGradeService, ownerAccommodationGradeService, reservationService, SelectedReservation, ReservationsForGrading, OwnerAccommodationGradesForShowing);
                gradingGuestWindow.Show();
            }
        }

        private void Details_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOwnerAccommodationGrade != null)
            {
                OwnerAccommodationGradeDetailsWindow ownerAccommodationGradeDetailsWindow = new OwnerAccommodationGradeDetailsWindow(SelectedOwnerAccommodationGrade);
                ownerAccommodationGradeDetailsWindow.Show();
            }
        }

        private void Report_Button_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Accept_Button_Click(object sender, RoutedEventArgs e)
        {
            AcceptReservationChangeRequestWindow acceptReservationChangeRequestWindow = new AcceptReservationChangeRequestWindow(reservationChangeRequestService, reservationService, LogedInOwner, SelectedReservationChangeRequest,OwnersPendingRequests);
            acceptReservationChangeRequestWindow.Show();
        }


        private void Decline_Button_Click(object sender, RoutedEventArgs e)
        {
            DeclineReservationChangeRequestForm declineReservationChangeRequestForm = new DeclineReservationChangeRequestForm(reservationChangeRequestService, reservationService, LogedInOwner, SelectedReservationChangeRequest, OwnersPendingRequests);
            declineReservationChangeRequestForm.Show();
        }

    }
}
