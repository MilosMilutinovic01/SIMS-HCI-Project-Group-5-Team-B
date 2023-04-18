using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class TourAttendanceService
    {
        private AppointmentService appointmentService;

        private ITourAttendanceRepository tourAttendanceRepository;
        public TourAttendanceService(ITourAttendanceRepository tourAttendanceRepository)
        {
            this.tourAttendanceRepository = tourAttendanceRepository;
            appointmentService = new AppointmentService();
        }

        public List<TourAttendance> GetAllFor(int guideGuestId)
        {
            return tourAttendanceRepository.GetAll().FindAll(ta => ta.GuideGuestId == guideGuestId);
        }

        public List<TourAttendance> GetAll()
        {
            return tourAttendanceRepository.GetAll();
        }
        public void Save(TourAttendance newTourAttendance)
        {
            tourAttendanceRepository.Save(newTourAttendance);
        }
        public void Delete(TourAttendance tourAttendance)
        {
            tourAttendanceRepository.Delete(tourAttendance);
        }
        public void Update(TourAttendance tourAttendance)
        {
            tourAttendanceRepository.Update(tourAttendance);
        }

        public List<int> FindAllGuestsByAppointment(int appointmentId)
        {
            return GetAll().Where(ta => ta.AppointmentId == appointmentId).Select(ta => ta.GuideGuestId).ToList();
        }

        public TourAttendance GetByTourAppointmentId(int id)
        {
            return GetAll().Find(ta => ta.AppointmentId == id);
        }

        public int GetTotalGuest(int appointmentId) 
        {
            int result = 0;
            foreach (TourAttendance ta in tourAttendanceRepository.GetAll())
            {
                if (ta.AppointmentId == appointmentId)
                    result += ta.PeopleAttending;
            }
            return result;
        }
        public Appointment GetMostVisitedTour(int year)
        {
            int id = 0;
            int people = 0;
            if (appointmentService.GetFinishedToursByYear(year) == null)
                return new Appointment();

            foreach(Appointment appointment in appointmentService.GetFinishedToursByYear(year))
            {
                if (people < GetTotalGuest(appointment.Id))
                {
                    id = appointment.Id;
                    people = GetTotalGuest(appointment.Id);
                }
            }

            if (id == 0)
                return null;
            return appointmentService.getById(id); ;
        }
    }
}
