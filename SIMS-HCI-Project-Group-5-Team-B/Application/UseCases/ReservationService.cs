using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.View;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System;
using System.Collections.Generic;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public struct ReservationRecommendation
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ReservationRecommendation(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }

    public class ReservationService
    {
        private IReservationRepository reservationRepository;
        private AccommodationService accommodationController;
        private IOwnerGuestRepository ownerGuestRepository;
        public ReservationService(AccommodationService accommodationController, IOwnerGuestRepository ownerGuestRepository, IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository; 
            this.accommodationController = accommodationController;
            this.ownerGuestRepository = ownerGuestRepository;
            GetAccomodationReference();
            GetOwnerGuestReference();

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
                Accommodation accommodation = accommodationController.getById(reservation.AccommodationId);
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



        public List<Reservation> GetSuiableReservationsForGrading(Owner owner)
        {
            ///TODO: nina ovdje ces sigurno doavati uslov da si ti vlasnik
            List<Reservation> reservations = GetAll();
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
            foreach (Reservation reservation in GetAll())
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
        
        public List<ReservationGridView> GetReservationsForGuestGrading(int ownerGuestId)
        {
            List<ReservationGridView> reservationViews = new List<ReservationGridView>();
            foreach (Reservation reservation in GetAll())
            {
                if(reservation.OwnerGuestId == ownerGuestId)
                {
                    bool isForGrading = true;
                    if (!(reservation.EndDate.AddDays(5) > DateTime.Today && reservation.EndDate < DateTime.Today && reservation.IsGradedByGuest == false))
                    {
                        isForGrading = false;
                    }

                    bool isModifiable = true;
                    if(reservation.StartDate <= DateTime.Today) 
                        isModifiable = false;
                    reservationViews.Add(new ReservationGridView(reservation, isForGrading,isModifiable));
                }
                

            }
            return reservationViews;
        }


        public bool IsAccomodationAvailableForChangingReservationDates(Reservation selectedReservation, DateTime startDate, DateTime endDate)
        {
            //u listu ubacije sve rezervacije od zeljenog smestaja, treba da preskoci rezervaciju koju zelimo da pomerimo
            List<Reservation> accomodationReservations = GetAccomodationReservations(selectedReservation.Accommodation);
            //izbacili smo rezervaciju za koju zelimo da pomerimo datume
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

