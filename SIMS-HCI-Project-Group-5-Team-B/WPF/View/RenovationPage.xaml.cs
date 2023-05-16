using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for RenovationPage.xaml
    /// </summary>
    public partial class RenovationPage : Page
    {

        //public RenovationGridView SelectedRenovationGridView { get; set; }
        public RenovationPage(IRenovationService renovationService, ReservationService reservationService, Owner owner, AccommodationService accommodationService)
        {
            InitializeComponent();
            
            //renovationViewModel = new RenovationViewModel(renovationService, reservationService, owner.Id/*, SelectedRenovationGridView*/);
            this.DataContext = new RenovationViewModel(renovationService, reservationService, owner,accommodationService/*, SelectedRenovationGridView*/);
        }

        /*private void Schedule_Button_Click(object sender, RoutedEventArgs e)
        {
            ScheduleRenovationForm scheduleRenovationForm = new ScheduleRenovationForm(renovationViewModel,accommodationService,owner);
            scheduleRenovationForm.Show();
        }*/


       
    }
}
