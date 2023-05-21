using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using System.Collections.Generic;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class ForumService
    {
        private IForumRepository forumRepository;
        private ICommentService commentService;
        private IOwnerGuestRepository ownerGuestRepository;

        public ForumService()
        {
            forumRepository = Injector.Injector.CreateInstance<IForumRepository>();
            commentService = Injector.ServiceInjector.CreateInstance<ICommentService>();
            ownerGuestRepository = Injector.Injector.CreateInstance<IOwnerGuestRepository>();
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
            }
        }

        
    }
}
