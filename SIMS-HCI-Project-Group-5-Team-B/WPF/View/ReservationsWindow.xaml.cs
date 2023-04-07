using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Model;
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

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for ReservationsWindow.xaml
    /// </summary>
    /// 

    public partial class ReservationsWindow : Window
    {
        private ReservationService reservationController;
        private OwnerAccommodationGradeSevice ownerAccommodationGradeController;
        private OwnerService ownerController;
        private SuperOwnerService superOwnerController;
        public ObservableCollection<ReservationView> ReservationViews { get; set; }
        public ReservationView SelectedReservationView { get; set; }
        public ReservationsWindow(ReservationService reservationController, OwnerAccommodationGradeSevice ownerAccommodationGradeController, SuperOwnerService superOwnerController, OwnerService ownerController)
        {
            InitializeComponent();
            this.DataContext = this;

            this.reservationController = reservationController;
            this.ownerAccommodationGradeController = ownerAccommodationGradeController;
            this.superOwnerController = superOwnerController;
            this.ownerController = ownerController;

            //add method for checking the userId when showing reservations
            ReservationViews = new ObservableCollection<ReservationView>(reservationController.GetReservationsForGuestGrading());
            
            
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grade_Button_Click(object sender, RoutedEventArgs e)
        {

            if(SelectedReservationView != null)
            {
                GradingOwnerAccommodation gradingOwnerAccommodatoinWindow = new GradingOwnerAccommodation(ownerAccommodationGradeController, reservationController, SelectedReservationView, superOwnerController, ownerController);
                gradingOwnerAccommodatoinWindow.Show();
                
            }

        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Enter))
            {
                Grade_Button_Click(sender, e);
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.G))
                Grade_Button_Click(sender,e);
        }

        
    }
}
