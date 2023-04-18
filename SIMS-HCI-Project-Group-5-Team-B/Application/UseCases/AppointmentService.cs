using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class AppointmentService
    {
        private IAppointmentRepository appointmentRepository;
        private TourAttendanceService tourAttendanceService;
        public AppointmentService(IAppointmentRepository appointmentRepository, TourAttendanceService tourAttendanceService)
        {
            this.appointmentRepository = appointmentRepository;
            this.tourAttendanceService = tourAttendanceService;
        }

        public Appointment Find(int appointmentId)
        {
            return appointmentRepository.GetAll().Find(a => a.Id == appointmentId);
        }

        public bool IsEnded(int appointmentId)
        {
            return appointmentRepository.GetAll().Find(a => a.Id == appointmentId).Ended;
        }

        public Tour GetLiveTourFor(int guideGuestId)
        {
            Appointment appointment;
            foreach(var attendance in tourAttendanceService.GetAllFor(guideGuestId))
            {
                appointment = Find(attendance.AppointmentId);
                if(appointment.Started && !appointment.Ended)
                {
                    return appointment.Tour;
                }
            }
            return null;
        }

        public List<Appointment> GetAllHeldFor(int guideGuestId)
        {
            List<Appointment> heldAppointments = new List<Appointment>();
            Appointment appointment;
            foreach(var tourAttendance in tourAttendanceService.GetAllFor(guideGuestId))
            {
                appointment = Find(tourAttendance.AppointmentId);
                if (appointment.Ended)
                {
                    heldAppointments.Add(appointment);
                }
            }
            return heldAppointments;
        }

        public List<TourAttendance> GetAllFor(int guideGuestId)
        {
            return tourAttendanceService.GetAllFor(guideGuestId);
        }

        public TourAttendance GetAttendance(int guideGuestId, int appointmentId)
        {
            foreach (var tourAttendance in tourAttendanceService.GetAllFor(guideGuestId))
            {
                if (tourAttendance.AppointmentId == appointmentId)
                {
                    return tourAttendance;
                }
            }
            return null;
        }
    }
}
