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
    /// Interaction logic for GradesWindow.xaml
    /// </summary>
    public partial class GradesWindow : Window
    {
        private OwnerGuestGradesViewModel ownerGuestGradesViewModel;
        public GradesWindow(int ownerGuestId)
        {
            InitializeComponent();
            ownerGuestGradesViewModel = new OwnerGuestGradesViewModel(ownerGuestId);
            this.DataContext = ownerGuestGradesViewModel;
        }

        private void Details_Click(object sender, RoutedEventArgs e)
        {
            GradeDetailsWindow gradeDetailsWindow = new GradeDetailsWindow(ownerGuestGradesViewModel.SelectedGrade);
            gradeDetailsWindow.Show();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
