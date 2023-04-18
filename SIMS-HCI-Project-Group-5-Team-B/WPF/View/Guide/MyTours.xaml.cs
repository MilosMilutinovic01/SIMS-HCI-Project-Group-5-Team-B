using Microsoft.VisualBasic;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
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

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    public partial class MyTours : Window
    {
        public string Year { get; set; }
        public ObservableCollection<Appointment> MostVisitedAppointment { get; set; }
        public Appointment SelectedAppointment { get; set; }
        public ObservableCollection<Appointment> FinishedAppointments { get; set; }
        public int userId;

        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;
        
        public MyTours(AppointmentService appointmentService, TourAttendanceService tourAttendanceService, int userId)
        {
            InitializeComponent();
            this.DataContext = this;

            this.userId = userId;
            this.appointmentService = appointmentService;
            this.tourAttendanceService = tourAttendanceService;

            MostVisitedAppointment = new ObservableCollection<Appointment>();
            FinishedAppointments = new ObservableCollection<Appointment>();

            RefreshData();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            MostVisitedAppointment.Clear();
            FinishedAppointments.Clear();

            if ((ComboBoxItem)ComboBoxYear.SelectedItem != null)
                Year = ((ComboBoxItem)ComboBoxYear.SelectedItem).Content.ToString();
            else
                Year = null;

            MostVisitedAppointment.Add(appointmentService.GetMostVisitedTour(Convert.ToInt32(Year), userId));

            if (appointmentService.GetFinishedToursByYear(Convert.ToInt32(Year), userId) == null)
                FinishedAppointments.Add(new Appointment());
            else
                foreach (Appointment t in appointmentService.GetFinishedToursByYear(Convert.ToInt32(Year), userId))
                    FinishedAppointments.Add(t);
        }

        private void ShowStatsButton_Click(object sender, RoutedEventArgs e)
        {
            TourStatistics tourStatistics = new TourStatistics(SelectedAppointment.Id, tourAttendanceService);
            tourStatistics.Show();
        }
    }
}
