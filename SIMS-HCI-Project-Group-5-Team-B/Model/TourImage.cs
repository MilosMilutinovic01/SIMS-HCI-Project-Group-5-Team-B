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
    public class TourImage : ISerializable, IDataErrorInfo, INotifyPropertyChanged
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
        private string url;
        public string Url
        {
            get { return url; }
            set
            {
                if (url != value)
                {
                    url = value;
                    OnPropertyChanged();
                }
            }
        }
        private int tourId;
        public int TourId
        {
            get { return tourId; }
            set
            {
                if (tourId != value)
                {
                    tourId = value;
                }
            }
        }
        public TourImage() { }
        public TourImage(string url,int tourId, string name = "image")
        {
            Name = name;
            Url = url;
            this.tourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Url,
                TourId.ToString()
            };

            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Url = values[2];
            TourId = int.Parse(values[3]);
        }
        Regex urlRegex = new Regex("[A-Za-z\\s]+");
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Url")
                {
                    if (string.IsNullOrEmpty(Url))
                        return "The field must be filled";

                    Match match = urlRegex.Match(Url);
                    if (!match.Success)
                        return "Url needs to be string";
                }
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Url" };

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
