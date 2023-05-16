using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SIMS_HCI_Project_Group_5_Team_B.DTO
{
    public class GuideGuestTourDTO
    {
        public Tour Tour { get; set; }
        public string FirstImage { get; set; }

        public GuideGuestTourDTO(Tour tour, string firstImage)
        {
            Tour = tour;
            FirstImage = firstImage;
        }
    }
}
