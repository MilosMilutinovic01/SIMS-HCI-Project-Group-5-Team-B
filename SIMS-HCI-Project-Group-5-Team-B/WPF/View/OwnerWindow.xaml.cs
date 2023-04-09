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
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {
        LocationController locationController;
        AccommodationService accommodationController;
        ReservationService reservationController;
        OwnerService ownerController;
        public List<Reservation> reservationsForGrading;
        OwnerAccommodationGradeSevice ownerAccommodationGradeController;
        SuperOwnerService superOwnerController;
        public Owner owner;
        //Added for dependency injection
        private OwnerGuestCSVRepository ownerGuestCSVRepository;
        private ReservationCSVRepository reservationCSVRepository;

        //private DateTime lastDisplayed;
        public OwnerWindow(string username)
        {
            InitializeComponent();
            ownerGuestCSVRepository = new OwnerGuestCSVRepository();
            reservationCSVRepository = new ReservationCSVRepository();
            locationController = new LocationController();
            ownerController = new OwnerService();
            accommodationController = new AccommodationService(locationController, ownerController);
            reservationController = new ReservationService(accommodationController, ownerGuestCSVRepository, reservationCSVRepository);  //MODIFIED
            ownerAccommodationGradeController = new OwnerAccommodationGradeSevice(reservationController);
            superOwnerController = new SuperOwnerService(reservationController, ownerAccommodationGradeController, ownerController, accommodationController);
            reservationsForGrading = new List<Reservation>();
            owner = ownerController.GetByUsername(username);
            owner.GradeAverage = superOwnerController.CalculateGradeAverage(owner);
            //owner.NumberReservations = superOwnerController.GetNumberOfReservations(owner);
            ownerController.Update(owner);
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
            
            AccommodationForm accommodationForm = new AccommodationForm(owner);
            accommodationForm.Show();
        }

        private void Grade_Guest_Click(object sender, RoutedEventArgs e)
        {
            ReservationsForGradingWindow reservationsForGradingWindow = new ReservationsForGradingWindow();
            reservationsForGradingWindow.Show();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
