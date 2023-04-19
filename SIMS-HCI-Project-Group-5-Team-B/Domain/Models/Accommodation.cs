using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public enum TYPE { Apartment = 0, House, Cottage };


namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Accommodation : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }
        private int ownerId;
        public int OwnerId
        {
            get { return ownerId; }
            set
            {
                if (value != ownerId)
                {
                    ownerId = value;
                    OnPropertyChanged();
                }
            }
        }

        public Owner Owner { get; set; }


        private string name;
        public string Name
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
            set
            {
                if (value != location)
                {
                    location = value;
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
            noticePeriod = 1;
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


            string[] csvValues =
            {
                Id.ToString(),
                ownerId.ToString(),
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
            ownerId = int.Parse(values[1]);
            name = values[2];
            if (values[3] == "Apartment")
            {
                type = TYPE.Apartment;
            }
            else if (values[3] == "House")
            {
                type = TYPE.House;
            }
            else if (values[3] == "Cottage")
            {
                type = TYPE.Cottage;
            }
            locationId = int.Parse(values[4]);
            maxGuests = int.Parse(values[5]);
            minReservationDays = int.Parse(values[6]);
            noticePeriod = int.Parse(values[7]);


            PictureURLsString = values[8];

            string[] URLs = PictureURLsString.Split(",");

            foreach (string url in URLs)
            {
                pictureURLs.Add(url);
            }


        }
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
                }
                else if (columnName == "PictureURLsString")
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
