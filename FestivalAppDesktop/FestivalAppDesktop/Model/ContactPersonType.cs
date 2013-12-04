using Oefening1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAppDesktop.Model
{
    class ContactPersonType
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

        public static ObservableCollection<ContactPersonType> GetContactPersonTypes()
        {
            ObservableCollection<ContactPersonType> lijst = new ObservableCollection<ContactPersonType>();

            string SQL = "SELECT * FROM ContactPersonType";
            Database.GetData(SQL);
            DbDataReader reader = Database.GetData(SQL);
            while (reader.Read())
            {
                ContactPersonType Nieuw = new ContactPersonType();
                Nieuw.ID = Int32.Parse(reader["ID"].ToString());
                Nieuw.name = reader["Name"].ToString();

                lijst.Add(Nieuw);
            }

            return lijst;
        }
        
    }
}
