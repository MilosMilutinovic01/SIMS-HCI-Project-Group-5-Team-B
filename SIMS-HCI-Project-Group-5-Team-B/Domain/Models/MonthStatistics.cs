using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.Models
{
    public class MonthStatistic
    {
        public string Month { get; set; }
        public int NumberOfRequests { get; set; }
        public MonthStatistic(string month, int number) 
        {
            this.Month = month;
            this.NumberOfRequests = number;
        }
    }
}
