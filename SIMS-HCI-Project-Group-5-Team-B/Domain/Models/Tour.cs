using Microsoft.VisualBasic;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Tour : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set
            {
                if (locationId != value)
                {
                    locationId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string language;
        public string Language
        {
            get { return language; }
            set
            {
                if (language != value)
                {
                    language = value;
                    OnPropertyChanged();
                }
            }
        }

        private int maxGuests;
        public int MaxGuests
        {
            get { return maxGuests; }
            set
            {
                if (maxGuests != value && value > 0)
                {
                    maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private double duration;
        public double Duration
        {
            get { return duration; }
            set
            {
                if (duration != value && value > 0)
                {
                    duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private string imageUrls;
        public string ImageUrls
        {
            get { return imageUrls; }
            set
            {
                if (imageUrls != value)
                {
                    imageUrls = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<KeyPoint> KeyPoints { get; set; }
        public Location Location { get; set; }

        public Tour()
        {
            KeyPoints = new List<KeyPoint>();
        }
        public Tour(string name, string description, string language, int maxGuests, DateTime start, double duration, string pictureURLsString)
        {
            this.name = name;
            this.description = description;
            this.language = language;
            this.maxGuests = maxGuests;
            this.duration = duration;
        }
        public string[] ToCSV()
        {
            string stringBuilder = "";
            foreach (KeyPoint kp in KeyPoints)
            {
                stringBuilder += kp.Name + ",";
            }
            stringBuilder = stringBuilder.Substring(0, stringBuilder.Length - 1);
            string[] csvValues =
            {
                Id.ToString(),
                name,
                locationId.ToString(),
                description,
                language,
                maxGuests.ToString(),
                stringBuilder,
                duration.ToString(),
                imageUrls
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            name = values[1];
            LocationId = int.Parse(values[2]);
            description = values[3];
            language = values[4];
            maxGuests = int.Parse(values[5]);
            string[] parts = values[6].Split(',');
            for (int i = 0; i < parts.Length; i++)
            {
                KeyPoints.Add(new KeyPoint(parts[i]));
            }
            duration = double.Parse(values[7]);
            imageUrls = values[8];
        }

        Regex maxGuestsRegex = new Regex("^([1-9][0-9]*)$");
        Regex durationRegex = new Regex("^[0-9]*\\.?[0-9]+([eE][-+]?[0-9]+)?$\r\n");
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "The field must be filled";
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "The field must be filled";
                }
                else if (columnName == "Language")
                {
                    if (string.IsNullOrEmpty(Language))
                        return "The field must be filled";
                }
                else if (columnName == "MaxGuests")
                {
                    if (MaxGuests < 1)
                        return "Value must be greater than zero";

                    Match match = maxGuestsRegex.Match(MaxGuests.ToString());
                    if (!match.Success)
                        return "Maximum guests needs to be number";
                }
                else if (columnName == "Duration")
                {
                    if (Duration < 0)
                    {
                        return "Value must be greater than zero";

                        Match match = durationRegex.Match(MaxGuests.ToString());
                        if (!match.Success)
                            return "Maximum guests needs to be number";
                    }
                }
                else if (columnName == "ImageUrls")
                {
                    if (string.IsNullOrEmpty(ImageUrls))
                        return "The field must be filled";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "Description", "Language", "MaxGuests", "Duration", "ImageUrls" };

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
