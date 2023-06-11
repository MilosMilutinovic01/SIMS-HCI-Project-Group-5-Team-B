using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class SpecialTourRequest : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                if(name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<TourRequest> tourRequests;
        public ObservableCollection<TourRequest> TourRequests
        {
            get => tourRequests;
            set
            {
                if(tourRequests != value)
                {
                    tourRequests = value;
                    OnPropertyChanged();
                }
            }
        }

        public SpecialTourRequest()
        {
            tourRequests = new ObservableCollection<TourRequest>();
        }


        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (String.IsNullOrWhiteSpace(Name))
                    {
                        return "Special tour request must have name";
                    }
                }
                else if (columnName == "TourRequests")
                {
                    if (TourRequests.Count == 0)
                    {
                        return "Special tour request must have atleast one part";
                    }
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Name", "TourRequests" };

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
