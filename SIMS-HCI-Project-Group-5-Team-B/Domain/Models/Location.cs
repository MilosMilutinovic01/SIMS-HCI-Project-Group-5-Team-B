using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Location : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (city != value)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }

        public string state;

        public string State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    OnPropertyChanged();
                }
            }
        }

        public Location() { }

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
        public override string ToString()
        {
            return State + ", " + City;
        }
        Regex locationRegex = new Regex("[A-Za-z\\s]+");
        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "The field must be filled";

                    Match match = locationRegex.Match(City);
                    if (!match.Success)
                        return "City needs to be string";
                }
                else if (columnName == "State")
                {
                    if (string.IsNullOrEmpty(State))
                        return "The field must be filled";

                    Match match = locationRegex.Match(State);
                    if (!match.Success)
                        return "State needs to be string";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "City", "State" };

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
