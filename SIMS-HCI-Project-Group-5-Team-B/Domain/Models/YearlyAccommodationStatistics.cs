using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class YearlyAccommodationStatistics : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                if(year != value)
                {
                    year = value;
                }
            }
        }

        private int numberOfReservations;

        public int NumberReservations
        {
            get { return numberOfReservations;}
            set
            {
                if(numberOfReservations != value)
                {
                    numberOfReservations = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(NumberReservations));
                }
            }
        }
        private int numberOfCancelledReservations;

        public int NumberOfCancelledReservations
        {
            get { return numberOfCancelledReservations;}
            set
            {
                if(numberOfCancelledReservations != value)
                {
                    numberOfCancelledReservations = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(NumberOfCancelledReservations));
                }
            }
        }


        private int numberOfChangedReservationDates;

        public int NumberOfChangedReservationDates
        {
            get { return numberOfChangedReservationDates;}
            set
            {
                if(numberOfChangedReservationDates != value)
                {
                    numberOfChangedReservationDates = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(NumberOfChangedReservationDates));
                }
            }
        }

        private int numberOfRenovationRequests;
        public int NumberRenovationRequests
        {
            get { return numberOfRenovationRequests;}
            set
            {
                if(numberOfRenovationRequests != value)
                {
                    numberOfRenovationRequests = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(NumberRenovationRequests));
                }
            }
        }

        private double busyness;
        public double Busyness
        {
            get { return busyness;}
            set
            {
                if(busyness != value)
                {
                    busyness = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(Busyness));
                }
            }
        }

        private bool isBusiest;

        public bool IsBusiest
        {
            get { return isBusiest; }
            set
            {
                if(isBusiest != value)
                {
                    isBusiest = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(IsBusiest));
                }
            }
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }




        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
