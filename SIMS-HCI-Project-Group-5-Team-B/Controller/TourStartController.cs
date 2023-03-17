using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class TourStartController
    {
        private Repository<TourStart> tourStartRepository;

        public TourStartController()
        {
            tourStartRepository = new Repository<TourStart>();
        }

        public List<TourStart> GetAll()
        {
            return tourStartRepository.GetAll();
        }
        public void Save(TourStart newTourStart)
        {
            tourStartRepository.Save(newTourStart);
        }
        public void SaveAll(List<TourStart> TourStart)
        {
            tourStartRepository.SaveAll(TourStart);
        }
        public void Delete(TourStart TourStart)
        {
            tourStartRepository.Delete(TourStart);
        }
        public void Update(TourStart TourStart)
        {
            tourStartRepository.Update(TourStart);
        }

        public List<TourStart> FindBy(string[] propertyNames, string[] values)
        {
            return tourStartRepository.FindBy(propertyNames, values);
        }

        public List<TourStart> getByTourId(int id)
        {
            return GetAll().FindAll(kp => kp.TourId == id);
        }
    }
}
