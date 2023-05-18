using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.RepositoryInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.Injector
{
    public class Injector
    {
        private static Dictionary<Type, object> _implementations = new Dictionary<Type, object>
    {
        { typeof(IOwnerGuestRepository), new OwnerGuestCSVRepository() },
        { typeof(IReservationChangeRequestRepository), new ReservationChangeRequestCSVRepository() },
        { typeof(IReservationRepository), new ReservationCSVRepository() },
        { typeof(IRenovationRequestRepository), new RenovationRequestCSVRepository() },
        { typeof(IRenovationRepository), new RenovationCSVRepository() },
        { typeof(IAccommodationRepository), new AccommodationCSVRepository() },
        { typeof(ISuperOwnerGuestTitleRepository), new SuperOwnerGuestTitleCSVRepository() },
        { typeof(IKeyPointRepository), new KeyPointCSVRepository() },
        { typeof(ILocationRepository), new LocationCSVRepository() },
        { typeof(ITourAttendanceRepository), new TourAttendanceCSVRepository() },
        { typeof(ITourGradeRepository), new TourGradeCSVRepository() },
        { typeof(ITourRepository), new TourCSVRepository() },
        { typeof(IAppointmentRepository), new AppointmentCSVRepository() },
        { typeof(ITourRequestRepository), new TourRequestCSVRepository() },
        { typeof(INotificationRepository), new NotificationCSVRepository() },
        { typeof(IUserRepository), new UserCSVRepository() },
        // Add more implementations here
    };
        public static void LoadData()
        {
            try{
                (_implementations[typeof(ITourRepository)] as TourCSVRepository).LoadData();
                (_implementations[typeof(IAppointmentRepository)] as AppointmentCSVRepository).LoadData();
                (_implementations[typeof(ITourRequestRepository)] as TourRequestCSVRepository).LoadData();
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured");
            }
        }

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
