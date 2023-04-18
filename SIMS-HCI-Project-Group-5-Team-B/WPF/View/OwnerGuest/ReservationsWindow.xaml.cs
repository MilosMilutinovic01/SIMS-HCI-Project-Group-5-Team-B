using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
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
using System.Windows.Shapes;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for ReservationsWindow.xaml
    /// </summary>
    /// 

    public partial class ReservationsWindow : Window
    {
        
        private ReservationsViewModel reservationViewModel;
        public ReservationsWindow(ReservationService reservationService, OwnerAccommodationGradeSevice ownerAccommodationGradeService, SuperOwnerService superOwnerController, OwnerService ownerService, int ownerGuestId,ReservationChangeRequestService reservationChangeRequestService)
        {
            InitializeComponent();
            
            reservationViewModel = new ReservationsViewModel(reservationService, ownerAccommodationGradeService, superOwnerController, ownerService, ownerGuestId, reservationChangeRequestService);
            this.DataContext = reservationViewModel;


        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grade_Button_Click(object sender, RoutedEventArgs e)
        {

            reservationViewModel.Grade();

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

        private void Modify_Button_Click(object sender, RoutedEventArgs e)
        {
            reservationViewModel.Modify();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            reservationViewModel?.Cancel();
        }
    }
}
