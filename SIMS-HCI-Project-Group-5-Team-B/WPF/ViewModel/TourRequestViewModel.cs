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
using System.Windows;
using System.Windows.Controls;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class TourRequestViewModel : ViewModel
    {
        #region fields
        private bool enabledAccept;
        public bool EnabledAccept 
        {
            get { return enabledAccept; } 
            set
            {
                if (enabledAccept != value)
                {
                    enabledAccept = value;
                    OnPropertyChanged(nameof(EnabledAccept));
                }
            }
        }
        private bool enabledDate;
        public bool EnabledDate
        {
            get { return enabledDate; }
            set
            {
                if (enabledDate != value)
                {
                    enabledDate = value;
                    OnPropertyChanged(nameof(EnabledDate));
                }
            }
        }
        public Location Location { get; set; }
        public List<string> States { get; set; }
        public string State { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        private string city;
        public string City 
        {
            get { return city; } 
            set
            {
                if(city != value) 
                {
                    city = value;
                    List<TourRequest> filtered = tourRequestService.FilterRequests(State, City, Language, MaxGuests, StartDate, EndDate);
                    TourRequests.Clear();
                    foreach (var item in filtered)
                    {
                        TourRequests.Add(item);
                    }
                }
            }
        }
        public string Language { get; set; }
        public List<string> Languages { get; set; }
        public int MaxGuests { get; set; }
        private DateTime startDate;
        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                if (startDate != value)
                {
                    startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }
        private DateTime displayStartDate;
        public DateTime DisplayStartDate
        {
            get { return displayStartDate; }
            set
            {
                if (displayStartDate != value)
                {
                    displayStartDate = value;
                    OnPropertyChanged(nameof(DisplayStartDate));
                }
            }
        }
        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if (endDate != value)
                {
                    endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }
        private DateTime displayEndDate;
        public DateTime DisplayEndDate
        {
            get { return displayEndDate; }
            set
            {
                if (displayEndDate != value)
                {
                    displayEndDate = value;
                    OnPropertyChanged(nameof(DisplayEndDate));
                }
            }
        }
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        private TourRequest selectedTourRequest;
        public TourRequest SelectedTourRequest
        {
            get { return selectedTourRequest; }
            set
            {
                if(selectedTourRequest != value)
                {
                    selectedTourRequest = value;
                    OnPropertyChanged(nameof(SelectedTourRequest));
                }
            }
        }
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }
        public RelayCommand FilterRequestsCommand { get; set; }
        public RelayCommand EnableAcceptCommand { get; set; }
        public RelayCommand EnableDateCommand { get; set; }
        public RelayCommand LoadCitiesCommand { get; set; }
        public RelayCommand AcceptCommand { get; set; }
        public RelayCommand ChangeDateCommand { get; set; }

        public TourRequestService tourRequestService;
        public LocationService locationService;
        public AppointmentService appointmentService;
        public UserService userService;
        public Frame frame;
        public User logged;
        #endregion

        #region actions
        private bool CanExecute_Command()
        {
            return true;
        }

        private void Execute_FilterRequestsCommand()
        {
            List<TourRequest> filtered = tourRequestService.FilterRequests(State, City, Language, MaxGuests, StartDate, EndDate);
            TourRequests.Clear();
            foreach(var request in filtered)
            {
                TourRequests.Add(request);
            }
        }

        private void Execute_EnableAcceptCommand()
        {
            if (SelectedTourRequest != null && SelectedDate != default)
                EnabledAccept = true;
            else
                EnabledAccept = false;
        }

        private void Execute_EnableDateCommand()
        {
            if (SelectedTourRequest != null)
                EnabledDate = true;
            else
                EnabledDate = false;
        }

        private void Execute_LoadCitiesCommand()
        {
            Cities.Clear();
            foreach(var item in locationService.GetCityByState(State))
            {
                Cities.Add(item);
            }
        }

        private void Execute_AcceptCommand()
        {
            bool isAvailable = appointmentService.IsAvailable(logged.Id, SelectedDate);
            if (!isAvailable)
            {
                MessageBox.Show("You must select other date because you have tour at that time!");
                return;
            }
            bool result = MessageBox.Show("Are you sure you want to accept selected request?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (!result)
                return;
            if (!tourRequestService.IsValid(SelectedTourRequest, SelectedDate))
            {
                MessageBox.Show("You must enter valid date!");
                return;
            }
            SelectedTourRequest.SelectedDate = SelectedDate;
            Page create = new CreateTourPage("request", SelectedTourRequest);
            this.frame.NavigationService.Navigate(create);
        }

        private void Execute_ChangeDateCommand()
        {
            DisplayStartDate = SelectedTourRequest.DateRangeStart;
            DisplayEndDate = SelectedTourRequest.DateRangeEnd;
            SelectedDate = DisplayStartDate;
        }
        #endregion

        #region constructor
        public TourRequestViewModel(Frame frame)
        {
            locationService = new LocationService();
            tourRequestService = new TourRequestService();
            appointmentService = new AppointmentService();
            userService = new UserService();
            TourRequests = new ObservableCollection<TourRequest>();
            SelectedTourRequest = new TourRequest();
            States = locationService.GetStates();
            Cities = new ObservableCollection<string>();
            Languages = tourRequestService.GetLanguagesFromRequests();
            foreach (var item in tourRequestService.GetAll())
            {
                TourRequests.Add(item);
            }
            this.FilterRequestsCommand = new RelayCommand(Execute_FilterRequestsCommand, CanExecute_Command);
            this.EnableAcceptCommand = new RelayCommand(Execute_EnableAcceptCommand, CanExecute_Command);
            this.LoadCitiesCommand = new RelayCommand(Execute_LoadCitiesCommand, CanExecute_Command);
            this.AcceptCommand = new RelayCommand(Execute_AcceptCommand, CanExecute_Command);
            this.ChangeDateCommand = new RelayCommand(Execute_ChangeDateCommand, CanExecute_Command);
            this.EnableDateCommand = new RelayCommand(Execute_EnableDateCommand, CanExecute_Command);

            List<TourRequest> filtered = tourRequestService.FilterRequests(State, City, Language, MaxGuests, StartDate, EndDate);
            TourRequests.Clear();
            foreach (var item in filtered)
            {
                TourRequests.Add(item);
            }
            EnabledAccept = false;
            this.frame = frame;
            logged = userService.getLogged();
        }
        #endregion
    }
}
