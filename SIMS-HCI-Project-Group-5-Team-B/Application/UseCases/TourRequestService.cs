using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class TourRequestService
    {
        private ITourRequestRepository tourRequestRepository;
        
        public TourRequestService()
        {
            tourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
        }

        public void Save(TourRequest newTourRequest)
        {
            tourRequestRepository.Save(newTourRequest);
        }

        public void Update(TourRequest newTourRequest)
        {
            tourRequestRepository.Update(newTourRequest);
        }

        public List<TourRequest> GetAll()
        {
            return tourRequestRepository.GetAll();
        }

        public List<TourRequest> GetFor(int guideGuestId)
        {
            return tourRequestRepository.GetAll().FindAll(req => req.GuideGuestId == guideGuestId);
        }

        public List<TourRequest> FilterRequests(string state, string city, string language, int maxGuests, DateTime start, DateTime end)
        {
            List<TourRequest> all = tourRequestRepository.GetAll();
            List<TourRequest> filtered = new List<TourRequest>();
            //filtered.AddRange(all.FindAll(request => request.Location.State.Equals(state)));
            //filtered.AddRange(all.FindAll(request => request.Location.City.Equals(city)));
            //filtered.AddRange(all.FindAll(request => request.MaxGuests == maxGuests));
            //filtered.AddRange(all.FindAll(request => request.DateRangeStart >= start && start != default));
            //filtered.AddRange(all.FindAll(request => request.DateRangeEnd <= end));
            //filtered.RemoveAll(all.FindAll(request => !request.Location.State.Equals(state)));
            //return filtered.DistinctBy(f => f.Id).ToList();
            return all.FindAll(request =>
                (request.Location.State.Equals(state) || state == default) &&
                (request.Location.City.Equals(city) || city == default) &&
                (request.Language.Equals(language) || language == default) &&
                (request.MaxGuests <= maxGuests ) &&
                (request.DateRangeStart.Date >= start.Date || start == default) &&
                (request.DateRangeEnd.Date <= end.Date || end == default));
        }

        public List<string> GetLanguagesFromRequests()
        {
            return tourRequestRepository.GetAll().Select(r => r.Language).Distinct().ToList();
        }

        //public void RejectRequest(TourRequest tourRequest)
        //{
        //    tourRequest.Status = TourRequestStatuses.REJECT;
        //}

        public void AcceptRequest(TourRequest tourRequest)
        {
            tourRequest.Status = TourRequestStatuses.ACCEPTED.ToString();
            tourRequestRepository.Update(tourRequest);
        }

        public bool IsValid(TourRequest tourRequest,DateTime date)
        {
            return tourRequest.DateRangeStart <= date && tourRequest.DateRangeEnd >= date;
        }
    }
}
