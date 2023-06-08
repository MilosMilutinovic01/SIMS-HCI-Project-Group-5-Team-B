using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Notifications;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications.Messages.Core;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ForumFormViewModel:INotifyPropertyChanged, IDataErrorInfo
    {
        private ICommentService commentService;
        private ForumService forumService;
        private int ownerGuestId;
        private LocationController locationController;
        private UserController userController;
        private OwnerGuestService ownerGuestService;
        private AccommodationService accommodationService;
        private OwnerService ownerService;
        private NotificationController notificationController;
        public string City { get; set; }
        private string state;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                Cities = locationController.GetCityByState(state);
                NotifyPropertyChanged(nameof(Cities));
                NotifyPropertyChanged(nameof(State));
                NotifyPropertyChanged(nameof(City));
            }

        }
        public List<string> States { get; set; }
        public List<string> Cities { get; set; }

        public string Content { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public ForumFormViewModel(int ownerGuestId) 
        { 
            this.ownerGuestId = ownerGuestId;
            commentService = ServiceInjector.CreateInstance<ICommentService>();
            forumService = new ForumService();
            locationController = new LocationController();
            ownerGuestService = new OwnerGuestService();
            userController = new UserController();
            ownerService = new OwnerService();
            accommodationService = new AccommodationService(locationController,ownerService);
            notificationController = new NotificationController();
            States = locationController.GetStates();

            //commands
            CloseCommand = new RelayCommand(OnClose);
            CreateCommand = new RelayCommand(OnCreate);
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
                if (columnName == "State")
                {
                    if (string.IsNullOrEmpty(State))
                    {
                        return "You must select state";
                    }

                }


                if (columnName == "City")
                {
                    if ( string.IsNullOrEmpty(City))
                    {
                        return "You must select city";
                    }
                    else
                    {
                        return null;
                    }


                }

                if(columnName == "Content")
                {
                    if(string.IsNullOrEmpty(Content))
                    {
                        return "You must post the first comment!";
                    }
                }
                
                return null;


            }
        }
        private readonly string[] _validatedProperties = { "City", "State", "Content" };
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

        public int FindLocationId()
        {
            if (string.IsNullOrEmpty(this.State) || string.IsNullOrEmpty(this.City))
            {
                return -1;
            }

            string State = this.State;
            string City = this.City;

            int locationId = -2;
            foreach (Location location in locationController.GetAll())
            {
                if (location.State == State && location.City == City)
                {
                    locationId = location.Id;
                    break;
                }
            }

            return locationId;
        }

        public void OnClose()
        {
           App.Current.Windows.OfType<ForumForm>().FirstOrDefault().Close();
        }

        public void OnCreate()
        {
            if(this.IsValid)
            {
                if(forumService.Exists(FindLocationId()))
                {
                    MessageBox.Show("Forum for that location already exists!");
                }
                else
                {
                    Forum forum = new Forum(FindLocationId(), ownerGuestId,false);
                    forumService.Save(forum);
                    OwnerGuest ownerGuest = ownerGuestService.GetById(ownerGuestId);
                    User user = userController.GetByUsername(ownerGuest.Username);
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

                    foreach (Notification notification in CreateOwnerNotification())
                    {
                        notificationController.Send(notification);
                    }

                    MessageBox.Show("New forum created!");
                    OnClose();
                }
            }
            else
            {
                MessageBox.Show("Some fields are not filled");
            }
            
        }

        
        private List<Notification> CreateOwnerNotification()
        { 
            List<Notification> notifications= new List<Notification>(); 
            foreach(Owner owner in ownerService.GetAll())
            {
                Location location = locationController.getById(FindLocationId());
                if (accommodationService.DoesOwnerHaveAccommodationOnLocation(owner, location))
                {
                    Notification notification = new Notification();
                    notification.IsRead = false;
                    notification.Message = GetNotificationMessage();
                    notification.ReceiverId = userController.GetByUsername(owner.Username).Id;
                    notifications.Add(notification);
                }
            }
            return notifications;
        }

        private string GetNotificationMessage()
        {
            StringBuilder sb = new StringBuilder($"New forum is opened on location :{City},{State} by {ownerGuestService.GetById(ownerGuestId).Name} {ownerGuestService.GetById(ownerGuestId).Surname} ");
            return sb.ToString();
        }


    }
}
