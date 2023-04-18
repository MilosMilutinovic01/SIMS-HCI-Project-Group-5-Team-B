using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class GuideGuest : ISerializable
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

        public GuideGuest()
        {
            //initially, there is only one guest, in order to not complicate the implementation of other features
        }

        public GuideGuest(int id, string name, string surname)
        {
            Id = id;
            this.name = name;
            this.surname = surname;
        }
        public GuideGuest(int id, string name)
        {
            Id = id;
            this.name = name;
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
