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
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for AccomodationDetailsWindow.xaml
    /// </summary>
    public partial class AccomodationDetailsWindow : Window
    {
       

        public AccomodationDetailsWindow(Accommodation SelectedAccomodation, ReservationService reservationService, int ownerGuestId)
        {
            InitializeComponent();
            DataContext = new AccommodatioDetailsViewModel(SelectedAccomodation, reservationService, ownerGuestId);
     

        }

       
    }
}
