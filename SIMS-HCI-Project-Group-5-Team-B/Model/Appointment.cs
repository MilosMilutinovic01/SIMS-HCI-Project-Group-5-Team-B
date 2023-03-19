using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class Appointment : ISerializable
    {
        public int Id { get; set; }
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
        private string tourName;
        public string TourName
        {
            get { return tourName; }
            set
            {
                if (tourName != value)
                {
                    tourName = value;
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
        private int guestsNumber;
        public int GuestsNumber
        {
            get { return guestsNumber; }
            set
            {
                if (guestsNumber != value)
                {
                    guestsNumber = value;
                }
            }
        }

        public Appointment()
        {
        }

        public Appointment(DateTime start, int tourId, string tourName, int guideId, int guestsNumber)
        {
            this.start = start;
            this.tourId = tourId;
            this.tourName = tourName;
            this.guideId = guideId;
            this.guestsNumber = guestsNumber;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                start.ToString(),
                tourId.ToString(),
                tourName,
                guideId.ToString(),
                guestsNumber.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Start = Convert.ToDateTime(values[1], CultureInfo.GetCultureInfo("en-US"));
            TourId = int.Parse(values[2]);
            TourName = values[3];
            GuideId = int.Parse(values[4]);
            GuestsNumber = int.Parse(values[5]);
        }
    }
}
