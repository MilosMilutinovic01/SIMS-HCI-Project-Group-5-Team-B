using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class OwnerGuestGradeDetailsViewModel
    {
        public OwnerGuestGradesDTO SelectedGrade { get; set; }
        public string GuestName { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public RelayCommand CloseCommand { get;}
        protected GradeDetailsWindow window;
        public OwnerGuestGradeDetailsViewModel(OwnerGuestGradesDTO SelectedGrade, GradeDetailsWindow window) 
        {
            this.SelectedGrade = SelectedGrade;
            GuestName = SelectedGrade.Grade.Reservation.OwnerGuest.Name + " " + SelectedGrade.Grade.Reservation.OwnerGuest.Surname;
            Start = SelectedGrade.Grade.Reservation.StartDate.ToShortDateString();
            End = SelectedGrade.Grade.Reservation.EndDate.ToShortDateString();
            this.window = window;

            //commands
            CloseCommand = new RelayCommand(Cancel_Execute,CanExecute);

        }

        public bool CanExecute()
        {
            return true;
        }

        public void Cancel_Execute()
        {
            window.Close();
        }
    }
}
