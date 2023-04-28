﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class YearlyAccommodationStatisticsService
    {
        private IReservationRepository reservationRepository;
        private IRenovationRequestRepository renovationRequestRepository;
        private IReservationChangeRequestRepository reservationChangeRequestRepository;


        public YearlyAccommodationStatisticsService()
        {
            this.reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();
            this.renovationRequestRepository = Injector.Injector.CreateInstance<IRenovationRequestRepository>();
            this.reservationChangeRequestRepository = Injector.Injector.CreateInstance<IReservationChangeRequestRepository>();

        }


        public List<YearlyAccommodationStatistics> GetYearlyAccommodationStatistics(int accommodationId)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            List<RenovationRequest> renovationRequests = renovationRequestRepository.GetAll();
            List<ReservationChangeRequest> reservationChangeRequests = reservationChangeRequestRepository.GetAll();

            List<Reservation> undeletedReservations = reservations.Where(r => r.Accommodation.Id == accommodationId && r.IsDeleted == false).ToList(); //dobavlja sve rezervacije za zeljeni smestaj koje nisu obrisane
            renovationRequests = renovationRequests.Where(r => r.Reservation.AccommodationId == accommodationId).ToList(); //dobavlja sve preporuke renoviranja za zeljeni smestaj 
            reservationChangeRequests = reservationChangeRequests.Where(r => r.Reservation.Accommodation.Id == accommodationId && r.RequestStatus == REQUESTSTATUS.Confirmed).ToList(); //dobavlja sve potvrdjenje zahteve za zeljeni smestaj
            List<Reservation> deletedReservations = reservations.Where(r => r.Accommodation.Id == accommodationId && r.IsDeleted == true).ToList(); //ako je zahtev potvrdjen znaci da je doslo do pomeranja


            //dobavlja sve razlicite godine za koje se desile rezervacije ili renoviranja ali za poseban smestaj
            List<int> years = new List<int>();
            years.AddRange(undeletedReservations.Select(r => r.StartDate.Year).Distinct());
            //*****OVO OBRISATI MENI JE GLUPO!!!!!!!!!!!!!!!!!!!
            years.AddRange(undeletedReservations.Select(r => r.EndDate.Year).Distinct());
            //******
            years.AddRange(deletedReservations.Select(r => r.StartDate.Year).Distinct());
            years.AddRange(reservationChangeRequests.Select(r => r.Start.Year).Distinct());
            years = years.Distinct().OrderBy(y => y).ToList();
            
            List<YearlyAccommodationStatistics> yearlyStatistics = new List<YearlyAccommodationStatistics>();
            foreach (int year in years)
            {
                YearlyAccommodationStatistics yearlyAccommodationStatistics = new YearlyAccommodationStatistics();
                yearlyAccommodationStatistics.Year = year;

                yearlyAccommodationStatistics.NumberReservations = undeletedReservations.Count(r => r.StartDate.Year == year);

                yearlyAccommodationStatistics.NumberOfCancelledReservations = deletedReservations.Count(r => r.StartDate.Year == year && r.IsDeleted == true);

                //ovde je pre bilo r => r.Reservation.StartDate.Year == year
                yearlyAccommodationStatistics.NumberOfChangedReservationDates = reservationChangeRequests.Count(r => r.Start.Year == year);

                yearlyAccommodationStatistics.NumberRenovationRequests = renovationRequests.Count(r => r.Reservation.StartDate.Year == year);


                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime lastDay = new DateTime(year, 12, 31);
                yearlyAccommodationStatistics.Busyness = GetNumberOfReservedDaysInYear(undeletedReservations, year) / ((lastDay - firstDay).TotalDays + 1);
                yearlyAccommodationStatistics.IsBusiest = false;

                yearlyStatistics.Add(yearlyAccommodationStatistics);
            }

            return yearlyStatistics;

        }

        public double GetNumberOfReservedDaysInYear(List<Reservation> reservations, int year)
        {
            double sum = 0;
            foreach (Reservation reservation in reservations)
            {
                if (reservation.EndDate.Year == year && reservation.StartDate.Year == year)
                {
                    sum = sum + (reservation.EndDate - reservation.StartDate).TotalDays + 1;
                }
                else if (reservation.EndDate.Year == year && reservation.StartDate.Year != year)
                {
                    DateTime firstDay = new DateTime(year, 1, 1);
                    sum = sum + (reservation.EndDate - firstDay).TotalDays + 1;
                }
                else if (reservation.EndDate.Year != year && reservation.StartDate.Year == year)
                {
                    DateTime lastDay = new DateTime(reservation.StartDate.Year, 12, 31);
                    sum = sum + (lastDay - reservation.StartDate).TotalDays + 1;
                }
            }

            return sum;
        }

        public void MarkBusiest(ObservableCollection<YearlyAccommodationStatistics> yearlyStatistics)
        {
            double maximumBusyness = 0;
            foreach (YearlyAccommodationStatistics yearlyAccommodationStatistics in yearlyStatistics)
            {
                if (yearlyAccommodationStatistics.Busyness > maximumBusyness)
                {
                    maximumBusyness = yearlyAccommodationStatistics.Busyness;
                }
            }

            foreach (YearlyAccommodationStatistics yearlyAccommodationStatistics in yearlyStatistics)
            {
                if (yearlyAccommodationStatistics.Busyness == maximumBusyness)
                {
                    yearlyAccommodationStatistics.IsBusiest = true;
                }
            }

        }

    }
}