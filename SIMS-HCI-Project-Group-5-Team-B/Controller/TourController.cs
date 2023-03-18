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

        public void Save(Tour tour)
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

        public List<Tour> Search(string Location, string Language, string TourLength, int NumberOfPeople)
        {
            List<Tour> result = new List<Tour>();

            string searchProperties = string.Empty;
            string searchValues = string.Empty;

            if (!Location.Equals(""))
            {
                searchProperties += "LocationString,";
                searchValues += Location.Replace(" ", "") + ",";
            }

            if (!TourLength.Equals(""))
            {
                searchProperties += "Duration,";
                searchValues += TourLength + ",";
            }

            if (!Language.Equals(""))
            {
                searchProperties += "Language,";
                searchValues += Language + ",";
            }

            if(searchProperties.Equals(String.Empty))
            {
                result = new List<Tour>(repository.GetAll());
            }
            else
            {
                searchProperties = searchProperties.Remove(searchProperties.Length - 1);
                searchValues = searchValues.Remove(searchValues.Length - 1);
                foreach (Tour tour in repository.FindBy(searchProperties.Split(','), searchValues.Split(',')))
                {
                    result.Add(tour);
                }
            }

            result.RemoveAll(tour => tour.MaxGuests - 0 < NumberOfPeople);//Change 0 to the number of the people on the tour

            return result;
        }

        public List<Tour> FindBy(string[] propertyNames, string[] values)
        {
            return repository.FindBy(propertyNames, values);
        }
    }
}
