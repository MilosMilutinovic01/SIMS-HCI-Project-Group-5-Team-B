using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class MyToursViewModel
    {
        #region fields
        public string Year { get; set; }
        public ObservableCollection<Appointment> MostVisitedAppointment { get; set; }
        public Appointment SelectedAppointment { get; set; }
        public ObservableCollection<Appointment> FinishedAppointments { get; set; }
        public int userId;

        private AppointmentService appointmentService;
        private TourAttendanceService tourAttendanceService;
        #endregion
        public MyToursViewModel(AppointmentService appointmentService, TourAttendanceService tourAttendanceService, int userId)
        {
            this.userId = userId;
            this.appointmentService = appointmentService;
            this.tourAttendanceService = tourAttendanceService;

            MostVisitedAppointment = new ObservableCollection<Appointment>();
            FinishedAppointments = new ObservableCollection<Appointment>();

            RefreshData();
        }

        private void RefreshData()
        {
            MostVisitedAppointment.Clear();
            FinishedAppointments.Clear();

            //if ((ComboBoxItem)ComboBoxYear.SelectedItem != null)
            //    Year = ((ComboBoxItem)ComboBoxYear.SelectedItem).Content.ToString();
            //else
            //    Year = null;
            //MostVisitedAppointment.Add(appointmentService.GetMostVisitedTour(Convert.ToInt32(Year), userId));

            //if (appointmentService.GetFinishedToursByYear(Convert.ToInt32(Year), userId) == null)
            //    FinishedAppointments.Add(new Appointment());
            //else
            //    foreach (Appointment t in appointmentService.GetFinishedToursByYear(Convert.ToInt32(Year), userId))
            //        FinishedAppointments.Add(t);
        }
    }
}
