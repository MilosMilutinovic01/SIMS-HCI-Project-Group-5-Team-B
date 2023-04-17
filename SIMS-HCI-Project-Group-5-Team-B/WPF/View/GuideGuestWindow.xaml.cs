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
    /// Interaction logic for GuideGuestWindow.xaml
    /// </summary>
    public partial class GuideGuestWindow : Window
    {
        public ObservableCollection<Appointment> HeldTours { get; set; }
        public ObservableCollection<Tour> LiveTour { get; set; }
        public Appointment SelectedHeldTour { get; set; }

        private GuideGuestViewModel guideGuestViewModel { get; set; }
        private User loggedUser { get; set; }
        private TourGradeService tourGradeService { get; set; }
        public GuideGuestWindow(User loggedUser, TourGradeService tourGradeService, AppointmentService appointmentService)
        {
            InitializeComponent();
            this.DataContext = this;

            this.loggedUser = loggedUser;
            this.tourGradeService = tourGradeService;

            this.guideGuestViewModel = new GuideGuestViewModel(appointmentService, tourGradeService);

            HeldTours = new ObservableCollection<Appointment>(guideGuestViewModel.GetAllHeldFor(loggedUser.Id));
            LiveTour = new ObservableCollection<Tour>();
            LiveTour.Add(guideGuestViewModel.getLiveTour(loggedUser.Id));
        }

        private void TrackTourButton_Click(object sender, RoutedEventArgs e)
        {
            if(LiveTour.Count == 0)
            {
                return;
            }
            foreach(var keyPoint in LiveTour[0].KeyPoints)
            {
                if(!keyPoint.Selected)
                {
                    MessageBox.Show("Tour is currently at " + keyPoint.Name);
                    break;
                }
            }
        }

        private void RateTourButton_Click(object sender, RoutedEventArgs e)
        {
            guideGuestViewModel.RateTour(SelectedHeldTour, loggedUser.Id);
        }
    }
}
