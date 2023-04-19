using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class OwnerAccommodationGrade : ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }

        public Reservation Reservation { get; set; }

        public int cleanliness;
        public int Cleanliness
        {
            get { return cleanliness; }
            set
            {
                if (value != cleanliness)
                {
                    cleanliness = value;
                }
            }
        }

        public int ownerCorrectness;
        public int OwnerCorrectness
        {
            get { return ownerCorrectness; }
            set
            {
                if (value != ownerCorrectness)
                {
                    ownerCorrectness = value;
                }
            }
        }

        public int stateOfInventory;
        public int StateOfInventory
        {
            get { return stateOfInventory; }
            set
            {
                if (value != stateOfInventory)
                {
                    stateOfInventory = value;
                }
            }
        }


        private int quietness;

        public int Quietness
        {
            get { return quietness; }
            set
            {
                if (value != quietness)
                {
                    quietness = value;
                }
            }
        }
        private int privacy;

        public int Privacy
        {
            get { return privacy; }
            set
            {
                if (value != privacy)
                {
                    privacy = value;
                }
            }
        }

        public string additionalComment;

        public string AdditionalComment
        {
            get { return additionalComment; }
            set
            {
                if (value != additionalComment)
                {
                    additionalComment = value;
                }
            }
        }

        public List<string> PictureURLs { get; set; }


        private string pictureURLsString;
        public string PictureURLsString
        {
            get { return pictureURLsString; }
            set
            {
                if (value != pictureURLsString)
                {
                    pictureURLsString = value;
                    OnPropertyChanged();
                }
            }
        }

        private double gradeAverage;

        public double GradeAverage
        {
            get { return gradeAverage; }
            set
            {
                if (value != gradeAverage)
                {
                    gradeAverage = value;
                    OnPropertyChanged();
                }
            }
        }

        public OwnerAccommodationGrade()
        {
            Reservation = new Reservation();
            PictureURLs = new List<string>();
            gradeAverage = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
               Id.ToString(),
               ReservationId.ToString(),
               cleanliness.ToString(),
               ownerCorrectness.ToString(),
               stateOfInventory.ToString(),
               quietness.ToString(),
               privacy.ToString(),
               additionalComment,
               pictureURLsString,
               gradeAverage.ToString()
            };

            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            cleanliness = int.Parse(values[2]);
            ownerCorrectness = int.Parse(values[3]);
            stateOfInventory = int.Parse(values[4]);
            quietness = int.Parse(values[5]);
            privacy = int.Parse(values[6]);
            additionalComment = values[7];
            pictureURLsString = values[8];
            gradeAverage = double.Parse(values[9]);

            string[] URLs = PictureURLsString.Split(",");

            foreach (string url in URLs)
            {
                PictureURLs.Add(url);
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Cleanliness")
                {

                    if (Cleanliness < 1 || Cleanliness > 5)
                        return "Value is not in range";
                }
                else if (columnName == "OwnerCorrectness")
                {
                    if (OwnerCorrectness < 1 || OwnerCorrectness > 5)
                        return "Value is not in range";
                }
                else if (columnName == "StateOfInventory")
                {
                    if (StateOfInventory < 1 || StateOfInventory > 5)
                        return "Value is not in range";
                }
                else if (columnName == "Quietness")
                {
                    if (Quietness < 1 || Quietness > 5)
                        return "Value is not in range";
                }
                else if (columnName == "Privacy")
                {
                    if (Privacy < 1 || Privacy > 5)
                        return "Value is not in range";
                }
                else if (columnName == "PictureURLsString")
                {
                    if (string.IsNullOrEmpty(PictureURLsString))
                        return null;
                    //maybe implement some input pattern
                }

                return null;
            }
        }


        private readonly string[] _validatedProperties = { "Cleanliness", "OwnerCorrectness", "StateOfInventory", "Quietness", "Privacy", "PictureURLsString" };

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

    }
}
