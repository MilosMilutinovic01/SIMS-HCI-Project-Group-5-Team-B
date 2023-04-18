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
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

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
        private List<Appointment> Appointments { get; set; }
        public Appointment SelectedAppointment { get; set; }

        private AppointmentController appointmentController { get; set; }

        private TemporaryTourAttendanceController tourAttendanceController;

        public TourAttendanceWindow(Tour selectedTour)
        {
            this.SelectedTour = selectedTour;
            SelectedAppointment = new Appointment();
            Available = new List<DateTime>();
            Appointments = new List<Appointment>();

            appointmentController = new AppointmentController();
            foreach (var appointment in appointmentController.GetAll())
            {
                if (appointment.TourId == SelectedTour.Id && appointment.Start > DateTime.Now)
                {
                    Available.Add(appointment.Start);
                    Appointments.Add(appointment);
                }
            }

            this.DataContext = this;
            InitializeComponent();
            ShowImages();
            tourAttendanceController = new TemporaryTourAttendanceController();
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
                MessageBox.Show("Number of people must be greater than zero");
                return;
            }
            if(SelectedAppointment.Start< DateTime.Now)
            {
                MessageBox.Show("Select date");
                return;
            }

            if (SelectedAppointment.FreeSpace >= NumberOfPeople)
            {
                tourAttendanceController.Save(new TourAttendance(SelectedAppointment.Id, NumberOfPeople, -1, -1));
                SelectedAppointment.FreeSpace -= NumberOfPeople;
                appointmentController.Update(SelectedAppointment);
                FreeSpaceTextBlock.Text = SelectedAppointment.FreeSpace.ToString();
            }
            else if(SelectedAppointment.FreeSpace > 0){
                string MessageBoxText = "Selected tour have only " + SelectedAppointment.FreeSpace.ToString() + " free spaces left, please register less people";
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
                    TourWindow tourWindow = new TourWindow(new User());
                    Setup(tourWindow);
                    tourWindow.SearchButton_Click(null, e);

                    this.Close();
                    tourWindow.Show();
                    this.Owner.Close();
                }
            }
        }

        private void DateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var appointment in Appointments)
            {
                if (appointment.Start.Equals(DateComboBox.SelectedItem))
                {
                    SelectedAppointment = appointment;
                    FreeSpaceTextBlock.Text = SelectedAppointment.FreeSpace.ToString();
                    break;
                }
            }
        }

        private void Setup(TourWindow tourWindow)
        {
            tourWindow.Location = new string(SelectedTour.Location.ToString());//SelectedTour.Locatio.ToString();
            tourWindow.PeopleAttending = 0;
            tourWindow.Lang = "";
            tourWindow.TourLength = "";
        }
    }
}
