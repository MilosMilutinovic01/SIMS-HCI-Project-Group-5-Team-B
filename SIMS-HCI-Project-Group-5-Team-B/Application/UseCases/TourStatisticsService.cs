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
    public class TourStatisticsService
    {
        private Repository<TourAttendance> tourAttendanceRepository;
        private AppointmentService appointmentService;

        public TourStatisticsService(AppointmentService appointmentService)
        {
            tourAttendanceRepository = new Repository<TourAttendance>();
            this.appointmentService = appointmentService;
        }


    }
}
