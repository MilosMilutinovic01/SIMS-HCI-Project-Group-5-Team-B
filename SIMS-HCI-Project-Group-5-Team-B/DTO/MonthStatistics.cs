using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.DTO
{
    public class MonthStatistic
    {
        public string Month { get; set; }
        public int NumberOfRequests { get; set; }
        public MonthStatistic(string month, int number)
        {
            Month = month;
            NumberOfRequests = number;
        }
    }
}
