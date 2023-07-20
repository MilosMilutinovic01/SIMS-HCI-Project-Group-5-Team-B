using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
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

        public List<TourRequest> GetAllWaiting()
        {
            return tourRequestRepository.GetAll().FindAll(request => request.Status == TourRequestStatuses.WAITING);
        }

        public List<TourRequest> GetFor(int guideGuestId)
        {
            return tourRequestRepository.GetAll().FindAll(req => req.GuideGuestId == guideGuestId);
        }

        public List<TourRequest> FilterRequests(string state, string city, string language, int maxGuests, DateTime start, DateTime end)
        {
            List<TourRequest> allRequests = tourRequestRepository.GetAll();
            return allRequests.FindAll(request =>
                (request.Location.State.Equals(state) || state == default) &&
                (request.Location.City.Equals(city) || city == default) &&
                (request.Language.Equals(language) || language == default) &&
                (request.MaxGuests <= maxGuests ) &&
                (request.DateRangeStart.Date >= start.Date || start == default) &&
                (request.DateRangeEnd.Date <= end.Date || end == default) &&
                (request.Status == TourRequestStatuses.WAITING));
        }

        public List<string> GetLanguagesFromRequests()
        {
            return tourRequestRepository.GetAll().Select(r => r.Language).Distinct().ToList();
        }

        public void AcceptRequest(TourRequest tourRequest, int tourId)
        {
            tourRequest.Status = TourRequestStatuses.ACCEPTED;
            tourRequest.AcceptedTourId = tourId;
            tourRequestRepository.Update(tourRequest);
            NotificationService notificationService = new NotificationService();
            notificationService.SendWithAdditionalInfo(tourRequest.GuideGuestId, "Guide accepted your tour request", tourId);
        }

        public bool IsValid(TourRequest tourRequest,DateTime date)
        {
            return tourRequest.DateRangeStart <= date && tourRequest.DateRangeEnd >= date;
        }

        public void TourCreatedFromLocatinoStatistics(int tourId)
        {
            Tour createdTour = new TourService().getById(tourId);

            NotificationService notificationService = new NotificationService();
            List<int> sentNotifications = new List<int>();

            foreach (var tourRequest in tourRequestRepository.GetAll())
            {
                if (sentNotifications.Contains(tourRequest.GuideGuestId)) continue;
                if (tourRequest.Location == createdTour.Location)
                {
                    sentNotifications.Add(tourRequest.GuideGuestId);
                    notificationService.SendWithAdditionalInfo(tourRequest.GuideGuestId, "Guide created tour similar to your request", tourId);
                }
            }
        }

        public void TourCreatedFromLanguageStatistics(int tourId)
        {
            Tour createdTour = new TourService().getById(tourId);

            NotificationService notificationService = new NotificationService();
            List<int> sentNotifications = new List<int>();

            foreach(var tourRequest in tourRequestRepository.GetAll())
            {
                if (sentNotifications.Contains(tourRequest.GuideGuestId)) continue;
                if(tourRequest.Language == createdTour.Language)
                {
                    sentNotifications.Add(tourRequest.GuideGuestId);
                    notificationService.SendWithAdditionalInfo(tourRequest.GuideGuestId, "Guide created tour similar to your request", tourId);
                }
            }
        }
    }
}
