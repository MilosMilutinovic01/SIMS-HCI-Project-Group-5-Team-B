using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.UserControls
{
    /// <summary>
    /// Interaction logic for NotificationUserControl.xaml
    /// </summary>
    public partial class NotificationUserControl : UserControl
    {
        public ObservableCollection<Notification> Notifications { get; set; }
        public NotificationUserControl()
        {
            InitializeComponent();
            this.DataContext = this;

            Notifications = new ObservableCollection<Notification>(new NotificationService().GetFor(0));
        }
    }
}
