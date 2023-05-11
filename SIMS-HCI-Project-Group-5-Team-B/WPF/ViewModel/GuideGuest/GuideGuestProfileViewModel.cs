using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel.GuideGuest
{
    public class GuideGuestProfileViewModel
    {
        public ObservableCollection<Voucher> Vouchers { get; set; }


        public GuideGuestProfileViewModel()
        {
            Vouchers = new ObservableCollection<Voucher>();
        }
    }
}
