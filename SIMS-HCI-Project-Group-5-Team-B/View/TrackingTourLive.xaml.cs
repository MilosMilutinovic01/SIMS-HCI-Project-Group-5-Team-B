using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
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
    public partial class TrackingTourLive : Window
    {
        private LocationController locationController;
        private KeyPointsController keyPointsController;
        private TourAttendanceController tourAttendanceController;
        private UserController userController;
        private TourController tourController;
        
        public ObservableCollection<TourAttendance> AvailableTourAttendances { get; set; }
        //public ObservableCollection<TourStart> TourStarts { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public ObservableCollection<User> GuideGuest { get; set; }
        public TourAttendance SelectedTourAttendance { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        public User SelectedGuest { get; set; }
        public TrackingTourLive()
        {
            InitializeComponent();
            DataContext = this;
            locationController = new LocationController();
            keyPointsController = new KeyPointsController();
            tourAttendanceController = new TourAttendanceController();
            userController = new UserController();
            tourController = new TourController(locationController);

            AvailableTourAttendances = new ObservableCollection<TourAttendance>(tourAttendanceController.GetAll());
            KeyPoints = new ObservableCollection<KeyPoint>();
            GuideGuest = new ObservableCollection<User>();
            TourStartButton.IsEnabled = false;
            KeyPointCheckButton.IsEnabled = false;
            SendRequestButton.IsEnabled = false;
            List<Tour> tours = tourController.GetAll();
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            //TODO show details about selected tour
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyPoints.Clear();
            foreach (KeyPoint keyPoint in keyPointsController.getByTourId(SelectedTourAttendance.TourId))
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
        }
        private void TourStartButton_Click(object sender, RoutedEventArgs e)
        {
            ////TourAttendance tourAttendance = new TourAttendance(SelectedTour.Id, 0, tourStartController.getByTourId(SelectedTour.Id).Start, SelectedTour.MaxGuests);
            //KeyPoints[0].Selected = true;
            //GuestSelectionWindow gsWindow = new GuestSelectionWindow(users.GetAll());
            //gsWindow.Show();
            //if (KeyPoints[0].Selected == true)
            //keyPointsController.Update(KeyPoints[0]);
            //KeyPoints.Clear();
            //foreach (KeyPoint keyPoint in keyPointsController.getByTourId(SelectedTour.Id))
            //{
            //    KeyPoints.Add(keyPoint);
            //}
            //AvailableToursDataGrid.IsHitTestVisible = false;
            //KeyPointCheckButton.IsEnabled = true;
            
        }

        private void KeyPointCheckButton_Click(object sender, RoutedEventArgs e)
        {
            //if (SelectedKeyPoint == KeyPoints[0] || SelectedKeyPoint.Selected == true)
            //    MessageBox.Show("Already selected!");
            //else if (KeyPoints[SelectedKeyPoint.Id - 2].Selected == true && SelectedKeyPoint.Id == KeyPoints.Count())
            //{
            //    TourStartButton.IsEnabled = true;
            //    SelectedKeyPoint.Selected = true;
            //    keyPointsController.Update(SelectedKeyPoint);
            //    MessageBox.Show("Tour ended!");
            //}
            //else if (KeyPoints[SelectedKeyPoint.Id - 2].Selected == true)
            //{
            //    SelectedKeyPoint.Selected = true;
            //    keyPointsController.Update(SelectedKeyPoint);
            //}
            //else
            //    MessageBox.Show("Can't select this key point before previous is selected!");
            //KeyPoints.Clear();
            //foreach (KeyPoint keyPoint in keyPointsController.getByTourId(SelectedTour.Id))
            //{
            //    KeyPoints.Add(keyPoint);
            //}
            //GuestSelectionWindow gsWindow = new GuestSelectionWindow(users.GetAll());
            //gsWindow.Show();
        }

        private void SendRequestButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void guestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SendRequestButton.IsEnabled = true;
            //if (SelectedGuest.Answer == true)
            //    SendRequestButton.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
