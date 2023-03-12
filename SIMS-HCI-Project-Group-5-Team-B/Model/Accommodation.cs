using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public enum TYPE { Apartment = 0, House, Cottage };


namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class Accommodation : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string name;
        public String Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        private TYPE type;

        public string Type
        {
            get
            {
                if (type == TYPE.Apartment)
                {
                    return "Apartment";
                }
                else if (type == TYPE.House)
                {
                    return "House";

                }
                else
                {
                    return "Cottage";
                }

            }

            set
            {
                if (value == "Apartment" && type != TYPE.Apartment)
                {
                    type = TYPE.Apartment;
                    OnPropertyChanged();
                }
                else if (value == "House" && type != TYPE.House)
                {
                    type = TYPE.House;
                    OnPropertyChanged();
                }
                else if (value == "Cottage" && type != TYPE.Cottage)
                {
                    type = TYPE.Cottage;
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
                if (value != maxGuests)
                {
                    maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private int minReservationDays;

        public int MinReservationDays
        {
            get { return minReservationDays; }
            set
            {
                if (value != minReservationDays)
                {
                    minReservationDays = value;
                    OnPropertyChanged();
                }
            }

        }

        private int noticePeriod;

        public int NoticePeriod
        {
            get { return noticePeriod; }
            set
            {
                if (value != noticePeriod)
                {
                    noticePeriod = value;
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


        /*public string locationString;

        public string LocationString
        {
            get { return locationString; }
            set
            {
                if (locationString != value)
                {
                    locationString = value;
                    OnPropertyChanged();

                }

            }
        }*/

        public List<string> pictureURLs;

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



        public Accommodation()
        {
            pictureURLs = new List<string>();
        }

        public Accommodation(string name, TYPE type, int maxGuests, int minReservationDays, int noticePeriod, int locationId)
        {
            this.name = name;
            this.type = type;
            this.maxGuests = maxGuests;
            this.minReservationDays = minReservationDays;
            this.noticePeriod = noticePeriod;
            this.locationId = locationId;
            pictureURLs = new List<string>();
            location = new Location();
        }

        public string[] ToCSV()

        {

            string tempType;
            if (type == TYPE.Apartment)
            {
                tempType = "Apartment";
            }
            else if (type == TYPE.House)
            {
                tempType = "House";
            }
            else
            {
                tempType = "Cottage";
            }


            /*StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(pictureURLs[0]);

            for(int i = 1 ;i < pictureURLs.Count; i++)
            {
                stringBuilder.Append("," + pictureURLs[i]);
            }*/



            string[] csvValues =
            {
                Id.ToString(),
                name,
                tempType,
                locationId.ToString(),
                maxGuests.ToString(),
                minReservationDays.ToString(),
                noticePeriod.ToString(),
                PictureURLsString


            };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            name = values[1];
            if (values[2] == "Apartment")
            {
                type = TYPE.Apartment;
            }
            else if (values[2] == "House")
            {
                type = TYPE.House;
            }
            else if (values[2] == "Cottage")
            {
                type = TYPE.Cottage;
            }
            locationId = int.Parse(values[3]);
            maxGuests = int.Parse(values[4]);
            minReservationDays = int.Parse(values[5]);
            noticePeriod = int.Parse(values[6]);


            PictureURLsString = values[7];

            string[] URLs = PictureURLsString.Split(",");

            foreach (string url in URLs)
            {
                pictureURLs.Add(url);
            }


        }
        public string Error => null;
        //Regex adresa_regex = new Regex("[A-Z].{0,20},[A-Z].{0,20}");
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "The field must be filled";
                }
                else if (columnName == "Type")
                {
                    if (string.IsNullOrEmpty(Type))
                        return "The field must be filled";
                }
                else if (columnName == "MaxGuests")
                {
                    if (MaxGuests < 1)
                    {
                        return "Value must be greater than zero";
                    }
                }
                else if (columnName == "MinReservationDays")
                {
                    if (MinReservationDays < 1)
                    {
                        return "Value must be greater than zero";
                    }
                }
                else if (columnName == "NoticePeriod")
                {
                    if (NoticePeriod < 1)
                    {
                        return "Value must be greater than zero";
                    }
                }else if(columnName == "PictureURLsString")
                {
                    if (string.IsNullOrEmpty(PictureURLsString))
                    {
                        return "This field must be filled";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "Type", "MaxGuests", "MinReservationDays", "NoticePeriod", "PictureURLsString" };

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
