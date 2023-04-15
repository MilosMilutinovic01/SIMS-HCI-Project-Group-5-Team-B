using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class AppointmentService
    {
        private Repository<Appointment> appointmentRepository;
        private TourService tourController;
        public AppointmentService(TourService tourController)
        {
            appointmentRepository = new Repository<Appointment>();
            this.tourController = tourController;
            GetTourReference();
        }
        public AppointmentService()
        {
            appointmentRepository = new Repository<Appointment>();
            this.tourController = new TourService();
            GetTourReference();
        }
        public List<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }
        public List<Appointment> GetAllAvaillable()
        {
            return appointmentRepository.GetAll().FindAll(a => a.Start.Date == DateTime.Now.Date && a.Cancelled == false);
        }
        public List<Appointment> GetUpcoming()
        {
            GetTourReference();
            return appointmentRepository.GetAll().Where(a => (a.Start - DateTime.Now).TotalHours >= 48 && a.Cancelled == false).ToList();
        }
        public void Save(Appointment newAppointment)
        {
            appointmentRepository.Save(newAppointment);
        }
        public void SaveAll(List<Appointment> newAppointment)
        {
            appointmentRepository.SaveAll(newAppointment);
        }
        public void Delete(Appointment appointment)
        {
            appointmentRepository.Delete(appointment);
        }
        public void Update(Appointment appointment)
        {
            appointmentRepository.Update(appointment);
        }
        public List<Appointment> FindBy(string[] propertyNames, string[] values)
        {
            return appointmentRepository.FindBy(propertyNames, values);
        }
        public Appointment getById(int id)
        {
            return GetAll().Find(a => a.Id == id);
        }
        public int makeId()
        {
            return appointmentRepository.NextId();
        }
        private void GetTourReference()
        {
            List<Appointment> appointments = appointmentRepository.GetAll();
            foreach (var appointment in appointments)
            {
                Tour tour = tourController.getById(appointment.TourId);
                if (tour != null)
                {
                    appointment.Tour = tour;
                }
            }
        }
    }
}
