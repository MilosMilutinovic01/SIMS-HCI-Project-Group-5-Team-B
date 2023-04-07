using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }

        private int guideGuestId;
        public int GuideGuestId
        {
            get { return guideGuestId; }
            set { guideGuestId = value; }
        }

        private int guideId;
        public int GuideId
        { 
            get { return guideId; }
            set { guideId = value; }
        }

        private DateTime received;

        public DateTime Received
        {
            get { return received; }
            set { received = value; }
        }

        public Voucher () { }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                guideGuestId.ToString(),
                guideId.ToString(),
                received.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            guideGuestId = int.Parse(values[1]);
            guideId = int.Parse(values[2]);
            received = DateTime.Parse(values[3]);
        }
    }
}
