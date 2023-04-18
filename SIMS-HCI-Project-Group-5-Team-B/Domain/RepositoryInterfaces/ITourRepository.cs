using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface ITourRepository
    {
        public void Save(Tour newTour);

        public void Delete(Tour tour);

        public void Update(Tour tour);

        public List<Tour> GetAll();
    }
}
