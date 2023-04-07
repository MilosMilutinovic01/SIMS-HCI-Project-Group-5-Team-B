using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class TourController
    {
        private Repository<Tour> tourRepository;
        private LocationController locationController;
        private KeyPointsController keyPointsController;
        
        public TourController(){
            tourRepository = new Repository<Tour>();
            this.locationController = new LocationController();
            keyPointsController = new KeyPointsController();
            GetLocationReference();
        }
        
        public TourController(LocationController locationController)
        {
            tourRepository = new Repository<Tour>();
            this.locationController = locationController;
            GetLocationReference();
        }
        
        public List<Tour> GetAll()
        {
            return tourRepository.GetAll();
        }
        
        public void Save(Tour newTour)
        {
            tourRepository.Save(newTour);
        }
        
        public void Delete(Tour tour)
        {
            tourRepository.Delete(tour);
        }
        
        public void Update(Tour tour)
        {
            tourRepository.Update(tour);
        }
        
        public List<Tour> FindBy(string[] propertyNames, string[] values)
        {
            return tourRepository.FindBy(propertyNames, values);
        }
        
        public Tour getById(int id)
        {
            return GetAll().Find(tour => tour.Id == id);
        }
        
        public int makeId()
        {
            return tourRepository.NextId();
        }
        private void GetLocationReference()
        {
            List<Tour> tours = tourRepository.GetAll();
            foreach (Tour tour in tours)
            {
                Location location = locationController.getById(tour.LocationId);
                if (location != null)
                {
                    tour.Location = location;
                }
            }
        }

        //private void GetKeyPointsReference()
        //{
        //    List<Tour> tours = tourRepository.GetAll();
        //    foreach (Tour tour in tours)
        //    {
        //        List<KeyPoint> keyPoints = keyPointsController.getByTourId(tour.Id);
        //        if (keyPoints != null)
        //        {
        //            tour.KeyPoints = keyPoints;
        //        }
        //    }
        //}
        public List<Tour> Search(string Location, string Language, string TourLength, int NumberOfPeople)
        {
            List<Tour> result = new List<Tour>();

            string searchProperties = string.Empty;
            string searchValues = string.Empty;

            if (!Location.Equals(""))
            {
                searchProperties += "Location;";
                searchValues += Location + ";";
            }

            if (!TourLength.Equals(""))
            {
                searchProperties += "Duration;";
                searchValues += TourLength + ";";
            }

            if (!Language.Equals(""))
            {
                searchProperties += "Language;";
                searchValues += Language + ";";
            }

            if(searchProperties.Equals(String.Empty))
            {
                result = new List<Tour>(tourRepository.GetAll());
            }
            else
            {
                searchProperties = searchProperties.Remove(searchProperties.Length - 1);
                searchValues = searchValues.Remove(searchValues.Length - 1);
                foreach (Tour tour in tourRepository.FindBy(searchProperties.Split(';'), searchValues.Split(';')))
                {
                    result.Add(tour);
                }
            }

            result.RemoveAll(tour => tour.MaxGuests - 0 < NumberOfPeople);//Change 0 to the number of the people on the tour

            return result;
        }
    }
}
