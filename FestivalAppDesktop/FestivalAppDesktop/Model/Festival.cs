using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAppDesktop.Model
{
    class Festival
    {
        private DateTime StartDate;

        public DateTime startdate
        {
            get { return StartDate; }
            set { StartDate = value; }
        }

        private DateTime EndDate;

        public DateTime enddate
        {
            get { return EndDate; }
            set { EndDate = value; }
        }
    }
}
