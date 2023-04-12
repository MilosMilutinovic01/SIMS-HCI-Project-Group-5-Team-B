using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface ITourAttendanceRepository
    {
        public void Save(TourAttendance newTourAttendance);

        public void Delete(TourAttendance tourAttendance);

        public void Update(TourAttendance tourAttendance);

        public List<TourAttendance> GetAll();
    }
}
