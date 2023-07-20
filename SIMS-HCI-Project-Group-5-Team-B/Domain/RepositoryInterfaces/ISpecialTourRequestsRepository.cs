using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface ISpecialTourRequestsRepository
    {
        public void Save(SpecialTourRequest newSpecialTourRequest);

        public void Delete(SpecialTourRequest specialTourRequest);

        public void Update(SpecialTourRequest specialTourRequest);

        public List<SpecialTourRequest> GetAll();
    }
}
