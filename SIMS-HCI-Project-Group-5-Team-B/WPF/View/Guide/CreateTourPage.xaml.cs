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
        private LocationController locationService;
        private KeyPointsController keyPointsService;
        private AppointmentService appointmentService;
        private TourRequestService tourRequestService;
        public Tour Tour { get; set; }
        public Location Location { get; set; }
        public KeyPoint KeyPoint { get; set; }
        public Appointment Appointment { get; set; }
        public DateTime DateTime { get; set; }
        public string SelectedLanguage { get; set; }
        public int MaxGuests { get; set; }
        public string Description { get; set; }
        public string SelectedImage { get; set; }

        public List<KeyPoint> keyPoints;
        public List<Appointment> appointments;
        public List<DateTime> starts;
        public List<string> locations { get; set; }
        public List<string> states { get; set; }
        public List<string> cities;
        public string flag;
        public string State { get; set; }
        public string City { get; set; }
        public TourRequest TourRequest;
        public string ImageUrlsString;
        public CreateTourPage()
        {
            InitializeComponent();
            this.DataContext = this;

            locationService = new LocationController();
            this.tourService = new TourController(locationService);
            keyPointsService = new KeyPointsController();
            this.appointmentService = new AppointmentService();

            Tour = new Tour();
            Location = new Location();
            KeyPoint = new KeyPoint();
            Appointment = new Appointment();

            keyPoints = new List<KeyPoint>();
            appointments = new List<Appointment>();
            starts = new List<DateTime>();
            locations = locationService.GetAllAsStrings();
            states = locationService.GetStates();
        }

        public CreateTourPage(string flag, TourRequest tourRequest)
        {
            InitializeComponent();
            this.DataContext = this;

            locationService = new LocationController();
            this.tourService = new TourController(locationService);
            keyPointsService = new KeyPointsController();
            this.appointmentService = new AppointmentService();
            this.tourRequestService = new TourRequestService();

            Tour = new Tour();
            Location = new Location();
            KeyPoint = new KeyPoint();
            Appointment = new Appointment();

            keyPoints = new List<KeyPoint>();
            appointments = new List<Appointment>();
            starts = new List<DateTime>();
            locations = locationService.GetAllAsStrings();
            states = locationService.GetStates();

            ComboBoxCities.IsEnabled = false;
            ComboBoxStates.IsEnabled = false;
            LanguageTextBox.IsEnabled = false;
            slider.IsEnabled = false;
            sliderTextBox.IsEnabled = false;
            DescriptionTextBox.IsEnabled = false;
            StartDatePicker.IsEnabled = false;
            AddStartButton.IsEnabled = false;

            Location.State = tourRequest.Location.State;
            State = tourRequest.Location.City;
            ComboBoxStates.SelectedValue = tourRequest.Location.State;
            cities = locationService.GetCityByState(tourRequest.Location.State);
            ComboBoxCities.ItemsSource = cities;
            ComboBoxCities.SelectedValue = tourRequest.Location.City;
            Location.City = tourRequest.Location.City;
            City = tourRequest.Location.City;
            SelectedLanguage = tourRequest.Language;
            Tour.Language = tourRequest.Language;
            DateTime = tourRequest.SelectedDate;
            StartDatePicker.Value = tourRequest.SelectedDate;
            MaxGuests = tourRequest.MaxGuests;
            Tour.MaxGuests = tourRequest.MaxGuests;
            this.flag = flag;
            this.TourRequest = tourRequest;
            this.Description = tourRequest.Description;
            Tour.Description = tourRequest.Description;
            appointments.Add(new Appointment(Tour.Id, -1, TourRequest.SelectedDate, Tour.MaxGuests));
        }

        public CreateTourPage(string language, int locationId)
        {
            InitializeComponent();
            this.DataContext = this;

            locationService = new LocationController();
            this.tourService = new TourController(locationService);
            keyPointsService = new KeyPointsController();
            this.appointmentService = new AppointmentService();
            this.tourRequestService = new TourRequestService();

            Tour = new Tour();
            Location = new Location();
            KeyPoint = new KeyPoint();
            Appointment = new Appointment();

            keyPoints = new List<KeyPoint>();
            appointments = new List<Appointment>();
            starts = new List<DateTime>();
            locations = locationService.GetAllAsStrings();
            states = locationService.GetStates();

            if(language == default)
            {
                ComboBoxCities.IsEnabled = false;
                ComboBoxStates.IsEnabled = false;
            }
            else
                LanguageTextBox.IsEnabled = false;
            
            if(locationId != -1)
            {
                Location = locationService.getById(locationId);
                ComboBoxStates.SelectedValue = Location.State;
                
                ComboBoxCities.ItemsSource = cities;
                ComboBoxCities.SelectedValue = Location.City;
                City = Location.City;
            }

            SelectedLanguage = language;
            Tour.Language = language;
        }

        private void CreateTourButton_Click(object sender, RoutedEventArgs e)
        {
            try
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

                Location existingLocation = locationService.GetLocation(Location);

                if (existingLocation != null)
                {
                    Tour.LocationId = existingLocation.Id;
                }
                else
                {
                    Tour.LocationId = locationService.makeId();
                    locationService.Save(Location);
                }
                keyPointsService.SaveAll(keyPoints);
                Tour.ImageUrls = ImageUrlsString;
                Tour.KeyPoints.AddRange(keyPoints);
                tourService.Save(Tour);
                foreach (DateTime start in starts)
                {
                    appointments.Add(new Appointment(Tour.Id, -1, start, Tour.MaxGuests));  //here need to go guideId where is -1
                }
                appointmentService.SaveAll(appointments);
                if (flag.Equals("request"))
                    tourRequestService.AcceptRequest(TourRequest, Tour.Id);
                if (!ComboBoxCities.IsEnabled)
                {
                    tourRequestService.TourCreatedFromLocatinoStatistics(Tour.Id);
                }
                else if(!LanguageTextBox.IsEnabled)
                {
                    tourRequestService.TourCreatedFromLanguageStatistics(Tour.Id);
                }
                MessageBox.Show("Tour created successfully!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            cities = locationService.GetCityByState(ComboBoxStates.SelectedItem.ToString());
            ComboBoxCities.ItemsSource = cities;
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string filename = "";
            string imageName = "";
            if (openFileDialog.ShowDialog() == true)
            {
                filename = openFileDialog.FileName;
                imageName = System.IO.Path.GetFileName(filename);
                foreach(var item in ImagesListBox.Items)
                {
                    if (item.Equals(imageName))
                    {
                        MessageBox.Show("Image already added!");
                        return;
                    }
                }
                ImagesListBox.Items.Add(imageName);
                string destinationPath = System.IO.Path.Combine("../../../Resources/TourImages/", imageName);
                if (!File.Exists(destinationPath))
                    File.Copy(filename, destinationPath);
            }
            ImageUrlsString += imageName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PreviewImageWindow previewImageWindow = new PreviewImageWindow("../../../Resources/TourImages/" + SelectedImage);
            previewImageWindow.Show();
        }

        private void ImagesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PreviewButton.IsEnabled = true;
            RemoveButton.IsEnabled = true;
            if(ImagesListBox.Items.IsEmpty)
            {
                PreviewButton.IsEnabled = false;
                RemoveButton.IsEnabled = false;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            ImagesListBox.Items.Remove(SelectedImage);
        }
    }
}
