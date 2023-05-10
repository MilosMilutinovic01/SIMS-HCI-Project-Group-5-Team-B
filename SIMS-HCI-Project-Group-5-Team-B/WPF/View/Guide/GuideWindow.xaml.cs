﻿using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
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
        
        private TourService tourService;
        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;
        private TourGradeService tourGradeService;
        public int userId;
        public GuideWindow(int userId)
        {
            this.userId = userId;
            InitializeComponent();
            LoadData();
            guideService = new GuideService();
        }

        private void LoadData()
        {
            tourService = new TourService();
            tourAttendanceService = new TourAttendanceService();
            tourGradeService = new TourGradeService();
            appointmentService = new AppointmentService();
        }
        private void AddTourClick(object sender, RoutedEventArgs e)
        {
            TourCreateForm tourForm = new TourCreateForm(tourService,appointmentService);
            tourForm.Show();
        }

        private void TrackinTourLiveClick(object sender, RoutedEventArgs e)
        {
            TrackingTourLiveWindow trackingTourLive = new TrackingTourLiveWindow(appointmentService, userId);
            trackingTourLive.Show();
        }

        private void TourCancellationClick(object sender, RoutedEventArgs e)
        {
            TourCancelWindow tourCancel = new TourCancelWindow(appointmentService, userId);
            tourCancel.Show();
        }

        private void SignOutClick(object sender, RoutedEventArgs e)
        {
            ReviewsWindow reviewsWindow = new ReviewsWindow(tourGradeService, userId, tourAttendanceService);
            reviewsWindow.Show();
        }

        private void MyToursClick(object sender, RoutedEventArgs e)
        {
            MyTours myTours = new MyTours(appointmentService, tourAttendanceService, userId);
            myTours.Show();
        }
    }
}