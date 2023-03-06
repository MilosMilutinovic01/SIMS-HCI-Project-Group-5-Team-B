using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class Location 
    {
        public string city;

        public String City
        {
            get { return city; }
            set
            {
                if (value != city)
                {
                    city = value;
                }
            }
        }

        public string state;

        public String State
        {
            get { return state; }
            set
            {
                if(value != state)
                {
                    state = value;
                }
            }
        }

    }
}
