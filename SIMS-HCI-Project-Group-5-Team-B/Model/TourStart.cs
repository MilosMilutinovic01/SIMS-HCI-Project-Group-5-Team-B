
using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class TourStart : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    //OnPropertyChanged();
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
                    //OnPropertyChanged();
                }
            }
        }
        private string clock;   //used for validation
        public string Clock
        {
            get { return clock; }
            set
            {
                if (clock != value)
                {
                    clock = value;
                    OnPropertyChanged();
                }
            }
        }

        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set
            {
                if (tourId != value)
                {
                    tourId = value;
                    //OnPropertyChanged();
                }
            }
        }

        // constructor to initialize the attributes
        public TourStart(int tourId, DateTime start)
        {
            this.tourId = tourId;
            this.start = start;
        }

        public TourStart(){}
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Start.ToString(),
                TourId.ToString()
            };

            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Start = Convert.ToDateTime(values[1], CultureInfo.GetCultureInfo("en-US"));
            TourId = int.Parse(values[2]);
        }
        Regex clockRegex = new Regex("[0-9]{2}:[0-9]{2}:[0-9]{2}");
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Clock")
                {
                    if (string.IsNullOrEmpty(Clock))
                        return "The field must be filled";

                    Match match = clockRegex.Match(Clock);
                    if (!match.Success)
                        return "Clock needs to be in format xx:xx:xx";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Clock" };

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
