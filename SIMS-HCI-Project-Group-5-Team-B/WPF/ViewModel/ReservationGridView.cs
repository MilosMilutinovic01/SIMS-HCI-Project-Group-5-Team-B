using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ReservationGridView : INotifyPropertyChanged
    {
        public Reservation Reservation { get; set; }
        public bool isForGrading;
        public bool isModifiable;
        private bool isCancelable;

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
                    NotifyPropertyChanged(nameof(IsModifiable));
                }
            }

        }

        public bool IsCancelable
        {
            get { return isCancelable; }
            set
            {
                if (value != isCancelable)
                {
                    isCancelable = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(IsCancelable));
                }
            }

        }
        public ReservationGridView(Reservation reservation, bool isForGrading, bool isModifiable, bool isCancelable)
        {
            Reservation = reservation;
            IsForGrading = isForGrading;
            IsModifiable = isModifiable;
            IsCancelable = isCancelable;
        }
    }
}

