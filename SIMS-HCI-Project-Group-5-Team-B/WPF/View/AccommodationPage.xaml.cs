using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.View;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for AccommodationPage.xaml
    /// </summary>
    public partial class AccommodationPage : Page
    {
        private AccommodationService accommodationService;
        private LocationController locationController;
        private OwnerService ownerService;
        private YearlyAccommodationStatisticsService yearlyAccommodationStatisticsService;
        public ObservableCollection<Accommodation> AccomodationsOfLogedInOwner { get; set; }
        public Owner LogedInOwner { get; set; }
       
        public AccommodationPage(int ownerId)
        {
            InitializeComponent();
            DataContext = this;
            locationController = new LocationController();
            ownerService = new OwnerService();
            yearlyAccommodationStatisticsService = new YearlyAccommodationStatisticsService();
            this.accommodationService = new AccommodationService(locationController, ownerService);
            AccomodationsOfLogedInOwner = new ObservableCollection<Accommodation>(accommodationService.GetAccommodationsOfLogedInOwner(ownerId));
            this.LogedInOwner = ownerService.getById(ownerId);
            
        }

        private void Create_Accommodation_Click(object sender, RoutedEventArgs e)
        {
            AccommodationForm accommodationForm = new AccommodationForm(AccomodationsOfLogedInOwner, LogedInOwner);
            accommodationForm.Show();
        }

        private void Suggestions_Button_Click(object sender, RoutedEventArgs e)
        {
            AccommodationLocationSuggestionsWindow accommodationLocationSuggestionsWindow = new AccommodationLocationSuggestionsWindow(accommodationService,yearlyAccommodationStatisticsService,LogedInOwner,AccomodationsOfLogedInOwner);
            accommodationLocationSuggestionsWindow.Show();
        }
    }
}
