using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class TrackingTourViewModel : INotifyPropertyChanged
    {
        private KeyPointsController keyPointsController;
        private AppointmentService appointmentService;
        private NotificationController notificationController;
        private TemporaryTourAttendanceController tourAttendanceController;

        public ObservableCollection<Appointment> AvailableAppointments { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public ObservableCollection<GuideGuest> GuideGuests { get; set; }
        public Appointment SelectedAppointment { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        public GuideGuest SelectedGuest { get; set; }
        public RelayCommand StartTourCommand { get; set; }
        //public RelayCommand StartTourCommand { get; set; }
        public int userId;

        #region actions
        private void Execute_StartTourCommand(object obj)
        {
            if (SelectedAppointment.Ended)
            {
                MessageBox.Show("Tour is ended!");
                return;
            }
            bool result = MessageBox.Show("Are you sure you want to start?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (result)
            {
                SelectedAppointment.Started = true;
                SelectedAppointment.CheckedKeyPointId = KeyPoints[0].Id;
                appointmentService.Update(SelectedAppointment);

                KeyPoints[0].Selected = true;
                keyPointsController.Update(KeyPoints[0]);
                //keyPointName = KeyPoints[0].Name;


                //AvailableAppointmentDataGrid.IsHitTestVisible = false;
                //KeyPointCheckButton.IsEnabled = true;
            }

            //TourStartButton.IsEnabled = false;
            //KeyPointCheckButton.IsEnabled = true;

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
        #endregion

        public TrackingTourViewModel() 
        {
            //this.userId = userId;
            //keyPointsController = new KeyPointsController();
            //this.appointmentService = appointmentService;
            //notificationController = new NotificationController();
            //tourAttendanceController = new TemporaryTourAttendanceController();

            ////AvailableAppointments = new ObservableCollection<Appointment>(appointmentService.GetAllAvaillable(userId));
            //KeyPoints = new ObservableCollection<KeyPoint>();
            //GuideGuests = new ObservableCollection<GuideGuest>();

            CheckStarted();
        }

        private void CheckStarted()
        {
            //foreach (Appointment appointment in appointmentService.GetAll())
            //{
            //    if (appointment.Started == true && appointment.Ended != true && userId == appointment.GuideId)
            //    {
            //        SelectedAppointment = appointment;
            //        //TourStartButton.IsEnabled = false;
            //        //KeyPointCheckButton.IsEnabled = true;
            //        //AvailableAppointmentDataGrid.IsHitTestVisible = false;
            //        //SendRequestButton.IsEnabled = true;
            //        break;
            //    }
            //}
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
    }
}
