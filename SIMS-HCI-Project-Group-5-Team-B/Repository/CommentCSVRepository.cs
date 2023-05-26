using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class CommentCSVRepository : CSVRepository<Comment>, ICommentRepository
    {
        public CommentCSVRepository() : base() { }

        public List<Comment> GetAll()
        {
            return _data;
        }

        public Comment GetById(int id)
        {
            return _data.Find(sup => sup.Id == id);
        }

        public void Save(Comment newComment)
        {
            newComment.Id = NextId();
            _data.Add(newComment);
            WriteCSV(_data);
        }

        private int NextId()
        {
            if (_data.Count() < 1)
            {
                return 1;
            }
            else
            {
                return _data.Max(d => d.Id) + 1;
            }
        }

        protected override Comment FromCSV(string[] values)
        {
            Comment comment = new Comment();

            comment.Id = int.Parse(values[0]);
            comment.UserId = int.Parse(values[1]);
            comment.ForumId = int.Parse(values[2]);
            comment.Content = values[3];


            return comment;
        }

        protected override string[] ToCSV(Comment obj)
        {
            string[] csvValues =
            {
                obj.Id.ToString(),
                obj.UserId.ToString(),
                obj.ForumId.ToString(),
                obj.Content
            };
            return csvValues;
        }
    }
}
