using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Application.UseCases
{
    public class VoucherService
    {
        private Repository<Voucher> voucherRepository;
        private TourAttendanceService tourAttendanceService;
        public VoucherService()
        {
            voucherRepository = new Repository<Voucher>();
            tourAttendanceService = new TourAttendanceService();
        }
        public List<Voucher> GetAll()
        {
            return voucherRepository.GetAll();
        }
        public void Save(Voucher newVoucher)
        {
            voucherRepository.Save(newVoucher);
        }
        public void SaveAll(List<Voucher> newVoucher)
        {
            voucherRepository.SaveAll(newVoucher);
        }
        public void Delete(Voucher voucher)
        {
            voucherRepository.Delete(voucher);
        }
        public void Update(Voucher voucher)
        {
            voucherRepository.Update(voucher);
        }
        public List<Voucher> FindBy(string[] propertyNames, string[] values)
        {
            return voucherRepository.FindBy(propertyNames, values);
        }

        public void SendVouchers(int tourAppointmentId)
        {
            List<int> guestIds = tourAttendanceService.FindAllGuestsByAppointment(tourAppointmentId);
            List<Voucher> vouchers = new List<Voucher>();
            foreach(int guestId in guestIds)
            {
                vouchers.Add(new Voucher(guestId, DateTime.Now));
            }
            SaveAll(vouchers);
        }
    }
}