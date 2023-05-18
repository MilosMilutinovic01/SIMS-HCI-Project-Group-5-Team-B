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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for RequestsForChangingReservationPage.xaml
    /// </summary>
    public partial class RequestsForChangingReservationPage : Page
    {
       
        public RequestsForChangingReservationPage(ReservationChangeRequestService reservationChangeRequestService, ReservationService reservationService, Owner LogedInOwner)
        {
            InitializeComponent();
            this.DataContext = new HandleReservationChangeRequestViewModel(reservationChangeRequestService, reservationService, LogedInOwner);
        }

    }
}
