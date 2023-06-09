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
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow(int ownerGuestId)
        {
            InitializeComponent();
            this.DataContext = new OwnerGuestReportViewModel(ownerGuestId);
        }
    }
}
