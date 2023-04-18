
﻿using SIMS_HCI_Project_Group_5_Team_B.Controller;
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
        private TourController tourController;
        public AppointmentService(IAppointmentRepository appointmentRepository, TourAttendanceService tourAttendanceService)
        {
            this.tourAttendanceService = tourAttendanceService;
            this.appointmentRepository = Injector.Injector.CreateInstance<IAppointmentRepository>();
            this.tourController = new TourController();
            GetTourReference();
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
        
        public AppointmentService(TourController tourController)
        {
            appointmentRepository = Injector.Injector.CreateInstance<IAppointmentRepository>();
            this.tourController = tourController;
            GetTourReference();
        }
        public AppointmentService()
        {
            appointmentRepository = Injector.Injector.CreateInstance<IAppointmentRepository>();
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
        public List<Appointment> GetFinishedToursByYear(int year) 
        {
            GetTourReference();
            List<Appointment> appointments = new List<Appointment>();
            foreach(Appointment a in GetAll())
            {
                if(a.Ended == true && a.Start.Year == year)
                    appointments.Add(a);
            }
            if (appointments.Count() == 0)
                return null;
            return appointments;
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
        public Appointment getById(int id)
        {
            return GetAll().Find(a => a.Id == id);
        }
        private void GetTourReference()
        {
            foreach (var appointment in GetAll())
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
