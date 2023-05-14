using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.View;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class OwnerProfileViewModel
    {
        public Owner Owner {get; set;}
        public int NumberOfGrades { get; set; }
        public OwnerProfileViewModel(Owner owner, SuperOwnerService superOwnerService)
        {
            this.Owner = owner;
            this.NumberOfGrades = superOwnerService.GetNumberOfGrades(owner);
        }





    }
}
