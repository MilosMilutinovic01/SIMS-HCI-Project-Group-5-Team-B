using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ToastNotifications.Messages.Core;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class AnywhereAnytimeViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private AccommodationService accommodationService;
        private ReservationService reservationService;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private ObservableCollection<AnywhereAnytimeReservation> aaSuggestions;
        public ObservableCollection<AnywhereAnytimeReservation> AASuggestions 
        { 
            get { return aaSuggestions; }
            set
            {
                if(aaSuggestions != value)
                {
                    aaSuggestions = value;
                    NotifyPropertyChanged(nameof(AASuggestions));
                }
            }
        }
        public AnywhereAnytimeReservation SelectedReservation { get; set; }

        private Nullable<DateTime> start;
        public Nullable<DateTime> Start
        {
            get { return start; }
            set
            {
                if (value != start)
                {
                    start = value;
                    NotifyPropertyChanged(nameof(Start));
                    //for validation
                    NotifyPropertyChanged(nameof(End));
                }
            }
        }
        private Nullable<DateTime> end;
        public Nullable<DateTime> End
        {
            get { return end; }
            set
            {
                if (value != end)
                {
                    end = value;
                    NotifyPropertyChanged(nameof(End));
                }
            }
        }

        private int guestNo;
        public int GuestNo
        {
            get { return guestNo; }
            set
            {
                if (value != guestNo)
                {
                    guestNo = value;
                    NotifyPropertyChanged(nameof(GuestNo));
                }
            }
        }
        private int days;
        public int Days
        {
            get { return days; }
            set
            {
                if (value != days)
                {
                    days = value;
                    NotifyPropertyChanged(nameof(Days));
                }
            }
        }

        public RelayCommand GuestIncreaseCommand { get; }
        public RelayCommand GuestDecreaseCommand { get; }
        public RelayCommand DaysIncreaseCommand { get; }
        public RelayCommand DaysDecreaseCommand { get; }
        public RelayCommand SearchCommand { get; }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if(columnName == "Days")
                {
                    if(Days <= 0)
                    {
                        return "Days must be greater than 0.";
                    }
                }

                if (columnName == "GuestNo")
                {
                    if (GuestNo <= 0)
                    {
                        return "Days must be greater than 0.";
                    }
                }

                if(columnName == "Start")
                {
                    if(Start == null)
                    {
                        return null;
                    }
                }

                if (columnName == "End")
                {
                    if (End == null && Start != null)
                    {
                        return "You must select end date!";
                    }
                    else if(Start > End)
                    {
                        return "End must be after start";
                    }
                }

                return null;
            }
        }



        public AnywhereAnytimeViewModel(AccommodationService accommodationService, ReservationService reservationService)
        {
            this.accommodationService = accommodationService;
            this.reservationService = reservationService;
            GuestNo = 1;
            Days = 1;
            GuestIncreaseCommand = new RelayCommand(GuestNumberIncrease_Executed);
            GuestDecreaseCommand = new RelayCommand(GuestNumberDecrease_Executed);
            DaysIncreaseCommand = new RelayCommand(ReservationDaysIncrease_Execute);
            DaysDecreaseCommand = new RelayCommand(ReservationDaysDecrease_Execute);
            SearchCommand = new RelayCommand(OnSearch);
        }


        public void ReservationDaysIncrease_Execute()
        {
            Days++;
        }

        public void ReservationDaysDecrease_Execute()
        {

            if (Days > 1)
            {
                Days--;

            }
        }

        public void GuestNumberDecrease_Executed()
        {

            if (GuestNo > 1)
            {
                GuestNo--;
            }

        }

        public void GuestNumberIncrease_Executed()
        {
            GuestNo++;
        }

        public void OnSearch()
        {
            if (this.IsValid)
            {
                AASuggestions = new ObservableCollection<AnywhereAnytimeReservation>(reservationService.GetAASuggestions(guestNo, start, end, days));  
            }
            else
            {
                MessageBox.Show("You must fill required fields.");
            }
            
        }

        private readonly string[] _validatedProperties = { "Days", "GuestNo", "Start", "End" };
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
    }
}
