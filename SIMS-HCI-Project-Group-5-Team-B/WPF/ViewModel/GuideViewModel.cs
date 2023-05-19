using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class GuideViewModel : ViewModel
    {
        #region fields
        public string Username { get; set; }

        public GuideService guideService;
        public TourService tourService;
        public AppointmentService appointmentService;
        public TourAttendanceService tourAttendanceService;
        public TourGradeService tourGradeService;
        public Frame frame;
        public Guide guide;
        public string SuperGuide { get; set; }
        private bool checker;
        public bool Checker
        {
            get { return checker; }
            set
            {
                checker = value;
                OnPropertyChanged();
            }
        }
        public NavigationService NavService { get; set; }

        public RelayCommand NavigateToCreateTourPageCommand { get; set; }

        public RelayCommand NavigateToTrackingTourPageCommand { get; set; }

        public RelayCommand NavigateToUpcomingToursPageCommand { get; set; }

        public RelayCommand NavigateToMyToursPageCommand { get; set; }

        public RelayCommand NavigateToReviewsPageCommand { get; set; }

        public RelayCommand NavigateToTourRequestsPageCommand { get; set; }

        public RelayCommand NavigateToTourRequestsWithStatisticsPageCommand { get; set; }

        public RelayCommandWithParams OpenMenuCommand { get; set; }

        public RelayCommand GoBackCommand { get; set; }
        #endregion

        #region actions
        private bool CanExecute_NavigateCommand()
        {
            return true;
        }

        private void Execute_GoBackCommand()
        {
            if(this.frame.NavigationService.CanGoBack)
                this.frame.NavigationService.GoBack();
        }

        private void Execute_NavigateToCreateTourPageCommand()
        {
            //this.NavService.Navigate(
            //    new Uri("WPF/View/Guide/CreateTourPage.xaml", UriKind.Relative));
            Page createTour = new CreateTourPage();
            this.frame.NavigationService.Navigate(createTour);
        }

        private void Execute_NavigateToTrackingTourPageCommand()
        {
            Page trackingTour = new TrackingTourPage();
            this.frame.NavigationService.Navigate(trackingTour);
        }

        private void Execute_NavigateToUpcomingToursPageCommand()
        {
            Page upcomingToursPage = new UpcomingToursPage();
            this.frame.NavigationService.Navigate(upcomingToursPage);
        }

        private void Execute_NavigateToMyToursPageCommand()
        {
            Page myTours = new MyToursPage();
            this.frame.NavigationService.Navigate(myTours);
        }

        private void Execute_NavigateToReviewsPageCommand()
        {
            Page reviews = new ReviewsPage();
            this.frame.NavigationService.Navigate(reviews);
        }

        private void Execute_NavigateToTourRequestsPageCommand()
        {
            //this.NavService.Navigate(
            //    new Uri("WPF/View/Guide/TourRequestsPage.xaml", UriKind.Relative));
            Page tourRequest = new TourRequestPage(this.frame);
            this.frame.NavigationService.Navigate(tourRequest);
        }

        private void Execute_NavigateToTourRequestsWithStatisticsPageCommand()
        {
            //this.NavService.Navigate(
            //    new Uri("WPF/View/Guide/TourRequestsWithStatisticsPage.xaml", UriKind.Relative));
            Page reviews = new ReviewsPage();
            this.frame.NavigationService.Navigate(reviews);
        }
        #endregion

        #region constructors
        public GuideViewModel(Guide guide, NavigationService navService, Frame frame) 
        {
            KeyPointCSVRepository keyPointCSVRepository = new KeyPointCSVRepository();
            LocationCSVRepository locationCSVRepository = new LocationCSVRepository();
            TourCSVRepository tourCSVRepository = new TourCSVRepository();
            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            TourGradeCSVRepository tourGradeCSVRepository = new TourGradeCSVRepository();
            AppointmentCSVRepository appointmentCSVRepository = new AppointmentCSVRepository();

            guideService = new GuideService();
            tourService = new TourService(tourCSVRepository);
            tourAttendanceService = new TourAttendanceService();
            tourGradeService = new TourGradeService();
            appointmentService = new AppointmentService();

            this.NavService = navService;
            Username = "Username: " + guideService.getById(1).Username;
            SuperGuide = "Super-guide: no";
            this.NavigateToCreateTourPageCommand = new RelayCommand(Execute_NavigateToCreateTourPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTrackingTourPageCommand = new RelayCommand(Execute_NavigateToTrackingTourPageCommand, CanExecute_NavigateCommand);
            this.NavigateToUpcomingToursPageCommand = new RelayCommand(Execute_NavigateToUpcomingToursPageCommand, CanExecute_NavigateCommand);
            this.NavigateToMyToursPageCommand = new RelayCommand(Execute_NavigateToMyToursPageCommand, CanExecute_NavigateCommand);
            this.NavigateToReviewsPageCommand = new RelayCommand(Execute_NavigateToReviewsPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTourRequestsPageCommand = new RelayCommand(Execute_NavigateToTourRequestsPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTourRequestsWithStatisticsPageCommand = new RelayCommand(Execute_NavigateToTourRequestsWithStatisticsPageCommand, CanExecute_NavigateCommand);
            this.OpenMenuCommand = new RelayCommandWithParams(execute => this.Checker = !this.Checker);
            this.GoBackCommand = new RelayCommand(Execute_GoBackCommand, CanExecute_NavigateCommand);
            this.Checker = false;
            this.frame = frame;
            this.guide = guide;
            this.frame.Content = new HomePage(this.guide, this.frame);
        }
        #endregion
    }
}
