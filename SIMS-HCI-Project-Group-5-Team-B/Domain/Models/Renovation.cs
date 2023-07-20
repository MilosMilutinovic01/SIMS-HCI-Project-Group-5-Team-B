using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class Renovation : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {

        public int Id { get; set; }


        private int accommodationId;
        public int AccommodationId
        {
            get { return accommodationId; }

            set { accommodationId = value; }

        }
        private Accommodation accommodation;
        public Accommodation Accommodation { get { return accommodation; } set { accommodation = value; } }
        private DateTime startDate;
        public DateTime StartDate
        {
            get
            { return startDate; }
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(StartDate));
                }
            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get
            { return endDate; }
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(EndDate));
                }
            }
        }

        public string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(Description));
                }
            }
        }

        private int renovationDays;
        public int RenovationDays
        {
            get { return renovationDays; }
            set
            {
                if (value != renovationDays)
                {
                    renovationDays = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(RenovationDays));
                }
            }
        }

        public bool IsDeleted { get; set; }

        public Renovation()
        {
            IsDeleted = false;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            StartDate = DateTime.Parse(values[2]);
            EndDate = DateTime.Parse(values[3]);
            Description = values[4];
            RenovationDays = int.Parse(values[5]); 
            IsDeleted = bool.Parse(values[6]);

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                StartDate.ToString(), 
                EndDate.ToString(),
                Description,
                RenovationDays.ToString(),
                IsDeleted.ToString()
            };
            return csvValues;
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }




        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "StartDate")
                {
                    if (StartDate < DateTime.Today)
                    {
                        if (Properties.Settings.Default.currentLanguage == "en-US")
                        {
                            return "The reservation can not be in the past";
                        }
                        else
                        {
                            return "Rezervacija ne sme biti u proslosti";
                        }
                    }

                    if (StartDate > EndDate)
                    {
                        if (Properties.Settings.Default.currentLanguage == "en-US")
                        {
                            return "Start date can not be bigger than end date";
                        }
                        else
                        {
                            return "Datum pocetka ne moze biti veci od datuma kraja";
                        }
                    }



                }
                else if (columnName == "EndDate")
                {
                    if (StartDate > EndDate)
                    {
                        if (Properties.Settings.Default.currentLanguage == "en-US")
                        {
                            return "Start date can not be bigger than end date";
                        }
                        else
                        {
                            return "Datum pocetka ne moze biti veci od datuma kraja";
                        }
                    }
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                    {
                        if (Properties.Settings.Default.currentLanguage == "en-US")
                        {
                            return "This field must be filled";
                        }
                        else
                        {
                            return "Polje mora biti popunjeno";
                        }
                    }
                }else if(columnName == "RenovationDays")
                {
                    if (RenovationDays < 1)
                    {
                        if (Properties.Settings.Default.currentLanguage == "en-US")
                        {
                            return "Value must be greater than one";
                        }
                        else
                        {
                            return "Vrednost mora biti veca od jedan";
                        }
                    }
                }

                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Accommodation","StartDate", "EndDate", "RenovationDays","Description" };

        private readonly string[] _validatedPropertiesForSearch = { "StartDate", "EndDate", "RenovationDays" };

        public event PropertyChangedEventHandler? PropertyChanged;
        public bool IsValidForSearch
        {
            get
            {
                foreach (var property in _validatedPropertiesForSearch)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

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
