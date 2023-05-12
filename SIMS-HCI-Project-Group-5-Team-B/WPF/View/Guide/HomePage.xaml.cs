using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePageViewModel homePageViewModel { get; set; }
        private TourService tourService;
        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;
        private TourGradeService tourGradeService;
        private void LoadData()
        {
            KeyPointCSVRepository keyPointCSVRepository = new KeyPointCSVRepository();
            LocationCSVRepository locationCSVRepository = new LocationCSVRepository();
            TourCSVRepository tourCSVRepository = new TourCSVRepository(keyPointCSVRepository, locationCSVRepository);

            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            TourGradeCSVRepository tourGradeCSVRepository = new TourGradeCSVRepository();
            AppointmentCSVRepository appointmentCSVRepository = new AppointmentCSVRepository(tourCSVRepository);

            tourService = new TourService(tourCSVRepository);
            tourAttendanceService = new TourAttendanceService(tourAttendanceCSVRepository);
            tourGradeService = new TourGradeService(tourGradeCSVRepository);
            appointmentService = new AppointmentService(appointmentCSVRepository, tourAttendanceService);
        }
        public HomePage(SIMS_HCI_Project_Group_5_Team_B.Domain.Models.Guide guide, Frame frame)
        {
            homePageViewModel = new HomePageViewModel(guide, frame);
            InitializeComponent();
            this.DataContext = this.homePageViewModel;
            LoadData();
        }
    }
}
