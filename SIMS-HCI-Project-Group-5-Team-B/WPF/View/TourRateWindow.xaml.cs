using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View
{
    /// <summary>
    /// Interaction logic for TourRateWindow.xaml
    /// </summary>
    public partial class TourRateWindow : Window
    {
        public TourGrade TourGrade { get; set; }
        private TourRateViewModel tourRateViewModel { get; set; }

        public TourRateWindow(TourGradeService tourGradeService, int guideGuestId, int tourAttendanceId)
        {
            TourGrade = new TourGrade();
            TourGrade.GuideGuestId = guideGuestId;
            TourGrade.TourAttendanceId = tourAttendanceId;

            tourRateViewModel = new TourRateViewModel(tourGradeService);
            InitializeComponent();
            this.DataContext = this;
        }

        private void RateTourButton_Click(object sender, RoutedEventArgs e)
        {
            if(TourGrade.IsValid)
            {
                tourRateViewModel.Save(TourGrade);
                this.Close();
            }
        }
    }
}
