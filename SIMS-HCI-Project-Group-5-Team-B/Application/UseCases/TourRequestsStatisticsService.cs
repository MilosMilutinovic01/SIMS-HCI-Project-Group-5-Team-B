using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public List<string> GetAllYears()
        {
            List<DateTime> allDates = tourRequestRepository.GetAll().Select(r => r.SelectedDate).ToList();
            List<string> years = new List<string>();
            foreach(var item in allDates) 
            {
                years.Add(item.Year.ToString());
            }
            return years.Distinct().ToList();
        }

        public List<MonthStatistic> GetStatisticsByMonths(string year, string state, string city, string language)
        {
            List<MonthStatistic> monthStatistics = new List<MonthStatistic>();

            var monthGroups = tourRequestRepository.GetAll().Where(request => (request.SelectedDate.Year.ToString() == year) &&
                                                                    (request.Location.State.Equals(state) || state == default) &&
                                                                    (request.Location.City.Equals(city) || city == default) &&
                                                                    (request.Language.Equals(language) || language == default))
                .GroupBy(request => request.SelectedDate.Month).Select(group => new { Month = group.Key, NumberOfRows = group.Count() });

            foreach (var monthGroup in monthGroups)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthGroup.Month);
                monthStatistics.Add(new MonthStatistic (monthName,monthGroup.NumberOfRows));
            }
            return monthStatistics;
        }

        public List<YearStatistics> GetStatisticsByYear(string state, string city, string language)
        {
            List<YearStatistics> yearStatistics = new List<YearStatistics>();

            // Calculate the month statistics
            var yearGroups = tourRequestRepository.GetAll().Where(request => (request.Location.State.Equals(state) || state == default) &&
                                                                    (request.Location.City.Equals(city) || city == default) &&
                                                                    (request.Language.Equals(language) || language == default))
                .GroupBy(request => request.SelectedDate.Year)
                .Select(group => new { Year = group.Key, NumberOfRows = group.Count() });

            foreach (var yearGroup in yearGroups)
            {
                yearStatistics.Add(new YearStatistics(yearGroup.Year, yearGroup.NumberOfRows));
            }
            return yearStatistics;
        }

        public string GetMostUsedLanguage()
        {
            return tourRequestRepository.GetAll()
                .Where(r => r.DateRangeStart >= DateTime.Now.AddYears(-1))
                .GroupBy(r => r.Language)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();
        }

        public int GetMostUsedLocationId()
        {
            return tourRequestRepository.GetAll()
                .Where(r => r.DateRangeStart >= DateTime.Now.AddYears(-1))
                .GroupBy(r => r.LocationId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();
        }
    }
}
