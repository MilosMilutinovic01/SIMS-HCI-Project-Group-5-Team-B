using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System.Collections.ObjectModel;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using System.Globalization;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class YearlyAccommodationStatisticsViewModel
    {
        public ObservableCollection<YearlyAccommodationStatistics> YearlyStatistics { get; set; }
        private YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService;
        private MonthlyAccommodationStatisticsService monthlyAccommodationStatisticsService;
        public YearlyAccommodationStatistics SelectedYearlyAccommodationStatistics { get; set; }
        public AccommodationService accommodationService { get; set; }

        public List<string> AccommodationNames { get; set; }

        public string SelectedAccommodationName { get; set; }
       
        private int accommodationId { get; set; }
        public Owner owner { get; set; }

        public RelayCommand ShowCommand { get; }
        public RelayCommand MonthlyStatisticsCommand { get; }

        public YearlyAccommodationStatisticsViewModel(YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService, AccommodationService accommodationService,Owner owner, MonthlyAccommodationStatisticsService monthlyAccommodationStatisticsService)
        {
            this.yearlyAccommodationStatisticsService = yearlyAccommodationStatisticsService;
            this.monthlyAccommodationStatisticsService = monthlyAccommodationStatisticsService;
            this.accommodationService = accommodationService;
            this.YearlyStatistics = new ObservableCollection<YearlyAccommodationStatistics>();
            this.AccommodationNames = new List<string>();
            GetAccommodationNames(owner);
            this.owner = owner;
            ShowCommand = new RelayCommand(Show_Execute, CanExecute);
            MonthlyStatisticsCommand = new RelayCommand(MonthlyStatistics_Execute, CanExecute);
        }



        public void GetAccommodationNames(Owner owner)
        {
            foreach (Accommodation accommodation in accommodationService.GetUndeleted())
            {
                if (accommodation.Owner.Id == owner.Id)
                {
                    AccommodationNames.Add(accommodation.Name);
                }
            }
        }


        public void GetYearlyAccommodationStatistics()
        {
            accommodationId = accommodationService.GetIdByName(SelectedAccommodationName, owner);
            YearlyStatistics.Clear();
            foreach (YearlyAccommodationStatistics yearlyAccommodationStatistics in yearlyAccommodationStatisticsService.GetYearlyStatistics(accommodationId))
            {
                YearlyStatistics.Add(yearlyAccommodationStatistics);
            }
        }

        public void MarkBusiest()
        {
            yearlyAccommodationStatisticsService.MarkBusiest(YearlyStatistics);
        }

        public bool CanExecute()
        {
            return true;
        }

        public void Show_Execute()
        {
            GetYearlyAccommodationStatistics();
            MarkBusiest();
        }

        public void MonthlyStatistics_Execute()
        {
            if(SelectedYearlyAccommodationStatistics != null)
            {
                MonthlyAccommodationStatisticsWindow monthlyAccommodationStatisticsWindow = new MonthlyAccommodationStatisticsWindow(SelectedYearlyAccommodationStatistics, monthlyAccommodationStatisticsService, accommodationId,accommodationService);
                monthlyAccommodationStatisticsWindow.Show();
            }
        }
    }
}
