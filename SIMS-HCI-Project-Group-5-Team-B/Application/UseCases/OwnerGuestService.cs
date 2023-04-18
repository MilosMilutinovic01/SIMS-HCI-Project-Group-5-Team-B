using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class OwnerGuestService
    {
        private OwnerGuestCSVRepository _ownerGuestRepository;

        public OwnerGuestService()
        {
            _ownerGuestRepository = new OwnerGuestCSVRepository();
        }

        public OwnerGuest GetByUsername(string username)
        {
            return _ownerGuestRepository.GetByUsername(username);
        }

        public OwnerGuest GetById(int id)
        {
            return _ownerGuestRepository.GetById(id);
        }
    }
}
