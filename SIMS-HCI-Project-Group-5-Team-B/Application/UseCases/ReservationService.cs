using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{

    public class ReservationService
    {
        private IReservationRepository reservationRepository;
        private IAccommodationRepository accommodationRepository;
        private IOwnerGuestRepository ownerGuestRepository;
        private IReservationChangeRequestRepository reservationChangeRequestRepository;
        private IRenovationRepository renovationRepository;
        private IRenovationService renovationService;
        public ReservationService()
        {
            this.reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();
            this.accommodationRepository = Injector.Injector.CreateInstance<IAccommodationRepository>();
            this.ownerGuestRepository = Injector.Injector.CreateInstance<IOwnerGuestRepository>();
            this.reservationChangeRequestRepository = Injector.Injector.CreateInstance<IReservationChangeRequestRepository>();
            this.renovationRepository = Injector.Injector.CreateInstance<IRenovationRepository>();
            this.renovationService = Injector.ServiceInjector.CreateInstance<IRenovationService>();
            GetAccomodationReference();
            GetOwnerGuestReference();

        }

        public List<Reservation> GetUndeleted()
        {
            return reservationRepository.GetUndeleted();
        }

        public List<Reservation> GetAll()
        {
            return reservationRepository.GetAll();
        }
        public void Save(Reservation newReservation)
        {
            reservationRepository.Save(newReservation);
            GetAccomodationReference();
            GetOwnerGuestReference();
        }

        public void SaveAll(List<Reservation> reservations)
        {
            reservationRepository.SaveAll(reservations);
            GetAccomodationReference();
            GetOwnerGuestReference();
        }
        public void Delete(Reservation reservation)
        {
            reservationRepository.Delete(reservation);
        }
        public void Update(Reservation reservation)
        {
            reservationRepository.Update(reservation);
            GetAccomodationReference();
            GetOwnerGuestReference();
        }

        public List<Reservation> FindBy(string[] propertyNames, string[] values)
        {
            return reservationRepository.FindBy(propertyNames, values);
        }

        public Reservation getById(int id)
        {
            return GetAll().Find(res => res.Id == id);
        }

        private void GetAccomodationReference()
        {
            foreach (Reservation reservation in GetAll())
            {
                Accommodation accommodation = accommodationRepository.GetById(reservation.AccommodationId);
                if (accommodation != null)
                {
                    reservation.Accommodation = accommodation;
                }
            }
        }


        private void GetOwnerGuestReference()
        {
            //OwnerGuest ownerGuest = new OwnerGuest();
            foreach (Reservation reservation in GetAll())
            {
                OwnerGuest ownerGuest = ownerGuestRepository.GetById(reservation.OwnerGuestId);
                reservation.OwnerGuest = ownerGuest;
            }
        }



        public List<Reservation> GetReservationsForGrading(Owner owner)
        {

            List<Reservation> reservations = GetUndeleted();
            List<Reservation> suitableReservations = new List<Reservation>();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.IsGraded == false && reservation.EndDate.AddDays(5) > DateTime.Today && reservation.EndDate <= DateTime.Today && reservation.Accommodation.Owner.Id == owner.Id)
                {
                    suitableReservations.Add(reservation);
                }
            }
            return suitableReservations;
        }
        public List<ReservationRecommendation> GetReservationRecommendations(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int reservationDays)
        {
            List<ReservationRecommendation> reservationRecommendations = new List<ReservationRecommendation>();
            reservationRecommendations = GetRecommendationsInTimeSpan(selectedAccommodation, startDate, endDate, reservationDays);
            if (reservationRecommendations.Count == 0)
            {
                reservationRecommendations = GetRecommendationsOutOfTimeSpan(selectedAccommodation, startDate.Date, endDate.Date, reservationDays);
            }

            return reservationRecommendations;
        }

        private List<ReservationRecommendation> GetRecommendationsInTimeSpan(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int reservationDays)
        {
            List<ReservationRecommendation> reservationRecommendations = new List<ReservationRecommendation>();
            DateTime start = startDate;
            DateTime end = startDate;
            while (start.AddDays(reservationDays - 1) <= endDate)
            {
                end = start.AddDays(reservationDays - 1);
                if (IsAccomodationAvailable(selectedAccommodation, start, end) && renovationService.IsAccomodationNotInRenovation(selectedAccommodation,start,end))
                {
                    reservationRecommendations.Add(new ReservationRecommendation(start, end));
                }
                start = start.AddDays(1);
            }

            return reservationRecommendations;
        }

        private List<ReservationRecommendation> GetRecommendationsOutOfTimeSpan(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int reservationDays)
        {
            List<ReservationRecommendation> reservationRecommendations = new List<ReservationRecommendation>();
            DateTime start = endDate.AddDays(1);
            DateTime end = endDate.AddDays(1);
            int count = 0;
            while (count != 3)
            {
                end = start.AddDays(reservationDays - 1);
                if (IsAccomodationAvailable(selectedAccommodation, start, end) && renovationService.IsAccomodationNotInRenovation(selectedAccommodation, start, end))
                {
                    reservationRecommendations.Add(new ReservationRecommendation(start, end));
                    count++;
                }
                start = start.AddDays(1);
            }

            return reservationRecommendations;
        }

        public List<Reservation> GetAccomodationReservations(Accommodation selectedAccommodation)
        {
            List<Reservation> accomodationReservations = new List<Reservation>();
            foreach (Reservation reservation in GetUndeleted())
            {
                if (reservation.AccommodationId == selectedAccommodation.Id)
                {
                    accomodationReservations.Add(reservation);
                }
            }
            return accomodationReservations;
        }

        public bool IsAccomodationAvailable(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate)
        {
            List<Reservation> accomodationReservations = GetAccomodationReservations(selectedAccommodation);

            foreach (Reservation reservation in accomodationReservations)
            {
                bool isInRange = startDate >= reservation.StartDate && startDate <= reservation.EndDate ||
                                 endDate >= reservation.StartDate && endDate <= reservation.EndDate;

                bool isOutOfRange = startDate <= reservation.StartDate && endDate >= reservation.EndDate;

                if (isInRange)
                {
                    return false;
                }
                else if (isOutOfRange)
                {
                    return false;
                }

            }
            return true;

        }



        public bool IsReservationDeletable(Reservation reservation)
        {
            // reservation can not be deleted if there are pending requests
            return reservation.IsDeletable() &&
            !reservationChangeRequestRepository.GetAll().Any(chreq => chreq.ReservationId == reservation.Id && chreq.RequestStatus == REQUESTSTATUS.Pending);
        }

        public bool IsReservationModifiable(Reservation reservation)
        {
            return reservation.IsModifiable() &&
            !reservationChangeRequestRepository.GetAll().Any(chreq => chreq.ReservationId == reservation.Id && chreq.RequestStatus == REQUESTSTATUS.Pending);
        }

        public bool IsReservationGradable(Reservation reservation)
        {
            return reservation.IsGradable();
        }

        public bool IsAccomodationAvailableForChangingReservationDates(Reservation selectedReservation, DateTime startDate, DateTime endDate)
        {
            List<Reservation> accomodationReservations = GetAccomodationReservations(selectedReservation.Accommodation);
            accomodationReservations.Remove(selectedReservation);

            foreach (Reservation reservation in accomodationReservations)
            {

                bool isInRange = startDate >= reservation.StartDate && startDate <= reservation.EndDate ||
                                 endDate >= reservation.StartDate && endDate <= reservation.EndDate;

                bool isOutOfRange = startDate <= reservation.StartDate && endDate >= reservation.EndDate;

                if (isInRange)
                {
                    return false;
                }
                else if (isOutOfRange)
                {
                    return false;
                }

            }
            return true;

        }


        public List<RenovationProposalDates> GetRenovationProposalDatesInTimeSpan(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int renovationDays)
        {
            List<RenovationProposalDates> renovationProposalDates = new List<RenovationProposalDates>();
            DateTime start = startDate;
            DateTime end = startDate;
            while (start.AddDays(renovationDays - 1) <= endDate)
            {
                end = start.AddDays(renovationDays - 1);
                if (IsAccomodationAvailable(selectedAccommodation, start, end) && renovationService.IsAccomodationNotInRenovation(selectedAccommodation,start,end))
                {
                    renovationProposalDates.Add(new RenovationProposalDates(start, end));
                }
                start = start.AddDays(1);
            }

            return renovationProposalDates;
        }

        public List<AnywhereAnytimeReservation> GetAASuggestions(int guestNumber, Nullable<DateTime> start, Nullable<DateTime> end, int reservationDays) 
        {
            //going through list of accommodations
            List<AnywhereAnytimeReservation> suggestions = new List<AnywhereAnytimeReservation>();
            Random rdn = new Random();
            foreach ( Accommodation a in accommodationRepository.GetAll())
            {
                if(a.IsGuestsDaysAppropriate(guestNumber, reservationDays))
                {
                    //if it meets requirenments, check dates
                    if(CheckAADates(start,end))
                    {
                        //we already checked if dates are null, they are not!
                        suggestions.AddRange(GetAccommodationAASuggestions(a, GetRecommendationsInTimeSpan(a, (DateTime)start, (DateTime)end, reservationDays)));
                    }
                    else
                    {
                        //out od time span, check for random adding of days
                        suggestions.AddRange(GetAccommodationAASuggestions(a, GetRecommendationsOutOfTimeSpan(a, DateTime.Today, DateTime.Today.AddDays(rdn.Next(120)), reservationDays)));
                    }
                }
                else
                {
                    continue;
                }
            }


            return Randomize(suggestions);
        }

        public bool CheckAADates(Nullable<DateTime> start, Nullable<DateTime> end)
        {
            if(start == null || end == null)
                return false;
            return true;
        }

        public List<AnywhereAnytimeReservation> GetAccommodationAASuggestions(Accommodation accommodation, List<ReservationRecommendation> reservationRecommendations)
        {
            List<AnywhereAnytimeReservation> suggestions= new List<AnywhereAnytimeReservation>();
            int couter = 0;
            foreach(ReservationRecommendation r in reservationRecommendations)
            {
                //just to break the loop and take first 3
                if (couter == 3)
                    break;
                suggestions.Add(new AnywhereAnytimeReservation(accommodation, r.Start, r.End));
                couter++;
            }
            return suggestions;
        }

        private List<AnywhereAnytimeReservation> Randomize(List<AnywhereAnytimeReservation> list)
        {
            Random rdn = new Random();
            var randomized =  list.OrderBy(item => rdn.Next());
            return randomized.ToList();
        }

        public int GetReservationsNumberInLastYear(int ownerGuestId)
        {
            return reservationRepository.GetOwnerGuestsReservationInLastYear(ownerGuestId).Count();
        }


    }
}

