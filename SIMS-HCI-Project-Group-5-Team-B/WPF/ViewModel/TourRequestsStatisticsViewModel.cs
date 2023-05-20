using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class TourRequestsStatisticsViewModel : ViewModel
    {
        #region fields
        public ObservableCollection<string> DisplayTypes { get; set; }
        public ObservableCollection<string> TypesForCreating { get; set; }
        public string DisplayType { get; set; }
        public string Type { get; set; }
        public string State { get; set; }
        public List<string> States { get; set; }
        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                    RefreshData(DisplayType);
                }
            }
        }
        public Location Location { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public string Language { get; set; }
        public List<string> Languages { get; set; }
        public ObservableCollection<YearStatistics> RequestsByYears { get; set; }
        public ObservableCollection<MonthStatistic> RequestsByMonths { get; set; }
        private TourRequestsStatisticsService tourRequestsStatisticsService;
        private LocationService locationService;
        private TourRequestService tourRequestService;
        private bool yearDataGridVisibility;
        public bool YearDataGridVisibility
        {
            get { return yearDataGridVisibility; }
            set
            {
                if (yearDataGridVisibility != value)
                {
                    yearDataGridVisibility = value;
                    OnPropertyChanged(nameof(YearDataGridVisibility));
                }
            }
        }
        private bool monthDataGridVisibility;
        public bool MonthDataGridVisibility
        {
            get { return monthDataGridVisibility; }
            set
            {
                if (monthDataGridVisibility != value)
                {
                    monthDataGridVisibility = value;
                    OnPropertyChanged(nameof(MonthDataGridVisibility));
                }
            }
        }
        public RelayCommand ChangeVisibilityCommand { get; set; }
        public RelayCommand RefreshStatisticsCommand { get; set; }
        public RelayCommand LoadCitiesCommand { get; set; }
        public RelayCommand CreateByStatisticsCommand { get; set; }

        public Frame frame;
        #endregion

        #region actions
        private bool CanExecute_Command()
        {
            return true;
        }

        private void Execute_ChangeVisibilityCommand()
        {
            if (DisplayType.Equals("Years"))
            {
                YearDataGridVisibility = true;
                MonthDataGridVisibility = false;
                RefreshData(DisplayType);
            }
            else
            {
                MonthDataGridVisibility = true;
                YearDataGridVisibility = false;
                RefreshData(DisplayType);
            }
        }

        private void Execute_RefreshStatisticsCommand()
        {
            RefreshData(DisplayType);
        }
        private void Execute_LoadCitiesCommand()
        {
            Cities.Clear();
            foreach (var item in locationService.GetCityByState(State))
            {
                Cities.Add(item);
            }
        }
        private void Execute_CreateByStatisticsCommand()
        {
            if(Type.Equals("Language"))
            {
                Language = tourRequestsStatisticsService.GetMostUsedLanguage();
                Page createTour = new CreateTourPage(Language, -1);
                this.frame.NavigationService.Navigate(createTour);
            }
            else
            {
                Location = locationService.getById(tourRequestsStatisticsService.GetMostUsedLocationId());
                Page createTour = new CreateTourPage(null, Location.Id);
                this.frame.NavigationService.Navigate(createTour);
            }
        }
        #endregion
        public TourRequestsStatisticsViewModel(Frame frame)
        {
            tourRequestsStatisticsService = new TourRequestsStatisticsService();
            locationService = new LocationService();
            tourRequestService = new TourRequestService();

            DisplayTypes = new ObservableCollection<string>();
            RequestsByMonths = new ObservableCollection<MonthStatistic>();
            RequestsByYears = new ObservableCollection<YearStatistics>();
            Cities = new ObservableCollection<string>();
            TypesForCreating = new ObservableCollection<string>();

            foreach (var item in tourRequestsStatisticsService.GetAllYears())
            {
                DisplayTypes.Add(item);
            }
            DisplayTypes.Add("Years");
            TypesForCreating.Add("Language");
            TypesForCreating.Add("Location");

            this.ChangeVisibilityCommand = new RelayCommand(Execute_ChangeVisibilityCommand, CanExecute_Command);
            this.RefreshStatisticsCommand = new RelayCommand(Execute_RefreshStatisticsCommand, CanExecute_Command);
            this.LoadCitiesCommand = new RelayCommand(Execute_LoadCitiesCommand, CanExecute_Command);
            this.CreateByStatisticsCommand = new RelayCommand(Execute_CreateByStatisticsCommand, CanExecute_Command);
            
            MonthDataGridVisibility = false;
            YearDataGridVisibility = true;

            States = locationService.GetStates();
            Languages = tourRequestService.GetLanguagesFromRequests();
            DisplayType = "Years";
            RefreshData(DisplayType);

            this.frame = frame;
        }

        private void RefreshData(string DisplayType)
        {
            RequestsByMonths.Clear();
            RequestsByYears.Clear();
            foreach (var item in tourRequestsStatisticsService.GetStatisticsByMonths(DisplayType, State, City, Language))
            {
                RequestsByMonths.Add(item);
            }
            foreach (var item in tourRequestsStatisticsService.GetStatisticsByYear(State, City, Language))
            {
                RequestsByYears.Add(item);
            }
        }
    }
}
