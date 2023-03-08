using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class Tour : ISerializable
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }
        private string location;
        public string Location
        {
            get { return location; }
            set
            {
                if (location != value)
                {
                    location = value;
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
                }
            }
        }
        public List<KeyPoints> KeyPoints;
        public List<DateTime> Start;
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

        public List<string> ImageURL;


        public Tour()
        {

        }
        public Tour(string name, string location, string description, string language, int maxGuests, List<KeyPoints> keyPoints, List<DateTime> start, double duration, List<string> imageURL)
        {
            this.name = name;
            this.location = location;
            this.description = description;
            this.language = language;
            this.maxGuests = maxGuests;
            KeyPoints = keyPoints;
            Start = start;
            this.duration = duration;
            ImageURL = imageURL;
        }

        public string FormatingKeyPoints(List<KeyPoints> keyPoints) 
        {
            string stringBuilder = "";
            foreach (KeyPoints kp in keyPoints) 
            {
                stringBuilder = string.Join(',', kp.ToString());   
            }
            return stringBuilder;
        }
        public string FormatingStart(List<DateTime> start)
        {
            string stringBuilder = "";
            foreach (DateTime s in start)
            {
                stringBuilder = string.Join(',', s.ToString("dd/MM/yyyy HH:mm"));
            }
            return stringBuilder;
        }
        public List<KeyPoints> LoadFromString(string keyPointList) 
        {
            List <KeyPoints> keyPoints = new List <KeyPoints>();
            string[] parts = keyPointList.Split(',');
            for(int i = 0; i <  parts.Length; i++) 
            {
                keyPoints.Add(new Model.KeyPoints(parts[i]));
            }
            return keyPoints;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                name,
                location,
                description,
                language,
                maxGuests.ToString(),
                FormatingKeyPoints(KeyPoints),
                FormatingStart(Start),
                duration.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            name = values[0];
            location = values[1];
            description = values[2];
            language = values[3];
            maxGuests = Convert.ToInt32(values[4]);
        }
    }
}
