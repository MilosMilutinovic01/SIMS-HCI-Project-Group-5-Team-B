using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for OwnerGuestWindow.xaml
    /// </summary>
    public partial class OwnerGuestWindow : Window
    {
        private AccommodationService accommodationService;
        private LocationController locationController;
        private ReservationService reservationService;
        private OwnerService ownerService;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        private SuperOwnerService superOwnerService;
        private OwnerGuestService ownerGuestService;
        private OwnerGuest activeOwnerGuest;
        private ReservationChangeRequestService reservationChangeRequestService;
        private string username;

        public OwnerGuestWindow(string username)
        {
            InitializeComponent();
            this.username = username;
            locationController = new LocationController();
            ownerService = new OwnerService();
            accommodationService = new AccommodationService(locationController, ownerService);
            reservationService = new ReservationService(accommodationService);
            ownerAccommodationGradeService = new OwnerAccommodationGradeSevice(reservationService);
            superOwnerService = new SuperOwnerService(ownerAccommodationGradeService, accommodationService);
            ownerGuestService = new OwnerGuestService();
            reservationChangeRequestService = new ReservationChangeRequestService();
            activeOwnerGuest =  ownerGuestService.GetByUsername(username);
            
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Del(ShowNotification));
        }

        private delegate void Del();

        private void ShowAccomodation_Button_Click(object sender, RoutedEventArgs e)
        {

            AccommodationsWindow accomodationsWindow = new AccommodationsWindow(activeOwnerGuest.Id, locationController, ownerService, accommodationService, reservationService);
            accomodationsWindow.Show();
        }

        private void Reservations_Button_Click(object sender, RoutedEventArgs e)
        {
            ReservationsWindow reservationsWindow = new ReservationsWindow(reservationService,ownerAccommodationGradeService,superOwnerService,ownerService, activeOwnerGuest.Id, reservationChangeRequestService);
            reservationsWindow.Show();
        }

        private void Notifications_Button_Click(object sender, RoutedEventArgs e)
        {
            NotificationsWindow notificationsWindow = new NotificationsWindow(activeOwnerGuest.Id);
            notificationsWindow.Show();
        }

        private void ShowNotification()
        {
            NotificationController notificationController = new NotificationController();
            UserController userController = new UserController();
            User user = userController.GetByUsername(username);
            if (notificationController.Exists(user.Id))
            {
                MessageBox.Show("You have new notifactions!");
            }
        }
    }
}
