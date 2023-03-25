using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class Owner: ISerializable
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

        public Owner()
        {
            //initially, there is only one owner, in order to not complicate the implementation of other features
           
        }
        public Owner(String name, String surname)
        {
            //initially, there is only one owner, in order to not complicate the implementation of other features
            this.Name = name;
            this.Surname = surname;
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
