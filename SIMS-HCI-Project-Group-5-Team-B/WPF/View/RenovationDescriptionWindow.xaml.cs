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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for RenovationDescriptionWindow.xaml
    /// </summary>
    public partial class RenovationDescriptionWindow : Window
    {
        
        public RenovationService renovationService;
        private readonly RenovationViewModel renovationViewModel;
        public RenovationDescriptionWindow(Renovation NewRenovation,RenovationService renovationService, ReservationService reservationService)
        {
            InitializeComponent();
            renovationViewModel = new RenovationViewModel(renovationService, reservationService);
            DataContext = renovationViewModel;
            renovationViewModel.NewRenovation = NewRenovation;
          
        }


        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            renovationViewModel.CreateRenovation();
        }
    }
}
