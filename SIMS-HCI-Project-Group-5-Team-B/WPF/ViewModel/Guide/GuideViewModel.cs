using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Properties;
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

        public Frame frame;
        public Guide guide;
        public string SuperGuide { get; set; }
        public bool Tooltips
        {
            get { return Properties.Settings.Default.Tooltips; }
            set
            {
                Properties.Settings.Default.Tooltips = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(Tooltips));
            }
        }
        public bool Help
        {
            get { return Properties.Settings.Default.Help; }
            set
            {
                Properties.Settings.Default.Help = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(Help));
            }
        }
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
        private bool isOpenedPopup;
        public bool IsOpenedPopup 
        {
            get { return isOpenedPopup; }
            set
            {
                isOpenedPopup = value;
                OnPropertyChanged(nameof(IsOpenedPopup));
            }
        }
        private bool isMenuOpened;
        public bool IsMenuOpened
        {
            get { return isMenuOpened; }
            set
            {
                isMenuOpened = value;
                OnPropertyChanged(nameof(IsMenuOpened));
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

        public RelayCommandMenu OpenMenuCommand { get; set; }

        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand FinishWizardCommand { get; set; }
        public RelayCommand ToggleOpenCommand { get; set; }
        #endregion

        #region actions
        private bool CanExecute_NavigateCommand()
        {
            return true;
        }

        private bool CanExecute_NavigateCommand1(object obj)
        {
            return true;
        }
        private void Execute_ToggleOpenCommand()
        {
            IsOpenedPopup = !IsOpenedPopup;
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
            Page tourRequest = new TourRequestAcceptPage(this.frame);
            this.frame.NavigationService.Navigate(tourRequest);
        }

        private void Execute_NavigateToTourRequestsWithStatisticsPageCommand()
        {
            //this.NavService.Navigate(
            //    new Uri("WPF/View/Guide/TourRequestsWithStatisticsPage.xaml", UriKind.Relative));
            Page tourRequestsStatistics = new TourRequestsStatisticsPage(this.frame);
            this.frame.NavigationService.Navigate(tourRequestsStatistics);
        }
        #endregion

        #region constructors
        public GuideViewModel(Guide guide, NavigationService navService, Frame frame) 
        {
            this.NavService = navService;
            this.NavigateToCreateTourPageCommand = new RelayCommand(Execute_NavigateToCreateTourPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTrackingTourPageCommand = new RelayCommand(Execute_NavigateToTrackingTourPageCommand, CanExecute_NavigateCommand);
            this.NavigateToUpcomingToursPageCommand = new RelayCommand(Execute_NavigateToUpcomingToursPageCommand, CanExecute_NavigateCommand);
            this.NavigateToMyToursPageCommand = new RelayCommand(Execute_NavigateToMyToursPageCommand, CanExecute_NavigateCommand);
            this.NavigateToReviewsPageCommand = new RelayCommand(Execute_NavigateToReviewsPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTourRequestsPageCommand = new RelayCommand(Execute_NavigateToTourRequestsPageCommand, CanExecute_NavigateCommand);
            this.NavigateToTourRequestsWithStatisticsPageCommand = new RelayCommand(Execute_NavigateToTourRequestsWithStatisticsPageCommand, CanExecute_NavigateCommand);
            this.OpenMenuCommand = new RelayCommandMenu(execute => this.IsMenuOpened = !this.IsMenuOpened, CanExecute_NavigateCommand1);
            this.GoBackCommand = new RelayCommand(Execute_GoBackCommand, CanExecute_NavigateCommand);
            this.ToggleOpenCommand = new RelayCommand(Execute_ToggleOpenCommand, CanExecute_NavigateCommand);
            this.Checker = false;
            this.frame = frame;
            this.guide = guide;
            this.frame.Content = new HomePage(this.guide, this.frame);
            this.IsMenuOpened = false;
            this.IsOpenedPopup = false;
            PageName = "Home page";
            HelpMessage = "Home page help message!";
        }
        #endregion
    }
}
