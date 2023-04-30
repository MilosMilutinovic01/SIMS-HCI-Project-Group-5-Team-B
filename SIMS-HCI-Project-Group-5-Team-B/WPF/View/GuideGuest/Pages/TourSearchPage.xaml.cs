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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.Pages
{
    /// <summary>
    /// Interaction logic for TourSearchPage.xaml
    /// </summary>
    public partial class TourSearchPage : Page
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public TourSearchPage()
        {
            this.DataContext = this;
            InitializeComponent();
            Tours = new ObservableCollection<Tour>((new TourService(new TourCSVRepository(new KeyPointCSVRepository(), new LocationCSVRepository()))).GetAll());
        }
    }
}
