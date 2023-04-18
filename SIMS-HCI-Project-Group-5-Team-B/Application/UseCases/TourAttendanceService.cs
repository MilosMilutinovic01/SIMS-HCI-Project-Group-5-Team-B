using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
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

    }
}
