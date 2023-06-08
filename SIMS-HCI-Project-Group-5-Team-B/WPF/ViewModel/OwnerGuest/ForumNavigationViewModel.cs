using SIMS_HCI_Project_Group_5_Team_B.Application.Injector;
using SIMS_HCI_Project_Group_5_Team_B.Application.UseCases;
using SIMS_HCI_Project_Group_5_Team_B.Controller;
using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.Domain.ServiceInterfaces;
using SIMS_HCI_Project_Group_5_Team_B.Utilities;
using SIMS_HCI_Project_Group_5_Team_B.WPF.View.OwnerGuest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications.Messages.Core;

namespace SIMS_HCI_Project_Group_5_Team_B.WPF.ViewModel
{
    public class ForumNavigationViewModel:INotifyPropertyChanged, IDataErrorInfo
    {
        private ForumService forumService;
        private int ownerGuestId;
        private LocationController locationController;

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

        public RelayCommand NewForumCommand { get; set; }
        public RelayCommand GoCommand { get; set; }
        
        public ForumNavigationViewModel(int ownerGuestId)
        {
            forumService = new ForumService();
            this.ownerGuestId = ownerGuestId;
            locationController = new LocationController();
            States = locationController.GetStates();

            NewForumCommand = new RelayCommand(OnNewForum);
            GoCommand = new RelayCommand(OnGo);

        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void OnNewForum()
        {
            ForumForm form = new ForumForm(ownerGuestId);
            form.Show();
        }

        public void OnGo()
        {
            if(this.IsValid)
            {
                Forum forum = forumService.GetByLocation(FindLocationId());
                if(forum != null)
                {
                    ForumsWIndow window = new ForumsWIndow(forum, ownerGuestId);
                    window.Show();
                }
                else
                {
                    MessageBox.Show("Forum on that location does not exist.");
                }
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
                    if (string.IsNullOrEmpty(City))
                    {
                        return "You must select city";
                    }
                    else
                    {
                        return null;
                    }


                }

                

                return null;


            }
        }
        private readonly string[] _validatedProperties = { "City", "State" };
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
    }
}
