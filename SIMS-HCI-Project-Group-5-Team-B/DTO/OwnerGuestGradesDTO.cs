using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.DTO
{
    public class OwnerGuestGradesDTO
    {
        public OwnerGuestGrade Grade { get; set; }
        public string CompletedOnTime { get; set; }
        public string GuestsComplaints { get; set; }
        public double AverageGrade { get; set; }
        public string OwnerName { get; set; }
        public OwnerGuestGradesDTO(OwnerGuestGrade grade)
        { 
            this.Grade = grade;
            if (grade.isPaymentCompletedOnTime)
            {
                CompletedOnTime = "Yes";
            }
            else
            {
                CompletedOnTime = "No";
            }
            if (grade.ComplaintsFromOtherGuests)
            {
                GuestsComplaints = "Yes";
            }
            else
            {
                GuestsComplaints = "No";
            }

            AverageGrade = grade.GetAverageGrade();
            OwnerName = grade.Reservation.Accommodation.Owner.Name + " " + grade.Reservation.Accommodation.Owner.Surname;
        }

        public OwnerGuestGradesDTO() { }
    }
}
