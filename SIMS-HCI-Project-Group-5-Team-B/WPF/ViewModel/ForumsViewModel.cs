﻿using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ForumsViewModel: INotifyPropertyChanged
    {
        private ForumService ForumService { get; set; }

        public ObservableCollection<Comment> Comments { get; set; }
        private Forum forum;

        public string ForumLocation { get; set; }
        private string forumStatus;
        public string ForumStatus 
        {
            get {  return forumStatus; }
            set
            {
                if(forumStatus != value)
                {
                    forumStatus = value;
                    NotifyPropertyChanged(nameof(ForumStatus));
                    
                }
            }
        
        }  
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand CloseForumCommand { get; set; }
        private UserController userController;
        private int ownerGuestId;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public ForumsViewModel(Forum forum, int ownerGuestId) 
        { 
            this.forum = forum; 
            ForumService = new ForumService();
            Comments = new ObservableCollection<Comment>(ForumService.GetForumsComments(forum.Id));
            ForumLocation = forum.Location.ToString();
            ForumStatus = "STATUS: " + forum.ForumStatus.ToString();
            CloseCommand = new RelayCommand(OnClose);
            userController = new UserController();
            this.ownerGuestId = ownerGuestId;

            CloseForumCommand = new RelayCommand(OnCloseForum, CloseForum_CanExecute);

        }

        public void OnClose()
        {
            App.Current.Windows.OfType<ForumsWIndow>().FirstOrDefault().Close();
        }

        public void OnCloseForum()
        {
            
            ForumService.CloseForum(forum.Id, ownerGuestId);
            ForumStatus = "STATUS: " + forum.ForumStatus.ToString();
        }

        public bool CloseForum_CanExecute()
        {
            if(forum.ForumStatus == FORUMSTATUS.Closed || forum.OwnerGuestId != ownerGuestId)
            {
                return false;
            }
            return true;
        }
    }
}
