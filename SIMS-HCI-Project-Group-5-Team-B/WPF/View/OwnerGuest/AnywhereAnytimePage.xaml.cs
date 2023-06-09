using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest
{
    /// <summary>
    /// Interaction logic for AnywhereAnytimePage.xaml
    /// </summary>
    public partial class AnywhereAnytimePage : Page
    {
        AccommodationService accommodationService;
        AnywhereAnytimeViewModel viewModel;
        public AnywhereAnytimePage(AccommodationService accommodationService, ReservationService reservationService, int ownerGuestId)
        {
            InitializeComponent();
            viewModel =  new AnywhereAnytimeViewModel(accommodationService, reservationService,ownerGuestId);
            this.DataContext = viewModel;
            this.accommodationService = accommodationService;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //guestno
            guestIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(250);
            guestIncBtn.Background = Brushes.LightGray;
            guestTB.Text = "2";
            await Task.Delay(250);
            guestIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(250);
            guestIncBtn.Background = Brushes.LightGray;
            guestTB.Text = "3";
            
            //dates
            await Task.Delay(500);
            StartDp.SelectedDate = DateTime.Today;
            await Task.Delay(1000);
            EndDP.SelectedDate = DateTime.Today.AddDays(16);
            await Task.Delay(1000);

            //days
            await Task.Delay(250);
            daysIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(250);
            daysIncBtn.Background = Brushes.LightGray;
            daysTB.Text = "2";

            await Task.Delay(250);
            daysIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(250);
            daysIncBtn.Background = Brushes.LightGray;
            daysTB.Text = "3";

            //Searchpress
            SearchBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(500);
            SearchBtn.Background = new SolidColorBrush(Color.FromRgb(162, 162, 200));
            await Task.Delay(500);

            //search result
            ObservableCollection<AnywhereAnytimeReservation> suggestions = new ObservableCollection<AnywhereAnytimeReservation>();
            AnywhereAnytimeReservation a1 = new AnywhereAnytimeReservation(accommodationService.GetById(10), DateTime.Today, DateTime.Today.AddDays(2));
            AnywhereAnytimeReservation a2 = new AnywhereAnytimeReservation(accommodationService.GetById(5), DateTime.Today.AddDays(1), DateTime.Today.AddDays(3));
            AnywhereAnytimeReservation a3 = new AnywhereAnytimeReservation(accommodationService.GetById(1), DateTime.Today.AddDays(1), DateTime.Today.AddDays(3));
            AnywhereAnytimeReservation a4 = new AnywhereAnytimeReservation(accommodationService.GetById(3), DateTime.Today.AddDays(1), DateTime.Today.AddDays(3));

            suggestions.Add(a1);
            suggestions.Add(a2);
            suggestions.Add(a3);
            suggestions.Add(a4);

            searchDG.ItemsSource = suggestions;

            await Task.Delay(2000);

            //end
            MessageBox.Show("Demo ended!","AnywhereAnytime",MessageBoxButton.OK, MessageBoxImage.Information);

            //reseting data
            daysTB.Text = "1";
            guestTB.Text = "1";
            StartDp.SelectedDate = null;
            EndDP.SelectedDate = null;
            suggestions.Clear();
            searchDG.ItemsSource = suggestions;
            searchDG.ItemsSource = viewModel.AASuggestions;
        }
    }
}
