using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System.Collections.ObjectModel;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{

    public class MonthlyAccommodationStatisticsViewModel
    {
        public ObservableCollection<MonthlyAccommodationStatistics> MonthlyAccommodationsStatistics { get; set; }
        private MonthlyAccommodationStatisticsService monthlyAccommodationStatisticsService;
        public RelayCommand CancelCommand { get; }
        public YearlyAccommodationStatistics SelectedYearlyAccommodationStatistics { get; set; }
        public MonthlyAccommodationStatisticsViewModel(MonthlyAccommodationStatisticsService monthlyAccommodationStatisticsService,YearlyAccommodationStatistics SelectedYearlyAccommodationStatistics, int SelectedAccommmodationId)
        {
            this.monthlyAccommodationStatisticsService = monthlyAccommodationStatisticsService;
            this.SelectedYearlyAccommodationStatistics = SelectedYearlyAccommodationStatistics;
            MonthlyAccommodationsStatistics = new ObservableCollection<MonthlyAccommodationStatistics>();
            GetMonthlyStatistics(SelectedAccommmodationId, SelectedYearlyAccommodationStatistics.Year);
            MarkBusiest();
            CancelCommand = new RelayCommand(Cancel_Execute, CanExecute);

        }

        public void GetMonthlyStatistics(int accommodationId, int year)
        {
            MonthlyAccommodationsStatistics.Clear();
            foreach(MonthlyAccommodationStatistics monthlyAccommodationStatistics in monthlyAccommodationStatisticsService.GetMonthlyStatistics(accommodationId, year))
            {
                MonthlyAccommodationsStatistics.Add(monthlyAccommodationStatistics);
            }
            //return MonthlyAccommodationsStatistics;
        }

       
        public void MarkBusiest()
        {
            monthlyAccommodationStatisticsService.MarkBusiest(MonthlyAccommodationsStatistics);
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Cancel_Execute()
        {
            App.Current.Windows[4].Close();
        }

    }
}
