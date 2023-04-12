using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class TourAttendanceCSVRepository : ITourAttendanceRepository
    {
        private Repository<TourAttendance> tourAttendanceRepository;

        public TourAttendanceCSVRepository()
        {
            tourAttendanceRepository = new Repository<TourAttendance>();
        }

        public void Delete(TourAttendance tourAttendance)
        {
            tourAttendanceRepository.Delete(tourAttendance);
        }

        public List<TourAttendance> GetAll()
        {
            return tourAttendanceRepository.GetAll();
        }

        public void Save(TourAttendance newTourAttendance)
        {
            tourAttendanceRepository.Save(newTourAttendance);
        }

        public void Update(TourAttendance tourAttendance)
        {
            tourAttendanceRepository.Update(tourAttendance);
        }
    }
}
