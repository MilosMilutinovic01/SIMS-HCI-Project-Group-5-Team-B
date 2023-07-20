using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class TourRequestStatisticsService
    {
        private TourRequestService tourRequestService { get; set; }
        public TourRequestStatisticsService()
        {
            tourRequestService = new TourRequestService();
        }

        public List<TourRequestLocationStatistics> CalculateLocationStatistics(int guideGuestId, int year = -1)
        {
            List<TourRequestLocationStatistics> list = new List<TourRequestLocationStatistics>();
            foreach(var stat in tourRequestService.GetFor(guideGuestId))
            {
                if (year != -1 && stat.DateRangeStart.Year != year) continue;
                var foundLocationStatistic = 
                    list.Find(trls => trls.Location.ToString() == stat.Location.ToString());
                if(foundLocationStatistic == null)
                {
                    if(stat.Status == TourRequestStatuses.ACCEPTED)
                    {
                        list.Add(new TourRequestLocationStatistics(year, stat.Location, 1, 0));
                    }
                    else
                    {
                        list.Add(new TourRequestLocationStatistics(year, stat.Location, 0, 1));
                    }
                }
                else
                {
                    if (stat.Status == TourRequestStatuses.ACCEPTED)
                    {
                        foundLocationStatistic.NumberOfAcceptedRequests++;
                    }
                    else
                    {
                        foundLocationStatistic.NumberOfRejectedRequests++;
                    }
                }
            }
            return list;
        }
        public List<TourRequestLanguageStatistics> CalculateLanguageStatistics(int guideGuestId, int year = -1)
        {
            List<TourRequestLanguageStatistics> list = new List<TourRequestLanguageStatistics>();
            foreach (var stat in tourRequestService.GetFor(guideGuestId))
            {
                if (year != -1 && stat.DateRangeStart.Year != year) continue;
                var foundLocationStatistic =
                    list.Find(trls => trls.Language == stat.Language);
                if (foundLocationStatistic == null)
                {
                    if (stat.Status == TourRequestStatuses.ACCEPTED)
                    {
                        list.Add(new TourRequestLanguageStatistics(year, stat.Language, 1, 0));
                    }
                    else
                    {
                        list.Add(new TourRequestLanguageStatistics(year, stat.Language, 0, 1));
                    }
                }
                else
                {
                    if (stat.Status == TourRequestStatuses.ACCEPTED)
                    {
                        foundLocationStatistic.NumberOfAcceptedRequests++;
                    }
                    else
                    {
                        foundLocationStatistic.NumberOfRejectedRequests++;
                    }
                }
            }
            return list;
        }
        public List<int> GetYearsWithRequests(int guideGuestId)
        {
            List<int> list = new List<int>();

            foreach(var tourRequest in tourRequestService.GetFor(guideGuestId))
            {
                if (!list.Contains(tourRequest.DateRangeStart.Year))
                {
                    list.Add(tourRequest.DateRangeStart.Year);
                }
            }

            return list;
        }
    }
}
