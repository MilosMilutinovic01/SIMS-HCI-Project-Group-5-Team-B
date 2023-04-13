using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class GuideService
    {
        private Repository<Guide> guideRepository;

        public GuideService()
        {
            guideRepository = new Repository<Guide>();

        }

        public List<Guide> GetAll()
        {
            return guideRepository.GetAll();
        }
        public void Save(Guide guide)
        {
            guideRepository.Save(guide);
        }
        public void Delete(Guide guide)
        {
            guideRepository.Delete(guide);
        }
        public void Update(Guide guide)
        {
            guideRepository.Update(guide);
        }

        public List<Guide> FindBy(string[] propertyNames, string[] values)
        {
            return guideRepository.FindBy(propertyNames, values);
        }
        public Guide getById(int id)
        {
            return GetAll().Find(guide => guide.Id == id);
        }

        public Guide GetByUsername(string username)
        {
            return GetAll().Find(guide => guide.Username.Equals(username));
        }
    }
}
