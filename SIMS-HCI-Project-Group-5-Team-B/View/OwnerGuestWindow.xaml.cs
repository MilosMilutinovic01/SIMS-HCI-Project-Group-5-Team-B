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
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.View;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for OwnerGuestWindow.xaml
    /// </summary>
    public partial class OwnerGuestWindow : Window
    {
        private AccommodationController accommodationController;
        private LocationController locationController;
        private ReservationController reservationController;
        private OwnerController ownerController;
        private OwnerAccommodationGradeController ownerAccommodationGradeController;

        public OwnerGuestWindow()
        {
            InitializeComponent();
            locationController = new LocationController();
            ownerController = new OwnerController();
            accommodationController = new AccommodationController(locationController, ownerController);
            reservationController = new ReservationController(accommodationController);
            ownerAccommodationGradeController = new OwnerAccommodationGradeController(reservationController);
        }

        private void ShowAccomodation_Button_Click(object sender, RoutedEventArgs e)
        {

            AccommodationsWindow accomodationsWindow = new AccommodationsWindow();
            accomodationsWindow.Show();
        }

        private void Reservations_Button_Click(object sender, RoutedEventArgs e)
        {
            ReservationsWindow reservationsWindow = new ReservationsWindow(reservationController,ownerAccommodationGradeController);
            reservationsWindow.Show();
        }
    }
}
