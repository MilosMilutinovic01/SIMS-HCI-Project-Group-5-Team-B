using Microsoft.VisualBasic;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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
    /// <summary>
    /// Interaction logic for MyTours.xaml
    /// </summary>
    public partial class MyTours : Window
    {
        public string Year { get; set; }
        public ObservableCollection<Appointment> MostVisitedTour { get; }
        public Appointment SelectedTour { get; set; }
        public ObservableCollection<Appointment> FinishedTours { get; }
        
        private TourAttendanceService tourAttendanceService;
        private AppointmentService appointmentService;
        public MyTours()
        {
            InitializeComponent();
            this.DataContext = this;
            tourAttendanceService = new TourAttendanceService();
            appointmentService = new AppointmentService();
            MostVisitedTour = new ObservableCollection<Appointment>();
            FinishedTours = new ObservableCollection<Appointment>();

            MostVisitedTour.Add(tourAttendanceService.GetMostVisitedTour());
            foreach(Appointment t in appointmentService.GetFinishedToursByYear(Convert.ToInt32(Year)))
            {
                FinishedTours.Add(t);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Year = ((ComboBoxItem)ComboBoxYear.SelectedItem).Content.ToString();
        }

        private void ShowStatsButton_Click(object sender, RoutedEventArgs e)
        {
            TourStatistics tourStatistics = new TourStatistics(SelectedTour.Id);
            tourStatistics.Show();
        }
    }
}
