using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for TourCreateForm.xaml
    /// </summary>
    public partial class TourCreateForm : Window
    {
        private TourService tourController;
        private LocationService locationController;
        private KeyPointsService keyPointsController;
        private AppointmentService appointmentService;
        public Tour Tour { get; set; }
        public Location Location { get; set; }
        public KeyPoint KeyPoint { get; set; }
        public Appointment Appointment { get; set; }
        public DateTime DateTime { get; set; }

        public List<KeyPoint> keyPoints;
        public List<Appointment> appointments;
        public List<DateTime> starts;
        public List<string> states { get; set; }
        public List<string> cities;
        public TourCreateForm()
        {
            InitializeComponent();
            this.DataContext = this;

            locationController = new LocationService();
            tourController = new TourService(locationController);
            keyPointsController = new KeyPointsService();
            appointmentService = new AppointmentService();

            Tour = new Tour();
            Location = new Location();
            KeyPoint = new KeyPoint();
            Appointment = new Appointment();

            keyPoints = new List<KeyPoint>();
            appointments = new List<Appointment>();
            starts = new List<DateTime>();
            states = locationController.GetStates();
        }

        private void CreateTourButton_Click(object sender, RoutedEventArgs e)
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
            if(!isValid)
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
            Tour.KeyPoints.AddRange(keyPoints);
            tourController.Save(Tour);
            foreach (DateTime start in starts)
            {
                appointments.Add(new Appointment(Tour.Id, -1, start, Tour.MaxGuests));
            }
            appointmentService.SaveAll(appointments);
            Close();
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            KeyPoint.TourId = tourController.makeId();
            if (KeyPointTextBox.Text.Equals(""))
                MessageBox.Show("You should fill the field!");
            else
            {
                keyPoints.Add(new KeyPoint(KeyPoint));
            }
        }
        private void AddStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime > DateTime.Now)
            {
                starts.Add(DateTime);
            }
            else
                MessageBox.Show("You must add date and time that is after currently!");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cities = locationController.GetCityByState(ComboBoxStates.SelectedItem.ToString());
            ComboBoxCities.ItemsSource = cities;
        }
    }
}
