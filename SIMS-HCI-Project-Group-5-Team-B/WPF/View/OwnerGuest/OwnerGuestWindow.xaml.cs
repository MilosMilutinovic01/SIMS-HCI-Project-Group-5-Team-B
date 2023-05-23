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
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for OwnerGuestWindow.xaml
    /// </summary>
    public partial class OwnerGuestWindow : Window
    {

        private string username;
        private OwnerGuestWindowViewModel ownerGuestWindowViewModel;
        

        public OwnerGuestWindow(string username)
        {
            InitializeComponent();       
            this.username = username;
            ownerGuestWindowViewModel = new OwnerGuestWindowViewModel(username,frame);
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Del(ShowNotification));
        }

        private delegate void Del();

        private void ShowAccomodation_Button_Click(object sender, RoutedEventArgs e)
        {

            ownerGuestWindowViewModel.ShowAccommodation();
        }

        private void Reservations_Button_Click(object sender, RoutedEventArgs e)
        {
            
            ownerGuestWindowViewModel.ShowReservations();
        }

        private void Notifications_Button_Click(object sender, RoutedEventArgs e)
        {
           ownerGuestWindowViewModel.ShowNotifications();
            
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

        private void Grades_Button_Click(object sender, RoutedEventArgs e)
        {
            ownerGuestWindowViewModel.ShowGrades();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F2) {
            ShowAccomodation_Button_Click(sender, e);
            }
            if(e.Key == Key.F4)
            {
                Reservations_Button_Click(sender, e);
            }
            if( e.Key == Key.F5)
            {
                Grades_Button_Click(sender, e);
            }
            if (e.Key == Key.F7)
            {
                Notifications_Button_Click(sender, e);
            }
            if(e.Key == Key.F6)
            {
                Forums_Click(sender, e);
            }
        }

        private void Forums_Click(object sender, RoutedEventArgs e)
        {
           ownerGuestWindowViewModel.ShowForums();
        }
    }
}
