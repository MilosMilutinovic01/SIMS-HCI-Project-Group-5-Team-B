using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AccommodationFormWithPopularLocation.xaml
    /// </summary>
    public partial class AccommodationFormWithPopularLocation : Window
    {
        public AccommodationFormWithPopularLocation(Location selectedLocation, ObservableCollection<Accommodation> accommodationsOfLogedInOwner,Owner owner,AccommodationService accommodationService)
        {
            InitializeComponent();
            this.DataContext = new AccommodationFormWithPopularLocationViewModel(selectedLocation,accommodationsOfLogedInOwner,owner,accommodationService);
        }
    }
}
