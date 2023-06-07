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
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest
{
    /// <summary>
    /// Interaction logic for AccommodationAADetails.xaml
    /// </summary>
    public partial class AccommodationAADetails : Window
    {
        public AccommodationAADetails(AnywhereAnytimeReservation SelectedReservation, ReservationService reservationService, int ownerGuestId, int guestNo, int resDays,ObservableCollection<AnywhereAnytimeReservation> suggestions)
        {
            InitializeComponent();
            this.DataContext = new AccommodationAADetailsViewModel(SelectedReservation,reservationService,ownerGuestId, guestNo, resDays,suggestions);
        }
    }
}
