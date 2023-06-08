using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class UserService
    {
        private static User loggedUser = null;
        private IUserRepository userRepository;
        public UserService()
        {
            userRepository = Injector.Injector.CreateInstance<IUserRepository>();
        }

        public bool LogIn(string username, string password)
        {
            foreach(var user in userRepository.GetAll())
            {
                if(user.Username == username && user.Password == password)
                {
                    loggedUser = user;
                    return true;
                }
            }
            return false;
        }

        public void DeleteUser(User user)
        {
            userRepository.Delete(user);
        }

        public User GetById(int id)
        {
            return userRepository.GetAll().Find(u => u.Id == id);
        }

        public User getLogged()
        {
            return loggedUser;
        }
    }
}
