using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide;
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

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for GuideWindow.xaml
    /// </summary>
    public partial class GuideWindow : Window
    {
        private GuideService guideService;
        
        private TourService tourService;
        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;
        private TourGradeService tourGradeService;
        public GuideWindow(string username)
        {
            InitializeComponent();
            LoadData();
            guideService = new GuideService();
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
        private void AddTourClick(object sender, RoutedEventArgs e)
        {
            //MainFrame.NavigationService.Navigate(new TourCreateForm());
            TourCreateForm tourForm = new TourCreateForm(tourService,appointmentService);
            tourForm.Show();
        }

        private void TrackinTourLiveClick(object sender, RoutedEventArgs e)
        {
            TrackingTourLiveWindow trackingTourLive = new TrackingTourLiveWindow(appointmentService);
            trackingTourLive.Show();
        }

        private void TourCancellationClick(object sender, RoutedEventArgs e)
        {
            TourCancelWindow tourCancel = new TourCancelWindow(appointmentService);
            tourCancel.Show();
        }

        private void SignOutClick(object sender, RoutedEventArgs e)
        {
            ReviewsWindow reviewsWindow = new ReviewsWindow(appointmentService);
            reviewsWindow.Show();
        }

        private void MyToursClick(object sender, RoutedEventArgs e)
        {
            MyTours myTours = new MyTours(appointmentService);
            myTours.Show();
        }
    }
}