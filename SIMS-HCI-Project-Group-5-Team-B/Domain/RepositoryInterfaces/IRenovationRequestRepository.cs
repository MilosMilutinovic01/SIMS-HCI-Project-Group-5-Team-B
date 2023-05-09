using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface IRenovationRequestRepository
    {
        public List<RenovationRequest> GetAll();
        public RenovationRequest GetById(int id);
        public void Save(RenovationRequest renovationRequest);
        public void Delete(RenovationRequest renovationRequest);
        public void Update(RenovationRequest renovationRequest);
    }
}
