using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.GuideGuest.UserControls
{
    public partial class TourInformationUserControl : UserControl
    {
        public GuideGuestTourDTO TourDTO
        {
            get { return (GuideGuestTourDTO)GetValue(TourDTOProperty); }
            set { SetValue(TourDTOProperty, value); }
        }

        public static readonly DependencyProperty TourDTOProperty =
            DependencyProperty.Register("TourDTO", typeof(GuideGuestTourDTO), typeof(TourInformationUserControl),
                new PropertyMetadata(null));


        public ICommand BookTourCommand
        {
            get { return (ICommand)GetValue(BookTourCommandProperty); }
            set { SetValue(BookTourCommandProperty, value); }
        }

        public static readonly DependencyProperty BookTourCommandProperty =
            DependencyProperty.Register("BookTourCommand", typeof(ICommand), typeof(TourInformationUserControl),
                new PropertyMetadata(null));


        public List<Voucher> Vouchers
        {
            get { return (List<Voucher>)GetValue(VouchersProperty); }
            set { SetValue(VouchersProperty, value); }
        }

        public static readonly DependencyProperty VouchersProperty =
            DependencyProperty.Register("Vouchers", typeof(List<Voucher>), typeof(TourInformationUserControl),
                new PropertyMetadata(null));


        public List<Appointment> Appointments
        {
            get { return (List<Appointment>)GetValue(AppointmentsProperty); }
            set { SetValue(AppointmentsProperty, value); }
        }

        public static readonly DependencyProperty AppointmentsProperty =
            DependencyProperty.Register("Appointments", typeof(List<Appointment>), typeof(TourInformationUserControl),
                new PropertyMetadata(null));


        public TourInformationUserControl()
        {
            InitializeComponent();
        }
    }
}
