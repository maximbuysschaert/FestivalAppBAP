using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAppDesktop.Model
{
    class Band
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

        private string Picture;

        public string picture
        {
            get { return Picture; }
            set { Picture = value; }
        }

        private string Description;

        public string description
        {
            get { return Description; }
            set { Description = value; }
        }

        private string Twitter;

        public string twitter
        {
            get { return Twitter; }
            set { Twitter = value; }
        }

        private string Facebook;

        public string facebook
        {
            get { return Facebook; }
            set { Facebook = value; }
        }
        private ObservableCollection<Genre> Genres;

        public ObservableCollection<Genre> genres
        {
            get { return Genres; }
            set { Genres = value; }
        }       
    }
}
