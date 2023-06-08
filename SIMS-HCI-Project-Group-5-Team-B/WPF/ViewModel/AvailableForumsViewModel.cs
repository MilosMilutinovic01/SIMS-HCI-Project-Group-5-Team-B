using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class AvailableForumsViewModel
    {
        public ObservableCollection<Forum> Forums { get; set; }
        public Forum SelectedForum { get; set; }
        private ForumService forumService;
        private AccommodationService accommodationService;
        public Owner Owner { get; set; }
        public RelayCommand ForumWindowCommand { get; }

        public AvailableForumsViewModel(Owner owner,AccommodationService accommodationService)
        {
            forumService = new ForumService();
            Forums = new ObservableCollection<Forum>(forumService.GetAll());
            this.Owner = owner;
            this.accommodationService = accommodationService;
            ForumWindowCommand = new RelayCommand(ForumWindow_Execute, ForumWindowCanExecute);
            UpdateForumsUsefulness(Forums);
            
        }

        public void UpdateForumsUsefulness(ObservableCollection<Forum> Forums)
        {
            //za slucaj ako se obise neka rezervacija
            foreach (Forum forum in Forums)
            {
                forum.IsVeryUseful = forumService.IsForumVeryUseful(forum);
                forumService.Update(forum);
            } 
        }

        public bool ForumWindowCanExecute()
        {
            return true;
        }

        public void ForumWindow_Execute()
        {
            if(SelectedForum != null)
            {
                ForumWindow forumWindow = new ForumWindow(SelectedForum,forumService,Owner,accommodationService,Forums);
                forumWindow.Show();
            } 
        }

    }
}
