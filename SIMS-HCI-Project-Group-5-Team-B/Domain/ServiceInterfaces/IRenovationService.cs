using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces
{
    public interface IRenovationService
    {
        public List<Renovation> GetAll();
        public Renovation GetById(int id);
        public void Save(Renovation renovation);
        public void Update(Renovation renovation);
        public void MarkRenovatiosThatTookPlaceInTheLastYear();
        public List<Renovation> GetUndeleted();
        public bool IsRenovationDeletable(Renovation renovation);
        public List<Renovation> GetPastRenovations(int ownerId);
        public List<RenovationGridView> GetFutureRenovationsView(int ownerId);
        public bool IsAccomodationNotInRenovation(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate);
    }
}
