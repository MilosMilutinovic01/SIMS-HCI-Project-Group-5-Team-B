using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for ReservationFormWindow.xaml
    /// </summary>
    public partial class ReservationForm : Window
    {
        Accommodation SelectedAccomodation;
        ReservationFormViewModel viewModel;

        public ReservationForm(ReservationService reservationService, Accommodation SelectedAccomodation, int ownerGuestId)
        {
            InitializeComponent();
            viewModel = new ReservationFormViewModel(reservationService, SelectedAccomodation, ownerGuestId);
            this.DataContext = viewModel;
            this.SelectedAccomodation = SelectedAccomodation;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //remember previous values;
            int incGuestNo = int.Parse( guestNoTB.Text);
            int incDays = int.Parse( ResDaysTB.Text);
            DateTime incStart = (DateTime) StartDP.SelectedDate;
            DateTime incIEnd = (DateTime) EndDP.SelectedDate;
            ObservableCollection<ReservationRecommendation> initial = viewModel.ReservationRecommendations;
            //setting data to the original
            guestNoTB.Text = "1";
            ResDaysTB.Text = SelectedAccomodation.MinReservationDays.ToString();
            
            await Task.Delay(1000);
            StartDP.SelectedDate = DateTime.Today.AddDays(1);
            await Task.Delay(1000);
            EndDP.SelectedDate = DateTime.Today.AddDays(16);
            await Task.Delay(1000);

            //click only once
            await Task.Delay(750);
            DaysIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(750);
            DaysIncBtn.Background = Brushes.LightGray;
            ResDaysTB.Text = (SelectedAccomodation.MinReservationDays + 1).ToString();

            await Task.Delay(1000);
            SearchBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(750);
            SearchBtn.Background = new SolidColorBrush(Color.FromRgb(162, 162, 200));
            await Task.Delay(750);

            //reccommendations
            ObservableCollection<ReservationRecommendation> lista = new ObservableCollection<ReservationRecommendation>();
            ReservationRecommendation r = new ReservationRecommendation(DateTime.Today.AddDays(1), DateTime.Today.AddDays(SelectedAccomodation.MinReservationDays +1));
            ReservationRecommendation r1 = new ReservationRecommendation(DateTime.Today.AddDays(2), DateTime.Today.AddDays(SelectedAccomodation.MinReservationDays + 2));
            ReservationRecommendation r2 = new ReservationRecommendation(DateTime.Today.AddDays(3), DateTime.Today.AddDays(SelectedAccomodation.MinReservationDays + 3));
            lista.Add(r);
            lista.Add(r1);
            lista.Add(r2);
            reccommDG.ItemsSource = lista;
            await Task.Delay(1000);

            //guest no
            guestIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(750);
            guestIncBtn.Background = Brushes.LightGray;
            if (SelectedAccomodation.MaxGuests >= 2)
            {
                guestNoTB.Text = "2";
            }
            await Task.Delay(750);
            guestIncBtn.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
            await Task.Delay(750);
            guestIncBtn.Background = Brushes.LightGray;
            if (SelectedAccomodation.MaxGuests >= 3)
            {
                guestNoTB.Text = "3";
            }

            await Task.Delay(750);
            reccommDG.SelectedIndex = 0;
            await Task.Delay(1000);

            DataGridRow row = (DataGridRow)reccommDG.ItemContainerGenerator.ContainerFromIndex(0);
            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(2);
            Button button = FindVisualChild<Button>(cell);
            
            if (button != null)
            {
                button.Background = new SolidColorBrush(Color.FromArgb(255, 201, 222, 245));
                await Task.Delay(1000);
                button.Background = new SolidColorBrush(Color.FromRgb(162, 162, 200));
                await Task.Delay(1000);
            }

            //after press
            MessageBoxResult result = MessageBox.Show("Reservation was successful", "Reservation", MessageBoxButton.OK, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                guestNoTB.Text = incGuestNo.ToString();
                ResDaysTB.Text = incDays.ToString();
                StartDP.SelectedDate = incStart;
                EndDP.SelectedDate = incIEnd;
                lista.Clear();
                reccommDG.ItemsSource = lista;
                viewModel.ReservationRecommendations = initial;
                reccommDG.ItemsSource = viewModel.ReservationRecommendations;

                MessageBox.Show("Demo Ended!", "Reservation", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DataContext = viewModel;
            }
        }
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child != null && child is T t)
                    return t;

                T childItem = FindVisualChild<T>(child);
                if (childItem != null)
                    return childItem;
            }

            return null;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.T))
            {
                Button_Click(sender, e);
            }
        }
    }
}
