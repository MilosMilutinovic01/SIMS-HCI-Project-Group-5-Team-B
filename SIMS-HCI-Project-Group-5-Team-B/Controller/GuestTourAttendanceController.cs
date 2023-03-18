using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class GuestTourAttendanceController
    {
        private Repository<GuestTourAttendance> guestTourAttendanceRepository;

        public GuestTourAttendanceController()
        {
            guestTourAttendanceRepository = new Repository<GuestTourAttendance>();
        }

        public List<GuestTourAttendance> GetAll()
        {
            return guestTourAttendanceRepository.GetAll();
        }
        public void Save(GuestTourAttendance newGuestTourAttendance)
        {
            guestTourAttendanceRepository.Save(newGuestTourAttendance);
        }
        public void Delete(GuestTourAttendance guestTourAttendance)
        {
            guestTourAttendanceRepository.Delete(guestTourAttendance);
        }
        public void Update(GuestTourAttendance guestTourAttendance)
        {
            guestTourAttendanceRepository.Update(guestTourAttendance);
        }

        public List<GuestTourAttendance> FindBy(string[] propertyNames, string[] values)
        {
            return guestTourAttendanceRepository.FindBy(propertyNames, values);
        }

        public GuestTourAttendance getById(int id)
        {
            return GetAll().Find(gta => gta.Id == id);
        }
    }
}
