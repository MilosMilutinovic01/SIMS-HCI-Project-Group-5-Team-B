using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public  interface IForumRepository
    {
        public void Save(Forum newForum);
        public List<Forum> GetAll();
        public Forum GetById(int id);
        public void Update(Forum forum);

    }
}
