using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class AppointmentController
    {
        private Repository<Tour> tourRepository;
        private Repository<TourStart> tourStartRepository;
        public AppointmentController()
        {
            tourRepository = new Repository<Tour>();
            tourStartRepository = new Repository<TourStart>();
        }
        public List<Appointment> GetAll()
        {
            List<Tour> tours = tourRepository.GetAll();
            List<Tour> availableTours = new List<Tour>();
            List<Appointment> appointments = new List<Appointment>();
            List<TourStart> tourStart = tourStartRepository.GetAll();
            foreach(TourStart ts in tourStart)
            {
                if(ts.Start.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                {
                    availableTours.AddRange(tours.FindAll(t => t.Id == ts.TourId));
                }
                foreach (Tour t in availableTours)
                {
                    appointments.Add(new Appointment(ts.Start, t.Id,t.Name, 0, t.MaxGuests));
                }
            }
            return appointments;
        }
        //public void Save(Appointment newAppointment)
        //{
        //    appointmentRepository.Save(newAppointment);
        //}
        //public void Delete(Appointment Appointment)
        //{
        //    appointmentRepository.Delete(Appointment);
        //}
        //public void Update(Appointment Appointment)
        //{
        //    appointmentRepository.Update(Appointment);
        //}
        //public List<Appointment> FindBy(string[] propertyNames, string[] values)
        //{
        //    return appointmentRepository.FindBy(propertyNames, values);
        //}
        //public Appointment getById(int id)
        //{
        //    return GetAll().Find(to => to.Id == id);
        //}
        //public int makeId()
        //{
        //    return appointmentRepository.NextId();
        //}
        //public List<Appointment> GetAvailableAppointments()
        //{
        //    List<Appointment> appointments = appointmentRepository.GetAll();
        //    List<Appointment> availableAppointments = new List<Appointment>();
        //    foreach (Appointment appointment in appointments)
        //    {
        //        if (appointment.Start.ToString("MM/dd/yyyy").Equals(DateTime.Now.ToString("MM/dd/yyyy")))
        //        {
        //            availableAppointments.Add(appointment);
        //        }
        //    }
        //    return availableAppointments;
        //}
        //private void GetLocationReference()
        //{
        //    List<Appointment> Appointments = AppointmentRepository.GetAll();
        //    foreach (Appointment Appointment in Appointments)
        //    {
        //        Location location = locationController.getById(Appointment.LocationId);
        //        if (location != null)
        //        {
        //            Appointment.Location = location;
        //        }
        //    }
        //}
    }
}
