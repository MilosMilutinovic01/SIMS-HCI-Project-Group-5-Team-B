using SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.Pages;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest
{
    /// <summary>
    /// Interaction logic for MainGuideGuestWindow.xaml
    /// </summary>
    public partial class MainGuideGuestWindow : Window
    {
        private MainGuideGuestWindowViewModel mainGuideGuestWindowViewModel;
        public MainGuideGuestWindow()
        {
            InitializeComponent();
            mainGuideGuestWindowViewModel = new MainGuideGuestWindowViewModel();
            this.DataContext = mainGuideGuestWindowViewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.NavigationService.CanGoBack)
            {
                MainFrame.NavigationService.GoBack();
            }
        }

        private void NotificationsButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotificationPopup.IsOpen == false)
            {
                NotificationPopup.PlacementTarget = NotificationsButton;
                NotificationPopup.IsOpen = true;
                NotificationPopup.StaysOpen = false;
            }
            else
            {
                NotificationPopup.IsOpen = false;
                NotificationPopup.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Content = mainGuideGuestWindowViewModel.GetTourSearchPage();
        }

        private void YourProfileButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Content = mainGuideGuestWindowViewModel.GetYourProfilePage();
        }
    }
}
