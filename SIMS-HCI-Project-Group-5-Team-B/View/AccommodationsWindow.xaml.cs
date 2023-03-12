using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for AccomodationsWindow.xaml
    /// </summary>
    public partial class AccommodationsWindow : Window, INotifyPropertyChanged
    {
        private AccommodationController accommodationController;
        private LocationController locationController;
        private ReservationController reservationController;
        public ObservableCollection<Accommodation> Accomodations { get; set; }
        public Accommodation SelectedAccommodation { get; set; }

        public AccommodationsWindow()
        {
            InitializeComponent();
            DataContext = this;
            locationController = new LocationController();
            accommodationController = new AccommodationController(locationController);
            Accomodations = new ObservableCollection<Accommodation>(accommodationController.GetAll());
            reservationController = new ReservationController(accommodationController);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(SelectedAccommodation != null)
            {
                AccomodationDetailsWindow accomodationDetailsWindow = new AccomodationDetailsWindow(SelectedAccommodation, reservationController);
                accomodationDetailsWindow.Show();
            }
        }
    }
}
