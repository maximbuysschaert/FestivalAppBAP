using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAppDesktop.Model
{
    class Ticket
    {
        private string ID;

        public string id
        {
            get { return ID; }
            set { ID = value; }
        }
        private string Ticketholder;

        public string ticketholder
        {
            get { return Ticketholder; }
            set { Ticketholder = value; }
        }
        private string TicketholderEmail;

        public string ticketholderemail
        {
            get { return TicketholderEmail; }
            set { TicketholderEmail = value; }
        }
        private TicketType TicketType;

        public TicketType tickettype
        {
            get { return TicketType; }
            set { TicketType = value; }
        }
        private int Amount;

        public int amount
        {
            get { return Amount; }
            set { Amount = value; }
        }
        
    }
}
