using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for TourForm.xaml
    /// </summary>
    public partial class TourForm : Window
    {
        private TourController tourController;
        private LocationController locationController;
        private KeyPointsController keyPointsController;
        private TourImageController imageController;
        private TourStartController tourStartController;
        public Tour Tour { get; set; }
        public Location Location { get; set; }
        public KeyPoints KeyPoint { get; set; }
        public TourImage TourImage { get; set; }
        public TourStart TourStart { get; set; }
        
        public List<KeyPoints> keyPoints;
        public List<TourStart> tourStarts;
        public List<TourImage> images;
        public TourForm()
        {
            Tour = new Tour();
            Location = new Location();
            KeyPoint = new KeyPoints();
            TourImage = new TourImage();
            TourStart = new TourStart();

            keyPoints = new List<KeyPoints>();
            images = new List<TourImage>();
            tourStarts = new List<TourStart>();
            
            locationController = new LocationController();
            tourController = new TourController(locationController);
            keyPointsController = new KeyPointsController();
            imageController = new TourImageController();
            tourStartController = new TourStartController();

            InitializeComponent();
            this.DataContext = this;
        }

        private void CreateTourButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = Tour.IsValid && Location.IsValid && KeyPoint.IsValid && TourImage.IsValid && TourStart.IsValid;
            if (keyPoints.Count() < 2)
            {
                MessageBox.Show("Must enter two or more keypoints!");
                return;
            }
            if (tourStarts.Count() == 0)
            {
                MessageBox.Show("Must enter at least one tour start!");
                return;
            }
            if(images.Count() == 0) 
            {
                MessageBox.Show("Must enter at least one image!");
                return;
            }
            if(!isValid)
            {
                MessageBox.Show("Tour can't be created because some fields are not valid");
                return;
            }

            Location existingLocation = locationController.GetAll().Find(l => l.City == Location.City && l.State == Location.State);

            if (existingLocation != null)
            {
                Tour.LocationId = existingLocation.Id;
            }
            else
            {
                Tour.LocationId = locationController.makeId();
                locationController.Save(Location);
            }

            SaveEntities();
            Close();
        }

        private void SaveEntities()
        {
            keyPointsController.SaveAll(keyPoints);
            imageController.SaveAll(images);
            tourStartController.SaveAll(tourStarts);
            tourController.Save(Tour);
        }

        private void CancelTourButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            int tourId = tourController.makeId();
            if (KeyPointTextBox.Text.Equals(""))
                MessageBox.Show("You should fill the field!");
            else
            {
                keyPoints.Add(new KeyPoints(KeyPointTextBox.Text, false, keyPoints.Count(), tourId));
                KeyPointsLabel.Content = "Added " + keyPoints.Count().ToString();
            }
        }
        private void AddStartButton_Click(object sender, RoutedEventArgs e)
        {
            int tourId = tourController.makeId();
            DateTime date;
            DateTime dateTime = DateTime.Now;
            if (DateTime.TryParse(StartTextBox.Text, out date) && StartDatePicker.SelectedDate != null)
            {
                DateTime selectedDate = (DateTime)StartDatePicker.SelectedDate;
                dateTime = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day,date.Hour, date.Minute, date.Second);
            }

            if (dateTime > DateTime.Now)
            {
                tourStarts.Add(new TourStart(tourId,dateTime));
                DateLabel.Content = "Added " + tourStarts.Count();
            }
            else
                MessageBox.Show("You must add date and time that is after currently!");
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            int tourId = tourController.makeId();
            if (ImageTextBox.Text.Equals(""))
                MessageBox.Show("You should fill the field!");
            else
            {
                images.Add(new TourImage(ImageTextBox.Text, tourId));
                ImageLabel.Content = "Added " + images.Count();
            }
        }
    }
}
