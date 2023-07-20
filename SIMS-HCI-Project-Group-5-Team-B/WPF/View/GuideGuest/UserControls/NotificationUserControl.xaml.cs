using SIMS_HCI_Project_Group_5_Team_B.DTO;
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
        public ObservableCollection<GuideGuestNotificationDTO> Notifications
        {
            get { return (ObservableCollection<GuideGuestNotificationDTO>)GetValue(NotificationsProperty); }
            set { SetValue(NotificationsProperty, value); }
        }

        public static readonly DependencyProperty NotificationsProperty =
            DependencyProperty.Register("Notifications", typeof(ObservableCollection<GuideGuestNotificationDTO>), typeof(NotificationUserControl),
                new PropertyMetadata(new ObservableCollection<GuideGuestNotificationDTO>()));



        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(NotificationUserControl),
                new PropertyMetadata(null));



        public ICommand JoinCommand
        {
            get { return (ICommand)GetValue(JoinCommandProperty); }
            set { SetValue(JoinCommandProperty, value); }
        }

        public static readonly DependencyProperty JoinCommandProperty =
            DependencyProperty.Register("JoinCommand", typeof(ICommand), typeof(NotificationUserControl),
                new PropertyMetadata(null));



        public ICommand VisitCommand
        {
            get { return (ICommand)GetValue(VisitCommandProperty); }
            set { SetValue(VisitCommandProperty, value); }
        }

        public static readonly DependencyProperty VisitCommandProperty =
            DependencyProperty.Register("VisitCommand", typeof(ICommand), typeof(NotificationUserControl), new PropertyMetadata(null));



        public NotificationUserControl()
        {
            InitializeComponent();
        }
    }
}
