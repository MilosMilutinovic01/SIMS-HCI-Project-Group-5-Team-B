using SIMS_HCI_Project_Group_5_Team_B.Model;
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
        private TourStartController tourStartController;
        public TourController(LocationController locationController)
        {
            tourRepository = new Repository<Tour>();
            tourStartController = new TourStartController();
            this.locationController = locationController;
            //GetLocationReference();
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
        public List<Tour> GetAvailableTours()
        {
            List<Tour> tours = tourRepository.GetAll();
            List<TourStart> tourStarts = tourStartController.GetAll();
            List<Tour> availableTours = new List<Tour>();
            foreach (TourStart ts in tourStarts)
            {
                if (ts.Start.ToShortDateString().Equals(DateTime.Now.ToShortDateString()))
                {
                    availableTours.AddRange(tours.FindAll(tour => tour.Id == ts.TourId));
                }
            }
            return availableTours;
        }
        //private void GetLocationReference()
        //{
        //    List<Tour> tours = tourRepository.GetAll();
        //    foreach (Tour tour in tours)
        //    {
        //        Location location = locationController.getById(tour.LocationId);
        //        if (location != null)
        //        {
        //            tour.Location = location;
        //        }
        //    }
        //}

    }
}
