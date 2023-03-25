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
    public partial class TrackingTourLiveWindow : Window
    {
        private KeyPointsController keyPointsController;
        private TourAttendanceController tourAttendanceController;

        public ObservableCollection<TourAttendance> AvailableTourAttendances { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public ObservableCollection<GuideGuest> GuideGuest { get; set; }
        public TourAttendance SelectedTourAttendance { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        public GuideGuest SelectedGuest { get; set; }

        public static bool answer = true;
        public static string keyPointName;
        public TrackingTourLiveWindow()
        {
            InitializeComponent();
            DataContext = this;

            keyPointsController = new KeyPointsController();
            tourAttendanceController = new TourAttendanceController();

            AvailableTourAttendances = new ObservableCollection<TourAttendance>(tourAttendanceController.GetAllAvaillable());
            KeyPoints = new ObservableCollection<KeyPoint>();
            GuideGuest = new ObservableCollection<GuideGuest>();

            GuideGuest.Add(new GuideGuest(1, "Uros", "Nikolovski"));
            
            TourStartButton.IsEnabled = true;
            KeyPointCheckButton.IsEnabled = false;
            SendRequestButton.IsEnabled = false;

            CheckStarted();
        }
        private void CheckStarted()
        {
            foreach(TourAttendance tourAttendance in  tourAttendanceController.GetAll())
            {
                if (tourAttendance.Started == true && tourAttendance.Ended != true) 
                {
                    SelectedTourAttendance = tourAttendance;
                    TourStartButton.IsEnabled = false;
                    KeyPointCheckButton.IsEnabled = true;
                    AvailableAttendanceDataGrid.IsHitTestVisible = false;
                    SendRequestButton.IsEnabled = true;
                    break;
                }
            }
        }

        private void TourStartButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedTourAttendance.Ended) 
            {
                MessageBox.Show("Tour is ended!");
                return;
            }
            bool result = MessageBox.Show("Are you sure you want to start?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (result)
            {
                SelectedTourAttendance.Started = true;
                tourAttendanceController.Update(SelectedTourAttendance);

                KeyPoints[0].Selected = true;
                keyPointsController.Update(KeyPoints[0]);
                keyPointName = KeyPoints[0].Name;


                AvailableAttendanceDataGrid.IsHitTestVisible = false;
                KeyPointCheckButton.IsEnabled = true;
            }

            TourStartButton.IsEnabled = false;
            KeyPointCheckButton.IsEnabled = true;

            RefreshKeyPoints();
        }

        private void KeyPointCheckButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValidKeyPoint = KeyPointsDataGrid.SelectedIndex == KeyPointsDataGrid.Items.Count - 1 && KeyPoints[KeyPointsDataGrid.SelectedIndex - 1].Selected == true;
            
            if (SelectedKeyPoint == KeyPoints[0] || SelectedKeyPoint.Selected == true)
            {
                MessageBox.Show("Already selected!");
                keyPointName = KeyPoints[0].Name;
            }
            else if (isValidKeyPoint)
            {
                SelectedKeyPoint.Selected = true;
                keyPointsController.Update(SelectedKeyPoint);
                keyPointName = SelectedKeyPoint.Name;

                SelectedTourAttendance.Ended = true;
                tourAttendanceController.Update(SelectedTourAttendance);
                
                MessageBox.Show("Tour ended!");
                SendRequestButton.IsEnabled = false;
                
                TourStartButton.IsEnabled = true;
                KeyPointCheckButton.IsEnabled = false;
                AvailableAttendanceDataGrid.IsHitTestVisible = true;
            }
            else if (KeyPoints[KeyPointsDataGrid.SelectedIndex - 1].Selected == true)
            {
                SelectedKeyPoint.Selected = true;
                keyPointsController.Update(SelectedKeyPoint);
                keyPointName = SelectedKeyPoint.Name;
            }
            else
            {
                MessageBox.Show("Can't select this key point before previous is selected!");
            }

            RefreshKeyPoints();
        }

        private void RefreshKeyPoints()
        {
            KeyPoints.Clear();
            List<KeyPoint> keyPoints = keyPointsController.getByTourId(SelectedTourAttendance.TourId);
            foreach (KeyPoint keyPoint in keyPoints)
            {
                KeyPoints.Add(keyPoint);
            }
        }

        private void AttendancesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshKeyPoints();
        }
        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = MessageBox.Show("Are you sure you want to end?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if(result)
            {
                SelectedTourAttendance.Ended = true;
                tourAttendanceController.Update(SelectedTourAttendance);

                AvailableAttendanceDataGrid.IsHitTestVisible = true;
                TourStartButton.IsEnabled = true;
                KeyPointCheckButton.IsEnabled = false;
            }
        }

        private void SendRequestButton_Click1(object sender, RoutedEventArgs e)
        {
            answer = false;
            MessageBox.Show("Sent!");
            SendRequestButton.IsEnabled = false;
        }

        private void GuestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SendRequestButton.IsEnabled = true;
        }
    }
}
