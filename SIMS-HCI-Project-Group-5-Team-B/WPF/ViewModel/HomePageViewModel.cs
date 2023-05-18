using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.View;
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
    public class HomePageViewModel : ViewModel
    {
        #region fields
        public string Username { get; set; }

        public string SuperGuide { get; set; }

        private GuideService guideService;
        private bool checker;
        public Frame frame;
        public Guide guide;
        public bool Checker
        {
            get { return checker; }
            set
            {
                checker = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToCreateTourPageCommand { get; set; }

        public RelayCommand NavigateToTrackingTourPageCommand { get; set; }

        public RelayCommand NavigateToUpcomingToursPageCommand { get; set; }

        public RelayCommand NavigateToMyToursPageCommand { get; set; }

        public RelayCommand NavigateToReviewsPageCommand { get; set; }

        public RelayCommand NavigateToTourRequestsPageCommand { get; set; }

        public RelayCommand NavigateToTourRequestsWithStatisticsPageommand { get; set; }

        public RelayCommand OpenMenuCommand { get; set; }

        public RelayCommand SignOutCommand { get; set; }

        public RelayCommand ResignCommand { get; set; }
        #endregion

        #region actions

        private bool CanExecute_NavigateCommand()
        {
            return true;
        }

        private void Execute_SignOutCommand()
        {
            bool result = MessageBox.Show("Are you sure you want to sign out?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if(result)
            {
                Window window = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (window != null)
                {
                    window.Close();
                }
            }
        }

        private void Execute_ResignCommand()
        {
            bool result = MessageBox.Show("Are you sure you want to resign?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (result)
            {
                Window window = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (window != null)
                {
                    window.Close();
                }
            }
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
            Page reviews = new ReviewsPage();
            this.frame.NavigationService.Navigate(reviews);
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
        public HomePageViewModel(Guide guide, Frame frame)
        {
            guideService = new GuideService();
            Username = "Username: " + guideService.getById(1).Username;
            SuperGuide = "Super-guide: no";
            this.NavigateToCreateTourPageCommand = new RelayCommand(Execute_NavigateToCreateTourPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTrackingTourPageCommand = new RelayCommand(Execute_NavigateToTrackingTourPageCommand, CanExecute_NavigateCommand);
            this.NavigateToUpcomingToursPageCommand = new RelayCommand(Execute_NavigateToUpcomingToursPageCommand, CanExecute_NavigateCommand);
            this.NavigateToMyToursPageCommand = new RelayCommand(Execute_NavigateToMyToursPageCommand, CanExecute_NavigateCommand);
            this.NavigateToReviewsPageCommand = new RelayCommand(Execute_NavigateToReviewsPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTourRequestsPageCommand = new RelayCommand(Execute_NavigateToTourRequestsPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTourRequestsWithStatisticsPageommand = new RelayCommand(Execute_NavigateToTourRequestsWithStatisticsPageCommand, CanExecute_NavigateCommand);
            //this.OpenMenuCommand = new RelayCommand(execute => this.Checker = !this.Checker, CanExecute_NavigateCommand);
            this.SignOutCommand = new RelayCommand(Execute_SignOutCommand, CanExecute_NavigateCommand);
            this.ResignCommand = new RelayCommand(Execute_ResignCommand, CanExecute_NavigateCommand);
            this.Checker = false;
            this.frame = frame;
            this.guide = guide;
        }
        #endregion
    }
}
