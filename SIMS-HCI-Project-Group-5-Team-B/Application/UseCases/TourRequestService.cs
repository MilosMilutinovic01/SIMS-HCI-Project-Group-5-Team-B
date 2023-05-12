using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class TourRequestService
    {
        private ITourRequestRepository tourRequestRepository;
        
        public TourRequestService()
        {
            tourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
        }

        public void Save(TourRequest newTourRequest)
        {
            tourRequestRepository.Save(newTourRequest);
        }

        public void GetAll()
        {
            tourRequestRepository.GetAll();
        }
    }
}
