using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class GuideGuestService
    {
        private UserService userService;
        private IGuideGuestRepository guideGuestRepository;

        public GuideGuestService()
        {
            userService = new UserService();
            guideGuestRepository = Injector.Injector.CreateInstance<IGuideGuestRepository>();
        }

        public GuideGuest getLoggedGuideGuest()
        {
            User loggedUser = userService.getLogged();
            if (loggedUser.Type != USERTYPE.GuideGuest)
            {
                throw new Exception("Guide guest is not logged in");
            }
            else
            {
                foreach(var guideGuest in guideGuestRepository.GetAll())
                {
                    if(guideGuest.Id == loggedUser.Id)
                    {
                        return guideGuest;
                    }
                }
            }
            throw new Exception("GuideGuest was no found");
        }
    }
}
