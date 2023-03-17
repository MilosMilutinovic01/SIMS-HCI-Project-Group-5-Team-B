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

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for TourWindow.xaml
    /// </summary>
    public partial class TourWindow : Window
    {
        public string Location { get; set; }
        public string TourLength { get; set; }
        public string Lang { get; set; }
        public string NumberOfPeople { get; set; }

        public TourWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void SearchButtom_Click(object sender, RoutedEventArgs e)
        {
            //TODO Search
        }
    }
}
