using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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
    /// Interaction logic for OwnerNotificationsPage.xaml
    /// </summary>
    public partial class OwnerNotificationsPage : Page
    {
        private OwnerNotificationsViewModel ownerNotificationsViewModel;
        public OwnerNotificationsPage(Owner owner)
        {
            InitializeComponent();
            ownerNotificationsViewModel = new OwnerNotificationsViewModel(owner.Id);
            DataContext = ownerNotificationsViewModel;
        }
    }
}
