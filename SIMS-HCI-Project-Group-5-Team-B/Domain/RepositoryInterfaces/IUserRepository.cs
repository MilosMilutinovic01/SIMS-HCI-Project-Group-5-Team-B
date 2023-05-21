using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface IUserRepository
    {
        public void Save(User newUser);

        public void Delete(User user);

        public void Update(User user);

        public List<User> GetAll();
    }
}
