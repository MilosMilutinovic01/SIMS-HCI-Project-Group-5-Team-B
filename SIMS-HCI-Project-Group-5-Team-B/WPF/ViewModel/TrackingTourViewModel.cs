using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
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
        private TourAttendanceService tourAttendanceService;

        public ObservableCollection<Appointment> AvailableAppointments { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public ObservableCollection<GuideGuest> GuideGuests { get; set; }
        private Appointment selectedAppointment;
        public Appointment SelectedAppointment 
        {
            get
            {
                return selectedAppointment;
            }
            set
            {
                if (selectedAppointment != value)
                {
                    selectedAppointment = value;
                    OnPropertyChanged(nameof(SelectedAppointment));
                    RefreshKeyPoints();
                }
            }
        }
        public KeyPoint SelectedKeyPoint { get; set; }
        public GuideGuest SelectedGuest { get; set; }
        public RelayCommand StartTourCommand { get; set; }
        public RelayCommand EndTourCommand { get; set; }
        public int userId;

        #region actions
        private bool CanExecute_NavigateCommand(object obj)
        {
            return true;
        }

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

                RefreshKeyPoints();
            }
        }

        private void Execute_EndTourCommand(object obj)
        {
            bool result = MessageBox.Show("Are you sure you want to end?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (result)
            {
                SelectedAppointment.Ended = true;
                appointmentService.Update(SelectedAppointment);
            }
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
            KeyPointCSVRepository keyPointCSVRepository = new KeyPointCSVRepository();
            LocationCSVRepository locationCSVRepository = new LocationCSVRepository();
            TourCSVRepository tourCSVRepository = new TourCSVRepository(keyPointCSVRepository, locationCSVRepository);
            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            TourGradeCSVRepository tourGradeCSVRepository = new TourGradeCSVRepository();
            AppointmentCSVRepository appointmentCSVRepository = new AppointmentCSVRepository(tourCSVRepository);

            this.keyPointsController = new KeyPointsController();
            this.tourAttendanceService = new TourAttendanceService(tourAttendanceCSVRepository);
            this.appointmentService = new AppointmentService(appointmentCSVRepository, tourAttendanceService);
            notificationController = new NotificationController();

            AvailableAppointments = new ObservableCollection<Appointment>(appointmentService.GetAllAvaillable(8));
            KeyPoints = new ObservableCollection<KeyPoint>();
            GuideGuests = new ObservableCollection<GuideGuest>();

            this.StartTourCommand = new RelayCommand(Execute_StartTourCommand, CanExecute_NavigateCommand);
            this.EndTourCommand = new RelayCommand(Execute_EndTourCommand, CanExecute_NavigateCommand);

            CheckStarted();
        }

        private void CheckStarted()
        {
            foreach (Appointment appointment in appointmentService.GetAll())
            {
                if (appointment.Started == true && appointment.Ended != true && userId == appointment.GuideId)
                {
                    SelectedAppointment = appointment;
                    //TourStartButton.IsEnabled = false;
                    //KeyPointCheckButton.IsEnabled = true;
                    //AvailableAppointmentDataGrid.IsHitTestVisible = false;
                    //SendRequestButton.IsEnabled = true;
                    break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
