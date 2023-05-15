using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for GradingPage.xaml
    /// </summary>
    public partial class GradingPage : Page
    {
        public GradingPage(ReservationService reservationService, OwnerAccommodationGradeSevice ownerAccommodationGradeService, OwnerGuestGradeService ownerGuestGradeService, Owner owner)
        {
            InitializeComponent();
            this.DataContext = new GradingViewModel(reservationService,ownerAccommodationGradeService,ownerGuestGradeService,owner);
        }
    }
}
