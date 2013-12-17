using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Genre
    {
        private int ID;

        public int id
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
    }
}
