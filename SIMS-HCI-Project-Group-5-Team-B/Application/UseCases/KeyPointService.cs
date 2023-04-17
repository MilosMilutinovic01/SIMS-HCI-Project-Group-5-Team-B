using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class KeyPointService
    {
        private IKeyPointRepository keyPointRepository;
        public KeyPointService(IKeyPointRepository keyPointRepository)
        {
            this.keyPointRepository = keyPointRepository;
        }

        public List<KeyPoint> GetAll()
        {
            return keyPointRepository.GetAll();
        }
        public void Save(KeyPoint newKeyPoints)
        {
            keyPointRepository.Save(newKeyPoints);
        }
        public void Delete(KeyPoint KeyPoints)
        {
            keyPointRepository.Delete(KeyPoints);
        }
        public void Update(KeyPoint KeyPoints)
        {
            keyPointRepository.Update(KeyPoints);
        }
        public List<KeyPoint> getByTourId(int id)
        {
            return keyPointRepository.GetAll().FindAll(kp => kp.TourId == id);
        }
    }
}
