using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Converters;
using Xceed.Wpf.Toolkit.Primitives;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest
{
    public class TourSearchPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GuideGuestTourDTO> Tours { get; set; }


        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> Cities { get; set; }


        private string selectedState = string.Empty;
        public string SelectedState
        {
            get => selectedState;
            set
            {
                selectedState = value;
                Cities.Clear();
                foreach (var city in locationService.GetCityByState(selectedState))
                {
                    Cities.Add(city);
                }
                OnPropertyChanged();
            }
        }
        private string selectedCity = string.Empty;
        public string SelectedCity
        {
            get => selectedCity;
            set
            {
                if(selectedCity != value)
                {
                    selectedCity = value;
                    OnPropertyChanged();
                }
            }
        }
        private int numberOfGuests;
        public int NumberOfGuests
        {
            get => numberOfGuests;
            set
            {
                if(numberOfGuests != value)
                {
                    numberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        private string language = string.Empty;
        public string Language
        {
            get => language;
            set
            {
                if(language != value)
                {
                    language = value;
                    OnPropertyChanged();
                }
            }
        }
        private int duration;
        public int Duration
        {
            get => duration;
            set
            {
                if(duration != value)
                {
                    duration = value;
                    OnPropertyChanged();
                }
            }
        }
        private GuideGuestTourDTO clickedTour;
        public GuideGuestTourDTO ClickedTour
        {
            get => clickedTour;
            set
            {
                if(clickedTour != value)
                {
                    clickedTour = value;
                    OnPropertyChanged();
                }
            }
        }


        public ICommand SearchCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand TourClickedCommand { get; }


        private LocationService locationService;
        private TourService tourService;

        public TourSearchPageViewModel()
        {
            locationService = new LocationService();
            tourService = new TourService();

            Tours = new ObservableCollection<GuideGuestTourDTO>(tourService.GuideGuestGetAll());

            States = new ObservableCollection<string>(locationService.GetStates());
            Cities = new ObservableCollection<string>();

            SearchCommand = new RelayCommand(Search_Execute, CanSearch);
            ResetCommand = new RelayCommand(Reset_Execute);
            TourClickedCommand = new RelayCommandWithParams(TourClicked_Execute);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        //Commands
        public bool CanSearch()
        {
            return NumberOfGuests > 0 && Duration >= 0;
        }
        public void Search_Execute()
        {
            Tours.Clear();
            foreach(var tour in tourService.GuideGuestSearch(new Location(SelectedCity, SelectedState), Language, Duration, NumberOfGuests)){
                Tours.Add(tour);
            }
        }
        public void Reset_Execute()
        {
            NumberOfGuests = 0;
            Duration = 0;
            Language = string.Empty;
            SelectedCity = string.Empty;
            SelectedState = null;//Clears the combobox
            SelectedState = string.Empty;//Enables to search without setting

            Tours.Clear();
            foreach(var tour in tourService.GuideGuestGetAll())
            {
                Tours.Add(tour);
            }
        }
        private void TourClicked_Execute(object obj)
        {
            ClickedTour = (obj as GuideGuestTourDTO);
        }
    }
}
