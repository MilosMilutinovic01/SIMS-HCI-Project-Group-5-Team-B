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
        public TourAttendanceWindow(Tour selectedTour)
        {
            this.SelectedTour = selectedTour;
            
            
            this.DataContext = this;
            InitializeComponent();
            ShowImages();
        }

        private void ShowImages()
        {
            imageListBox.Items.Clear();

            foreach (String imageSource in SelectedTour.PictureURLsString.Split(','))
            {
                imageListBox.Items.Add(imageSource);
            }
        }

        private void Attendance_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}
