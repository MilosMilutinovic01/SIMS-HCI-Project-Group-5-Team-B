using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for DeclineReservationChangeRequestForm.xaml
    /// </summary>
    public partial class DeclineReservationChangeRequestForm : Window
    {


        private readonly HandleReservationChangeRequestViewModel _viewModel;
        public ObservableCollection<ReservationChangeRequest> OwnersPendingRequests { get; set; }
        public ReservationChangeRequest SelectedReservationChangeRequest { get; set; }
        public DeclineReservationChangeRequestForm(ReservationChangeRequestService reservationChangeRequestService, ReservationService reservationService, Owner owner, ReservationChangeRequest SelectedReservationChangeRequest, ObservableCollection<ReservationChangeRequest> OwnersPendingRequests)
        {
            InitializeComponent();
            _viewModel = new HandleReservationChangeRequestViewModel(reservationChangeRequestService, reservationService, owner, SelectedReservationChangeRequest);
            DataContext = this;
            this.OwnersPendingRequests = OwnersPendingRequests;
            this.SelectedReservationChangeRequest = SelectedReservationChangeRequest;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeclineReservationChangeRequest(SelectedReservationChangeRequest);
            OwnersPendingRequests.Remove(SelectedReservationChangeRequest);
            Close();
        }
    }
}
