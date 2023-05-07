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
    /// Interaction logic for ScheduleRenovationForm.xaml
    /// </summary>
    public partial class ScheduleRenovationForm : Window
    {
        private readonly RenovationViewModel renovationViewModel;
        public AccommodationService accommodationService;
        public ObservableCollection<RenovationGridView> FutureRenovations;

        public ScheduleRenovationForm(RenovationService renovationService, AccommodationService accommodationService, ReservationService reservationService,Owner owner, ObservableCollection<RenovationGridView> FutureRenovations)
        {
            InitializeComponent();
            renovationViewModel = new RenovationViewModel(renovationService, reservationService,owner.Id);
            this.DataContext = renovationViewModel;
            this.accommodationService = accommodationService;
            this.FutureRenovations = FutureRenovations;
            ShowAccommodations(owner);
        }

        private void Schedule_RenovationButton_Click(object sender, RoutedEventArgs e)
        {
            renovationViewModel.CreateRenovation();
            if (DateTime.Today.AddDays(5) < renovationViewModel.NewRenovation.StartDate)
            {
                FutureRenovations.Add(new RenovationGridView(renovationViewModel.NewRenovation, true));
            }
            else
            {
                FutureRenovations.Add(new RenovationGridView(renovationViewModel.NewRenovation, false));
            }
            Close();
        }
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            renovationViewModel.SeachAvailableDates();
        }

        private void DataGrid_SelectionChanged(object sender,RoutedEventArgs e)
        {

        }

        public void ShowAccommodations(Owner owner)
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
                    renovationViewModel.NewRenovation.AccommodationId = (int)comboBoxItem.Tag;
                    renovationViewModel.NewRenovation.Accommodation = accommodationService.GetById((int)comboBoxItem.Tag);

                }
            }

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
