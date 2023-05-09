using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class SuperOwnerGuestTitleService
    {
        private ISuperOwnerGuestTitleRepository superOwnerGuestTitleRepository;
        private IOwnerGuestRepository ownerGuestRepository;
        private IReservationRepository reservationRepository;
        public SuperOwnerGuestTitleService() 
        {
            superOwnerGuestTitleRepository = Injector.Injector.CreateInstance<ISuperOwnerGuestTitleRepository>();
            ownerGuestRepository = Injector.Injector.CreateInstance<IOwnerGuestRepository>();
            reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();
            GetOwnerGuestReference();
        }

        public void Delete(SuperOwnerGuestTitle superOwnerGuestTitle)
        {
           superOwnerGuestTitleRepository.Delete(superOwnerGuestTitle);
            GetOwnerGuestReference();
        }
        
        public void Update(SuperOwnerGuestTitle superOwnerGuestTitle)
        {
            superOwnerGuestTitleRepository.Update(superOwnerGuestTitle);
            GetOwnerGuestReference();
        }

        public void Save(SuperOwnerGuestTitle superOwnerGuestTitle)
        {
            superOwnerGuestTitleRepository.Save(superOwnerGuestTitle);
            GetOwnerGuestReference();
        }

        public List<SuperOwnerGuestTitle> GetAll()
        {
            return superOwnerGuestTitleRepository.GetAll();
        }

        public SuperOwnerGuestTitle GetById(int id)
        {
            return superOwnerGuestTitleRepository.GetById(id);
        }

        private void GetOwnerGuestReference()
        {
            foreach(SuperOwnerGuestTitle superOwnerGuestTitle in GetAll())
            {
                OwnerGuest ownerGuest = ownerGuestRepository.GetById(superOwnerGuestTitle.OwnerGuestId);
                if(ownerGuest != null)
                {
                    superOwnerGuestTitle.OwnerGuest = ownerGuest;
                }
            }
        }
        //call when initializing? and when creating reservations
        public void BecomeSuperOwnerGuest()
        {
            //function to give superOwnerGuestTitle when needed
            foreach(OwnerGuest ownerGuest in ownerGuestRepository.GetAll())
            {
                //check if there is already one active title
                if (CheckSuperOwnerGuestTitle(ownerGuest.Id))
                    continue;

                //Check count of reservations 
                if(reservationRepository.GetOwnerGuestsReservationInLastYear(ownerGuest.Id).Count >= 10)
                {
                    //all requirenments are met
                    SuperOwnerGuestTitle superOwnerGuestTitle = new SuperOwnerGuestTitle(DateTime.Today, 5, ownerGuest.Id);
                    superOwnerGuestTitleRepository.Save(superOwnerGuestTitle);
                }
            }

        }      

        public bool CheckSuperOwnerGuestTitle(int ownerGuestId)
        {
            //check >= for activationDate
            return GetAll().Any(title => title.OwnerGuestId == ownerGuestId && title.ActivationDate > DateTime.Today.AddYears(-1));
        }

        //rezervacije sa popustom 
        //call when reserving
        public void UpdatePoints(int ownerGuestId)
        {
                //if our guest is superGuest, Apply discount when reserving
                SuperOwnerGuestTitle superOwnerGuestTitle = superOwnerGuestTitleRepository.GetActiveByOwnerGuestId(ownerGuestId);
                if(superOwnerGuestTitle != null)
                {
                    superOwnerGuestTitle.AvailablePoints = DecreasePoints(superOwnerGuestTitle.AvailablePoints);
                    superOwnerGuestTitleRepository.Update(superOwnerGuestTitle);
                }
            
        }

        private int DecreasePoints(int points)
        {
            if(points > 0)
            {
                --points;
            }
            return points;
        }

        public SuperOwnerGuestTitle GetActiveByOwnerGuestId(int ownerGuestId)
        {
            return superOwnerGuestTitleRepository.GetActiveByOwnerGuestId(ownerGuestId);
        }
    }
}
