using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface ILocationRepository
    {
        public void Save(Location newLocation);

        public void Delete(Location location);

        public void Update(Location location);

        public List<Location> GetAll();
    }
}
