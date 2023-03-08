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
using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    /// <summary>
    /// Interaction logic for OwnerWindow.xaml
    /// </summary>
    public partial class OwnerWindow : Window
    {

        Repository<Accommodation> accommodationRepository;
        Repository<Location> locationRepository;
        
        public OwnerWindow()
        {
            InitializeComponent();
            accommodationRepository = new Repository<Accommodation>();
            locationRepository = new Repository<Location>();
           
        }

        private void Create_Accommodation_Click(object sender, RoutedEventArgs e)
        {
            
            AccommodationForm accommodationForm = new AccommodationForm(accommodationRepository,locationRepository);
            accommodationForm.Show();
        }
    }
}
