using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class UserController
    {
        private Repository<User> userRepository;

        public UserController()
        {
            userRepository = new Repository<User>();
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public void Save(User newUser)
        {
            userRepository.Save(newUser);
        }

        public void Delete(User tour)
        {
            userRepository.Delete(tour);
        }

        public void Update(User user)
        {
            userRepository.Update(user);
        }

        public List<User> FindBy(string[] propertyNames, string[] values)
        {
            return userRepository.FindBy(propertyNames, values);
        }

        public User getById(int id)
        {
            return GetAll().Find(tour => tour.Id == id);
        }

        public User LogIn(string username, string password)
        {
            foreach(var user in GetAll())
            {
                if (user.Username.Equals(username) && user.Password.Equals(password))
                {
                    return user;
                }
            }
            return null;
        }

        public User GetByUsername(string username)
        {
            return GetAll().Find(user=> user.Username == username);
        }
    }
}
