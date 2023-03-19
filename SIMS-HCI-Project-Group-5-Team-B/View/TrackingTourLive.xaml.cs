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
        private GuestTourAttendanceController guestTourAttendanceController;

        public ObservableCollection<TourAttendance> AvailableTourAttendances { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public ObservableCollection<GuideGuest> GuideGuest { get; set; }
        public TourAttendance SelectedTourAttendance { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        public GuideGuest SelectedGuest { get; set; }

        public GuestTourAttendance guestTourAttendance;
        public bool checkedFlag;
        public TrackingTourLive()
        {
            InitializeComponent();
            DataContext = this;
            locationController = new LocationController();
            keyPointsController = new KeyPointsController();
            tourAttendanceController = new TourAttendanceController();
            userController = new UserController();
            tourController = new TourController(locationController);
            guestTourAttendanceController = new GuestTourAttendanceController();

            AvailableTourAttendances = new ObservableCollection<TourAttendance>(tourAttendanceController.GetAll());
            KeyPoints = new ObservableCollection<KeyPoint>();
            GuideGuest = new ObservableCollection<GuideGuest>();
            GuideGuest.Add(new GuideGuest(1, "Uros", "Nikolovski"));
            TourStartButton.IsEnabled = false;
            KeyPointCheckButton.IsEnabled = false;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyPointCheckButton.IsEnabled = true;
            if (SelectedTourAttendance.Started)
            {
                KeyPointCheckButton.IsEnabled = true;
                TourStartButton.IsEnabled = false;
            }
            else
            {
                KeyPointCheckButton.IsEnabled = true;
                TourStartButton.IsEnabled = true;
            }
        }
        private void TourStartButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTourAttendance.Ended) 
            {
                MessageBox.Show("Tour is ended!");
                return;
            }
            tourAttendanceController.Update(SelectedTourAttendance);
            guestTourAttendance = new GuestTourAttendance(SelectedTourAttendance.Id, 5,-1);
            guestTourAttendanceController.Save(guestTourAttendance);
            if (checkedFlag == false)
            {
                guestTourAttendance.KeyPointGuestArrivedId = 1;
                guestTourAttendanceController.Update(guestTourAttendance);
                checkedFlag = true;
            }
            bool result = MessageBox.Show("Confirm?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (result)
            {
                SelectedTourAttendance.Started = true;
                KeyPoints[0].Selected = true;
                keyPointsController.Update(KeyPoints[0]);
                AvailableToursDataGrid.IsHitTestVisible = false;
                KeyPointCheckButton.IsEnabled = true;
            }
            KeyPoints.Clear();
            foreach (KeyPoint keyPoint in keyPointsController.getByTourId(SelectedTourAttendance.TourId))
            {
                KeyPoints.Add(keyPoint);
            }
        }

        private void KeyPointCheckButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedKeyPoint == KeyPoints[0] || SelectedKeyPoint.Selected == true)
                MessageBox.Show("Already selected!");
            else if(keyPointsDataGrid.SelectedIndex == keyPointsDataGrid.Items.Count - 1 && KeyPoints[keyPointsDataGrid.SelectedIndex - 1].Selected == true)
            {
                TourStartButton.IsEnabled = true;
                SelectedKeyPoint.Selected = true;
                keyPointsController.Update(SelectedKeyPoint);
                SelectedTourAttendance.Ended = true;
                tourAttendanceController.Update(SelectedTourAttendance);
                if (checkedFlag == false)
                {
                    guestTourAttendance.KeyPointGuestArrivedId = SelectedKeyPoint.Id;
                    guestTourAttendanceController.Update(guestTourAttendance);
                    checkedFlag = true;
                }
                AvailableToursDataGrid.IsHitTestVisible = true;
                KeyPointCheckButton.IsEnabled = false;
                SelectButton.IsEnabled = true;
                MessageBox.Show("Tour ended!");
            }
            else if (KeyPoints[SelectedKeyPoint.Id - 2].Selected == true)
            {
                SelectedKeyPoint.Selected = true;
                keyPointsController.Update(SelectedKeyPoint);
                if (checkedFlag == false)
                {
                    guestTourAttendance.KeyPointGuestArrivedId = SelectedKeyPoint.Id;
                    guestTourAttendanceController.Update(guestTourAttendance);
                    checkedFlag = true;
                }

            }
            else
                MessageBox.Show("Can't select this key point before previous is selected!");
            KeyPoints.Clear();
            foreach (KeyPoint keyPoint in keyPointsController.getByTourId(SelectedTourAttendance.TourId))
            {
                KeyPoints.Add(keyPoint);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedTourAttendance.Ended = true;
            tourAttendanceController.Update(SelectedTourAttendance);
            AvailableToursDataGrid.IsHitTestVisible = true;
            KeyPointCheckButton.IsEnabled = false;
        }

        private void DataGrid_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            KeyPoints.Clear();
            KeyPointCheckButton.IsEnabled = false;
            TourStartButton.IsEnabled = true;
            foreach (KeyPoint keyPoint in keyPointsController.getByTourId(SelectedTourAttendance.TourId))
            {
                KeyPoints.Add(keyPoint);
            }
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            checkedFlag = false;
            GuideGuest.Clear();
        }

        private void DataGrid_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
