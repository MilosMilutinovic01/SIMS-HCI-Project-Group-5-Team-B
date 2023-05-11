using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToastNotifications.Lifetime;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class TourRequest : INotifyPropertyChanged
    {
        public enum TourRequestStatuses { WAITING, EXPIRED, ACCEPTED }

        private Location location;
        public Location Location
        {
            get => location;
            set
            {
                if(location != value)
                {
                    location = value;
                    OnPropertyChanged();
                }
            }
        }

        private int locationId;
        public int LocationId
        {
            get => locationId;
            set
            {
                if(locationId != value)
                {
                    locationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                if(description != value)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string language;
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

        private int maxGuests;
        public int MaxGuests
        {
            get => maxGuests;
            set
            {
                if(maxGuests != value)
                {
                    maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly dateRangeStart;
        public DateOnly DateRangeStart
        {
            get => dateRangeStart;
            set
            {
                if(dateRangeStart != value)
                {
                    dateRangeStart = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateOnly dateRangeEnd;
        public DateOnly DateRangeEnd
        {
            get => dateRangeEnd;
            set
            {
                if (dateRangeEnd != value)
                {
                    dateRangeEnd = value;
                    OnPropertyChanged();
                }
            }
        }

        private TourRequestStatuses tourRequestStatus;
        public TourRequestStatuses TourRequestStatus
        {
            get => tourRequestStatus;
            set
            {
                if(tourRequestStatus != value)
                {
                    tourRequestStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourRequest(int locationId, string description, string language, int maxGuests, DateOnly dateRangeStart, DateOnly dateRangeEnd, TourRequestStatuses tourRequestStatus)
        {
            LocationId = locationId;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            DateRangeStart = dateRangeStart;
            DateRangeEnd = dateRangeEnd;
            TourRequestStatus = tourRequestStatus;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
