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
        private RenovationRequestViewModel viewModel;
        public RenovationRequestForm(int accommodationid)
        {
            InitializeComponent();
            viewModel = new RenovationRequestViewModel(accommodationid);
            this.DataContext = viewModel;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SetLevel(sender);
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Send();
            Close();
        }
    }
}
