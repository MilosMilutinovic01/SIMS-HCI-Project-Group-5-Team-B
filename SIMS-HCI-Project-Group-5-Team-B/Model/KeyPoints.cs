using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    internal class KeyPoints
    {
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

        public KeyPoints(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name.ToString();
        }
    }
}
