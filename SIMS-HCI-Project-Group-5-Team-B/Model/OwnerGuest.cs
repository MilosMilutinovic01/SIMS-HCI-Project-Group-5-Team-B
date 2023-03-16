using SIMS_HCI_Project_Group_5_Team_B.Serializer;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class OwnerGuest : ISerializable
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set { name = value; }
        }
        
        public OwnerGuest()
        {
            //initially, there is only one guest, in order to not complicate the implementation of other features
            Id = 0;
            Name = "Jelena";
            Surname = "Kovač";
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            name = values[1];
            surname = values[2];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                name,
                surname
            };
            return csvValues;
        }
    }
}
