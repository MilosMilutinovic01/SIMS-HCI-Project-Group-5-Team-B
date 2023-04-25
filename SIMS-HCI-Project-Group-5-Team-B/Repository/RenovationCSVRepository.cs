using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;


namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class RenovationCSVRepository : IRenovationRepository
    {
        private readonly Repository<Renovation> renovationRepo;

        public RenovationCSVRepository()
        {
            this.renovationRepo = new Repository<Renovation>();
        }

        public void Delete(Renovation renovation)
        {
            renovationRepo.Delete(renovation);
        }

        public List<Renovation> GetAll()
        {
            return renovationRepo.GetAll();
        }

        public Renovation GetById(int id)
        {
            return renovationRepo.FindId(id);
        }

        public void Save(Renovation renovation)
        {
            renovationRepo.Save(renovation);
        }

        public void Update(Renovation renovation)
        {
            renovationRepo.Update(renovation);
        }

        public List<Renovation> GetRenovationForAccommodation(int accommodationId)
        {
            List<Renovation> renovations = new List<Renovation>();
            foreach (Renovation renovation in GetUndeleted())
            {
                if (renovation.AccommodationId == accommodationId)
                {
                    renovations.Add(renovation);
                }
            }
            return renovations;
        }

        public List<Renovation> GetUndeleted()
        {
            List<Renovation> undeletedRenovations = new List<Renovation>();
            foreach(Renovation renovation in GetAll())
            {
                if (!renovation.IsDeleted)
                {
                    undeletedRenovations.Add(renovation);
                }
            }
            return undeletedRenovations;
        }

    }
}
