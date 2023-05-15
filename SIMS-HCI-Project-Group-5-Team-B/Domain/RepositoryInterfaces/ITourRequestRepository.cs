using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface ITourRequestRepository
    {
        public void Save(TourRequest newTouRequest);
        
        public void Delete(TourRequest tourRequest);

        public void Update(TourRequest tourRequest);

        public List<TourRequest> GetAll();
    }
}
