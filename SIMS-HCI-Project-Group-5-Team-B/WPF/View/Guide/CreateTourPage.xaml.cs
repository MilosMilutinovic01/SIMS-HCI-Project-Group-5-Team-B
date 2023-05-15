using Microsoft.Win32;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for CreateTourPage.xaml
    /// </summary>
    public partial class CreateTourPage : Page
    {
        //public CreateTourViewModel createTourViewModel { get; set; }
        //public CreateTourPage()
        //{
        //    InitializeComponent();
        //    createTourViewModel = new CreateTourViewModel();
        //    this.DataContext = createTourViewModel;
        //}
        private TourController tourService;
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
        //public List<string> states { get; set; }
        //public List<string> cities;
        public CreateTourPage()
        {
            InitializeComponent();
            this.DataContext = this;

            locationController = new LocationController();
            this.tourService = new TourController(locationController);
            keyPointsController = new KeyPointsController();
            this.appointmentService = appointmentService;

            Tour = new Tour();
            Location = new Location();
            KeyPoint = new KeyPoint();
            Appointment = new Appointment();

            keyPoints = new List<KeyPoint>();
            appointments = new List<Appointment>();
            starts = new List<DateTime>();
            locations = locationController.GetAllAsStrings();
            //states = locationController.GetStates();
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
        }
        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            KeyPoint.TourId = tourService.makeId();
            if (KeyPointTextBox.Text.Equals(""))
                MessageBox.Show("You should fill the field!");
            else
            {
                keyPoints.Add(new KeyPoint(KeyPoint));
                KeyPointsListBox.Items.Add(KeyPoint.Name);
            }
        }
        private void AddStartButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime > DateTime.Now)
            {
                starts.Add(DateTime);
                StartsListBox.Items.Add(DateTime.ToString());
            }
            else
                MessageBox.Show("You must add date and time that is after currently!");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //cities = locationController.GetCityByState(ComboBoxStates.SelectedItem.ToString());
            //ComboBoxCities.ItemsSource = cities;
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName;
                string imageName = System.IO.Path.GetFileName(filename);
                ImagesListBox.Items.Add(imageName);
            }
        }
    }
}
