using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.DTO
{
    public class Card
    {
        private string guestName;
        public string GuestName
        {
            get { return guestName; }
            set { guestName = value; }
        }

        private string tourName;
        public string TourName
        {
            get { return tourName; }
            set { tourName = value; }
        }

        private string keyPointName;
        public string KeyPointName
        {
            get { return keyPointName; }
            set { keyPointName = value; }
        }

        private int rating;
        public int Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        private bool valid;
        public bool Valid
        {
            get { return valid; }
            set { valid = value; }
        }

        private bool reported;
        public bool Reported
        {
            get { return reported; }
            set { reported = value; }
        }

        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Card(string guestName, string tourName, string keyPointName, int rating, string comment, bool valid, bool reported)
        {
            this.guestName = guestName;
            this.tourName = tourName;
            this.keyPointName = keyPointName;
            this.rating = rating;
            this.comment = comment;
            this.valid = valid;
            this.reported = reported;
        }

        public Card()
        {

        }
    }
}
