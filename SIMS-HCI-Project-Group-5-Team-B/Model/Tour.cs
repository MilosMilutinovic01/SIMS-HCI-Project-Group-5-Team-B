using Microsoft.VisualBasic;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
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
        private Location location;
        public Location Location
        {
            get { return location; }
            set { location = value; }
        }
        private string locationString;
        public string LocationString
        {
            get { return locationString; }
            set
            {
                if (value != locationString)
                {
                    locationString = value;
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
                if (value != locationId)
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
                if (maxGuests != value && maxGuests <= 0)
                {
                    maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<KeyPoints> keyPoints;

        private string keyPointIds;
        public string KeyPointIds
        {
            get { return keyPointIds; }
            set
            {
                if (value != keyPointIds)
                {
                    keyPointIds = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<string> starts;
        public List<string> Starts
        {
            get { return starts; }
            set { starts = value; }
        }
        private double duration;
        public double Duration
        {
            get { return duration; }
            set
            {
                if (duration != value && duration <= 0)
                {
                    duration = value;
                }
            }
        }

        private string pictureURLsString;

        public string PictureURLsString
        {
            get { return pictureURLsString; }
            set
            {
                if (value != pictureURLsString)
                {
                    pictureURLsString = value;
                    OnPropertyChanged();
                }
            }
        }

        public Tour()
        {
            keyPoints = new List<KeyPoints>();
            starts = new List<string>();
        }
        public Tour(string name, string description, string language, int maxGuests, double duration, string pictureURLsString)
        {
            this.name = name;
            location = new Location();
            this.description = description;
            this.language = language;
            this.maxGuests = maxGuests;
            keyPoints = new List<KeyPoints>();
            this.duration = duration;
            this.pictureURLsString = pictureURLsString;
        }

        /*public string FormatingKeyPoints(List<KeyPoints> keyPoints) 
        {
            string stringBuilder = "";
            foreach (KeyPoints kp in keyPoints) 
            {
                stringBuilder = string.Join(',', kp.ToString());   
            }
            return stringBuilder;
        }
        
        */

        //This method format start of tour for storage
        public string FormatingStart(List<string> start)
        {
            string stringBuilder = "";
            foreach (string s in start)
            {
                stringBuilder = string.Join(',', s);
            }
            return stringBuilder;
        }

        // This method loads start of tours in list
        public List<String> Load(string startsString)
        {
            string[] parts = startsString.Split(',');
            List<string> starts = new List<string>();
            starts.AddRange(parts);
            return starts;
        }

        //This method create string from Key Points, used for storage
        public string CreateKeyPointIds(List<KeyPoints> keyPoints) 
        {
            List<string> ids = new List<string>();
            foreach (KeyPoints keyPoint in keyPoints) 
            {
                ids.Add(keyPoint.Id.ToString());
            }
            string result = string.Join(",", ids);
            return result;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                name,
                locationId.ToString(),
                description,
                language,
                maxGuests.ToString(),
                keyPointIds,
                FormatingStart(starts),
                duration.ToString(),
                pictureURLsString
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            name = values[1];
            locationId = int.Parse(values[2]);
            description = values[3];
            language = values[4];
            maxGuests = int.Parse(values[5]);
            keyPointIds = values[6];
            starts = Load(values[7]);
            duration = double.Parse(values[8]);
            pictureURLsString = values[9];
        }

        Regex locationRegex = new Regex("[A-Za-z\\s]+,[A-Za-z]+");
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
                else if(columnName == "MaxGuests")
                {
                    if(MaxGuests < 1)
                        return "Value must be greater than zero";
                }
                else if (columnName == "Duration")
                {
                    if (Duration < 1)
                    {
                        return "Value must be greater than zero";
                    }
                }
                else if (columnName == "PictureURLsString")
                {
                    if (string.IsNullOrEmpty(PictureURLsString))
                        return "The field must be filled";
                }
                else if( columnName == "LocationString")
                {
                    if (string.IsNullOrEmpty(LocationString))
                        return "The field must be filled";

                    Match match = locationRegex.Match(LocationString);
                    if (!match.Success)
                        return "Location needs to be in format: city, state";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "Description", "Language", "MaxGuests", "Duration", "PictureURLsString" };

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
