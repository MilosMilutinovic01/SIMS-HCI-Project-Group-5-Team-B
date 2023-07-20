using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces
{
    public interface IGuideGuestRepository
    {
        public void Save(GuideGuest newGuideGuest);

        public void Delete(GuideGuest guideGuest);

        public void Update(GuideGuest guideGuest);

        public List<GuideGuest> GetAll();
    }
}
