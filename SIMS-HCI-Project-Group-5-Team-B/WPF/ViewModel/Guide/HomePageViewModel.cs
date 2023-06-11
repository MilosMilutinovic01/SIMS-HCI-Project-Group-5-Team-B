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
using System.Windows.Media.Effects;
using System.Windows.Navigation;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class HomePageViewModel : ViewModel
    {
        #region fields
        public string Username { get; set; }

        public string SuperGuide { get; set; }

        private UserService userService;
        private GuideService guideService;
        private AppointmentService appointmentService;
        private VoucherService voucherService;
        private bool checker;
        public Frame frame;
        public SIMS_HCI_Project_Group_5_Team_B.Domain.Models.Guide guide;
        public bool Checker
        {
            get { return checker; }
            set
            {
                checker = value;
                OnPropertyChanged();
            }
        }
        public bool IsVisibleWizard
        {
            get
            {
                return Properties.Settings.Default.Wizard;
            }
            set
            {
                Properties.Settings.Default.Wizard = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged(nameof(IsVisibleWizard));
            }
        }
        private bool isOpenedPopup;
        public bool IsOpenedPopup
        {
            get
            {
                return isOpenedPopup;
            }
            set
            {
                if(isOpenedPopup != value)
                {
                    isOpenedPopup = value;
                    OnPropertyChanged(nameof(IsOpenedPopup));
                }
            }
        }

        public RelayCommand OpenMenuCommand { get; set; }

        public RelayCommand SignOutCommand { get; set; }
        public RelayCommand OpenPopupCommand { get; set; }

        public RelayCommand ResignCommand { get; set; }
        public RelayCommand NavigateToUpcomingToursPageCommand { get; set; }
        public RelayCommand NavigateToMyToursPageCommand { get; set; }
        public RelayCommand NavigateToReviewsPageCommand { get; set; }
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
        private void Execute_OpenPopupCommand()
        {
            IsOpenedPopup = !IsOpenedPopup;
        }

        private void Execute_ResignCommand()
        {
            bool result = MessageBox.Show("Are you sure you want to resign?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (result)
            {
                this.guide.Resigned = true;
                guideService.Update(this.guide);
                userService.DeleteUser(userService.GetById(guide.Id));
                //appointmentService.CancelAllGuideAppointments(guide.Id);
                Window window = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                if (window != null)
                {
                    window.Close();
                }
            }
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
        #endregion

        #region constructors
        public HomePageViewModel(SIMS_HCI_Project_Group_5_Team_B.Domain.Models.Guide guide, NavigationService navService, Frame frame)
        {
            PageName = "Home page";
            HelpMessage = "Home page help message";

            userService = new UserService();
            guideService = new GuideService();
            appointmentService = new AppointmentService();
            Username = "Username: " + guide.Username;
            if (guide.AverageGrade > 4)
                SuperGuide = "Super-guide: yes";
            else
                SuperGuide = "Super-guide: no";
            this.NavigateToUpcomingToursPageCommand = new RelayCommand(Execute_NavigateToUpcomingToursPageCommand, CanExecute_NavigateCommand);
            this.NavigateToMyToursPageCommand = new RelayCommand(Execute_NavigateToMyToursPageCommand, CanExecute_NavigateCommand);
            this.NavigateToReviewsPageCommand = new RelayCommand(Execute_NavigateToReviewsPageCommand, CanExecute_NavigateCommand);
            this.SignOutCommand = new RelayCommand(Execute_SignOutCommand, CanExecute_NavigateCommand);
            this.ResignCommand = new RelayCommand(Execute_ResignCommand, CanExecute_NavigateCommand);
            this.OpenPopupCommand = new RelayCommand(Execute_OpenPopupCommand, CanExecute_NavigateCommand);
            this.Checker = false;
            this.frame = frame;
            this.guide = guide;
            IsOpenedPopup = false;
        }
        #endregion
    }
}
