using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for GradingOwnerAccommodation.xaml
    /// </summary>
    public partial class GradingOwnerAccommodation : Window
    {
       
        public GradingOwnerAccommodation(OwnerAccommodationGradeSevice ownerAccommodationGradeService, ReservationService reservationService, SingleReservationViewModel reservationView, SuperOwnerService superOwnerService, OwnerService ownerService)
        {
            InitializeComponent();
            this.DataContext = new GradingOwnerAccommodationViewModel(ownerAccommodationGradeService,reservationService,reservationView,superOwnerService,ownerService);
           
            
        }

    }
}
