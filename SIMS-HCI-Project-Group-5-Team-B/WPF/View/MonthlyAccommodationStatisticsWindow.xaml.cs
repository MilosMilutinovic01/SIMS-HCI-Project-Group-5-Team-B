using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System.Collections.ObjectModel;


namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for MonthlyAccommodationStatisticsWindow.xaml
    /// </summary>
    public partial class MonthlyAccommodationStatisticsWindow : Window
    {
        private readonly MonthlyAccommodationStatisticsViewModel monthlyAccommodationStatisticsViewModel;
        public YearlyAccommodationStatistics SelectedYearlyAccommodationStatistics { get; set; }
        public ObservableCollection<MonthlyAccommodationStatistics> MonthlyAccommodationsStatistics { get; set; }
        
        public MonthlyAccommodationStatisticsWindow(YearlyAccommodationStatistics SelectedYearlyAccommodationStatistics,MonthlyAccommodationStatisticsService monthlyAccommodationStatisticsService, int SelectedAccommmodationId)
        {
            InitializeComponent();
            monthlyAccommodationStatisticsViewModel = new MonthlyAccommodationStatisticsViewModel(monthlyAccommodationStatisticsService);
            DataContext = this;
            this.SelectedYearlyAccommodationStatistics = SelectedYearlyAccommodationStatistics;
            this.MonthlyAccommodationsStatistics = monthlyAccommodationStatisticsViewModel.GetMonthlyStatistics(SelectedAccommmodationId, SelectedYearlyAccommodationStatistics.Year);
            monthlyAccommodationStatisticsViewModel.MarkBusiest(MonthlyAccommodationsStatistics);

        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
