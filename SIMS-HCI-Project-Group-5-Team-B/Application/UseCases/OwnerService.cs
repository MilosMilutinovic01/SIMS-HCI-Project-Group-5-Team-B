using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class OwnerService
    {
        private Repository<Owner> ownerRepository;

        public OwnerService()
        {
            ownerRepository = new Repository<Owner>();

        }

        public List<Owner> GetAll()
        {
            return ownerRepository.GetAll();
        }
        public void Save(Owner owner)
        {
            ownerRepository.Save(owner);
        }
        public void Delete(Owner owner)
        {
            ownerRepository.Delete(owner);
        }
        public void Update(Owner owner)
        {
            ownerRepository.Update(owner);
        }

        public List<Owner> FindBy(string[] propertyNames, string[] values)
        {
            return ownerRepository.FindBy(propertyNames, values);
        }
        public Owner getById(int id)
        {
            return GetAll().Find(own => own.Id == id);
        }

        public Owner GetByUsername(string username)
        {
            return GetAll().Find(owner => owner.Username.Equals(username));
        }


    }
}
