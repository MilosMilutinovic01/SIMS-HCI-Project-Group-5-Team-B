using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        public List<Accommodation> GetAll();
        public Accommodation GetById(int id);
        public void Save(Accommodation accommodation);
        public void Delete(Accommodation accommodation);
        public void Update(Accommodation accommodation);
        public List<Accommodation> FindBy(string[] propertyNames, string[] values);
        public void SaveAll(List<Accommodation> accommodations);

        public List<Accommodation> GetUndeleted();

    }
}
