using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class RenovationGridView : INotifyPropertyChanged
    {
        public Renovation Renovation { get; set; }
        public bool canBeCalledOff;

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

        public bool CanBeCalledOff
        {
            get { return canBeCalledOff; }
            set
            {
                if(value != canBeCalledOff)
                {
                    canBeCalledOff = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(CanBeCalledOff));
                }
            }
        }

        public RenovationGridView(Renovation renovation, bool canBeCalledOff)
        {
            Renovation = renovation;
            CanBeCalledOff = canBeCalledOff;
        }

    }
}
