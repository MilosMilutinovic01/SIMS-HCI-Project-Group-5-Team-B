using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.Pages;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest
{
    public class MainGuideGuestWindowViewModel
    {
        private GuideGuestProfilePage guideGuestProfilePage;
        private TourSearchPage tourSearchPage;
        

        public RelayCommandWithParams CloseNotificationCommand { get; }
        public RelayCommandWithParams VisitTourNotificationCommand { get; }
        public RelayCommandWithParams JoinTourNotificationCommand { get; }

        public RelayCommand BackCommand { get; }
        public RelayCommand HomeCommand { get; }
        public RelayCommand ProfileCommand { get; }

        public ObservableCollection<GuideGuestNotificationDTO> Notifications { get; set; }
        private NotificationService notificationService;

        private NavigationService frameNavigationService;
        public MainGuideGuestWindowViewModel(NavigationService frameNavigationService)
        {
            this.frameNavigationService = frameNavigationService;

            notificationService = new NotificationService();
            Notifications = new ObservableCollection<GuideGuestNotificationDTO>(notificationService.GetForGuideGuest(0));

            guideGuestProfilePage = new GuideGuestProfilePage();
            tourSearchPage = new TourSearchPage();



            CloseNotificationCommand = new RelayCommandWithParams(CloseNotification_Execute);
            VisitTourNotificationCommand = new RelayCommandWithParams(VisitTourNotification_Execute);
            JoinTourNotificationCommand = new RelayCommandWithParams(JoinTourNotification_Execute);

            BackCommand = new RelayCommand(Back_Execute);
            HomeCommand = new RelayCommand(Home_Execute);
            ProfileCommand = new RelayCommand(Profile_Execute);
        }

        private void Profile_Execute()
        {
            frameNavigationService.Content = guideGuestProfilePage;
        }
        private void Home_Execute()
        {
            frameNavigationService.Content = tourSearchPage;
        }
        private void Back_Execute()
        {
            if (frameNavigationService.CanGoBack)
            {
                frameNavigationService.GoBack();
            }
        }
        private void JoinTourNotification_Execute(object obj)
        {
            var notificationDTO = obj as GuideGuestNotificationDTO;
            throw new NotImplementedException();
        }
        private void VisitTourNotification_Execute(object obj)
        {
            var notificationDTO = obj as GuideGuestNotificationDTO;
            throw new NotImplementedException();
        }
        private void CloseNotification_Execute(object obj)
        {
            var notificationDTO = obj as GuideGuestNotificationDTO;
            throw new NotImplementedException();
        }
    }
}
