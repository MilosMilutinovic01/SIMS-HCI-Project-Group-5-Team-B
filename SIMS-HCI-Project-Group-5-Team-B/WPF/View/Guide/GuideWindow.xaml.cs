using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide;
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
    /// Interaction logic for GuideWindow.xaml
    /// </summary>
    public partial class GuideWindow : Window
    {
        private GuideService guideService;
        public Guide LogedInGuide;
        public GuideWindow(string username)
        {
            InitializeComponent();

            guideService = new GuideService();

            LogedInGuide = guideService.GetByUsername(username);
        }

        private void AddTourClick(object sender, RoutedEventArgs e)
        {
            //MainFrame.NavigationService.Navigate(new TourCreateForm());
            TourCreateForm tourForm = new TourCreateForm();
            tourForm.Show();
        }

        private void TrackinTourLiveClick(object sender, RoutedEventArgs e)
        {
            TrackingTourLiveWindow trackingTourLive = new TrackingTourLiveWindow();
            trackingTourLive.Show();
        }

        private void TourCancellationClick(object sender, RoutedEventArgs e)
        {
            TourCancelWindow tourCancel = new TourCancelWindow(LogedInGuide);
            tourCancel.Show();
        }

        private void SignOutClick(object sender, RoutedEventArgs e)
        {
            ReviewsWindow reviewsWindow = new ReviewsWindow();
            reviewsWindow.Show();
        }
    }
}