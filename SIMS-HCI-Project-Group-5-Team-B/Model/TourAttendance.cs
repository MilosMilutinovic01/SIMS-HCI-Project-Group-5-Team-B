using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class TourAttendance : ISerializable
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
        public Tour Tour { get; set; }
        public TourAttendance() { }
        public TourAttendance(int tourId, int guideId, DateTime start, int freeSpace)
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
                start.ToString(),
                freeSpace.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId = int.Parse(values[1]);
            GuideId = int.Parse(values[2]);
            Start = Convert.ToDateTime(values[3], CultureInfo.GetCultureInfo("en-US"));
            FreeSpace = int.Parse(values[4]);
        }
    }
}
