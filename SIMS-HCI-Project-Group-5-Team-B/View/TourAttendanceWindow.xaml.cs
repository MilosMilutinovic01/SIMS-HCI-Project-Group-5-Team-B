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

        private GuestTourAttendanceController controller = new GuestTourAttendanceController();

        public TourAttendanceWindow(Tour selectedTour)
        {
            this.SelectedTour = selectedTour;
            Available = new List<DateTime>();
            //TODO Add available dates

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

            //TODO
            if (true)//Check if there is enough free space on the TourAttendance
            {
                controller.Save(new GuestTourAttendance(0, -1/*Tour attendance Id*/, NumberOfPeople,-1));
            }
            else if(true){//Check if tour have any free space
                string MessageBoxText = "Selected tour have only " + "" + " free spaces left, do you want to register less people";//Add number
                string MessageBoxCaption = "Error attending";

                MessageBoxResult CancleAttending = MessageBox.Show(MessageBoxText, MessageBoxCaption, MessageBoxButton.YesNo);

                if(CancleAttending == MessageBoxResult.Yes)
                {
                    NumberOfPeople = -1;//Free Space
                }
            }
            if(true)//Tour does not have any free space, recomend other
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
                    tourWindow.NumberOfPeople = 0;
                    tourWindow.Location = "";
                    tourWindow.TourLength = "";
                    tourWindow.SearchButton_Click(null, e);
                    this.Close();
                    this.Owner.Close();
                    tourWindow.Show();
                }
            }
        }
    }
}
