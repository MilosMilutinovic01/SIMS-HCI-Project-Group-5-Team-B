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
    public enum TourRequestStatuses { WAITING, EXPIRED, ACCEPTED }


    public class TourRequest : INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }
        private int guideGuestId;
        public int GuideGuestId
        {
            get => guideGuestId;
            set
            {
                if(guideGuestId != value)
                {
                    guideGuestId = value;
                    OnPropertyChanged();
                }
            }
        }
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

        private DateTime dateRangeStart;
        public DateTime DateRangeStart
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

        private DateTime dateRangeEnd;
        public DateTime DateRangeEnd
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

        private TourRequestStatuses status;
        public string Status
        {
            get
            {
                if (status == TourRequestStatuses.WAITING) return "WAITING";
                else if (status == TourRequestStatuses.EXPIRED) return "EXPIRED";
                else return "ACCEPTED";
            }
            set
            {
                if(value == "WAITING" && status != TourRequestStatuses.WAITING)
                {
                    status = TourRequestStatuses.WAITING;
                    OnPropertyChanged();
                }
                else if (value == "EXPIRED" && status != TourRequestStatuses.EXPIRED)
                {
                    status = TourRequestStatuses.EXPIRED;
                    OnPropertyChanged();
                }
                else if (value == "ACCEPTED" && status != TourRequestStatuses.ACCEPTED)
                {
                    status = TourRequestStatuses.ACCEPTED;
                    OnPropertyChanged();
                }
            }
        }

        public TourRequest(int guideGuestId, int locationId, string description, string language, int maxGuests, DateTime dateRangeStart, DateTime dateRangeEnd, string status)
        {
            GuideGuestId = guideGuestId;
            LocationId = locationId;
            Description = description;
            Language = language;
            MaxGuests = maxGuests;
            DateRangeStart = dateRangeStart;
            DateRangeEnd = dateRangeEnd;
            Status = status;
        }

        public TourRequest() { }




        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Description")
                {
                    if(Description == null || Description == string.Empty)
                    {
                        return "Tour request must have description";
                    }
                }
                else if (columnName == "Language")
                {
                    if (Language == null || Language == string.Empty)
                    {
                        return "Tour request must have language";
                    }
                }
                else if (columnName == "MaxGuests")
                {
                    if(MaxGuests < 1)
                    {
                        return "Max guests must be greater than 0";
                    }
                }
                else if (columnName == "DateRangeStart")
                {
                    if (DateRangeStart < DateTime.Now.AddDays(2))
                    {
                        return "Date range must start 2 days from now";
                    }
                }
                else if (columnName == "DateRangeEnd")
                {
                    if (DateRangeEnd > DateRangeStart)
                    {
                        return "Date range can't end before it started";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "GuideGeneralKnowlegde", "GuideLanguageKnowledge", "TourFun" };

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







        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
