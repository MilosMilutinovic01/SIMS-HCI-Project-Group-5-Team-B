using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ReservationViewModel : INotifyPropertyChanged
    {
        public Reservation Reservation { get; set; }
        public bool isForGrading;
        public bool isModifiable;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public bool IsForGrading
        {
            get { return isForGrading; }
            set
            {
                if (value != isForGrading)
                {
                    isForGrading = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(IsForGrading)); 
                }
            }

        }

        public bool IsModifiable
        {
            get { return isModifiable; }
            set
            {
                if (value != isModifiable)
                {
                    isModifiable = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(IsForGrading)); 
                }
            }

        }
        public ReservationViewModel(Reservation reservation, bool isForGrading, bool isModifiable)
        {
            Reservation = reservation;
            IsForGrading = isForGrading;
            IsModifiable = isModifiable;
        }
    }
}

