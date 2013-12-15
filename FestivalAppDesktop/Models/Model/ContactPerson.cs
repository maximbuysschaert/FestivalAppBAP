using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class ContactPerson
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
    }
}
