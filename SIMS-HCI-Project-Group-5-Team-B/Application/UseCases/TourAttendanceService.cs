using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
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
        private Repository<TourAttendance> tourAttendanceRepository;
        private AppointmentService appointmentService;

        public TourAttendanceService()
        {
            tourAttendanceRepository = new Repository<TourAttendance>();
            appointmentService = new AppointmentService();
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

        public List<TourAttendance> FindBy(string[] propertyNames, string[] values)
        {
            return tourAttendanceRepository.FindBy(propertyNames, values);
        }

        public List<int> FindAllGuestsByAppointment(int appointmentId)
        {
            return GetAll().Where(ta => ta.TourAppointmentId == appointmentId).Select(ta => ta.GuideGuestId).ToList();
        }

        public TourAttendance GetByTourAppointmentId(int id)
        {
            return GetAll().Find(ta => ta.TourAppointmentId == id);
        }

        public Appointment GetMostVisitedTour()//treba za odredjenu godinu
        {
            int id = 0;
            int people = 0;
            Appointment appointment = new Appointment();
            foreach(TourAttendance ta in tourAttendanceRepository.GetAll())
            {
                if (ta.PeopleAttending > people)
                {
                    id = ta.TourAppointmentId;
                    people = ta.PeopleAttending;
                }
            }
            return appointmentService.getById(id); ;
        }
    }
}
