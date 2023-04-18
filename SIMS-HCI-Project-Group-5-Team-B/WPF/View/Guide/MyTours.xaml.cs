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
        public ObservableCollection<Appointment> MostVisitedTour { get; set; }
        public Appointment SelectedTour { get; set; }
        public ObservableCollection<Appointment> FinishedTours { get; set; }

        public AppointmentService appointmentService;
        
        public MyTours(AppointmentService appointmentService)
        {
            InitializeComponent();
            this.DataContext = this;

            this.appointmentService = appointmentService;

            MostVisitedTour = new ObservableCollection<Appointment>();
            FinishedTours = new ObservableCollection<Appointment>();

            MostVisitedTour.Add(appointmentService.GetMostVisitedTour(Convert.ToInt32(Year)));
            if (appointmentService.GetFinishedToursByYear(Convert.ToInt32(Year)) == null)
                FinishedTours.Add(new Appointment());
            else
            {
                foreach (Appointment t in appointmentService.GetFinishedToursByYear(Convert.ToInt32(Year)))
                {
                    FinishedTours.Add(t);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MostVisitedTour.Clear();
            FinishedTours.Clear();
            Year = ((ComboBoxItem)ComboBoxYear.SelectedItem).Content.ToString();
            MostVisitedTour.Add(appointmentService.GetMostVisitedTour(Convert.ToInt32(Year)));
            if (appointmentService.GetFinishedToursByYear(Convert.ToInt32(Year)) == null)
                FinishedTours.Add(new Appointment());
            else
            {
                foreach (Appointment t in appointmentService.GetFinishedToursByYear(Convert.ToInt32(Year)))
                {
                    FinishedTours.Add(t);
                }
            }
        }

        private void ShowStatsButton_Click(object sender, RoutedEventArgs e)
        {
            TourStatistics tourStatistics = new TourStatistics(SelectedTour.Id, appointmentService);
            tourStatistics.Show();
        }
    }
}
