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
    /// Interaction logic for ReservationChangeRequestForm.xaml
    /// </summary>
    public partial class ReservationChangeRequestForm : Window
    {
        
        public ReservationChangeRequestForm(ObservableCollection<ReservationChangeRequest> ReservaitionChangeRequests,SingleReservationViewModel selectedReservationView, ReservationChangeRequestService reservationChangeRequestService, ReservationService reservationService)
        {
            InitializeComponent();
            this.DataContext = new ReservationChangeRequestViewModel(ReservaitionChangeRequests, selectedReservationView, reservationChangeRequestService, reservationService);
        }

    }
}
