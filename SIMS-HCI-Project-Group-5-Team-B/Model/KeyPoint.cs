using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Model
{
    public class KeyPoint : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool selected;
        public bool Selected
        {
            get { return selected; }
            set
            {
                if (selected != value)
                {
                    selected = value;
                }
            }
        }

        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set
            {
                if (value != tourId)
                {
                    tourId = value;
                }
            }
        }
        public KeyPoint() { }
        public KeyPoint(KeyPoint copy)
        {
            this.Name = copy.Name;
            this.Selected = copy.Selected;
            this.TourId = copy.TourId;
        }
        public KeyPoint(string name)
        {
            this.Name = name;
        }
        public KeyPoint(string name, bool selected, int tourId)
        {
            this.name = name;
            this.selected = selected;
            this.tourId = tourId;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                name,
                selected.ToString(),
                tourId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            name = values[1];
            selected = Convert.ToBoolean(values[2]);
            tourId = int.Parse(values[3]);
        }

        Regex nameRegex = new Regex("[A-Za-z\\s]+");
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "The field must be filled";

                    Match match = nameRegex.Match(Name);
                    if (!match.Success)
                        return "Name needs to be string";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Name" };

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
