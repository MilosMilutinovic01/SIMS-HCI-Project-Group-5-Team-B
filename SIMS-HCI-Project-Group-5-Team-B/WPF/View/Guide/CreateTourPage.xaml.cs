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
using System.IO;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Effects;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for CreateTourPage.xaml
    /// </summary>
    public partial class CreateTourPage : Page, INotifyPropertyChanged
    {
        //public CreateTourViewModel createTourViewModel { get; set; }
        //public CreateTourPage()
        //{
        //    InitializeComponent();
        //    createTourViewModel = new CreateTourViewModel();
        //    this.DataContext = createTourViewModel;
        //}
        private TourService tourService;
        private LocationService locationService1;
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
        public RelayCommand OpenPopupCommand { get; set; }
        private bool CanExecute_NavigateCommand()
        {
            return true;
        }
        private void Execute_OpenPopupCommand()
        {
            IsOpenedPopup = !IsOpenedPopup;
        }

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
            this.tourService = new TourService();// new TourController(locationService);
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
            this.OpenPopupCommand = new RelayCommand(Execute_OpenPopupCommand, CanExecute_NavigateCommand);
            IsOpenedPopup = false;
        }

        public CreateTourPage(string flag, TourRequest tourRequest)
        {
            InitializeComponent();
            this.DataContext = this;

            locationService = new LocationController();
            this.tourService = new TourService();// new TourController(locationService);
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
            StartsListBox.Items.Add(DateTime.ToString());
            DateTime = tourRequest.SelectedDate;
            starts.Clear();
            starts.Add(DateTime);
            StartDatePicker.Value = tourRequest.SelectedDate;
            MaxGuests = tourRequest.MaxGuests;
            Tour.MaxGuests = tourRequest.MaxGuests;
            this.flag = flag;
            this.TourRequest = tourRequest;
            this.Description = tourRequest.Description;
            Tour.Description = tourRequest.Description;
            this.OpenPopupCommand = new RelayCommand(Execute_OpenPopupCommand, CanExecute_NavigateCommand);
            IsOpenedPopup = false;
        }

        public CreateTourPage(string language, int locationId)
        {
            InitializeComponent();
            this.DataContext = this;

            locationService = new LocationController();
            locationService1 = new LocationService();
            this.tourService = new TourService();// new TourController(locationService);
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
                
            Tour.Language = language;
            Tour.Location = locationService1.getById(locationId);
            flag = "";
            this.OpenPopupCommand = new RelayCommand(Execute_OpenPopupCommand, CanExecute_NavigateCommand);
            IsOpenedPopup = false;
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
                    appointments.Add(new Appointment(Tour.Id, 8, start, Tour.MaxGuests));
                }
                appointmentService.SaveAll(appointments);
                if (flag.Equals("request"))
                    tourRequestService.AcceptRequest(TourRequest, Tour.Id);
                else
                {
                    if (!ComboBoxCities.IsEnabled)
                    {
                        tourRequestService.TourCreatedFromLocatinoStatistics(Tour.Id);
                    }
                    else if(!LanguageTextBox.IsEnabled)
                    {
                        tourRequestService.TourCreatedFromLanguageStatistics(Tour.Id);
                    }
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
            Window window = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (window != null)
            {
                window.Effect = new BlurEffect();
            }
            PreviewImageWindow previewImageWindow = new PreviewImageWindow("../../../Resources/TourImages/" + SelectedImage);
            previewImageWindow.ShowDialog();
            window.Effect = null;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HelpTextBox.Text = "Welcome to the Tour Creation Page!\r\n" +
                "This page allows you to create a new tour by providing various details. " +
                "Here's a step-by-step guide on how to use each element on this page:\r\n1. " +
                "Tour Name: Enter the name of your tour in the provided TextBox. " +
                "Choose a descriptive name that represents the tour's theme or destination.\r\n2. " +
                "Location: Select the state and city of the tour using the two ComboBoxes. " +
                "Choose the appropriate options from the dropdown lists.\r\n3. " +
                "Language: Enter the language spoken on the tour in the TextBox provided. " +
                "Specify the primary language used during the tour.\r\n4. " +
                "Maximum Guests: Use the slider to set the maximum number of guests for the tour. " +
                "Adjust the slider's position according to your desired limit.\r\n5. " +
                "Key Points: Enter descriptions or highlights of the tour's key points in the TextBox. " +
                "Click the \"Add Key Point\" button to add the entered text as a key point. " +
                "Remember, you must have a minimum of two key points for the tour.\r\n6. " +
                "Start Date: Select the start date of the tour using the DateTimePicker control. " +
                "Choose the desired date by clicking on the control and selecting from the calendar.\r\n7." +
                " Duration: Use the slider to specify the duration of the tour. " +
                "Adjust the slider to set the number of days or hours for the tour's duration.\r\n8. " +
                "Image: Click the image icon to add an image for the tour. " +
                "Select an image file from your file system when prompted. " +
                "You can also preview and delete added images using the provided buttons.\r\n9. " +
                "Description: Enter a detailed description of the tour in the TextBox. " +
                "Provide information about the tour's itinerary, highlights, and any additional relevant details.\r\n10." +
                " Create Tour: Click the \"Create Tour\" button to finalize the tour creation process. " +
                "Ensure that you have entered all the required information before creating the tour.\r\n" +
                "If you encounter any issues or require further assistance, please consult the help documentation or contact our support team.\r\nHappy tour creation!";
            if (HelpTextBox.Visibility == Visibility.Hidden)
                HelpTextBox.Visibility = Visibility.Visible;
            else
                HelpTextBox.Visibility = Visibility.Hidden;
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
