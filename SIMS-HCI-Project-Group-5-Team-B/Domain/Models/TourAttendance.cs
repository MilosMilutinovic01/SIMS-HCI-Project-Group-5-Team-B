using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class TourAttendance : ISerializable
    {
        public int Id { get; set; }

        private int guideGuestId;
        public int GuideGuestId
        {
            get { return guideGuestId; }
            set { guideGuestId = value; }
        }
        private int tourAttendanceId;
        public int TourAttendanceId
        {
            get { return tourAttendanceId; }
            set { tourAttendanceId = value; }
        }
        private int peopleAttending;
        public int PeopleAttending
        {
            get { return peopleAttending; }
            set { peopleAttending = value; }
        }
        private int keyPointGuestArrivedId;
        public int KeyPointGuestArrivedId
        {
            get { return keyPointGuestArrivedId; }
            set { keyPointGuestArrivedId = value; }
        }
        public TourAttendance()
        {

        }

        public TourAttendance(int tourAttendanceId, int peopleAttending, int keyPointGuestArrivedId, int guideGuestId)
        {
            TourAttendanceId = tourAttendanceId;
            PeopleAttending = peopleAttending;
            KeyPointGuestArrivedId = keyPointGuestArrivedId;
            GuideGuestId = guideGuestId;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                guideGuestId.ToString(),
                tourAttendanceId.ToString(),
                peopleAttending.ToString(),
                keyPointGuestArrivedId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            guideGuestId = int.Parse(values[1]);
            tourAttendanceId = int.Parse(values[2]);
            peopleAttending = int.Parse(values[3]);
            keyPointGuestArrivedId = int.Parse(values[4]);
        }
    }
}
