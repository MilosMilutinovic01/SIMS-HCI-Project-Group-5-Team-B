using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Guide: User, ISerializable
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
        public Guide()
        {
            Id = 0;
            Name = "Milos";
            Surname = "Milutinovic";
        }
        public Guide(string name, string surname)
        {
            Name = name;
            Surname = surname;
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
