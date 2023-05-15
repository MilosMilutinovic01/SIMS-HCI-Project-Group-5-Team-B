using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class TourService
    {
        private ITourRepository tourRepository;
        public TourService(ITourRepository tourRepository)
        {
            this.tourRepository = tourRepository;
        }
        
        public List<Tour> GetAll()
        {
            return tourRepository.GetAll();
        }
        public Tour Get(int tourId)
        {
            return tourRepository.GetAll().Find(t => t.Id == tourId);
        }

        public void Save(Tour tour)
        {
            tourRepository.Save(tour);
        }

        public int makeId()
        {
            return tourRepository.NextId();
        }

        public Tour getById(int id)
        {
            return GetAll().Find(tour => tour.Id == id);
        }
    }
}
