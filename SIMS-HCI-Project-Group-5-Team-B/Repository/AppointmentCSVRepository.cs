using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class AppointmentCSVRepository : IAppointmentRepository
    {
        Repository<Appointment> appointmentRepository;

        public AppointmentCSVRepository()
        {
            appointmentRepository = new Repository<Appointment>();
        }

        public void Delete(Appointment appointment)
        {
            appointmentRepository.Delete(appointment);
        }

        public List<Appointment> GetAll()
        {
            return appointmentRepository.GetAll();
        }

        public void Save(Appointment appointment)
        {
            appointmentRepository.Save(appointment);
        }

        public void SaveAll(List<Appointment> appointments)
        {
            appointmentRepository.SaveAll(appointments);
        }

        public void Update(Appointment appointment)
        {
            appointmentRepository.Update(appointment);
        }
    }
}
