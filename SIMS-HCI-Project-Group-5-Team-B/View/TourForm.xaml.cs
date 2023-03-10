using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class TourForm : Window, IDataErrorInfo, INotifyPropertyChanged
    {
        Repository<Tour> tourRepository;
        Repository<Location> locationRepository;
        Repository<KeyPoints> keyPointsRepository;

        public Tour tour { get; set; }
        public List<Tour> tours;
        public Location location { get; set; }
        public List<KeyPoints> keyPoints;

        public string locationString;

        public string LocationString
        {
            get { return locationString; }
            set
            {
                if (locationString != value)
                {
                    locationString = value;
                    OnPropertyChanged();

                }

            }
        }
        public string startString;
        public string StartString
        {
            get { return startString; }
            set
            {
                if (startString != value)
                {
                    startString = value;
                    OnPropertyChanged();

                }

            }
        }
        public TourForm()
        {
            tour = new Tour();
            locationString = "";
            keyPoints = new List<KeyPoints>();
            InitializeComponent();
            this.DataContext = this;
            tourRepository = new Repository<Tour>();
            locationRepository = new Repository<Location>();
            keyPointsRepository = new Repository<KeyPoints>();
            tours = tourRepository.GetAll();
            
        }

        private void CreateTourClick(object sender, RoutedEventArgs e)
        {
            if (tour.IsValid && this.IsValid && keyPoints.Count() >= 2 && tour.starts.Count() > 0)
            {
                int count = 2;
                String delimiter = ",";
                string[] locationValues = locationString.Split(delimiter, count);
                location = new Location(locationValues[0], locationValues[1]);

                List<Location> savedLocations = locationRepository.GetAll();

                

                if (savedLocations.Count() != 0)
                {
                    for (int i = 0; i < savedLocations.Count(); i++)
                    {
                        if (savedLocations[i].City == location.City && savedLocations[i].State == location.State)
                        {
                            //location already exists in data
                            keyPointsRepository.SaveAll(keyPoints);
                            tour.LocationId = savedLocations[i].Id;
                            tour.KeyPointIds = tour.CreateKeyPointIds(keyPoints);
                            tourRepository.Save(tour);
                            break;
                        }
                        else if (i == savedLocations.Count() - 1)
                        {
                            locationRepository.Save(location);
                            keyPointsRepository.SaveAll(keyPoints);
                            tour.LocationId = location.Id;
                            tour.KeyPointIds = tour.CreateKeyPointIds(keyPoints);
                            tourRepository.Save(tour);
                        }
                    }
                }
                else
                {
                    locationRepository.Save(location);
                    tour.LocationId = location.Id;
                    tourRepository.Save(tour);
                }
                /*locationRepository.Save(location);
                accommodation.LocationId = location.Id;
                accommodationRepository.Save(accommodation);*/
                Close();
            }
            else
            {
                MessageBox.Show("Tour can't be created, because fields are not valid");
            }
        }

        private void CancelTourClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        Regex locationRegex = new Regex("[A-Za-z\\s]+,[A-Za-z]+");
        Regex startRegex = new Regex("[0-9]{1,2}\\/[0-9]{1,2}\\/[0-9]{4} [1-9]{1,2}\\:[0-9]{2}\\:[0-9]{2}");
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "LocationString")
                {
                    if (string.IsNullOrEmpty(LocationString))
                        return "Filed must be filled";

                    Match match = locationRegex.Match(LocationString);
                    if (!match.Success)
                        return "Location needs to be in format: city, state";
                }
                else if (columnName == "StartString")
                {
                    if (string.IsNullOrEmpty(StartString))
                        return "Filed must be filled";

                    Match match = startRegex.Match(StartString);
                    if (!match.Success)
                        return "Start needs to be in format: MM/dd/yyyy HH:mm:ss";
                }
                return null;
            }

        }
        private readonly string[] _validatedProperties = { "LocationString", "StartString" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        private void AddKeyPointsButton_Click(object sender, RoutedEventArgs e)
        {
            if (KeyPointTextBox.Text.Equals(""))
                MessageBox.Show("You should fill the field!");
            else
            {
                if(keyPoints.Count == 0)
                    keyPoints.Add(new KeyPoints(KeyPointTextBox.Text, false, 0,tours.Count()+1));
                else
                    keyPoints.Add(new KeyPoints(KeyPointTextBox.Text, false, keyPoints.Count(),tours.Count()+1));
                KeyPointTextBox.Clear();
                KeyPointsLabel.Content = "Added " + keyPoints.Count().ToString();
            }
        }

        private void AddStartButton_Click(object sender, RoutedEventArgs e)
        {
            CultureInfo provider = new CultureInfo("en-US");
            DateTime test = DateTime.ParseExact(StartTextBox.Text, "MM/dd/yyyy HH:mm:ss", provider);
            if (test <= DateTime.Now)
            {
                MessageBox.Show("You must add date and time that is after currently!");
            }else
            {
                tour.starts.Add(StartTextBox.Text);
                DateLabel.Content = "Added " + tour.starts.Count;
            }
        }
    }
}
