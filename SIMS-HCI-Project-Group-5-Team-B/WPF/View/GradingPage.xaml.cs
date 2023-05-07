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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for GradingPage.xaml
    /// </summary>
    public partial class GradingPage : Page
    {
        public ObservableCollection<Reservation> ReservationsForGrading { get; set; }
        public ObservableCollection<OwnerAccommodationGrade> OwnerAccommodationGradesForShowing { get; set; }
        public OwnerAccommodationGrade SelectedOwnerAccommodationGrade { get; set; }

        public ReservationService reservationService;
        public OwnerAccommodationGradeSevice ownerAccommodationGradeService;
        public OwnerGuestGradeService ownerGuestGradeService;
        public Owner owner;
        public Reservation SelectedReservation { get; set; }
        public GradingPage(ReservationService reservationService, OwnerAccommodationGradeSevice ownerAccommodationGradeService, OwnerGuestGradeService ownerGuestGradeService, Owner owner)
        {
            InitializeComponent();
            DataContext = this;
            this.reservationService = reservationService;
            this.ownerAccommodationGradeService = ownerAccommodationGradeService;
            this.ownerGuestGradeService = ownerGuestGradeService;
            this.owner = owner;
            ReservationsForGrading = new ObservableCollection<Reservation>(reservationService.GetReservationsForGrading(owner));
            OwnerAccommodationGradesForShowing = new ObservableCollection<OwnerAccommodationGrade>(ownerAccommodationGradeService.GetOwnerAccommodationGradesForShowing(owner));
        }

        private void Grade_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                GradingGuestWindow gradingGuestWindow = new GradingGuestWindow(ownerGuestGradeService, ownerAccommodationGradeService, reservationService, SelectedReservation, ReservationsForGrading, OwnerAccommodationGradesForShowing);
                gradingGuestWindow.Show();
            }
        }

        private void Details_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedOwnerAccommodationGrade != null)
            {
                OwnerAccommodationGradeDetailsWindow ownerAccommodationGradeDetailsWindow = new OwnerAccommodationGradeDetailsWindow(SelectedOwnerAccommodationGrade);
                ownerAccommodationGradeDetailsWindow.Show();
            }
        }

    }
}
