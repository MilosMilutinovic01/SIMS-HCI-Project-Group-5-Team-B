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
using SIMS_HCI_Project_Group_5_Team_B.View;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for OwnerGuestWindow.xaml
    /// </summary>
    public partial class OwnerGuestWindow : Window
    {
        public OwnerGuestWindow()
        {
            InitializeComponent();
        }

        private void ShowAccomodation_Click(object sender, RoutedEventArgs e)
        {
            AccomodationsWindow accomodationsWindow = new AccomodationsWindow();
            accomodationsWindow.Show();
        }

        
    }
}
