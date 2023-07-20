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
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest
{
    /// <summary>
    /// Interaction logic for RenovationRequestForm.xaml
    /// </summary>
    public partial class RenovationRequestForm : Window
    {

        public RenovationRequestForm(int reservationId)
        {

            InitializeComponent();           
            this.DataContext = new RenovationRequestViewModel(reservationId);
        }

        
    }
}
