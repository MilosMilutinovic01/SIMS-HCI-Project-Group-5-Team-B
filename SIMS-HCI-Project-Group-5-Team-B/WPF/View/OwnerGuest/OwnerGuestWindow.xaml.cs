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
        private AccommodationService accommodationController;
        private LocationController locationController;
        private ReservationService reservationController;
        private OwnerService ownerController;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeController;
        private SuperOwnerService superOwnerController;
        private OwnerGuestService ownerGuestService;
        private OwnerGuest activeOwnerGuest;
        private ReservationChangeRequestService reservationChangeRequestService;
        private string username;

        public OwnerGuestWindow(string username)
        {
            InitializeComponent();
            this.username = username;
            locationController = new LocationController();
            ownerController = new OwnerService();
            accommodationController = new AccommodationService(locationController, ownerController);
            reservationController = new ReservationService(accommodationController);
            ownerAccommodationGradeController = new OwnerAccommodationGradeSevice(reservationController);
            superOwnerController = new SuperOwnerService(ownerAccommodationGradeController, accommodationController);
            ownerGuestService = new OwnerGuestService();
            reservationChangeRequestService = new ReservationChangeRequestService();
            activeOwnerGuest =  ownerGuestService.GetByUsername(username);
            
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Del(ShowNotification));
        }

        private delegate void Del();
        private void ShowAccomodation_Button_Click(object sender, RoutedEventArgs e)
        {

            AccommodationsWindow accomodationsWindow = new AccommodationsWindow(activeOwnerGuest.Id, locationController, ownerController, accommodationController, reservationController);
            accomodationsWindow.Show();
        }

        private void Reservations_Button_Click(object sender, RoutedEventArgs e)
        {
            ReservationsWindow reservationsWindow = new ReservationsWindow(reservationController,ownerAccommodationGradeController,superOwnerController,ownerController, activeOwnerGuest.Id, reservationChangeRequestService);
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
