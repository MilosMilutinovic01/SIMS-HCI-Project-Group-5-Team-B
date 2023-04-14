using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Appointment : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set
            {
                if (tourId != value)
                {
                    tourId = value;
                }
            }
        }

        private int guideId;
        public int GuideId
        {
            get { return guideId; }
            set
            {
                if (guideId != value)
                {
                    guideId = value;
                }
            }
        }

        private DateTime start;
        public DateTime Start
        {
            get { return start; }
            set
            {
                if (start != value)
                {
                    start = value;
                }
            }
        }

        private int freeSpace;
        public int FreeSpace
        {
            get { return freeSpace; }
            set
            {
                if (freeSpace != value)
                {
                    freeSpace = value;
                }
            }
        }

        private string time;    //using for validation
        public string Time
        {
            get { return time; }
            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool started;
        public bool Started
        {
            get { return started; }
            set
            {
                if (started != value)
                {
                    started = value;
                }
            }
        }

        private bool ended;
        public bool Ended
        {
            get { return ended; }
            set
            {
                if (ended != value)
                {
                    ended = value;
                }
            }
        }

        public bool cancelled;
        public bool Cancelled
        {
            get { return cancelled; }
            set
            {
                if (cancelled != value)
                {
                    cancelled = value;
                }
            }
        }

        private int checkedKeyPointId;
        public int CheckedKeyPointId
        {
            get { return checkedKeyPointId; }
            set
            {
                if (checkedKeyPointId != value)
                {
                    checkedKeyPointId = value;
                }
            }
        }
        public Tour Tour { get; set; }
        public Appointment() { }
        public Appointment(int tourId, int guideId, DateTime start, int freeSpace)
        {
            this.tourId = tourId;
            this.guideId = guideId;
            this.start = start;
            this.freeSpace = freeSpace;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                tourId.ToString(),
                guideId.ToString(),
                start.ToString(CultureInfo.GetCultureInfo("en-US")),
                freeSpace.ToString(),
                started.ToString(),
                ended.ToString(),
                cancelled.ToString(),
                checkedKeyPointId.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            tourId = int.Parse(values[1]);
            guideId = int.Parse(values[2]);
            start = Convert.ToDateTime(values[3], CultureInfo.GetCultureInfo("en-US"));
            freeSpace = int.Parse(values[4]);
            started = Convert.ToBoolean(values[5]);
            ended = Convert.ToBoolean(values[6]);
            cancelled = Convert.ToBoolean(values[7]);
            checkedKeyPointId = int.Parse(values[8]);
        }

        Regex timeRegex = new Regex("[0-9]{2}:[0-9]{2}:[0-9]{2}");
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Time")
                {
                    if (string.IsNullOrEmpty(Time))
                        return "The field must be filled";

                    Match match = timeRegex.Match(Time);
                    if (!match.Success)
                        return "Time needs to be in format xx:xx:xx";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Time" };

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