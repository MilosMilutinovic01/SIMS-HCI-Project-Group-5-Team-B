using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        
        public StatisticsPage(YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService, AccommodationService accommodationService, Owner owner, MonthlyAccommodationStatisticsService monthlyAccommodationStatisticsService)
        {
            InitializeComponent();
            this.DataContext = new YearlyAccommodationStatisticsViewModel(yearlyAccommodationStatisticsService, accommodationService, owner, monthlyAccommodationStatisticsService);
        }

    }
}
