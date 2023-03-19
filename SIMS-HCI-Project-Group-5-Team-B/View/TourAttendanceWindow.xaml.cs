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
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for TourAttendanceWindow.xaml
    /// </summary>
    public partial class TourAttendanceWindow : Window
    {
        public Tour SelectedTour { get; set; }
        public int NumberOfPeople { get; set; }
        public List<DateTime> Available { get; set; }
        private DateTime SelectedDate { get; set; }
        private List<TourAttendance> Attendances { get; set; }
        public TourAttendance SelectedAttendance { get; set; }

        private TourAttendanceController tourAttendanceController { get; set; }

        private GuestTourAttendanceController controller = new GuestTourAttendanceController();

        public TourAttendanceWindow(Tour selectedTour)
        {
            this.SelectedTour = selectedTour;
            SelectedAttendance = new TourAttendance();
            Available = new List<DateTime>();
            Attendances = new List<TourAttendance>();

            tourAttendanceController = new TourAttendanceController();
            foreach (var tourAttendance in tourAttendanceController.GetAll())
            {
                if (tourAttendance.TourId == SelectedTour.Id && tourAttendance.Start > DateTime.Now)
                {
                    Available.Add(tourAttendance.Start);
                    Attendances.Add(tourAttendance);
                }
            }

            this.DataContext = this;
            InitializeComponent();
            ShowImages();
        }

        private void ShowImages()
        {
            imageListBox.Items.Clear();

            foreach (String imageSource in SelectedTour.ImageUrls.Split(','))
            {
                imageListBox.Items.Add(imageSource);
            }
        }

        private void Attendance_Click(object sender, RoutedEventArgs e)
        {
            if(NumberOfPeople <= 0)
            {
                MessageBox.Show("No no");
                return;
            }
            if(SelectedAttendance.Start< DateTime.Now)
            {
                MessageBox.Show("Select date");
                return;
            }

            if (SelectedAttendance.FreeSpace >= NumberOfPeople)
            {
                controller.Save(new GuestTourAttendance(1, SelectedAttendance.Id, NumberOfPeople, -1));
                SelectedAttendance.FreeSpace -= NumberOfPeople;
                tourAttendanceController.Update(SelectedAttendance);
                Attendances.Clear();
                foreach (var tourAttendance in tourAttendanceController.GetAll())
                {
                    if (tourAttendance.TourId == SelectedTour.Id && tourAttendance.Start > DateTime.Now)
                    {
                        Attendances.Add(tourAttendance);
                    }
                }
                FreeSpaceTextBlock.Text = SelectedAttendance.FreeSpace.ToString();
            }
            else if(SelectedAttendance.FreeSpace > 0){
                string MessageBoxText = "Selected tour have only " + SelectedAttendance.FreeSpace.ToString() + " free spaces left, please register less people";//Add number
                string MessageBoxCaption = "Error attending";

                MessageBoxResult CancleAttending = MessageBox.Show(MessageBoxText, MessageBoxCaption, MessageBoxButton.OK);
            }
            else//Tour does not have any free space, recomend other
            {
                string MessageBoxText = "There is no free space on this tour, do you want us to recemond you some other tours";
                string MessageBoxCaption = "No free space";

                MessageBoxResult ShowAlternatives = MessageBox.Show(MessageBoxText, MessageBoxCaption, MessageBoxButton.YesNo);

                if (ShowAlternatives == MessageBoxResult.Yes)
                {
                    //Show alternatives
                    TourWindow tourWindow = new TourWindow();
                    //Needs change when everything is linked
                    tourWindow.Lang = new string(SelectedTour.Language);//SelectedTour.Locatio.ToString();
                    tourWindow.PeopleAttending = 0;
                    tourWindow.Location = "";
                    tourWindow.TourLength = "";
                    tourWindow.SearchButton_Click(null, e);
                    this.Close();
                    this.Owner.Close();
                    tourWindow.Show();
                }
            }
        }

        private void DateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var attendance in Attendances)
            {
                if (attendance.Start.Equals(DateComboBox.SelectedItem))
                {
                    SelectedAttendance = attendance;
                    FreeSpaceTextBlock.Text = SelectedAttendance.FreeSpace.ToString();
                    break;
                }
            }
        }
    }
}
