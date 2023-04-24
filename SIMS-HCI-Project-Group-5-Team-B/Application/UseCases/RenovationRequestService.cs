using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class RenovationRequestService
    {
        private IRenovationRequestRepository renovationRequestRepository;
        private IAccommodationRepository accommodationRepository;
        public RenovationRequestService()
        {
            renovationRequestRepository = Injector.Injector.CreateInstance<IRenovationRequestRepository>();
            accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
            GetAccommodationReference();
        }

        public void Delete(RenovationRequest renovationRequest)
        {
            renovationRequestRepository.Delete(renovationRequest);
            GetAccommodationReference();
        }

        public List<RenovationRequest> GetAll()
        {
            return renovationRequestRepository.GetAll();
        }

        public RenovationRequest GetById(int id)
        {
            return renovationRequestRepository.GetById(id);
        }

        public void Save(RenovationRequest renovationRequest)
        {
            renovationRequestRepository.Save(renovationRequest);
            GetAccommodationReference();
        }

        public void Update(RenovationRequest renovationRequest)
        {
            renovationRequestRepository.Update(renovationRequest);
            GetAccommodationReference();
        }

        //TODO After Merge
        private void GetAccommodationReference()
        {
            foreach(RenovationRequest renovationRequest in GetAll())
            {
                Accommodation accommodation = accommodationRepository.GetById(renovationRequest.AccommodationId);
                if(accommodation != null)
                {
                    renovationRequest.Accommodation = accommodation;
                }
            }
        }



    }
}
