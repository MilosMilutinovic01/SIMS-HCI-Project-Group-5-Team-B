using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class ReservationRecommendation
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ReservationRecommendation(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
