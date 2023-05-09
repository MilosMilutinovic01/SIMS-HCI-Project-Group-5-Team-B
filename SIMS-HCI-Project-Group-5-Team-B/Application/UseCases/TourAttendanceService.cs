
﻿using SIMS_HCI_Project_Group_5_Team_B.Controller;
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

        private ITourAttendanceRepository tourAttendanceRepository;
        public TourAttendanceService(ITourAttendanceRepository tourAttendanceRepository)
        {
            this.tourAttendanceRepository = tourAttendanceRepository;
        }

        public List<TourAttendance> GetAllFor(int guideGuestId)
        {
            return tourAttendanceRepository.GetAll().FindAll(ta => ta.GuideGuestId == guideGuestId);
        }
        public List<TourAttendance> GetAll()
        {
            return tourAttendanceRepository.GetAll();
        }

        public List<int> FindAllGuestsByAppointment(int appointmentId)
        {
            return tourAttendanceRepository.GetAll().Where(ta => ta.AppointmentId == appointmentId).Select(ta => ta.GuideGuestId).ToList();
        }

        public TourAttendance GetByTourAppointmentId(int id)
        {
            return tourAttendanceRepository.GetAll().Find(ta => ta.AppointmentId == id);
        }

        public TourAttendance GetById(int id)
        {
            return tourAttendanceRepository.GetAll().Find(ta => ta.Id == id);
        }

        public int GetNumberOfGuestsWithVoucher(List<int> allGuestsWithVoucher, int totalGuests)
        {
            int result = 0;
            foreach (TourAttendance ta in GetAll())
                foreach (int id in allGuestsWithVoucher.Distinct())
                    if (ta.GuideGuestId == id)
                        result += 1;
            return result % totalGuests;
        }

        public int GetTotalGuest(int appointmentId)
        {
            int result = 0;
            foreach (TourAttendance ta in GetAll())
            {
                if (ta.AppointmentId == appointmentId)
                    result += ta.PeopleAttending;
            }
            return result;
        }
    }
}
