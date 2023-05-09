using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class RenovationRequestCSVRepository : IRenovationRequestRepository
    {
        private Repository<RenovationRequest> renovationRequestRepository;
        public RenovationRequestCSVRepository()
        {
            renovationRequestRepository = new Repository<RenovationRequest>();
        }

        public void Delete(RenovationRequest renovationRequest)
        {
            renovationRequestRepository.Delete(renovationRequest);
        }

        public List<RenovationRequest> GetAll()
        {
            return renovationRequestRepository.GetAll();
        }

        public RenovationRequest GetById(int id)
        {
            return renovationRequestRepository.FindId(id);
        }

        public void Save(RenovationRequest renovationRequest)
        {
            renovationRequestRepository.Save(renovationRequest);
        }

        public void Update(RenovationRequest renovationRequest)
        {
            renovationRequestRepository.Update(renovationRequest);
        }
    }
}
