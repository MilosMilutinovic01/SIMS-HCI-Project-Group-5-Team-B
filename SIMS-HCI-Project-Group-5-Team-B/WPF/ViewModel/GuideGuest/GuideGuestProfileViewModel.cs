using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Definitions.Series;
using LiveCharts.Wpf;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToastNotifications.Events;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest
{
    public class GuideGuestProfileViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public ObservableCollection<TourAttendance> TourAttendances { get; set; }


        public ObservableCollection<string> YearsWithTourRequests { get; set; }
        private ObservableCollection<string> languageLabels;
        public ObservableCollection<string> LanguageLabels
        {
            get => languageLabels;
            set
            {
                languageLabels = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<string> locationLabels;
        public ObservableCollection<string> LocationLabels
        {
            get => locationLabels;
            set
            {
                locationLabels = value;
                OnPropertyChanged();
            }
        }
        private SeriesCollection languageSeries;
        public SeriesCollection LanguageSeries
        {
            get => languageSeries;
            set
            {
                languageSeries = value;
                OnPropertyChanged();
            }
        }
        private SeriesCollection locationseries;
        public SeriesCollection LocationSeries
        {
            get => locationseries;
            set
            {
                locationseries = value;
                OnPropertyChanged();
            }
        }
        public Func<string, string> Values { get; set; }



        private string selectedYear;
        public string SelectedYear
        {
            get => selectedYear;
            set
            {
                if(selectedYear != value)
                {
                    selectedYear = value;
                    UpdateChartData();
                    OnPropertyChanged();
                }
            }
        }


        #region RegularTourRequestForm variables
        private TourRequest backupTourRequest;
        private TourRequest selectedTourRequest;
        public TourRequest SelectedTourRequest
        {
            get => selectedTourRequest;
            set
            {
                if (selectedTourRequest != value)
                {
                    selectedTourRequest = value;
                    if(value != null)
                    {
                        RegularTourSelectedState = selectedTourRequest.Location.State;
                        RegularTourSelectedCity = selectedTourRequest.Location.City;
                    }
                    else
                    {
                        RegularTourSelectedState = string.Empty;
                        RegularTourSelectedCity = string.Empty;
                    }
                    OnPropertyChanged();
                }
            }
        }
        private bool showRegularTourRequestForm;
        public bool ShowRegularTourRequestForm
        {
            get => showRegularTourRequestForm;
            set
            {
                if(showRegularTourRequestForm != value)
                {
                    showRegularTourRequestForm = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand EditRegularTourRequestCommand { get; }
        public ICommand AddNewRegularTourRequestCommand { get; }
        public ICommand SaveRegularTourRequestCommand { get; }
        public ICommand CancelRegularTourRequestCommand { get; }
        #endregion
        #region Location variables for regular and special tour requests
        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<string> RegularTourCities { get; set; }
        private string regularTourSelectedState = string.Empty;
        public string RegularTourSelectedState
        {
            get => regularTourSelectedState;
            set
            {
                regularTourSelectedState = value;
                RegularTourCities.Clear();
                foreach (var city in locationService.GetCityByState(regularTourSelectedState))
                {
                    RegularTourCities.Add(city);
                }
                OnPropertyChanged();
            }
        }
        private string regularTourSelectedCity = string.Empty;
        public string RegularTourSelectedCity
        {
            get => regularTourSelectedCity;
            set
            {
                if (regularTourSelectedCity != value)
                {
                    regularTourSelectedCity = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public SIMS_HCI_Project_Group_5_Team_B.Domain.Models.GuideGuest LoggedGuideGuest { get; set; }
        
        
        private TourRequestService tourRequestService;
        private TourRequestStatisticsService tourRequestStatisticsService;
        private GuideGuestService guideGuestService;
        private LocationService locationService;
        public GuideGuestProfileViewModel()
        {
            tourRequestService = new TourRequestService();
            tourRequestStatisticsService = new TourRequestStatisticsService();
            guideGuestService = new GuideGuestService();
            locationService = new LocationService();

            LoggedGuideGuest = guideGuestService.getLoggedGuideGuest();

            Vouchers = new ObservableCollection<Voucher>(new VoucherService().GetAllFor(LoggedGuideGuest.Id));
            TourRequests = new ObservableCollection<TourRequest>(tourRequestService.GetFor(LoggedGuideGuest.Id));
            LoadYearsWithTourRequests();


            States = new ObservableCollection<string>(locationService.GetStates());
            RegularTourCities = new ObservableCollection<string>();


            EditRegularTourRequestCommand = new RelayCommand(EditRegularTourRequest_Execute, CanEditRegularTourRequest);
            AddNewRegularTourRequestCommand = new RelayCommand(AddNewRegularTourRequest_Execute);
            SaveRegularTourRequestCommand = new RelayCommand(SaveRegularTourRequest_Execute);
            CancelRegularTourRequestCommand = new RelayCommand(CancelRegularTourRequest_Execute);

        }

        private void LoadYearsWithTourRequests()
        {
            YearsWithTourRequests = new ObservableCollection<string>();
            foreach(var year in tourRequestStatisticsService.GetYearsWithRequests(LoggedGuideGuest.Id))
            {
                YearsWithTourRequests.Add(year.ToString());
            }
            YearsWithTourRequests.Add("Show all years");
        }
        private void UpdateChartData()
        {
            if (SelectedYear == null) return;
            List<TourRequestLanguageStatistics> languageStatistics;
            List<TourRequestLocationStatistics> locationStatistics;
            if(SelectedYear != "Show all years")
            {
                languageStatistics = tourRequestStatisticsService.CalculateLanguageStatistics(LoggedGuideGuest.Id, int.Parse(SelectedYear));
                locationStatistics = tourRequestStatisticsService.CalculateLocationStatistics(LoggedGuideGuest.Id, int.Parse(SelectedYear));
            }
            else
            {
                languageStatistics = tourRequestStatisticsService.CalculateLanguageStatistics(LoggedGuideGuest.Id);
                locationStatistics = tourRequestStatisticsService.CalculateLocationStatistics(LoggedGuideGuest.Id);
            }

            if (LanguageLabels == null)
            {
                LanguageLabels = new ObservableCollection<string>();
                LocationLabels = new ObservableCollection<string>();
            }
            LanguageLabels.Clear();
            LocationLabels.Clear();

            LanguageSeries = new SeriesCollection{
                new StackedColumnSeries
                {
                    Title = "Accepted requests",
                    Values = new ChartValues<int>()
                }
            };
            LanguageSeries.Add(
                new StackedColumnSeries
                {
                    Title = "Rejected requests",
                    Values = new ChartValues<int>()
                });
            
            LocationSeries = new SeriesCollection{
                new StackedColumnSeries
                {
                    Title = "Accepted requests",
                    Values = new ChartValues<int>()
                }
            };
            LocationSeries.Add(
                new StackedColumnSeries
                {
                    Title = "Rejected requests",
                    Values = new ChartValues<int>()
                });
            Values = value => value.ToString();

            foreach (var stat in languageStatistics)
            {
                LanguageSeries[0].Values.Add(stat.NumberOfAcceptedRequests);
                LanguageSeries[1].Values.Add(stat.NumberOfRejectedRequests);
                LanguageLabels.Add(stat.Language);
            }
            foreach (var stat in locationStatistics)
            {
                LocationSeries[0].Values.Add(stat.NumberOfAcceptedRequests);
                LocationSeries[1].Values.Add(stat.NumberOfRejectedRequests);
                LocationLabels.Add(stat.Location.ToString());
            }
        }

        
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //Commands
        private bool CanEditRegularTourRequest()
        {
            return SelectedTourRequest != null;
        }
        private void EditRegularTourRequest_Execute()
        {
            backupTourRequest = new TourRequest();
            backupTourRequest.LocationId = SelectedTourRequest.LocationId;
            backupTourRequest.Location = new Location();
            backupTourRequest.Location.City = SelectedTourRequest.Location.City;
            backupTourRequest.Location.State = SelectedTourRequest.Location.State;
            backupTourRequest.Description = SelectedTourRequest.Description;
            backupTourRequest.Language = SelectedTourRequest.Language;
            backupTourRequest.MaxGuests = SelectedTourRequest.MaxGuests;
            backupTourRequest.DateRangeStart = SelectedTourRequest.DateRangeStart;
            backupTourRequest.DateRangeEnd = SelectedTourRequest.DateRangeEnd;

            ShowRegularTourRequestForm = true;
        }
        private void AddNewRegularTourRequest_Execute()
        {
            backupTourRequest = null;
            SelectedTourRequest = new TourRequest();
            SelectedTourRequest.AcceptedTourId = -1;
            SelectedTourRequest.SpecialTourId = -1;
            ShowRegularTourRequestForm = true;
        }
        private void CancelRegularTourRequest_Execute()
        {
            if(backupTourRequest != null)
            {
                SelectedTourRequest.LocationId = backupTourRequest.LocationId ;
                SelectedTourRequest.Location.City = backupTourRequest.Location.City;
                SelectedTourRequest.Location.State = backupTourRequest.Location.State;
                SelectedTourRequest.Description = backupTourRequest.Description ;
                SelectedTourRequest.Language = backupTourRequest.Language ;
                SelectedTourRequest.MaxGuests = backupTourRequest.MaxGuests ;
                SelectedTourRequest.DateRangeStart = backupTourRequest.DateRangeStart ;
                SelectedTourRequest.DateRangeEnd = backupTourRequest.DateRangeEnd ;
            }
            ShowRegularTourRequestForm = false;
            backupTourRequest = null;
        }
        private void SaveRegularTourRequest_Execute()
        {
            SelectedTourRequest.LocationId = locationService.GetLocation(RegularTourSelectedState, RegularTourSelectedCity).Id;
            SelectedTourRequest.Location.State = RegularTourSelectedState;
            SelectedTourRequest.Location.City = RegularTourSelectedCity;
            if(backupTourRequest == null)
            {
                tourRequestService.Save(SelectedTourRequest);
                TourRequests.Add(SelectedTourRequest);
            }
            else
            {
                tourRequestService.Update(SelectedTourRequest);
            }
            ShowRegularTourRequestForm = false;
        }
    }
}
