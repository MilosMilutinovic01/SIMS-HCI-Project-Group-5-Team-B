using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
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
        public Location Location { get; set; }
        public List<string> States { get; set; }
        public string State { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public string City { get; set; }
        public string Language { get; set; }
        public List<string> Languages { get; set; }
        public int MaxGuests { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public TourRequest SelectedTourRequest { get; set; }
        public DateTime SelectedDate { get; set; }
        public RelayCommand FilterRequestsCommand { get; set; }
        public RelayCommand ChangeDateCommand { get; set; }
        public RelayCommand LoadCitiesCommand { get; set; }
        //public RelayCommand RejectCommand { get; set; }
        public RelayCommand AcceptCommand { get; set; }

        public TourRequestService tourRequestService;
        public LocationService locationService;
        #endregion

        #region actions
        private bool CanExecute_Command()
        {
            return true;
        }

        private void Execute_FilterRequestsCommand()
        {
            //if (State == null || City == null || Language == null || MaxGuests == 0 || StartDate == default || EndDate == default)
            //{
            //    MessageBox.Show("You must fill the fields!");
            //    return;
            //}
            //Location = locationService.GetByStateAndCity(State, City);
            List<TourRequest> filtered = tourRequestService.FilterRequests(State, City, Language, MaxGuests, StartDate, EndDate);
            TourRequests.Clear();
            foreach(var item in filtered)
            {
                TourRequests.Add(item);
            }
        }

        private void Execute_ChangeDateCommand()
        {
            SelectedDate = SelectedTourRequest.DateRangeStart.AddDays(1);
        }

        private void Execute_LoadCitiesCommand()
        {
            Cities.Clear();
            foreach(var item in locationService.GetCityByState(State))
            {
                Cities.Add(item);
            }
        }

        //private void Execute_RejectCommand()
        //{
        //    bool result = MessageBox.Show("Are you sure you want to reject selected request?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        //    if (!result)
        //        return;
        //}

        private void Execute_AcceptCommand()
        {
            bool result = MessageBox.Show("Are you sure you want to accept selected request?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (!result)
                return;
            SelectedTourRequest.SelectedDate = SelectedDate;
            tourRequestService.AcceptRequest(SelectedTourRequest);
        }
        #endregion

        #region constructor
        public TourRequestViewModel()
        {
            locationService = new LocationService();
            tourRequestService = new TourRequestService();
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
            this.ChangeDateCommand = new RelayCommand(Execute_ChangeDateCommand, CanExecute_Command);
            this.LoadCitiesCommand = new RelayCommand(Execute_LoadCitiesCommand, CanExecute_Command);
            //this.RejectCommand = new RelayCommand(Execute_RejectCommand, CanExecute_Command);
            this.AcceptCommand = new RelayCommand(Execute_AcceptCommand, CanExecute_Command);
            //SelectedDate = DateTime.Now;
            //SelectedTourRequest.DateRangeEnd = DateTime.Now.AddDays(7);
            //SelectedTourRequest.DateRangeStart = DateTime.Now.AddDays(-7);
        }
        #endregion
    }
}
