using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public TourService()
        {
            this.tourRepository = Injector.Injector.CreateInstance<ITourRepository>();
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
      
        public List<Tour> Search(Location Location, string Language, int Duration, int NumberOfPeople)
        {
            return tourRepository.GetAll().FindAll(t => (Location.State == string.Empty || t.Location.State == Location.State) &&
                                                        (Location.City == string.Empty || t.Location.City == Location.City) &&
                                                        (Language == string.Empty || t.Language == Language) &&
                                                        (Duration == 0 || t.Duration == Duration) &&
                                                        t.MaxGuests >= NumberOfPeople);
        }
    }
}
