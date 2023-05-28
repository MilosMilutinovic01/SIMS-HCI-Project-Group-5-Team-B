using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ForumCommentsViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public Forum SelectedForum { get; set; }
        public ObservableCollection<Comment> Comments { get; set; }
        private ForumService forumService;
        private ICommentService commentService;
        private UserController userController;
        private OwnerGuestService ownerGuestService;
        private AccommodationService accommodationService;
        public RelayCommand CloseCommand {get; }
        public RelayCommand AddCommentCommand { get; }
        public Comment NewComment { get; set; }
        public Owner Owner { get; set; }
        public string Content { get; set; }
        public bool ForumOpened { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool CanAddComment { get; set; }
        public ForumCommentsViewModel(Forum SelectedForum, ForumService forumService,Owner owner,AccommodationService accommodationService)
        {
            this.SelectedForum = SelectedForum;
            this.Owner = owner;
            userController = new UserController();
            ownerGuestService = new OwnerGuestService();
            commentService = ServiceInjector.CreateInstance<ICommentService>();
            this.forumService = forumService;
            this.accommodationService = accommodationService;
            Comments = new ObservableCollection<Comment>(forumService.GetForumsComments(SelectedForum.Id));
            if(SelectedForum.ForumStatus == FORUMSTATUS.Closed)
            {
                ForumOpened = false;
            }
            else
            {
                ForumOpened = true;
            }

            CanAddComment = (accommodationService.DoesOwnerHaveAccommodationOnLocation(owner, SelectedForum.Location) && ForumOpened);
            WasNotOnLocationUpdate(Comments);
            CloseCommand = new RelayCommand(CloseExecute, CloseCanExecute);
            AddCommentCommand = new RelayCommand(AddCommentExecute, AddCommentCanExecute);
        }

        public void WasNotOnLocationUpdate(ObservableCollection<Comment> Comments)
        {
            foreach(Comment comment in Comments)
            {
                if (ownerGuestService.GetByUsername(comment.User.Username) != null)
                {
                    if(forumService.WasGuestOnLocation(ownerGuestService.GetByUsername(comment.User.Username), SelectedForum.Location))
                    {
                        comment.WasNotOnLocation = false;
                    }
                    else
                    {
                        comment.WasNotOnLocation = true;
                    }
                    commentService.Update(comment);
                }
            }
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string Error => null;

        public string this[string columnName]
        {
            get
            {

                if (columnName == "Content")
                {
                    if(string.IsNullOrEmpty(Content) && CanAddComment == false && SelectedForum.ForumStatus == FORUMSTATUS.Active)
                    {
                        if (Properties.Settings.Default.currentLanguage == "en-US")
                        {
                            return "You can't comment on this forum because you dont have accommodation on location!";
                        }
                        else
                        {
                            return "Ne mozete ostavljati komentare na forumu jer nemate smestaj na lokaciji";
                        }
                    }
                    else if(string.IsNullOrEmpty(Content) && CanAddComment == false && SelectedForum.ForumStatus == FORUMSTATUS.Closed){

                        if (Properties.Settings.Default.currentLanguage == "en-US")
                        {
                            return "You can't comment on this forum because it is closed!";
                        }
                        else
                        {
                            return "Ne mozete ostavljati komentare na forumu jer je forum ugasen";
                        }
                    }
                    else if (string.IsNullOrEmpty(Content))
                    {
                       if(Properties.Settings.Default.currentLanguage == "en-US")
                       {
                            return "Comment is not written yet!";
                       }
                       else
                       {
                            return "Komentar jos nije napisan";
                       }
                    }
                    
                }

                return null;
            }
        }


        private readonly string[] _validatedProperties = { "Content" };
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

        public bool CloseCanExecute()
        {
            return true;
        }

        public bool AddCommentCanExecute()
        {
            return true;
        }

        public void CloseExecute()
        {
            App.Current.Windows[4].Close();
        }


        public void AddCommentExecute()
        {
            if (this.IsValid)
            {
                User user = userController.GetByUsername(Owner.Username);
                Comment comment = new Comment(user.Id, SelectedForum.Id, Content);
                comment.IsFromOwnerWithAccommodationOnLocation = true;
                comment.WasNotOnLocation = false; //jer je vlasnik uvek bio na lokaciji de ima smestaj
                commentService.Save(comment);
                Comments.Add(comment);
                SelectedForum.IsVeryUseful = forumService.IsForumVeryUseful(SelectedForum);
                forumService.Update(SelectedForum);

            }
            else
            {
                if (Properties.Settings.Default.currentLanguage == "en-US")
                {
                    MessageBox.Show("Comment field is not filled!");
                }
                else
                {
                    MessageBox.Show("Polje za komentar nije popunjeno!");
                }
            }
        }

    }
}
