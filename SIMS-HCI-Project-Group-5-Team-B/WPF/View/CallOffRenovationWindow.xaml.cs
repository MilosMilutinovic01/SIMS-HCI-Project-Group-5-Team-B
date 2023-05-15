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
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System.Collections.ObjectModel;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for CallOffRenovationWindow.xaml
    /// </summary>
    public partial class CallOffRenovationWindow : Window
    {
        public CallOffRenovationWindow(IRenovationService renovationService,ReservationService reservationService,Owner owner,RenovationGridView SelectedRenovationGridView, ObservableCollection<RenovationGridView> FutureRenovations, AccommodationService accommodationService)
        {
            InitializeComponent();
            RenovationViewModel renovationViewModel = new RenovationViewModel(renovationService, reservationService, owner,accommodationService);
            this.DataContext = renovationViewModel;
            renovationViewModel.SelectedRenovationGridView = SelectedRenovationGridView;
            renovationViewModel.FutureRenovations = FutureRenovations;
            
        }
    }
}
