using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class ForumCSVRepository : CSVRepository<Forum>, IForumRepository
    {
        public ForumCSVRepository() : base() { }

        public List<Forum> GetAll()
        {
            return _data;
        }

        public Forum GetById(int id)
        {
            return _data.Find(sup => sup.Id == id);
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

        public void Save(Forum newForum)
        {
            newForum.Id = NextId();
            _data.Add(newForum);
            WriteCSV(_data);
        }
        public void Update(Forum forum)
        {
            Forum? current = _data.Find(d => d.Id == forum.Id);
            if (current == null)
            {
                Save(forum);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, forum);
            WriteCSV(_data);
        }

        protected override Forum FromCSV(string[] values)
        {
            Forum forum = new Forum();

            forum.Id = int.Parse(values[0]);
            forum.LocationId = int.Parse(values[1]);
            forum.OwnerGuestId = int.Parse(values[2]);
            if (values[3].Equals("Active"))
                forum.ForumStatus = FORUMSTATUS.Active;
            else
                forum.ForumStatus = FORUMSTATUS.Closed;
            

            return forum;
        }

        protected override string[] ToCSV(Forum obj)
        {
            string[] csvValues =
            {
                obj.Id.ToString(),
                obj.LocationId.ToString(),
                obj.OwnerGuestId.ToString(),
                obj.ForumStatus.ToString()
            };
            return csvValues;
        }
    }
}
