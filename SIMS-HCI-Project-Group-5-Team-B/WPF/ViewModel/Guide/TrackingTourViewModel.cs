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
        private TemporaryTourAttendanceController tourAttendanceController;
        private UserService userService;

        public ObservableCollection<Appointment> AvailableAppointments { get; set; }
        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public ObservableCollection<SIMS_HCI_Project_Group_5_Team_B.Domain.Models.GuideGuest> GuideGuests { get; set; }
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
                    LoadGuests();
                }
            }
        }
        private bool isEnabledEndButton;
        public bool IsEnabledEndButton
        {
            get
            {
                return isEnabledEndButton;
            }
            set
            {
                if (isEnabledEndButton != value)
                {
                    isEnabledEndButton = value;
                    OnPropertyChanged(nameof(IsEnabledEndButton));
                }
            }
        }
        private bool isEnabledStartButton;
        public bool IsEnabledStartButton
        {
            get
            {
                return isEnabledStartButton;
            }
            set
            {
                if (isEnabledStartButton != value)
                {
                    isEnabledStartButton = value;
                    OnPropertyChanged(nameof(IsEnabledStartButton));
                }
            }
        }
        private bool isHitTestVisibleDataGrid;
        public bool IsHitTestVisibleDataGrid
        {
            get
            {
                return isHitTestVisibleDataGrid;
            }
            set
            {
                if (isHitTestVisibleDataGrid != value)
                {
                    isHitTestVisibleDataGrid = value;
                    OnPropertyChanged(nameof(IsHitTestVisibleDataGrid));
                }
            }
        }
        private bool isOpenedPopup;
        public bool IsOpenedPopup
        {
            get
            {
                return isOpenedPopup;
            }
            set
            {
                if (isOpenedPopup != value)
                {
                    isOpenedPopup = value;
                    OnPropertyChanged(nameof(IsOpenedPopup));
                }
            }
        }
        public KeyPoint SelectedKeyPoint { get; set; }
        public SIMS_HCI_Project_Group_5_Team_B.Domain.Models.GuideGuest SelectedGuest { get; set; }
        public RelayCommand StartTourCommand { get; set; }
        public RelayCommand EndTourCommand { get; set; }
        public RelayCommand CheckKeyPointCommand { get; set; }
        public RelayCommand OpenPopupCommand { get; set; }
        public int userId;

        #region actions
        private bool CanExecute_NavigateCommand()
        {
            return true;
        }
        private void Execute_OpenPopupCommand()
        {
            IsOpenedPopup = !IsOpenedPopup;
        }
        private void Execute_StartTourCommand()
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

        private void Execute_EndTourCommand()
        {
            bool result = false;
            if(SelectedAppointment.Ended)
            {
                MessageBox.Show("Tour already ended!");
                return;
            }
            if (SelectedAppointment.Started == true)
                result = MessageBox.Show("Are you sure you want to end?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            else
                MessageBox.Show("You must start appointment first!");
            if (result)
            {
                SelectedAppointment.Ended = true;
                appointmentService.Update(SelectedAppointment);
            }
        }

        private void Execute_CheckKeyPointCommand()
        {
            List<KeyPoint> kp = appointmentService.getById(SelectedAppointment.Id).Tour.KeyPoints;
            int targetId = SelectedKeyPoint.Id - 1;
            KeyPoint previousKeyPoint = keyPointsController.GetById(targetId);
            if (SelectedAppointment == null || SelectedAppointment.Started == false)
            {
                MessageBox.Show("Must start tour first");
                return;
            }
            if (SelectedKeyPoint == KeyPoints[0] || SelectedKeyPoint.Selected == true)
            {
                MessageBox.Show("Already selected!");
                return;
                //keyPointName = KeyPoints[0].Name;
            }
            //else if (isLastKeyPoint)
            else if (previousKeyPoint.Selected != true)
            {
                MessageBox.Show("Can't select this key point before previous is selected!");
                return;
                
            }
            else if(SelectedKeyPoint.Id == kp.Last().Id)
            {
                SelectedKeyPoint.Selected = true;
                keyPointsController.Update(SelectedKeyPoint);
                //keyPointName = SelectedKeyPoint.Name;

                SelectedAppointment.Ended = true;
                SelectedAppointment.CheckedKeyPointId = SelectedKeyPoint.Id;
                appointmentService.Update(SelectedAppointment);

                MessageBox.Show("Tour ended!");
                return;
            }   
            else
            {
                SelectedAppointment.CheckedKeyPointId = SelectedKeyPoint.Id;
                appointmentService.Update(SelectedAppointment);

                SelectedKeyPoint.Selected = true;
                keyPointsController.Update(SelectedKeyPoint);
                //keyPointName = SelectedKeyPoint.Name;
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
        #endregion

        public TrackingTourViewModel() 
        {
            this.keyPointsController = new KeyPointsController();
            this.tourAttendanceService = new TourAttendanceService();
            this.appointmentService = new AppointmentService();
            notificationController = new NotificationController();
            tourAttendanceController = new TemporaryTourAttendanceController();
            userService = new UserService();

            AvailableAppointments = new ObservableCollection<Appointment>(appointmentService.GetAllAvaillable(8));
            KeyPoints = new ObservableCollection<KeyPoint>();
            GuideGuests = new ObservableCollection<SIMS_HCI_Project_Group_5_Team_B.Domain.Models.GuideGuest>();

            this.StartTourCommand = new RelayCommand(Execute_StartTourCommand, CanExecute_NavigateCommand);
            this.EndTourCommand = new RelayCommand(Execute_EndTourCommand, CanExecute_NavigateCommand);
            this.CheckKeyPointCommand = new RelayCommand(Execute_CheckKeyPointCommand, CanExecute_NavigateCommand);
            this.OpenPopupCommand = new RelayCommand(Execute_OpenPopupCommand, CanExecute_NavigateCommand);

            //SelectedAppointment = appointmentService.StartedAppointment(userService.getLogged().Id);
            //if(SelectedAppointment != null)
            //{
            //    IsEnabledStartButton = false;
            //    IsEnabledEndButton = true;
            //    IsHitTestVisibleDataGrid = false;
            //}
            //else
            //{
            IsEnabledStartButton = true;
            IsEnabledEndButton = true;
            IsHitTestVisibleDataGrid = true;
            IsOpenedPopup = false;
            //}
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

        private void LoadGuests()
        {
            if (SelectedAppointment != null)
            {
                GuideGuests.Clear();
                foreach (int guest in tourAttendanceController.FindAllGuestsByAppointment(SelectedAppointment.Id))
                {
                    UserController userController = new UserController();
                    GuideGuests.Add(new SIMS_HCI_Project_Group_5_Team_B.Domain.Models.GuideGuest(guest, userController.getById(guest).Username));
                }
            }
            RefreshKeyPoints();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
