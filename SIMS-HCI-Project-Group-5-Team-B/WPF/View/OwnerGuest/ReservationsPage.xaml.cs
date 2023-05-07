using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for ReservationsWindow.xaml
    /// </summary>
    /// 

    public partial class ReservationsPage : Page
    {

        public ReservationsPage(ReservationService reservationService, OwnerAccommodationGradeSevice ownerAccommodationGradeService, SuperOwnerService superOwnerController, OwnerService ownerService, int ownerGuestId, ReservationChangeRequestService reservationChangeRequestService)
        {
            InitializeComponent();

            this.DataContext = new ReservationsViewModel(reservationService, ownerAccommodationGradeService, superOwnerController, ownerService, ownerGuestId, reservationChangeRequestService); 


        }
    }
}
