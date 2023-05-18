using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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
      
        public List<Tour> Search(Location location, string language, int duration, int numberOfPeople)
        {
            return tourRepository.GetAll().FindAll(t => (location.State == string.Empty || t.Location.State == location.State) &&
                                                        (location.City == string.Empty || t.Location.City == location.City) &&
                                                        (language == string.Empty || t.Language == language) &&
                                                        (duration == 0 || t.Duration == duration) &&
                                                        t.MaxGuests >= numberOfPeople);
        }

        public List<GuideGuestTourDTO> GuideGuestSearch(Location location, string language, int duration, int numberOfPeople)
        {
            List<GuideGuestTourDTO> list = new List<GuideGuestTourDTO>();
            foreach(var tour in Search(location, language, duration, numberOfPeople))
            {
                list.Add(new GuideGuestTourDTO(tour, tour.ImageUrls.Split(',')[0]));
            }
            return list;
        }

        public List<GuideGuestTourDTO> GuideGuestGetAll()
        {
            List<GuideGuestTourDTO> list = new List<GuideGuestTourDTO>();
            foreach (var tour in GetAll())
            {
                list.Add(new GuideGuestTourDTO(tour, tour.ImageUrls.Split(',')[0]));
            }
            return list;
        }
    }
}
