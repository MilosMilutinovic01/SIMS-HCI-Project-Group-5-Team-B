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
using System.Collections.ObjectModel;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for GradingGuestWindow.xaml
    /// </summary>
    public partial class GradingGuestWindow : Window
    {
        public GradingGuestWindow(OwnerGuestGradeService ownerGuestGradeService, OwnerAccommodationGradeSevice ownerAccommodationGradeService,ReservationService reservationService,Reservation SelectedReservation, ObservableCollection<Reservation> ReservationsForGrading, ObservableCollection<OwnerAccommodationGrade> OwnerAccommodationGradesForShowing)
        {
            InitializeComponent();
            this.DataContext = new GradingGuestViewModel(ownerGuestGradeService,ownerAccommodationGradeService,reservationService,SelectedReservation,ReservationsForGrading,OwnerAccommodationGradesForShowing);
        }
    }
}
