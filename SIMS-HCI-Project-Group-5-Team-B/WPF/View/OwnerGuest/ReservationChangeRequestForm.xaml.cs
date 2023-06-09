using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for ReservationChangeRequestForm.xaml
    /// </summary>
    public partial class ReservationChangeRequestForm : Window
    {
        SingleReservationViewModel selectedReservationView;
        public ReservationChangeRequestForm(ObservableCollection<ReservationChangeRequest> ReservaitionChangeRequests,SingleReservationViewModel selectedReservationView, ReservationChangeRequestService reservationChangeRequestService, ReservationService reservationService)
        {
            InitializeComponent();
            this.DataContext = new ReservationChangeRequestViewModel(ReservaitionChangeRequests, selectedReservationView, reservationChangeRequestService, reservationService);
            this.selectedReservationView = selectedReservationView;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000);
            StartDP.Text = "12/12/2023";
            await Task.Delay(2000);
            EndDP.Text = "12/29/2023";
            await Task.Delay(2000);
            //simulacija pritiska dugmeta
            SaveBtn.Background = Brushes.LightBlue;
            await Task.Delay(500);
            SaveBtn.Background = new SolidColorBrush(Color.FromRgb(162,162,200));
            await Task.Delay(500);
           MessageBoxResult result = MessageBox.Show("Request sent!","Reservation Change Request", MessageBoxButton.OK, MessageBoxImage.Information);
            if(result == MessageBoxResult.OK)
            {
                StartDP.SelectedDate = selectedReservationView.Reservation.StartDate;
                EndDP.SelectedDate= selectedReservationView.Reservation.EndDate;
                 MessageBox.Show("Demo Finished", "Reservation Change Request", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }
}
