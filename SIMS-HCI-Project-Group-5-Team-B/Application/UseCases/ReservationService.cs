using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{

    public class ReservationService
    {
        private IReservationRepository reservationRepository;
        private AccommodationService accommodationService;
        private IOwnerGuestRepository ownerGuestRepository;
        private IReservationChangeRequestRepository reservationChangeRequestRepository;
        public ReservationService(AccommodationService accommodationService)
        {
            this.reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();
            this.accommodationService = accommodationService;
            this.ownerGuestRepository = Injector.Injector.CreateInstance<IOwnerGuestRepository>();
            this.reservationChangeRequestRepository = Injector.Injector.CreateInstance<IReservationChangeRequestRepository>();
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
                Accommodation accommodation = accommodationService.getById(reservation.AccommodationId);
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
                if (IsAccomodationAvailable(selectedAccommodation, start, end))
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
                if (IsAccomodationAvailable(selectedAccommodation, start, end))
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
            return reservation.isModifiable() &&
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

    }
}

