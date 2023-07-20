using System;
using System.Collections.Generic;
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
        private List<TourRequest> tourRequests;
        public List<TourRequest> TourRequests
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
            tourRequests = new List<TourRequest>();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
