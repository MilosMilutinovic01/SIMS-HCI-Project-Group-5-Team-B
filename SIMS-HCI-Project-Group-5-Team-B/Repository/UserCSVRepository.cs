using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Repository
{
    public class UserCSVRepository : CSVRepository<User>, IUserRepository
    {
        public UserCSVRepository() : base() { }

        public void Delete(User user)
        {
            User? founded = _data.Find(d => d.Id == user.Id);
            if (founded != null)
            {
                _data.Remove(founded);
                WriteCSV(_data);
            }
        }

        public List<User> GetAll()
        {
            return _data;
        }

        public void Save(User newUser)
        {
            newUser.Id = NextId();
            _data.Add(newUser);
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

        public void Update(User user)
        {
            User? current = _data.Find(d => d.Id == user.Id);
            if (current == null)
            {
                Save(user);
                return;
            }
            int index = _data.IndexOf(current);
            _data.Remove(current);
            _data.Insert(index, user);
            WriteCSV(_data);
        }

        protected override User FromCSV(string[] values)
        {
            User newUser = new User();

            newUser.Id = int.Parse(values[0]);
            newUser.Username = values[1];
            newUser.Password = values[2];
            if (string.Equals(values[3], "Owner"))
                newUser.Type = USERTYPE.Owner;
            else if (string.Equals(values[3], "OwnerGuest"))
                newUser.Type = USERTYPE.OwnerGuest;
            else if (string.Equals(values[3], "Guide"))
                newUser.Type = USERTYPE.Guide;
            else
                newUser.Type = USERTYPE.GuideGuest;

            return newUser;
        }

        protected override string[] ToCSV(User obj)
        {
            string[] csvValues = { obj.Id.ToString(), obj.Username, obj.Password, obj.Type.ToString() };
            return csvValues;
        }
    }
}
