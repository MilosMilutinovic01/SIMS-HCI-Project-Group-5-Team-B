using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.Pages;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest
{
    public class MainGuideGuestWindowViewModel
    {
        private GuideGuestProfilePage guideGuestProfilePage;
        private TourInformationPage tourInformationPage;
        private TourSearchPage tourSearchPage;
        

        public RelayCommandWithParams CloseNotificationCommand { get; }
        public RelayCommandWithParams VisitTourNotificationCommand { get; }
        public RelayCommandWithParams JoinTourNotificationCommand { get; }



        public ObservableCollection<GuideGuestNotificationDTO> Notifications { get; set; }
        private NotificationService notificationService;
        public MainGuideGuestWindowViewModel()
        {
            notificationService = new NotificationService();
            Notifications = new ObservableCollection<GuideGuestNotificationDTO>(notificationService.GetForGuideGuest(0));

            guideGuestProfilePage = new GuideGuestProfilePage();
            tourInformationPage = new TourInformationPage();
            tourSearchPage = new TourSearchPage();

            CloseNotificationCommand = new RelayCommandWithParams(CloseNotification_Execute);
            VisitTourNotificationCommand = new RelayCommandWithParams(VisitTourNotification_Execute);
            JoinTourNotificationCommand = new RelayCommandWithParams(JoinTourNotification_Execute);

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

        public object GetYourProfilePage()
        {
            return guideGuestProfilePage;
        }

        public object GetTourSearchPage()
        {
            return tourSearchPage;
        }




    }
}
