using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class TourRequestsStatisticsService
    {
        private ITourRequestRepository tourRequestRepository;

        public TourRequestsStatisticsService()
        {
            this.tourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
        }

        //public List<TourRequest> GetStatistics(string state, string city, string language, string type)
        //{
        //    if(!type.Equals("years"))
        //    {
        //        return tourRequestRepository.GetAll().find
        //    }
        //}
    }
}
