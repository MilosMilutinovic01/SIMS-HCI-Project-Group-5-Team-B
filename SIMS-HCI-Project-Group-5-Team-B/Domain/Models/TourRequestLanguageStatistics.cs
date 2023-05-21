using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class TourRequestLanguageStatistics : INotifyPropertyChanged
    {
        private int year;
        public int Year
        {
            get => year;
            set
            {
                if(year != value)
                {
                    year = value;
                    OnPropertyChanged();
                }
            }
        }
        private string language;
        public string Language
        {
            get => language;
            set
            {
                if(language != value)
                {
                    language = value;
                    OnPropertyChanged();
                }
            }
        }
        private int numberOfAcceptedRequests;
        public int NumberOfAcceptedRequests
        {
            get => numberOfAcceptedRequests;
            set
            {
                if(numberOfAcceptedRequests != value)
                {
                    numberOfAcceptedRequests = value;
                    OnPropertyChanged();
                }
            }
        }
        private int numberOfRejectedRequests;
        public int NumberOfRejectedRequests
        {
            get => numberOfRejectedRequests;
            set
            {
                if(numberOfRejectedRequests != value)
                {
                    numberOfRejectedRequests = value;
                    OnPropertyChanged();
                }
            }
        }

        public TourRequestLanguageStatistics(int year, string language, int numberOfAcceptedRequests, int numberOfRejectedRequests)
        {
            Year = year;
            Language = language;
            NumberOfAcceptedRequests = numberOfAcceptedRequests;
            NumberOfRejectedRequests = numberOfRejectedRequests;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
