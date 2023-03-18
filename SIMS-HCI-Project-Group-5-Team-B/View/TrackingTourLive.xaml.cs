using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
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

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for TrackingTourLive.xaml
    /// </summary>
    public partial class TrackingTourLive : Window
    {
        //private TourController tourController;
        //private LocationController locationController;
        private KeyPointsController keyPointsController;
        private AppointmentController appointmentController;
        public ObservableCollection<Appointment> AvailableAppointemnts { get; set; }
        public ObservableCollection<KeyPoints> KeyPoints { get; set; }
        public ObservableCollection<GuideGuest> GuideGuest { get; set; }
        public Appointment SelectedAppointment { get; set; }
        public KeyPoints SelectedKeyPoint { get; set; }
        public GuideGuest SelectedGuest { get; set; }
        public TrackingTourLive()
        {
            InitializeComponent();
            DataContext = this;
            //locationController = new LocationController();
            keyPointsController = new KeyPointsController();
            //tourController = new TourController(locationController);
            appointmentController = new AppointmentController();
            AvailableAppointemnts = new ObservableCollection<Appointment>(appointmentController.GetAll());
            KeyPoints = new ObservableCollection<KeyPoints>();
            GuideGuest = new ObservableCollection<GuideGuest>();
            //GuestsListBox.Items.Add();    //add guests to listbox
            TourStartButton.IsEnabled = false;
            KeyPointCheckButton.IsEnabled = false;
            SendRequestButton.IsEnabled = false;
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            //TODO show details about selected tour
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyPoints.Clear();
            foreach (KeyPoints keyPoint in keyPointsController.getByTourId(SelectedAppointment.TourId))
            {
                KeyPoints.Add(keyPoint);
            }
            if (KeyPoints[0].Selected == false)
            {
                KeyPointCheckButton.IsEnabled = true;
                TourStartButton.IsEnabled = true;
            }
            else
            {
                KeyPointCheckButton.IsEnabled = true;
                TourStartButton.IsEnabled = false;
            }

            GuideGuest.Add(new GuideGuest(0, "Uros", "Nikolovski"));
        }
        private void TourStartButton_Click(object sender, RoutedEventArgs e)
        {
            KeyPoints[0].Selected = true;
            keyPointsController.Update(KeyPoints[0]);
            KeyPoints.Clear();
            foreach (KeyPoints keyPoint in keyPointsController.getByTourId(SelectedAppointment.TourId))
            {
                KeyPoints.Add(keyPoint);
            }
            AvailableToursDataGrid.IsHitTestVisible = false;
            KeyPointCheckButton.IsEnabled = true;
            
        }

        private void KeyPointCheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedKeyPoint == KeyPoints[0] || SelectedKeyPoint.Selected == true)
                MessageBox.Show("Already selected!");
            else if(KeyPoints[SelectedKeyPoint.Order - 1].Selected == true && SelectedKeyPoint.Order + 1 == KeyPoints.Count())
            {
                TourStartButton.IsEnabled = true;
                SelectedKeyPoint.Selected = true;
                keyPointsController.Update(SelectedKeyPoint);
                MessageBox.Show("Tour ended!");
            }
            else if (KeyPoints[SelectedKeyPoint.Order - 1].Selected == true)
            {
                SelectedKeyPoint.Selected = true;
                keyPointsController.Update(SelectedKeyPoint);
            }
            else
                MessageBox.Show("Can't select this key point before previous is selected!");
            KeyPoints.Clear();
            foreach (KeyPoints keyPoint in keyPointsController.getByTourId(SelectedAppointment.TourId))
            {
                KeyPoints.Add(keyPoint);
            }
        }

        private void SendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void guestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SendRequestButton.IsEnabled = true;
            if (SelectedGuest.Answer == true)
                SendRequestButton.IsEnabled = false;
        }
    }
}
