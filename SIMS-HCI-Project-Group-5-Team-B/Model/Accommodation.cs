using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;

public enum TYPE {Apartment = 0,House, Cottage };


namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class Accommodation : ISerializable, IDataErrorInfo
    {
        private string name;
        public String Name
        {
            get { return name; }
            set
            {
                if(value != name)
                {
                    name = value;
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
                    return "apartment";
                }
                else if (type == TYPE.House)
                {
                    return "house";

                } else
                {
                    return "cottage";
                }
                
            }

            set
            {
                if(value == "apartment" && type != TYPE.Apartment)
                {
                    type = TYPE.Apartment;
                }
                else if (value == "house" && type != TYPE.House)
                {
                    type = TYPE.House;
                }
                else if(value == "cottage" && type != TYPE.Cottage)
                {
                    type = TYPE.Cottage;
                }
            }
        }

        private int maxGuests;
        public int MaxGuests
        {
            get { return maxGuests; }
            set
            {
                if (value != maxGuests && maxGuests < 1 )
                {
                    maxGuests = value;
                }
            }
        }

        private int minReservationDays;

        public int MinReservationDays
        {
            get { return minReservationDays; }
            set
            {
                if (value != minReservationDays && minReservationDays < 1)
                {
                    minReservationDays = value;
                }
            }

        }

        private int noticePeriod;

        public int NoticePeriod
        {
            get { return noticePeriod; }
            set
            {
                if (value != noticePeriod && noticePeriod < 1)
                {
                    noticePeriod = value;
                }
            }
        }

        private Location location;

        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set
            {
                if (value != locationId)
                {
                    locationId = value;
                }
            }
        }

        public List<string> pictureURLs;

        public Accommodation()
        {
            pictureURLs = new List<string>();
        }

        public Accommodation(string name,TYPE type,int maxGuests, int minReservationDays, int noticePeriod, int locationId)
        {
            this.name = name;
            this.type = type;
            this.maxGuests = maxGuests;
            this.minReservationDays = minReservationDays;
            this.noticePeriod = noticePeriod;
            this.locationId = locationId;
            pictureURLs = new List<string>();
        }

        public string[] ToCSV()

        {

            string tempType;
            if(type == TYPE.Apartment)
            {
                tempType = "Apartment";
            }
            else if(type == TYPE.House)
            {
                tempType = "House";
            }
            else
            {
                tempType = "Cottage";
            }


            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(pictureURLs[0]);

            for(int i = 1 ;i < pictureURLs.Count; i++)
            {
                stringBuilder.Append("," + pictureURLs[i]);
            }

            

            string[] csvValues =
            {
                Name,
                tempType,
                locationId.ToString(),
                maxGuests.ToString(),
                minReservationDays.ToString(),
                noticePeriod.ToString(),
                stringBuilder.ToString()
                

            };
            return csvValues;
        }


        public void FromCSV(string[] values)
        {
            name = values[0];
            if(values[1] == "Apartment")
            {
                type = TYPE.Apartment;
            }
            else if(values[1] == "House")
            {
                type = TYPE.House;
            }
            else if(values[1] == "Cottage")
            {
                type = TYPE.Cottage;
            }
            locationId = int.Parse(values[2]);
            maxGuests = int.Parse(values[3]);
            minReservationDays = int.Parse(values[4]);
            noticePeriod = int.Parse(values[5]);


            string pURLs = values[6];

            string[] URLs = pURLs.Split(",");

            foreach(string url in URLs)
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
                    if(MinReservationDays < 1)
                    {
                        return "Value must be greater than zero";
                    }
                }
                else if(columnName == "NoticePeriod")
                {
                    if(NoticePeriod < 1)
                    {
                        return "Value must be greater than zero";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "Type", "MaxGuests", "MinReservationDays", "NoticePeriod"};

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
