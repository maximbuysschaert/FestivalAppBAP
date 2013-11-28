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
    class ContactPerson
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }        

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        

        private string _company;
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }


        private ContactPersonType _contactPersonType;
        public ContactPersonType ContactPersonType
        {
            get { return _contactPersonType; }
            set { _contactPersonType = value; }
        }


        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }


        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        private string _cellPhone;
        public string CellPhone
        {
            get { return _cellPhone; }
            set { _cellPhone = value; }
        }
        

        public static ObservableCollection<ContactPerson> GetContactPersons()
        {
            ObservableCollection<ContactPerson> lijst = new ObservableCollection<ContactPerson>();

            string SQL = "SELECT * FROM ContactPerson";
            Database.GetData(SQL);
            DbDataReader reader = Database.GetData(SQL);
            while (reader.Read())
            {
                ContactPerson Nieuw = new ContactPerson();
                Nieuw.ID = Int32.Parse(reader["ID"].ToString());
                Nieuw.FirstName = reader["FirstName"].ToString();
                Nieuw.LastName = reader["LastName"].ToString();
                Nieuw.Company = reader["Company"].ToString();
                //Nieuw.ContactPersonType = (ContactPersonType) reader["JobRole"];
                Nieuw.Address = reader["Address"].ToString();
                Nieuw.City = reader["City"].ToString();
                Nieuw.Email = reader["Email"].ToString();
                Nieuw.Phone = reader["Phone"].ToString();
                Nieuw.CellPhone = reader["Cellphone"].ToString();

                lijst.Add(Nieuw);
            }

            return lijst;
        }
    }
}
