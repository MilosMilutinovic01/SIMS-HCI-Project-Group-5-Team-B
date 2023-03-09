using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.View;
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

namespace SIMS_HCI_Project_Group_5_Team_B
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User user;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            ComboBoxType.SelectedIndex = 0;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if(ComboBoxType.SelectedIndex == 0)//Guide is selected
            {

                //Pozovi funkciju koju hoces za VODICA

            } else if(ComboBoxType.SelectedIndex == 1)//Guide_Guest is selected
            {

                //Pozovi funkciju koju hoces za GOSTA 2
                TourWindow tourWindow = new TourWindow();
                tourWindow.Show();

            }
            else if(ComboBoxType.SelectedIndex == 2)//Owner is selected
            {

                //Pozovi funkciju koju hoces za VLASNIKA

            }
            else if(ComboBoxType.SelectedIndex == 3)//Owner_Guest is selected
            {

                //Pozovi funkciju koju hoces za GOSTA 1

            }
            
        }
    }
}
