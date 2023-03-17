using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class TourImageController
    {
        private Repository<TourImage> tourImageRepository;

        public TourImageController()
        {
            tourImageRepository = new Repository<TourImage>();
        }

        public List<TourImage> GetAll()
        {
            return tourImageRepository.GetAll();
        }
        public void Save(TourImage newTourImage)
        {
            tourImageRepository.Save(newTourImage);
        }
        public void SaveAll(List<TourImage> TourImage)
        {
            tourImageRepository.SaveAll(TourImage);
        }
        public void Delete(TourImage TourImage)
        {
            tourImageRepository.Delete(TourImage);
        }
        public void Update(TourImage TourImage)
        {
            tourImageRepository.Update(TourImage);
        }

        public List<TourImage> FindBy(string[] propertyNames, string[] values)
        {
            return tourImageRepository.FindBy(propertyNames, values);
        }

        public List<TourImage> getByTourId(int id)
        {
            return GetAll().FindAll(ti => ti.TourId == id);
        }
    }
}
