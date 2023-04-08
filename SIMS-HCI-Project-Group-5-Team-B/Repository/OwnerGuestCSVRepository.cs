using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class OwnerGuestCSVRepository: IOwnerGuestRepository
    {
        private Repository<OwnerGuest> ownerguestRepository;

        public OwnerGuestCSVRepository()
        {
            this.ownerguestRepository = new Repository<OwnerGuest>();
        }

        public List<OwnerGuest> GetAll()
        {
            return this.ownerguestRepository.GetAll();
        }
        public OwnerGuest GetById(int id)
        {
           return ownerguestRepository.FindId(id);    
        }

        public OwnerGuest GetByUsername(string username)
        {
            return GetAll().Find(ownerGuest => ownerGuest.Username.Equals(username));
        }
    }
}
