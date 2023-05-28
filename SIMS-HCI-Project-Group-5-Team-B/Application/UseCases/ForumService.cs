using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class ForumService
    {
        private IForumRepository forumRepository;
        private ICommentService commentService;
        private IOwnerGuestRepository ownerGuestRepository;
        private IReservationRepository reservationRepository;
        private LocationController locationController;

        public ForumService()
        {
            forumRepository = Injector.Injector.CreateInstance<IForumRepository>();
            commentService = Injector.ServiceInjector.CreateInstance<ICommentService>();
            ownerGuestRepository = Injector.Injector.CreateInstance<IOwnerGuestRepository>();
            reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();
            locationController = new LocationController();
            GetReferences();
        }

        public void Update(Forum forum){
            forumRepository.Update(forum);
            GetReferences();
        }

        public void Save(Forum forum)
        {
            forumRepository.Save(forum);
            GetReferences();
        }

        public List<Forum> GetAll()
        {
            return forumRepository.GetAll();
        }

        public Forum GetById(int id)
        {
            return forumRepository.GetById(id);
        }

        private void GetReferences()
        {
            foreach(Forum forum in forumRepository.GetAll())
            {
                OwnerGuest guest = ownerGuestRepository.GetById(forum.OwnerGuestId);
                if (guest != null)
                {
                    forum.ForumOwner = guest;
                }
                //find all comments
                List<Comment> comments = commentService.GetAll().FindAll(comm => comm.ForumId == forum.Id);
                forum.Comments = comments;

                //find the location
                Location location = locationController.getById(forum.LocationId);
                forum.Location = location;
            }
        }

        public bool Exists(int locationId)
        {
            //CHECK FOR NOT ACTIVE FORUMS!!!!!!
            return GetAll().Any(foru => foru.LocationId == locationId && foru.ForumStatus == FORUMSTATUS.Active);
        }

        public List<Comment> GetForumsComments(int forumId)
        {
            return forumRepository.GetById(forumId).Comments;
        }

        public Forum GetByLocation(int locationId)
        {
            return GetAll().Find(forum => forum.LocationId == locationId);
        }

        public void CloseForum(int forumId, int ownerGuestId)
        {

            Forum forum = GetById(forumId);
            if(forum.OwnerGuestId == ownerGuestId)
            {
                forum.ForumStatus = FORUMSTATUS.Closed;
                forumRepository.Update(forum);
            }
 
        }

        
        public int NumberOfOwnerComments(Forum forum)
        {
            int numberOfOwnerComments = 0;
            foreach(Comment comment in forum.Comments)
            {
                if(comment.IsFromOwnerWithAccommodationOnLocation == true)
                {
                    numberOfOwnerComments++;
                }
            }
            return numberOfOwnerComments;
        }

        //ova fja sluzi takodje da odredit da li neko komentar moze da se reportuje,ako je ovaj bio na lokaciji onda ne moze da se repotuje komentar je validan
        public bool WasGuestOnLocation(OwnerGuest ownerGuest,Location location)
        {
            foreach(Reservation reservation in reservationRepository.GetUndeleted())
            {
                if (reservation.OwnerGuest.Id == ownerGuest.Id && reservation.Accommodation.Location.City == location.City && reservation.Accommodation.Location.State == location.State && reservation.EndDate < System.DateTime.Today)
                {
                    return true;
                }
            }
            return false;
        }

        public int NumberOfValidGuestComments(Forum forum)
        {
            int numberOfValidGuestComments = 0;
            foreach(Comment comment in forum.Comments)
            {
                //moramo dobiti onog ko je napisao komentar
                OwnerGuest ownerGuest = ownerGuestRepository.GetByUsername(comment.User.Username);
                if(ownerGuest != null && WasGuestOnLocation(ownerGuest,forum.Location))
                {
                    numberOfValidGuestComments++;
                }
            }
            return numberOfValidGuestComments;
        }

        public bool IsForumVeryUseful(Forum forum)
        {
            if(NumberOfOwnerComments(forum) >= 2 && NumberOfValidGuestComments(forum) >= 2)
            {
                return true;
            }
            return false;
        }
    }
}
