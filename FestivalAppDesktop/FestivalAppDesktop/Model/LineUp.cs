using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAppDesktop.Model
{
    class LineUp
    {
        private string ID;

        public string id
        {
            get { return ID; }
            set { ID = value; }
        }
        private DateTime Date;

        public DateTime date
        {
            get { return Date; }
            set { Date = value; }
        }
        private string From;

        public string from
        {
            get { return From; }
            set { From = value; }
        }
        private string Until;

        public string until
        {
            get { return Until; }
            set { Until = value; }
        }
        private Stage Stage;

        public Stage stage
        {
            get { return Stage; }
            set { Stage = value; }
        }
        private Band Band;

        public Band band
        {
            get { return Band; }
            set { Band = value; }
        }       
    }
}
