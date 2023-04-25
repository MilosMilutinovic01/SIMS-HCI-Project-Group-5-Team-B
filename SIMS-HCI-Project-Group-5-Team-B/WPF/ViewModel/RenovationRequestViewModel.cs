using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class RenovationRequestViewModel
    {
        public RenovationRequest NewRenovationRequest { get; set; }
        private RenovationRequestService renovationRequestService;
        public RenovationRequestViewModel(int reservationId)
        {
            NewRenovationRequest = new RenovationRequest(reservationId);
            renovationRequestService = new RenovationRequestService();
        }

        public void Send()
        {
            if(NewRenovationRequest.IsValid)
            {
                renovationRequestService.Save(NewRenovationRequest);
                MessageBox.Show("Renovation request sent!");
            }
            else
            {
                MessageBox.Show("Renovation request can not be sent brcause data is not valid");
            }
        }

        public void SetLevel(object sender)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
            NewRenovationRequest.Level = (ReservationLevel)int.Parse(item.Tag.ToString());
            
        }
    }
}
