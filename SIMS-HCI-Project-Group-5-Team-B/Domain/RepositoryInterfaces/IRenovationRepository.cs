using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface IRenovationRepository
    {
        public List<Renovation> GetUndeleted();
        public List<Renovation> GetAll();
        public Renovation GetById(int id);
        public void Save(Renovation renovation);
        public void Delete(Renovation renovation);
        public void Update(Renovation renovation);
        public List<Renovation> GetRenovationForAccommodation(int accommodationId);

    }
}
