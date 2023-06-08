using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class NewCommentViewModel: INotifyPropertyChanged, IDataErrorInfo
    {

        private ForumService forumService;
        private ICommentService commentService;
        private UserController userController;
        private OwnerGuestService ownerGuestService;
        private Forum forum;
        private ObservableCollection<Comment> Comments;
        private int ownerGuestId;

        private string content;
        public string Content
        {
            get { return content; }
            set
            {
                if(value != content)
                {
                    content = value;
                    NotifyPropertyChanged(nameof(Content));
                }
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if(columnName == "Content")
                {
                    if (string.IsNullOrEmpty(Content))
                    {
                        return "Comment must be filled";
                    }
                }

                return null;
            }
        }

        public RelayCommand CloseCommand { get; set; }
        public RelayCommand PostCommand { get; set; }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        
        public NewCommentViewModel(int ownerguestId,ForumService forumService,  Forum forum, ObservableCollection<Comment> Comments) 
        { 
            this.commentService = ServiceInjector.CreateInstance<ICommentService>();
            this.forumService = forumService;
            this.forum = forum;
            this.Comments = Comments;
            this.ownerGuestId = ownerguestId;
            CloseCommand = new RelayCommand(OnClose);
            PostCommand = new RelayCommand(OnPost);
            ownerGuestService = new OwnerGuestService();
            userController = new UserController();
            
        }   

        public void OnClose()
        {
            App.Current.Windows.OfType<NewCommentWindow>().FirstOrDefault()?.Close();
        }

        public void OnPost()
        {
            if(this.IsValid)
            {
                OwnerGuest ownerGuest = ownerGuestService.GetById(ownerGuestId);
                User user = userController.GetByUsername(ownerGuest.Username);

                //
                Comment comment = new Comment(user.Id, forum.Id, Content);
                comment.IsFromOwnerWithAccommodationOnLocation = false;
                if (forumService.WasGuestOnLocation(ownerGuest, forum.Location))
                {
                    comment.WasNotOnLocation = false;
                }
                else
                {
                    comment.WasNotOnLocation = true;
                }
                comment.CanReport = false; //dok se ne utvrdi drugacije, odmah je false jer razliciti vlasnici mogu razlitite stvari da reportuju
                                           //zavisi da li vlasnik ima smestaj na lokaciji forma i da li je forum ugasen
                comment.NumberOfReports = 0;
                //comment.IsAlreadyReported = false;
                comment.OwnersWhoReportedCommentString = "-1";
                commentService.Save(comment);

                forum.IsVeryUseful = forumService.IsForumVeryUseful(forum);
                forumService.Update(forum);
                //Updating observable list
                Comments.Add(comment);
                MessageBox.Show("COmment successfully posted!");
                OnClose();

            }
            else
            {
                MessageBox.Show("Comment can not be empty!");
            }
        }
        public bool PostExecute()
        {
            return this.IsValid;
        }

        private readonly string[] _validatedProperties = {  "Content" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }

    }
}
