using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for TourWindow.xaml
    /// </summary>
    public partial class TourWindow : Window
    {
        public string Location { get; set; }
        public string TourLength { get; set; }
        public string Lang { get; set; }
        public int PeopleAttending { get; set; }

        public Tour SelectedTour { get; set; }
        public ObservableCollection<Tour> tours { get; set; }
        
        
        private TourController tourController;
        
        private TourService tourService;
        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;
        private TourGradeService tourGradeService;
        private User loggedUser;

        public TourWindow(User loggedUser)
        {
            //if (TrackingTourLiveWindow.answer == false)
            //{
            //    bool result = MessageBox.Show("Do you want to join " + TrackingTourLiveWindow.keyPointName + "?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            //    TrackingTourLiveWindow.answer = true;
            //}

            InitializeComponent();
            this.DataContext = this;
            this.loggedUser = loggedUser;

            LoadData();

            tourController = new TourController();
            tours = new ObservableCollection<Tour>(tourController.GetAll());
        }

        private void LoadData()
        {
            KeyPointCSVRepository keyPointCSVRepository = new KeyPointCSVRepository();
            LocationCSVRepository locationCSVRepository = new LocationCSVRepository();
            TourCSVRepository tourCSVRepository = new TourCSVRepository(keyPointCSVRepository, locationCSVRepository);

            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            TourGradeCSVRepository tourGradeCSVRepository = new TourGradeCSVRepository();
            AppointmentCSVRepository appointmentCSVRepository = new AppointmentCSVRepository(tourCSVRepository);

            tourService = new TourService(tourCSVRepository);
            tourAttendanceService = new TourAttendanceService(tourAttendanceCSVRepository);
            tourGradeService = new TourGradeService(tourGradeCSVRepository);
            appointmentService = new AppointmentService(appointmentCSVRepository, tourAttendanceService);
        }

        public void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if(PeopleAttending < 0) return;
            tours.Clear();
            foreach(Tour tour in tourController.Search(Location, Lang, TourLength, PeopleAttending))
            {
                tours.Add(tour);
            }
        }

        private void TourReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridTour.SelectedCells.Count > 0)
            {
                TourAttendanceWindow tourAttendanceWindow = new TourAttendanceWindow(SelectedTour);
                tourAttendanceWindow.Show();
                tourAttendanceWindow.Owner = this;
            }
        }

        private void YourProfile_Click(object sender, RoutedEventArgs e)
        {
            GuideGuestWindow guideGuestWindow = new GuideGuestWindow(loggedUser, tourGradeService, appointmentService);
            guideGuestWindow.Show();
        }
    }
}
