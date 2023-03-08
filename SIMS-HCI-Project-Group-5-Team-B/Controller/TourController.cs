using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class TourController
    {
        private Repository<Tour> repository;

        public TourController()
        {
            repository = new Repository<Tour>();
        }

        public void Add(Tour tour)
        {
            repository.Save(tour);
        }

        public void Delete(Tour tour)
        {
            repository.Delete(tour);
        }

        public void Update(Tour tour)
        {
            repository.Update(tour);
        }

        public List<Tour> GetAll()
        {
            return repository.GetAll();
        }
    }
}
