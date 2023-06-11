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
using System.Windows.Shapes;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for FullReviewWindow.xaml
    /// </summary>
    public partial class FullReviewWindow : Window
    {
        public string GuestName { get; set; }
        public string TourName { get; set; }
        public string KeyPointName { get; set; }
        public string Rating { get; set; }
        public string Comment { get; set; }
        public FullReviewWindow(Card card)
        {
            InitializeComponent();
            this.DataContext = this;
            this.GuestName = "Guest name: " + card.GuestName;
            this.TourName = "Tour name: " + card.TourName;
            this.KeyPointName = "Key point name: " + card.KeyPointName;
            double rating = (double) (card.TourFun + card.GeneralKnowledge + card.LanguageKnowledge) / 3;
            this.Rating = "Rating: " + String.Format("{0:0.00}", rating);
            this.Comment = "Full comment: " + card.Comment;
        }
    }
}
