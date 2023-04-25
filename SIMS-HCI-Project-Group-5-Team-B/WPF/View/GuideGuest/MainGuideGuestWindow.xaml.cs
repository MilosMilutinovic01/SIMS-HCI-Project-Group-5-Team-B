using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest;
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
            mainGuideGuestWindowViewModel = new MainGuideGuestWindowViewModel(MainFrame.NavigationService);
        }

        private void NavigationGrid_Click(object sender, RoutedEventArgs e)
        {
            mainGuideGuestWindowViewModel.Navigate((e.Source as Button).Name);
        }
    }
}
