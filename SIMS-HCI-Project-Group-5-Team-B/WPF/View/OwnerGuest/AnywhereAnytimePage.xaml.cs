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
            //remembernig initial values
            int incGuestNo = int.Parse(guestTB.Text);
            int incDays = int.Parse(daysTB.Text);
            Nullable<DateTime> incStart = StartDp.SelectedDate;
            Nullable<DateTime> incIEnd = EndDP.SelectedDate;
            ObservableCollection<AnywhereAnytimeReservation> initial = viewModel.AASuggestions;

            //setting data 
            guestTB.Text = "1";
            daysTB.Text = "1";
            //guestno
            await Task.Delay(1000);
            guestIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(750);
            guestIncBtn.Background = Brushes.LightGray;
            guestTB.Text = "2";
            await Task.Delay(750);
            guestIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(750);
            guestIncBtn.Background = Brushes.LightGray;
            guestTB.Text = "3";

            //dates
            StartDp.SelectedDate = null;
            
            await Task.Delay(750);
            StartDp.SelectedDate = DateTime.Today;
            await Task.Delay(1000);
            EndDP.SelectedDate = DateTime.Today.AddDays(16);
            await Task.Delay(1000);

            //days
            await Task.Delay(750);
            daysIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(750);
            daysIncBtn.Background = Brushes.LightGray;
            daysTB.Text = "2";

            await Task.Delay(750);
            daysIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(750);
            daysIncBtn.Background = Brushes.LightGray;
            daysTB.Text = "3";

            await Task.Delay(750);
            //Searchpress
            SearchBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(1000);
            SearchBtn.Background = new SolidColorBrush(Color.FromRgb(162, 162, 200));
            await Task.Delay(750);

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
            searchDG.SelectedIndex = 0;
            await Task.Delay(750);

            //end
            MessageBox.Show("You can click on details to see details of the Reservation","AnywhereAnytime",MessageBoxButton.OK, MessageBoxImage.Information);
            await Task.Delay(750);
            MessageBox.Show("Demo ended!", "AnywhereAnytime",MessageBoxButton.OK, MessageBoxImage.Information);

            //reseting data
            daysTB.Text = incDays.ToString();
            guestTB.Text = incGuestNo.ToString();
            StartDp.SelectedDate = incStart;
            EndDP.SelectedDate = incIEnd;
            suggestions.Clear();
            searchDG.ItemsSource = suggestions;
            viewModel.AASuggestions = initial;
            searchDG.ItemsSource = viewModel.AASuggestions;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.T))
            {
                Button_Click(sender, e);
            }
        }
    }
}
