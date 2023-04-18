using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
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
        private AppointmentController appointmentController;
        private NotificationController notificationController;
        private TemporaryTourAttendanceController tourAttendanceController;

        public ObservableCollection<Appointment> AvailableAppointments { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public ObservableCollection<GuideGuest> GuideGuest { get; set; }
        public Appointment SelectedAppointment { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        public GuideGuest SelectedGuest { get; set; }

        //public static bool answer = true;
        //public static string keyPointName;
        public TrackingTourLiveWindow()
        {
            InitializeComponent();
            DataContext = this;

            keyPointsController = new KeyPointsController();
            appointmentController = new AppointmentController();
            notificationController = new NotificationController();
            tourAttendanceController = new TemporaryTourAttendanceController();

            AvailableAppointments = new ObservableCollection<Appointment>(appointmentController.GetAllAvaillable());
            KeyPoints = new ObservableCollection<KeyPoint>();
            GuideGuest = new ObservableCollection<GuideGuest>();

            //GuideGuest.Add(new GuideGuest(1, "Uros", "Nikolovski"));
            
            TourStartButton.IsEnabled = true;
            KeyPointCheckButton.IsEnabled = false;
            SendRequestButton.IsEnabled = false;

            CheckStarted();
        }
        private void CheckStarted()
        {
            foreach(Appointment appointment in  appointmentController.GetAll())
            {
                if (appointment.Started == true && appointment.Ended != true) 
                {
                    SelectedAppointment = appointment;
                    TourStartButton.IsEnabled = false;
                    KeyPointCheckButton.IsEnabled = true;
                    AvailableAppointmentDataGrid.IsHitTestVisible = false;
                    SendRequestButton.IsEnabled = true;
                    break;
                }
            }
        }

        private void TourStartButton_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedAppointment.Ended) 
            {
                MessageBox.Show("Tour is ended!");
                return;
            }
            bool result = MessageBox.Show("Are you sure you want to start?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (result)
            {
                SelectedAppointment.Started = true;
                SelectedAppointment.CheckedKeyPointId = KeyPoints[0].Id;
                appointmentController.Update(SelectedAppointment);

                KeyPoints[0].Selected = true;
                keyPointsController.Update(KeyPoints[0]);
                //keyPointName = KeyPoints[0].Name;


                AvailableAppointmentDataGrid.IsHitTestVisible = false;
                KeyPointCheckButton.IsEnabled = true;
            }

            TourStartButton.IsEnabled = false;
            KeyPointCheckButton.IsEnabled = true;

            RefreshKeyPoints();
        }

        private void KeyPointCheckButton_Click(object sender, RoutedEventArgs e)
        {
            bool isLastKeyPoint = KeyPointsDataGrid.SelectedIndex == KeyPointsDataGrid.Items.Count - 1 && KeyPoints[KeyPointsDataGrid.SelectedIndex - 1].Selected == true;
            
            if (SelectedKeyPoint == KeyPoints[0] || SelectedKeyPoint.Selected == true)
            {
                MessageBox.Show("Already selected!");
                //keyPointName = KeyPoints[0].Name;
            }
            else if (isLastKeyPoint)
            {
                SelectedKeyPoint.Selected = true;
                keyPointsController.Update(SelectedKeyPoint);
                //keyPointName = SelectedKeyPoint.Name;

                SelectedAppointment.Ended = true;
                SelectedAppointment.CheckedKeyPointId = SelectedKeyPoint.Id;
                appointmentController.Update(SelectedAppointment);
                
                MessageBox.Show("Tour ended!");
                SendRequestButton.IsEnabled = false;
                
                TourStartButton.IsEnabled = true;
                KeyPointCheckButton.IsEnabled = false;
                AvailableAppointmentDataGrid.IsHitTestVisible = true;
            }
            else if (KeyPoints[KeyPointsDataGrid.SelectedIndex - 1].Selected == true)
            {
                SelectedAppointment.CheckedKeyPointId = SelectedKeyPoint.Id;
                appointmentController.Update(SelectedAppointment);

                SelectedKeyPoint.Selected = true;
                keyPointsController.Update(SelectedKeyPoint);
                //keyPointName = SelectedKeyPoint.Name;
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
            List<KeyPoint> keyPoints = keyPointsController.getByTourId(SelectedAppointment.TourId);
            foreach (KeyPoint keyPoint in keyPoints)
            {
                KeyPoints.Add(keyPoint);
            }
        }

        private void AppointmentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedAppointment != null)
            {
                foreach (int guest in tourAttendanceController.FindAllGuestsByAppointment(SelectedAppointment.Id))
                {
                    UserController userController = new UserController();
                    GuideGuest.Add(new GuideGuest(guest, userController.getById(guest).Username));
                }
            }
            RefreshKeyPoints();
        }
        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = MessageBox.Show("Are you sure you want to end?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if(result)
            {
                SelectedAppointment.Ended = true;
                appointmentController.Update(SelectedAppointment);

                AvailableAppointmentDataGrid.IsHitTestVisible = true;
                TourStartButton.IsEnabled = true;
                KeyPointCheckButton.IsEnabled = false;
            }
        }

        private void SendRequestButton_Click1(object sender, RoutedEventArgs e)
        {
            //answer = false;
            foreach (int guestId in tourAttendanceController.FindAllGuestsByAppointment(SelectedAppointment.Id))
            {
                if(guestId == SelectedGuest.Id)
                {
                    Notification notification = new Notification(0, guestId, "Join tour " + SelectedAppointment.Tour.Name + "!", false);
                    notificationController.Send(notification);
                }
            }
            MessageBox.Show("Sent!");
            SendRequestButton.IsEnabled = false;
        }

        private void GuestsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SendRequestButton.IsEnabled = true;
        }
    }
}
