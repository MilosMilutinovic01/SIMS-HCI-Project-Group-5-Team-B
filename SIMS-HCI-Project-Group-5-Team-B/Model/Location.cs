using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class Location : ISerializable, INotifyPropertyChanged
    {

        
        public int Id { get; set; }

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

        public Location()
        {

        }

        public Location(string city, string state)
        {
            this.city = city;
            this.state = state;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
               Id.ToString(),
                city,
                state,
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            city = values[1];
            state = values[2];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
