﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using SIMS_HCI_Project_Group_5_Team_B.Model;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class ReservationController
    {
        private Repository<Reservation> reservationRepository;
        private AccommodationController accommodationController;
        public ReservationController( AccommodationController accommodationController)
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
            foreach(Reservation reservation in GetAll())
            {
                Accommodation accommodation = accommodationController.getById(reservation.AccommodationId); 
                if(accommodation != null)
                {
                    reservation.Accommodation = accommodation;
                }
            }
        }

        private void GetOwnerGuestReference()
        {
            OwnerGuest ownerGuest = new OwnerGuest();
            foreach(Reservation reservation in GetAll())
            {
                reservation.OwnerGuest = ownerGuest;
            }
        }

        public List<Reservation> GetSuiableReservationsForGrading()
        {
            List<Reservation> reservations = GetAll();
            List<Reservation> suitableReservations = new List<Reservation>();
            foreach(Reservation reservation in reservations)
            {
                if(reservation.IsGraded == false && reservation.EndDate.AddDays(5) > DateTime.Today  && reservation.EndDate <= DateTime.Today)
                {
                    suitableReservations.Add(reservation);
                }
            }
            return suitableReservations;
        }
    }
}
