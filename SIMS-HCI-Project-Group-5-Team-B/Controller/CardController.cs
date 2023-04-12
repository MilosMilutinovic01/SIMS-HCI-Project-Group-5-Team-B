using SIMS_HCI_Project_Group_5_Team_B.Domain.Models;
using SIMS_HCI_Project_Group_5_Team_B.DTO;
using SIMS_HCI_Project_Group_5_Team_B.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_HCI_Project_Group_5_Team_B.Controller
{
    public class CardController
    {

        public CardController()
        {
            
        }

        public List<Card> GetAll()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new Card());
            return null;
        }
    }
}
