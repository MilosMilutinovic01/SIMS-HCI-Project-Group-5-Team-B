using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System.Collections.Generic;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class TourAttendanceController
    {
        private Repository<TourAttendance> tourAttendanceRepository;
        private TourController tourController;
        public TourAttendanceController(TourController tourController)
        {
            tourAttendanceRepository = new Repository<TourAttendance>();
            this.tourController = tourController;
            GetTourReference();
        }
        public TourAttendanceController()
        {
            tourAttendanceRepository = new Repository<TourAttendance>();
            this.tourController = new TourController();
            GetTourReference();
        }
        public List<TourAttendance> GetAll()
        {
            return tourAttendanceRepository.GetAll();
        }
        public void Save(TourAttendance newTourAttendance)
        {
            tourAttendanceRepository.Save(newTourAttendance);
        }
        public void SaveAll(List<TourAttendance> newTourAttendances)
        {
            tourAttendanceRepository.SaveAll(newTourAttendances);
        }
        public void Delete(TourAttendance tourAttendance)
        {
            tourAttendanceRepository.Delete(tourAttendance);
        }
        public void Update(TourAttendance tourAttendance)
        {
            tourAttendanceRepository.Update(tourAttendance);
        }
        public List<TourAttendance> FindBy(string[] propertyNames, string[] values)
        {
            return tourAttendanceRepository.FindBy(propertyNames, values);
        }
        public TourAttendance getById(int id)
        {
            return GetAll().Find(to => to.Id == id);
        }
        public int makeId()
        {
            return tourAttendanceRepository.NextId();
        }
        private void GetTourReference()
        {
            List<TourAttendance> tourAttendances = tourAttendanceRepository.GetAll();
            foreach (TourAttendance ta in tourAttendances)
            {
                Tour tour = tourController.getById(ta.TourId);
                if (tour != null)
                {
                    ta.Tour = tour;
                }
            }
        }
    }
}
