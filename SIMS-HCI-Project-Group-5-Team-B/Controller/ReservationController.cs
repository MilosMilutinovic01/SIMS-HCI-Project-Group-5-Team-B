using SIMS_HCI_Project_Group_5_Team_B.Model;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public struct ReservationRecommendation
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ReservationRecommendation(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }
    }

    public class ReservationView : INotifyPropertyChanged
    {
        public Reservation Reservation { get; set; }
        public bool isForGrading;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public bool IsForGrading 
        {
            get { return isForGrading; }
            set
            {
                if (value != isForGrading) 
                {
                    isForGrading = value;
                    OnPropertyChanged();
                    NotifyPropertyChanged(nameof(IsForGrading));  //sta je ov????
                }
            }
        
        }
        public ReservationView(Reservation reservation, bool isForGrading)
        {
            this.Reservation = reservation;
            this.IsForGrading = isForGrading;
        }
    }

    public class ReservationController
    {
        private Repository<Reservation> reservationRepository;
        private AccommodationController accommodationController;
        public ReservationController(AccommodationController accommodationController)
        {
            reservationRepository = new Repository<Reservation>();
            this.accommodationController = accommodationController;
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
        }

        public void SaveAll(List<Reservation> reservations)
        {
            reservationRepository.SaveAll(reservations);
        }
        public void Delete(Reservation reservation)
        {
            reservationRepository.Delete(reservation);
        }
        public void Update(Reservation reservation)
        {
            reservationRepository.Update(reservation);
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
            OwnerGuest ownerGuest = new OwnerGuest();
            foreach (Reservation reservation in GetAll())
            {
                reservation.OwnerGuest = ownerGuest;
            }
        }

        public List<Reservation> GetSuiableReservationsForGrading()
        {
            List<Reservation> reservations = GetAll();
            List<Reservation> suitableReservations = new List<Reservation>();
            foreach (Reservation reservation in reservations)
            {
                if (reservation.IsGraded == false && reservation.EndDate.AddDays(5) > DateTime.Today && reservation.EndDate <= DateTime.Today)
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
                bool isInRange = (startDate >= reservation.StartDate && startDate <= reservation.EndDate) ||
                                 (endDate >= reservation.StartDate && endDate <= reservation.EndDate);

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
        //TODO add another condition - username must be the current active one! - from active OwnerGuest
        public List<ReservationView> GetReservationsForGuestGrading()
        {
            List<ReservationView> reservationViews = new List<ReservationView>();
            foreach (Reservation reservation in GetAll())
            {
                
                bool boolProp = true;
                if (!(reservation.EndDate.AddDays(5) > DateTime.Today && reservation.EndDate < DateTime.Today && reservation.IsGradedByGuest == false))
                {
                    boolProp = false;
                }

                reservationViews.Add(new ReservationView(reservation, boolProp));
                
            }
            return reservationViews;
        }
    }
}

