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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for AccommodationLocationSuggestionsWindow.xaml
    /// </summary>
    public partial class AccommodationLocationSuggestionsWindow : Window
    {
        public AccommodationLocationSuggestionsWindow()
        {
            InitializeComponent();
        }

        public void AddAccommodation_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void DeleteAccommodation_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
