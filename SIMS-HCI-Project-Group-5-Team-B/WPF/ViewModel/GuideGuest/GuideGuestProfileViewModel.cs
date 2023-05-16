using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Events;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest
{
    public class GuideGuestProfileViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public ObservableCollection<TourRequest> TourRequests { get; set; }
        public ObservableCollection<TourAttendance> TourAttendances { get; set; }


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


        private TourRequestService tourRequestService;
        public GuideGuestProfileViewModel()
        {
            tourRequestService = new TourRequestService();

            Vouchers = new ObservableCollection<Voucher>(new VoucherService().GetAll());
            TourRequests = new ObservableCollection<TourRequest>(tourRequestService.GetFor(0));

            EditRegularTourRequestCommand = new RelayCommand(EditRegularTourRequest_Execute, CanEditRegularTourRequest);
            AddNewRegularTourRequestCommand = new RelayCommand(AddNewRegularTourRequest_Execute);
            SaveRegularTourRequestCommand = new RelayCommand(SaveRegularTourRequest_Execute);
            CancelRegularTourRequestCommand = new RelayCommand(CancelRegularTourRequest_Execute);
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
            SelectedTourRequest = null;

            ShowRegularTourRequestForm = !ShowRegularTourRequestForm;
        }
        private void CancelRegularTourRequest_Execute()
        {
            if(backupTourRequest != null && SelectedTourRequest != null)
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
        }
        private void SaveRegularTourRequest_Execute()
        {
            tourRequestService.Update(SelectedTourRequest);
            ShowRegularTourRequestForm = false;
        }
    }
}
