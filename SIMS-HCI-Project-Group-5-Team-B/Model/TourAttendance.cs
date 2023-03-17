using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    internal class TourAttendance
    {
        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set { tourId = value; }
        }
        private int guestId;

        public int GuestId
        {
            get { return guestId; }
            set { guestId = value; }
        }
        private DateTime start;
        public DateTime Start
        {
            get { return start; }
            set { start = value; }
        }
        public KeyPoints KeyPoints
        {
            get { return keyPoints; }
            set { keyPoints = value; }
        }
        private KeyPoints keyPoints;

        public TourAttendance()
        {
            keyPoints = new KeyPoints();
        }
    }
}
