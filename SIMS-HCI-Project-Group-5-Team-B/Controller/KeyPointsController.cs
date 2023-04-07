using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class KeyPointsController
    {
        private Repository<KeyPoint> keyPointsRepository;

        public KeyPointsController()
        {
            keyPointsRepository = new Repository<KeyPoint>();
        }

        public List<KeyPoint> GetAll()
        {
            return keyPointsRepository.GetAll();
        }
        public void Save(KeyPoint newKeyPoints)
        {
            keyPointsRepository.Save(newKeyPoints);
        }
        public void SaveAll(List<KeyPoint> keyPoints) 
        {
            keyPointsRepository.SaveAll(keyPoints);
        }
        public void Delete(KeyPoint KeyPoints)
        {
            keyPointsRepository.Delete(KeyPoints);
        }
        public void Update(KeyPoint KeyPoints)
        {
            keyPointsRepository.Update(KeyPoints);
        }
        public List<KeyPoint> getByTourId(int id)
        {
            return keyPointsRepository.GetAll().FindAll(kp => kp.TourId == id);
        }
        public List<KeyPoint> FindBy(string[] propertyNames, string[] values)
        {
            return keyPointsRepository.FindBy(propertyNames, values);
        }
    }
}
