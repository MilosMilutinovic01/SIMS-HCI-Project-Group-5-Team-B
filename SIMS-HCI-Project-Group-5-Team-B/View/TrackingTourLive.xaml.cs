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
        private TourController tourController;
        private LocationController locationController;
        private KeyPointsController keyPointsController;
        public ObservableCollection<Tour> AvailableTours { get; set; }
        public ObservableCollection<KeyPoints> KeyPoints { get; set; }
        public Tour SelectedTour { get; set; }
        public KeyPoints SelectedKeyPoint { get; set; }
        public TrackingTourLive()
        {
            InitializeComponent();
            DataContext = this;
            locationController = new LocationController();
            keyPointsController = new KeyPointsController();
            tourController = new TourController(locationController);
            AvailableTours = new ObservableCollection<Tour>(tourController.GetAvailableTours());
            KeyPoints = new ObservableCollection<KeyPoints>();
            //GuestsListBox.Items.Add();    //add guests to listbox
            TourStartButton.IsEnabled = false;
            KeyPointCheckButton.IsEnabled = false;
        }

        private void buttonView_Click(object sender, RoutedEventArgs e)
        {
            //TODO show details about selected tour
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyPoints.Clear();
            foreach (KeyPoints keyPoint in keyPointsController.getByTourId(SelectedTour.Id))
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
            KeyPoints[0].Selected = true;
            keyPointsController.Update(KeyPoints[0]);
            KeyPoints.Clear();
            foreach (KeyPoints keyPoint in keyPointsController.getByTourId(SelectedTour.Id))
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
            foreach (KeyPoints keyPoint in keyPointsController.getByTourId(SelectedTour.Id))
            {
                KeyPoints.Add(keyPoint);
            }
        }
    }
}
