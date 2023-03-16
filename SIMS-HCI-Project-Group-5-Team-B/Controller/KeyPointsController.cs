using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    class KeyPointsController
    {
        private Repository<KeyPoints> keyPointsRepository;

        public KeyPointsController()
        {
            keyPointsRepository = new Repository<KeyPoints>();
        }

        public List<KeyPoints> GetAll()
        {
            return keyPointsRepository.GetAll();
        }
        public void Save(KeyPoints newKeyPoints)
        {
            keyPointsRepository.Save(newKeyPoints);
        }
        public void Delete(KeyPoints KeyPoints)
        {
            keyPointsRepository.Delete(KeyPoints);
        }
        public void Update(KeyPoints KeyPoints)
        {
            keyPointsRepository.Update(KeyPoints);
        }

        public List<KeyPoints> FindBy(string[] propertyNames, string[] values)
        {
            return keyPointsRepository.FindBy(propertyNames, values);
        }

        public List<KeyPoints> getByTourId(int id)
        {
            return GetAll().FindAll(kp => kp.TourId == id);
        }
    }
}
