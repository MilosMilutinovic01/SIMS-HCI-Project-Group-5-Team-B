using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.DTO
{
    public enum TourAttendanceStatus { LIVE, WAITING, HELD_GRADED, HELD_NOT_GRADED}
    public class GuideGuestTourAttendanceDTO
    {
        public TourAttendance TourAttendance { get; set; }
        public Appointment Appointment { get; set; }
        public TourAttendanceStatus Status { get; set; }
        public GuideGuestTourAttendanceDTO(TourAttendance tourAttendance, Appointment appointment)
        {
            TourAttendance = tourAttendance;
            Appointment = appointment;
            TourGradeService tourGradeService = new TourGradeService();
            if (Appointment.Started)
            {
                if(!Appointment.Ended)
                {
                    Status = TourAttendanceStatus.LIVE;
                }
                else
                {
                    if(tourGradeService.IsRated(tourAttendance.GuideGuestId, tourAttendance.Id))
                    {
                        Status = TourAttendanceStatus.HELD_GRADED;
                    }
                    else
                    {
                        Status = TourAttendanceStatus.HELD_NOT_GRADED;
                    }
                }
            }
            else
            {
                Status = TourAttendanceStatus.WAITING;
            }
            Status = TourAttendanceStatus.LIVE;
        }
    }
}
