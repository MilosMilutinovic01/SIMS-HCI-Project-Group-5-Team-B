using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class GuideGuestViewModel
    {
        private AppointmentService appointmentService;
        private TourGradeService tourGradeService;
        public GuideGuestViewModel(AppointmentService appointmentService, TourGradeService tourGradeService)
        {
            this.appointmentService = appointmentService;
            this.tourGradeService = tourGradeService;
        }

        public List<Appointment> GetAllHeldFor(int guideGuestId)
        {
            return appointmentService.GetAllHeldFor(guideGuestId);
        }

        public Tour getLiveTour(int guideGuestId)
        {
            return appointmentService.GetLiveTourFor(guideGuestId);
        }

        public void RateTour(Appointment selectedHeldAppointment, int guideGuestId)
        {
            if(selectedHeldAppointment == null)
            {
                MessageBox.Show("Select tour!");
                return;
            }

            TourAttendance tourAttendance = appointmentService.GetAttendance(guideGuestId, selectedHeldAppointment.Id);
            if(tourAttendance == null)
            {
                MessageBox.Show("Internal error!");
                return;
            }

            if(tourGradeService.IsRated(guideGuestId, tourAttendance.Id))
            {
                MessageBox.Show("Selected tour is already reted!");
                return;
            }
            else
            {
                TourRateWindow tourRateWindow = new TourRateWindow(tourGradeService, guideGuestId, tourAttendance.Id);
                tourRateWindow.Show();
                return;
            }
        }
    }
}
