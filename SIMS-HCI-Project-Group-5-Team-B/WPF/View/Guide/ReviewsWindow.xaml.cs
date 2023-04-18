using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for ReviewsWindow.xaml
    /// </summary>
    public partial class ReviewsWindow : Window
    {
        private ReviewsViewModel reviewsViewModel;
        private TourGradeService tourGradeService;
        private TourAttendanceService tourAttendanceService;
        public ReviewsWindow(TourGradeService tourGradeService, int userId, TourAttendanceService tourAttendanceService)
        {
            InitializeComponent();

            this.tourAttendanceService = tourAttendanceService;
            this.tourGradeService = tourGradeService;
            reviewsViewModel = new ReviewsViewModel(tourGradeService, userId, tourAttendanceService);
            this.DataContext = reviewsViewModel;
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            this.DataContext = reviewsViewModel;
        }
    }
}
