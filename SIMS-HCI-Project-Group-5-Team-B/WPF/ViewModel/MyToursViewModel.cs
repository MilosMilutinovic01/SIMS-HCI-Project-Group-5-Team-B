using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class MyToursViewModel
    {
        #region fields
        public string SelectedYear 
        {
            get { return "2023"; }
            set
            {
                if(SelectedYear != value) 
                {
                    SelectedYear = value;
                    RefreshData();
                }
            }
        }
        public RelayCommand ShowStatisticsCommand { get; set; }
        public List<string> Years { get; set; }
        public ObservableCollection<Appointment> MostVisitedAppointment { get; set; }
        public Appointment SelectedAppointment { get; set; }
        public ObservableCollection<Appointment> FinishedAppointments { get; set; }
        public int userId;

        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;


        #endregion

        #region actions
        private bool CanExecute_NavigateCommand()
        {
            return true;
        }

        private void Execute_ShowStatisticsCommand()
        {
            if (SelectedAppointment == null)
            {
                MessageBox.Show("You must select appointment!");
                return;
            }
            TourStatistics tourStatistics = new TourStatistics(SelectedAppointment.Id, tourAttendanceService);
            tourStatistics.Show();
        }
        #endregion
        public MyToursViewModel(int userId)
        {
            this.userId = userId;
            KeyPointCSVRepository keyPointCSVRepository = new KeyPointCSVRepository();
            LocationCSVRepository locationCSVRepository = new LocationCSVRepository();
            TourCSVRepository tourCSVRepository = new TourCSVRepository();
            TourAttendanceCSVRepository tourAttendanceCSVRepository = new TourAttendanceCSVRepository();
            TourGradeCSVRepository tourGradeCSVRepository = new TourGradeCSVRepository();
            AppointmentCSVRepository appointmentCSVRepository = new AppointmentCSVRepository();

            this.tourAttendanceService = new TourAttendanceService();
            this.appointmentService = new AppointmentService();

            MostVisitedAppointment = new ObservableCollection<Appointment>();
            FinishedAppointments = new ObservableCollection<Appointment>();

            this.ShowStatisticsCommand = new RelayCommand(Execute_ShowStatisticsCommand, CanExecute_NavigateCommand);
            Years = new List<string>();
            Years.Add("2023");

            RefreshData();
        }

        private void RefreshData()
        {
            MostVisitedAppointment.Clear();
            FinishedAppointments.Clear();

            MostVisitedAppointment.Add(appointmentService.GetMostVisitedTour(Convert.ToInt32(SelectedYear), userId));

            if (appointmentService.GetFinishedToursByYear(Convert.ToInt32(SelectedYear), userId) == null)
                FinishedAppointments.Add(new Appointment());
            else
                foreach (Appointment a in appointmentService.GetFinishedToursByYear(Convert.ToInt32(SelectedYear), userId))
                    FinishedAppointments.Add(a);
        }
    }
}
