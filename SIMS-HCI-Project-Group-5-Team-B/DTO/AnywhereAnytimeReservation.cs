using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.DTO
{
    public class AnywhereAnytimeReservation
    {
        public Accommodation Accommodation { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public AnywhereAnytimeReservation()
        {
            Accommodation = new Accommodation();

        }

        public AnywhereAnytimeReservation(Accommodation accommodation, DateTime start, DateTime end)
        {
            this.Accommodation = accommodation;
            this.Start = start;
            this.End = end;
        }
    }
}
