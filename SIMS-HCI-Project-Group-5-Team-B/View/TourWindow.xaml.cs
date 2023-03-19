using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;


namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for TourWindow.xaml
    /// </summary>
    public partial class TourWindow : Window
    {
        public string Location { get; set; }
        public string TourLength { get; set; }
        public string Lang { get; set; }
        public int PeopleAttending { get; set; }

        public Tour SelectedTour { get; set; }


        private TourController tc;

        public ObservableCollection<Tour> tours { get; set; }

        public TourWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            tc = new TourController();
            tours = new ObservableCollection<Tour>(tc.GetAll());
        }

        public void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if(PeopleAttending < 0) return;
            tours.Clear();
            foreach(Tour tour in tc.Search(Location, Lang, TourLength, PeopleAttending))
            {
                tours.Add(tour);
            }
        }

        private void TourReservationButton_Click(object sender, RoutedEventArgs e)
        {
            if(DataGridTour.SelectedCells.Count > 0)
            {
                TourAttendanceWindow tourAttendanceWindow = new TourAttendanceWindow(SelectedTour);
                tourAttendanceWindow.Show();
                tourAttendanceWindow.Owner = this;
            }
        }
    }
}
