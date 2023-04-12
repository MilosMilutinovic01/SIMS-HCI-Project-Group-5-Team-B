using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class TourCSVRepository : ITourRepository
    {
        private Repository<Tour> tourRepository;

        public TourCSVRepository()
        {
            tourRepository = new Repository<Tour>();
        }

        public void Delete(Tour tour)
        {
            tourRepository.Delete(tour);
        }

        public List<Tour> GetAll()
        {
            return tourRepository.GetAll();
        }

        public void Save(Tour newTour)
        {
            tourRepository.Save(newTour);
        }

        public void Update(Tour tour)
        {
            tourRepository.Update(tour);
        }
    }
}
