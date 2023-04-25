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
    /// Interaction logic for RenovationDescriptionWindow.xaml
    /// </summary>
    public partial class RenovationDescriptionWindow : Window
    {
        
        public RenovationService renovationService;
        private readonly RenovationViewModel renovationViewModel;
        public ObservableCollection<RenovationGridView> FutureRenovations { get; set; }
        public RenovationDescriptionWindow(Renovation NewRenovation,RenovationService renovationService, ReservationService reservationService, int ownerId, ObservableCollection<RenovationGridView> FutureRenovations, RenovationGridView SelectedRenovationGridView)
        {
            InitializeComponent();
            renovationViewModel = new RenovationViewModel(renovationService, reservationService,ownerId, SelectedRenovationGridView);
            DataContext = renovationViewModel;
            renovationViewModel.NewRenovation = NewRenovation;
            this.FutureRenovations = FutureRenovations;
          
        }


        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
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

        }
    }
}
