using System;
using System.Collections.Generic;
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
using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Controller;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        LocationController locationController;
        AccommodationController accommodationController;
        ReservationController reservationController;
        public List<Reservation> reservationsForGrading;

        //private DateTime lastDisplayed;
        public OwnerWindow()
        {
            InitializeComponent();
            locationController = new LocationController();
            accommodationController = new AccommodationController(locationController);
            reservationController = new ReservationController(accommodationController);
            reservationsForGrading = new List<Reservation>();

           
            //lastDisplayed = Properties.Settings.Default.LastShownDate;
            
           
        }

        private void NotifyOwnerToGradeGuests(object sender, RoutedEventArgs e)
        {
            reservationsForGrading = reservationController.GetSuiableReservationsForGrading();
            if (reservationsForGrading.Count != 0 /*&& DateTime.Today != lastDisplayed*/)
            {
                MessageBox.Show("You have guests to grade!!!");
                //lastDisplayed = DateTime.Today;
                //Properties.Settings.Default.LastShownDate = lastDisplayed;
                //Properties.Settings.Default.Save();
            }
        }

        private void Create_Accommodation_Click(object sender, RoutedEventArgs e)
        {
            
            AccommodationForm accommodationForm = new AccommodationForm();
            accommodationForm.Show();
        }

        private void Grade_Guest_Click(object sender, RoutedEventArgs e)
        {
            ReservationsForGradingWindow reservationsForGradingWindow = new ReservationsForGradingWindow();
            reservationsForGradingWindow.Show();
        }
    }
}
