using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface ISuperOwnerGuestTitleRepository
    {
        public void Save(SuperOwnerGuestTitle superOwnerGuestTitle);

        public void Delete(SuperOwnerGuestTitle superOwnerGuestTitle);

        public void Update(SuperOwnerGuestTitle superOwnerGuestTitle);

        public List<SuperOwnerGuestTitle> GetAll();

        public SuperOwnerGuestTitle GetById(int id);
        public SuperOwnerGuestTitle GetActiveByOwnerGuestId(int ownerGuestId);
    }
}
