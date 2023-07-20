using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;
        private Repository<User> _userRepository;

        public CommentService()
        {
            _userRepository = new Repository<User>();
            _commentRepository = Injector.Injector.CreateInstance<ICommentRepository>();
            GetUserReference();
        }

        public void Update(Comment comment)
        {
            _commentRepository.Update(comment);
            GetUserReference();
        }

        public List<Comment> GetAll()
        {
            return _commentRepository.GetAll();
        }

        public Comment GetById(int id)
        {
            return _commentRepository.GetById(id);
        }

        public void Save(Comment newComment)
        {
            _commentRepository.Save(newComment);
            GetUserReference();
        }

        private void GetUserReference()
        {
            foreach(Comment comment in  _commentRepository.GetAll())
            {
                User user = _userRepository.FindId(comment.UserId);
                if (user != null)
                {
                    comment.User = user;
                }
            }
        }
    }
}
