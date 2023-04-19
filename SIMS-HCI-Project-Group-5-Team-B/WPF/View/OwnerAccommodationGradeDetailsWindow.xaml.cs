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
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;

namespace SIMS_HCI_Project_Group_5_Team_B.View
{
    
    public partial class OwnerAccommodationGradeDetailsWindow : Window
    {

        public OwnerAccommodationGrade SelectedOwnerAccommodationGrade { get; set; }
        public OwnerAccommodationGradeDetailsWindow(OwnerAccommodationGrade SelectedOwnerAccommodationGrade)
        {
            InitializeComponent();
            DataContext = this;
            this.SelectedOwnerAccommodationGrade = SelectedOwnerAccommodationGrade;
            ShowImages();
        }

        private void ShowImages()
        {
            imageListBox.Items.Clear();

            foreach (String imageSource in SelectedOwnerAccommodationGrade.PictureURLs)
            {
                imageListBox.Items.Add(imageSource);
            }
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
