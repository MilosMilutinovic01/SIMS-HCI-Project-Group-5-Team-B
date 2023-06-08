using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces
{
    public interface ICommentService
    {
        public void Save(Comment newComment);
        public List<Comment> GetAll();
        public Comment GetById(int id);
        public void Update(Comment comment);
    }
}
