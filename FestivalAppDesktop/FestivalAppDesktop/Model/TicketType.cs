using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAppDesktop.Model
{
    class TicketType
    {
        private string ID;

        public string id
        {
            get { return ID; }
            set { ID = value; }
        }
        private string Name;

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        private double Price;

        public double price
        {
            get { return Price; }
            set { Price = value; }
        }
        private int AvailableTickets;

        public int availabletickets
        {
            get { return AvailableTickets; }
            set { AvailableTickets = value; }
        }
        
    }
}
