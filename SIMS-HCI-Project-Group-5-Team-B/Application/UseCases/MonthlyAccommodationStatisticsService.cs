using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel;
using System.Collections.ObjectModel;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class MonthlyAccommodationStatisticsService
    {
        private IReservationRepository reservationRepository;
        private IRenovationRequestRepository renovationRequestRepository;
        private IReservationChangeRequestRepository reservationChangeRequestRepository;


        public MonthlyAccommodationStatisticsService()
        {
            this.reservationRepository = Injector.Injector.CreateInstance<IReservationRepository>();
            this.renovationRequestRepository = Injector.Injector.CreateInstance<IRenovationRequestRepository>();
            this.reservationChangeRequestRepository = Injector.Injector.CreateInstance<IReservationChangeRequestRepository>();

        }

        public List<MonthlyAccommodationStatistics> GetMonthlyStatistics(int accommodationId, int year)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            List<RenovationRequest> renovationRequests = renovationRequestRepository.GetAll();
            List<ReservationChangeRequest> reservationChangeRequests = reservationChangeRequestRepository.GetAll();

            List<Reservation> undeletedReservations = reservations.Where(r => r.Accommodation.Id == accommodationId && r.IsDeleted == false && (r.StartDate.Year == year || r.EndDate.Year == year)).ToList();
            List<Reservation> deletedReservations = reservations.Where(r => r.Accommodation.Id == accommodationId && r.IsDeleted == true && r.StartDate.Year == year).ToList();
            renovationRequests = renovationRequests.Where(r => r.Reservation.AccommodationId == accommodationId && r.Reservation.StartDate.Year == year).ToList();
            reservationChangeRequests = reservationChangeRequests.Where(r => r.Reservation.Accommodation.Id == accommodationId && r.RequestStatus == REQUESTSTATUS.Confirmed && r.Start.Year == year).ToList(); //&& r.Reservation.StartDate.Year == year

            List<MonthlyAccommodationStatistics> monthlyAccommodationsStatistics = new List<MonthlyAccommodationStatistics>();
            for (int month = 1; month <= 12; month++)
            {
                MonthlyAccommodationStatistics monthlyAccommodationStatistics = new MonthlyAccommodationStatistics();
                monthlyAccommodationStatistics.Year = year;

                monthlyAccommodationStatistics.Month = month;

                monthlyAccommodationStatistics.NumberReservations = undeletedReservations.Count(r => r.StartDate.Month == month);

                monthlyAccommodationStatistics.NumberOfCancelledReservations = deletedReservations.Count(r => r.StartDate.Month == month);

                monthlyAccommodationStatistics.NumberOfChangedReservationDates = reservationChangeRequests.Count(r => r.Start.Month == month);

                monthlyAccommodationStatistics.NumberRenovationRequests = renovationRequests.Count(r => r.Reservation.StartDate.Month == month);

                monthlyAccommodationsStatistics.Add(monthlyAccommodationStatistics);
                DateTime firstDay = new DateTime(year, month, 1);
                DateTime lastDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                monthlyAccommodationStatistics.Busyness = GetNumberOfReservedDaysInMonth(undeletedReservations, year, month) / ((lastDay - firstDay).TotalDays + 1);
                monthlyAccommodationStatistics.IsBusiest = false;
            }

            return monthlyAccommodationsStatistics;
        }


        public double GetNumberOfReservedDaysInJanuary(Reservation reservation, int year, int month)
        {
            double sum = 0;
            if (reservation.StartDate.Month != month && reservation.EndDate.Month == month && reservation.StartDate.Year != year && reservation.EndDate.Year == year)
            {
                DateTime firstDayOfMonth = new DateTime(year, month, 1);//prvi dan u mesecu januaru
                sum = sum + (reservation.EndDate - firstDayOfMonth).TotalDays + 1;
            }
            //05.01.2023 - 07.01.2023.
            else if (reservation.StartDate.Month == month && reservation.EndDate.Month == month && reservation.StartDate.Year == year && reservation.EndDate.Year == year)
            {
                sum = sum + (reservation.EndDate - reservation.StartDate).TotalDays + 1;
            }
            //28.01.2023 - 02.02.2023
            else if (reservation.StartDate.Month == month && reservation.EndDate.Month != month && reservation.StartDate.Year == year && reservation.EndDate.Year == year)
            {
                DateTime lastDayOfMonth = new DateTime(year, month, 31);// posednji dan u januaru
                sum = sum + (lastDayOfMonth - reservation.EndDate).TotalDays + 1;
            }
            return sum;
        }


        public double GetNumberOfReservedDaysInDecember(Reservation reservation, int year, int month)
        {
            double sum = 0;
            //29.11.2023 - 03.12.2023
            if (reservation.StartDate.Month != month && reservation.EndDate.Month == month && reservation.StartDate.Year == year && reservation.EndDate.Year == year)
            {
                DateTime firstDayOfMonth = new DateTime(year, month, 1);//prvi dan u decembru
                sum = sum + (reservation.EndDate - firstDayOfMonth).TotalDays + 1;
            }
            //05.12.2023 - 07.12.2023.
            else if (reservation.StartDate.Month == month && reservation.EndDate.Month == month && reservation.StartDate.Year == year && reservation.EndDate.Year == year)
            {
                sum = sum + (reservation.EndDate - reservation.StartDate).TotalDays + 1;
            }
            //30.12.2023 - 02.01.2024
            else if (reservation.StartDate.Month == month && reservation.EndDate.Month != month && reservation.StartDate.Year == year && reservation.EndDate.Year != year)
            {
                DateTime lastDayOfMonth = new DateTime(year, month, 31);
                sum = sum + (lastDayOfMonth - reservation.StartDate).TotalDays + 1;
            }
            return sum;
        }


        public double GetNumberOfReservedDaysInOtherMonths(Reservation reservation, int year, int month)
        {
            double sum = 0;
            //28.03.2023 - 04.04.2023
            if (reservation.StartDate.Month != month && reservation.EndDate.Month == month && reservation.StartDate.Year == year && reservation.EndDate.Year == year)
            {
                DateTime firstDatOfMonth = new DateTime(year, month, 1);
                sum = (reservation.EndDate - firstDatOfMonth).TotalDays + 1;
            }
            //07.04.2023 - 10.04.2023
            else if (reservation.StartDate.Month == month && reservation.EndDate.Month == month && reservation.StartDate.Year == year && reservation.EndDate.Year == year)
            {
                sum = sum + (reservation.EndDate - reservation.StartDate).TotalDays + 1;
            }
            //25.04.2023 - 03.05.2023
            else if (reservation.StartDate.Month == month && reservation.EndDate.Month != month && reservation.StartDate.Year == year && reservation.EndDate.Year == year)
            {
                DateTime lastDayOfMonth = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                sum = sum + (lastDayOfMonth - reservation.StartDate).TotalDays + 1;
            }
            return sum;
        }

        public double GetNumberOfReservedDaysInMonth(List<Reservation> reservations, int year, int month)
        {
            //godina 2023
            double sum = 0;
            foreach (Reservation reservation in reservations)
            {
                //januar
                if (month == 1)
                {
                    sum = sum + GetNumberOfReservedDaysInJanuary(reservation, year, month);
                }
                //decembar
                else if (month == 12)
                {
                    sum = sum + GetNumberOfReservedDaysInDecember(reservation, year, month);
                }
                //ostali meseci
                //primer godina 2023, april
                else
                {
                    sum = sum + GetNumberOfReservedDaysInOtherMonths(reservation, year, month);
                }
            }

            return sum;
        }

        public void MarkBusiest(ObservableCollection<MonthlyAccommodationStatistics> monthlyStatistics)
        {
            double maximumBusyness = 0;
            foreach (MonthlyAccommodationStatistics monthlyAccommodationStatistics in monthlyStatistics)
            {
                if (monthlyAccommodationStatistics.Busyness > maximumBusyness)
                {
                    maximumBusyness = monthlyAccommodationStatistics.Busyness;
                }
            }

            if (maximumBusyness != 0)
            {
                foreach (MonthlyAccommodationStatistics monthlyAccommodationStatistics in monthlyStatistics)
                {

                    if (monthlyAccommodationStatistics.Busyness == maximumBusyness)
                    {
                        monthlyAccommodationStatistics.IsBusiest = true;
                    }
                }
            }

        }




    }
}
