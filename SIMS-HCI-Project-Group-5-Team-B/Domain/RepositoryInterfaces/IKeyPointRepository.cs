using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface IKeyPointRepository
    {
        public void Save(KeyPoint newKeyPoint);

        public void Delete(KeyPoint keyPoint);

        public void Update(KeyPoint keyPoint);

        public List<KeyPoint> GetAll();
    }
}
