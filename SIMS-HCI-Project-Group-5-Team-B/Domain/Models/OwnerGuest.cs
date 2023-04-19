using SIMS_HCI_Project_Group_5_Team_B.Serializer;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class OwnerGuest : User, ISerializable
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                }
            }
        }
        private string surname;
        public string Surname
        {
            get { return surname; }
            set
            {
                if (value != surname)
                {
                    surname = value;
                }
            }
        }

        public OwnerGuest()
        {
            
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Username = values[1];
            Password = values[2];
            name = values[3];
            surname = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Username,
                Password,
                name,
                surname
            };
            return csvValues;
        }
    }
}
