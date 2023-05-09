using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class AccommodationCSVRepository : IAccommodationRepository
    {
        Repository<Accommodation> accommodationRepository;

        public AccommodationCSVRepository()
        {
            accommodationRepository = new Repository<Accommodation>();
        }

        public void Delete(Accommodation accommodation)
        {
            accommodationRepository.Delete(accommodation);
        }

        public List<Accommodation> FindBy(string[] propertyNames, string[] values)
        {
            return accommodationRepository.FindBy(propertyNames, values);
        }


        public Accommodation GetById(int id)
        {
            return accommodationRepository.FindId(id);
        }

        public void Save(Accommodation accommodation)
        {
            accommodationRepository.Save(accommodation);
        }

        public void SaveAll(List<Accommodation> accommodations)
        {
            accommodationRepository.SaveAll(accommodations);
        }

        public void Update(Accommodation accommodation)
        {
            accommodationRepository.Update(accommodation);
        }

        public List<Accommodation> GetAll()
        {
            return accommodationRepository.GetAll();
        }

    }
}
