using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class Guide
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
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
                if (surname != value)
                {
                    surname = value;
                }
            }
        }
        public Guide() 
        {
            this.Id = 0;
            this.name = "Milos";
            this.surname = "Milutinovic";
        }
        public Guide(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }
    }
}
