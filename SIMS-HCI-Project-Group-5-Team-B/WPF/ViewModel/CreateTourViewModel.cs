using Microsoft.Win32;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class CreateTourViewModel : INotifyPropertyChanged
    {
        #region fields
        private TourController tourService;
        private TourAttendanceService tourAttendanceService;
        private LocationController locationController;
        private KeyPointsController keyPointsController;
        private AppointmentService appointmentService;
        public Tour Tour { get; set; }
        public Location Location { get; set; }
        public KeyPoint KeyPoint { get; set; }
        public Appointment Appointment { get; set; }
        public DateTime DateTime { get; set; }

        public List<KeyPoint> keyPoints;
        public List<Appointment> appointments;
        public List<DateTime> starts;
        public List<string> locations { get; set; }
        public List<string> states { get; set; }
        public string SelectedState 
        {
            get
            {
                return SelectedState;
            }
            set
            {
                if (SelectedState != value)
                {
                    SelectedState = value;
                    OnPropertyChanged(nameof(SelectedState));
                    LoadCities(SelectedState);
                }
            }
        }
        public List<string> cities { get; set; }

        public RelayCommand CreateTourCommand { get; set; }
        public RelayCommand AddKeyPointCommand { get; set; }
        public RelayCommand AddStartCommand { get; set; }
        public RelayCommand LoadCitiesCommand { get; set; }
        public RelayCommand AddImageCommand { get; set; }
        #endregion

        #region actions
        private bool CanExecute_Command(object obj)
        {
            return true;
        }

        private void Execute_CreateTourCommand(object obj)
        {
            bool isValid = Tour.IsValid && KeyPoint.IsValid;
            if (keyPoints.Count() < 2)
            {
                MessageBox.Show("Must enter two or more keypoints!");
                return;
            }
            if (starts.Count() == 0)
            {
                MessageBox.Show("Must enter at least one tour start!");
                return;
            }
            if (!isValid)
            {
                MessageBox.Show("Tour can't be created because some fields are not valid");
                return;
            }

            Location existingLocation = locationController.GetLocation(Location);

            if (existingLocation != null)
            {
                Tour.LocationId = existingLocation.Id;
            }
            else
            {
                Tour.LocationId = locationController.makeId();
                locationController.Save(Location);
            }
            foreach (DateTime start in starts)
            {
                keyPointsController.SaveAll(keyPoints);
            }
            Tour.KeyPoints.AddRange(keyPoints);
            tourService.Save(Tour);
            foreach (DateTime start in starts)
            {
                appointments.Add(new Appointment(Tour.Id, -1, start, Tour.MaxGuests));
            }
            appointmentService.SaveAll(appointments);

            MessageBox.Show("Tour created succesfully!");
        }

        //private void Execute_AddKeyPointCommand(object obj)
        //{
        //    KeyPoint.TourId = tourService.makeId();
        //    if (KeyPointTextBox.Text.Equals(""))
        //        MessageBox.Show("You should fill the field!");
        //    else
        //    {
        //        keyPoints.Add(new KeyPoint(KeyPoint));
        //        KeyPointsListBox.Items.Add(KeyPoint.Name);
        //    }
        //}

        //private void Execute_AddStartCommand(object obj)
        //{
        //    if (DateTime > DateTime.Now)
        //    {
        //        starts.Add(DateTime);
        //        StartsListBox.Items.Add(DateTime.ToString());
        //    }
        //    else
        //        MessageBox.Show("You must add date and time that is after currently!");
        //}

        private void LoadCities(String state)
        {
            cities = locationController.GetCityByState(state);
        }

        private void Execute_AddImageCommand(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                string imageName = System.IO.Path.GetFileName(filename);
                //ImagesListBox.Items.Add(imageName);
                Tour.ImageUrls = imageName;
            }
        }
        #endregion

        #region constructors
        public CreateTourViewModel()
        {
            locationController = new LocationController();
            this.tourService = new TourController(locationController);
            keyPointsController = new KeyPointsController();
            KeyPointCSVRepository keyPointCSVRepository = new KeyPointCSVRepository();
            LocationCSVRepository locationCSVRepository = new LocationCSVRepository();
            TourCSVRepository tourCSVRepository = new TourCSVRepository(keyPointCSVRepository, locationCSVRepository);
            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            AppointmentCSVRepository appointmentCSVRepository = new AppointmentCSVRepository(tourCSVRepository);

            this.tourAttendanceService = new TourAttendanceService(tourAttendanceCSVRepository);
            this.appointmentService = new AppointmentService(appointmentCSVRepository, tourAttendanceService);

            this.CreateTourCommand = new RelayCommand(Execute_CreateTourCommand, CanExecute_Command);
            //this.AddKeyPointCommand = new RelayCommand(Execute_AddKeyPointCommand, CanExecute_Command);
            //this.AddStartCommand = new RelayCommand(Execute_AddStartCommand, CanExecute_Command);
            this.AddImageCommand = new RelayCommand(Execute_AddImageCommand, CanExecute_Command);

            Tour = new Tour();
            Location = new Location();
            KeyPoint = new KeyPoint();
            Appointment = new Appointment();

            keyPoints = new List<KeyPoint>();
            appointments = new List<Appointment>();
            starts = new List<DateTime>();
            locations = locationController.GetAllAsStrings();
            states = locationController.GetStates();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
