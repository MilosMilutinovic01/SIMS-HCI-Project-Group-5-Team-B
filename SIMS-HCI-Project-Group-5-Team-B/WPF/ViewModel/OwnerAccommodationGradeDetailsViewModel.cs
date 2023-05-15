using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.View;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
   
    public class OwnerAccommodationGradeDetailsViewModel
    {
        private ListBox imageListBox;
        public RelayCommand CancelCommand { get; }
        public OwnerAccommodationGrade SelectedOwnerAccommodationGrade { get; set; }

        public OwnerAccommodationGradeDetailsViewModel(OwnerAccommodationGrade SelectedOwnerAccommodationGrade,ListBox imageListBox)
        {
            this.SelectedOwnerAccommodationGrade = SelectedOwnerAccommodationGrade;
            this.imageListBox = imageListBox;
            CancelCommand = new RelayCommand(Cancel_Execute, CanExecute);
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

        private void Cancel_Execute()
        {
            App.Current.Windows[4].Close();
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}
