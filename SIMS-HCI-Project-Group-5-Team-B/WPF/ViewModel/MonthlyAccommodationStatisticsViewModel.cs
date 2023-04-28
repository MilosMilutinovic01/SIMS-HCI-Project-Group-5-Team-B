using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System.Collections.ObjectModel;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{

    public class MonthlyAccommodationStatisticsViewModel
    {
        public ObservableCollection<MonthlyAccommodationStatistics> MonthlyAccommodationsStatistics { get; set; }
        private MonthlyAccommodationStatisticsService monthlyAccommodationStatisticsService;

        public MonthlyAccommodationStatisticsViewModel(MonthlyAccommodationStatisticsService monthlyAccommodationStatisticsService)
        {
            this.monthlyAccommodationStatisticsService = monthlyAccommodationStatisticsService;
            MonthlyAccommodationsStatistics = new ObservableCollection<MonthlyAccommodationStatistics>(); 
        }

        public ObservableCollection<MonthlyAccommodationStatistics> GetMonthlyStatistics(int accommodationId, int year)
        {
            MonthlyAccommodationsStatistics.Clear();
            foreach(MonthlyAccommodationStatistics monthlyAccommodationStatistics in monthlyAccommodationStatisticsService.GetMonthlyStatistics(accommodationId, year))
            {
                MonthlyAccommodationsStatistics.Add(monthlyAccommodationStatistics);
            }
            return MonthlyAccommodationsStatistics;
        }

       
        public void MarkBusiest(ObservableCollection<MonthlyAccommodationStatistics> monthlyStatistics)
        {
            monthlyAccommodationStatisticsService.MarkBusiest(monthlyStatistics);
        }


    }
}
