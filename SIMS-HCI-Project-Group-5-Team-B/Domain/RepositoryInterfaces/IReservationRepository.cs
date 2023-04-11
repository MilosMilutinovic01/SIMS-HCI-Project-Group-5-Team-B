using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface IReservationRepository
    {
        public List<Reservation> GetUndeleted();
        public List<Reservation> GetAll();
        public Reservation GetById(int id);
        public void Save(Reservation reservation);
        public void Delete(Reservation reservation);
        public void Update(Reservation reservation);
        public List<Reservation> FindBy(string[] propertyNames, string[] values);
        public void SaveAll(List<Reservation> reservations);
    }
}
