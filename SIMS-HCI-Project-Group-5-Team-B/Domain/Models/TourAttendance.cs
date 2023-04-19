using SIMS_HCI_Project_Group_5_Team_B.Serializer;

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
        private int appointmentId;
        public int AppointmentId
        {
            get { return appointmentId; }
            set { appointmentId = value; }
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
        private int voucherId;
        public int VucherId
        {
            get { return voucherId; }
            set { voucherId = value; }
        }

        public TourAttendance()
        {

        }


        public TourAttendance(int appointmentId, int peopleAttending, int keyPointGuestArrivedId, int guideGuestId, int voucherId)
        {
            this.appointmentId = appointmentId;
            this.peopleAttending = peopleAttending;
            this.keyPointGuestArrivedId = keyPointGuestArrivedId;
            this.guideGuestId = guideGuestId;
            this.voucherId = voucherId;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                guideGuestId.ToString(),
                appointmentId.ToString(),
                peopleAttending.ToString(),
                keyPointGuestArrivedId.ToString(),
                voucherId.ToString(),
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            guideGuestId = int.Parse(values[1]);
            appointmentId = int.Parse(values[2]);
            peopleAttending = int.Parse(values[3]);
            keyPointGuestArrivedId = int.Parse(values[4]);
            voucherId = int.Parse(values[5]);
        }
    }
}
