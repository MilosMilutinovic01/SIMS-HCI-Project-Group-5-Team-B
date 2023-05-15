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
            //ShowAccommodations(owner);
        }

        /*private void Show_Statistics_Button_Click(object sender, RoutedEventArgs e)
        {
            yearlyAccommodationStatisticsViewModel.GetYearlyAccommodationStatistics(SelectedAccommmodationId);
            yearlyAccommodationStatisticsViewModel.MarkBusiest(yearlyAccommodationStatisticsViewModel.YearlyStatistics);
        }
        */
        /*public void ShowAccommodations(Owner owner)
        {
            foreach (Accommodation accommodation in accommodationService.GetAll())
            {
                if (accommodation.Owner.Id == owner.Id)
                {
                    ComboBoxItem cbItem = new ComboBoxItem();
                    cbItem.Content = accommodation.Name;
                    cbItem.Tag = accommodation.Id;
                    if ((int)cbItem.Tag == accommodation.Id)
                    {
                        cbItem.IsSelected = true; // za prikaz podatka
                    }
                    Accommodation_ComboBox.Items.Add(cbItem);
                }
            }
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (ComboBoxItem comboBoxItem in Accommodation_ComboBox.Items)
            {
                if (comboBoxItem.IsSelected)
                {
                    //ovde treba da prosledi dalje izabrani smestaj za statistiku, treba proslediti fji koja ce izlistati statistiku za izabrani smetsaj ,prolsediti smetaj metodi
                    // Get the selected item from the combo box
                    SelectedAccommmodationId = (int)comboBoxItem.Tag;
                }
            }

        }*/

    }
}
