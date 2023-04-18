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
        public int Id { get; set; }

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

        private int generalKnowledge;
        public int GeneralKnowledge
        {
            get { return generalKnowledge; }
            set { generalKnowledge = value; }
        }

        private int languageKnowledge;
        public int LanguageKnowledge
        {
            get { return languageKnowledge; }
            set { languageKnowledge = value; }
        }

        private int tourFun;
        public int TourFun
        {
            get { return tourFun; }
            set { tourFun = value; }
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

        public Card(int id, string guestName, string tourName, string keyPointName, int generalKnowledge, int languageKnowledge, int tourFun, string comment, bool valid, bool reported)
        {
            Id = id;
            this.guestName = guestName;
            this.tourName = tourName;
            this.keyPointName = keyPointName;
            this.generalKnowledge = generalKnowledge;
            this.languageKnowledge = languageKnowledge;
            this.tourFun = tourFun;
            this.comment = comment;
            this.valid = valid;
            this.reported = reported;
        }

        public Card()
        {

        }
    }
}
