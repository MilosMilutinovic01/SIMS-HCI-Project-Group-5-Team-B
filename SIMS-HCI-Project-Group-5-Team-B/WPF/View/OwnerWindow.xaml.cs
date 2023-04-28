using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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
        public ReservationChangeRequest SelectedReservationChangeRequest { get; set; }
        private readonly HandleReservationChangeRequestViewModel handleReservationChangeRequestViewModel;
        private ReservationChangeRequestService reservationChangeRequestService;
        private RenovationService renovationService;
        public ObservableCollection<Notification> Notifications { get; set; }

        public ObservableCollection<Renovation> PastRenovations { get; set; }
        public ObservableCollection<RenovationGridView> FutureRenovations { get; set; }
        private readonly RenovationViewModel renovationViewModel;
        public RenovationGridView SelectedRenovationGridView { get; set; }

        private readonly YearlyAccommodationStatisticsViewModel yearlyAccommodationStatisticsViewModel;
        private RenovationRequestService renovationRequestService;
        public ObservableCollection<YearlyAccommodationStatistics> YearlySelectedAccommodationStatistics { get; set; }
        private int SelectedAccommmodationId;
        public YearlyAccommodationStatistics SelectedYearlyAccommodationStatistics { get; set; }
        private YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService;
        private MonthlyAccommodationStatisticsService monthlyAccommodationStatisticsService;

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
            superOwnerService = new SuperOwnerService(ownerAccommodationGradeService, accommodationService);

            reservationsForGrading = new List<Reservation>();
            LogedInOwner = ownerService.GetByUsername(username);
            LogedInOwner.GradeAverage = superOwnerService.CalculateGradeAverage(LogedInOwner);

            if (LogedInOwner.GradeAverage > 4.5 && superOwnerService.GetNumberOfGrades(LogedInOwner) >= 50)
            {
                LogedInOwner.IsSuperOwner = true;
            }
            else
            {
                LogedInOwner.IsSuperOwner = false;
            }

            ownerService.Update(LogedInOwner);
            //lastDisplayed = Properties.Settings.Default.LastShownDate;
            AccomodationsOfLogedInOwner = new ObservableCollection<Accommodation>(accommodationService.GetAccommodationsOfLogedInOwner(LogedInOwner));
            ReservationsForGrading = new ObservableCollection<Reservation>(reservationService.GetReservationsForGrading(LogedInOwner));
            OwnerAccommodationGradesForShowing = new ObservableCollection<OwnerAccommodationGrade>(ownerAccommodationGradeService.GetOwnerAccommodationGradesForShowing(LogedInOwner));

            reservationChangeRequestService = new ReservationChangeRequestService();
            handleReservationChangeRequestViewModel = new HandleReservationChangeRequestViewModel(reservationChangeRequestService, reservationService, LogedInOwner, SelectedReservationChangeRequest);
            OwnersPendingRequests = new ObservableCollection<ReservationChangeRequest>(handleReservationChangeRequestViewModel.OwnersPendingRequests);

            /*foreach(ReservationChangeRequest reservationChangeRequest in OwnersPendingRequests)
            {
                if (reservationService.IsAccomodationAvailableForChangingReservationDates(reservationChangeRequest.Reservation, reservationChangeRequest.Start, reservationChangeRequest.End))
                {
                    reservationChangeRequest.IsAvailable = "Yes";
                }
                else
                {
                    reservationChangeRequest.IsAvailable = "No";
                }
                reservationChangeRequestService.Update(reservationChangeRequest);

            }*/
            

            OwnerNotificationsViewModel ownerNotificationsViewModel = new OwnerNotificationsViewModel(LogedInOwner.Id);
            Notifications = new ObservableCollection<Notification>(ownerNotificationsViewModel.Notifications);
            renovationService = new RenovationService();

            renovationViewModel = new RenovationViewModel(renovationService, reservationService, LogedInOwner.Id, SelectedRenovationGridView);
            PastRenovations = new ObservableCollection<Renovation>(renovationViewModel.PastRenovations);
            FutureRenovations = new ObservableCollection<RenovationGridView>(renovationViewModel.FutureRenovations);
            ShowAccommodations(LogedInOwner);
            renovationRequestService = new RenovationRequestService();
            yearlyAccommodationStatisticsService = new YearlyAccommodationStatisticsService();
            monthlyAccommodationStatisticsService = new MonthlyAccommodationStatisticsService();
            yearlyAccommodationStatisticsViewModel = new YearlyAccommodationStatisticsViewModel(yearlyAccommodationStatisticsService);

        }

        private void NotifyOwner(object sender, RoutedEventArgs e)
        {
            reservationsForGrading = reservationService.GetReservationsForGrading(LogedInOwner);
            if (reservationsForGrading.Count != 0 && Notifications.Count != 0)
            {
                MessageBox.Show("You have guests to grade!!!");
                MessageBox.Show("You have new notifications!");
            }
            else if (reservationsForGrading.Count != 0)
            {
                MessageBox.Show("You have guests to grade!!!");

            }
            else if (Notifications.Count != 0)
            {
                MessageBox.Show("You have new notifications!");
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
            AcceptReservationChangeRequestWindow acceptReservationChangeRequestWindow = new AcceptReservationChangeRequestWindow(reservationChangeRequestService, reservationService, LogedInOwner, SelectedReservationChangeRequest, OwnersPendingRequests);
            acceptReservationChangeRequestWindow.Show();
        }


        private void Decline_Button_Click(object sender, RoutedEventArgs e)
        {
            DeclineReservationChangeRequestForm declineReservationChangeRequestForm = new DeclineReservationChangeRequestForm(reservationChangeRequestService, reservationService, LogedInOwner, SelectedReservationChangeRequest, OwnersPendingRequests);
            declineReservationChangeRequestForm.Show();
        }

        private void Schedule_Button_Click(object sender, RoutedEventArgs e)
        {
            ScheduleRenovationForm scheduleRenovationForm = new ScheduleRenovationForm(renovationService, accommodationService,reservationService,LogedInOwner, FutureRenovations, SelectedRenovationGridView);
            scheduleRenovationForm.Show();
        }

        private void CallOf_Button_Click(object sender, RoutedEventArgs e)
        {
            CallOffRenovationWindow callOffRenovationWindow = new CallOffRenovationWindow(SelectedRenovationGridView, FutureRenovations, renovationService, reservationService,LogedInOwner.Id);
            callOffRenovationWindow.Show();
        }

        private void Monthly_Statistics_Button_Click(object sender, RoutedEventArgs e)
        {
            MonthlyAccommodationStatisticsWindow monthlyAccommodationStatisticsWindow = new MonthlyAccommodationStatisticsWindow(SelectedYearlyAccommodationStatistics, monthlyAccommodationStatisticsService,SelectedAccommmodationId);
            monthlyAccommodationStatisticsWindow.Show();
        }

        private void Show_Statistics_Button_Click(object sender, RoutedEventArgs e)
        {
            YearlySelectedAccommodationStatistics = new ObservableCollection<YearlyAccommodationStatistics>(yearlyAccommodationStatisticsViewModel.GetYearlyAccommodationStatistics(SelectedAccommmodationId));
            yearlyAccommodationStatisticsViewModel.MarkBusiest(YearlySelectedAccommodationStatistics);
            dgStatistics.ItemsSource = YearlySelectedAccommodationStatistics;
        }

        public void ShowAccommodations(Owner owner)
        {
            foreach (Accommodation accommodation in accommodationService.GetAll())
            {
                if (accommodation.Owner.Id == owner.Id)
                {
                    ComboBoxItem cbItem = new ComboBoxItem();
                    cbItem.Content = accommodation.Name;
                    cbItem.Tag = accommodation.Id;
                    if ((int)cbItem.Tag == accommodation.Id)
                    {
                        cbItem.IsSelected = true; // za prikaz podatka
                    }
                    Accommodation_ComboBox.Items.Add(cbItem);
                }
            }
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (ComboBoxItem comboBoxItem in Accommodation_ComboBox.Items)
            {
                if (comboBoxItem.IsSelected)
                {
                    //ovde treba da prosledi dalje izabrani smestaj za statistiku, treba proslediti fji koja ce izlistati statistiku za izabrani smetsaj ,prolsediti smetaj metodi
                    // Get the selected item from the combo box
                   SelectedAccommmodationId =(int)comboBoxItem.Tag;
                }
            }

        }

    }
}
