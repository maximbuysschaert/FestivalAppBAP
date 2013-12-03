using FestivalAppDesktop.Model;
using GalaSoft.MvvmLight.Command;
using Oefening1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FestivalAppDesktop.ViewModel
{
    class ContactPersonenVM : ObservableObject, IPage
    {
        public ContactPersonenVM()
        {
            _ContactPersonen = ContactPerson.GetContactPersons();
            ReadOnlyProperty = true;
            EnableDisableControls = true;
            EnableDisableListView = true;
            EnableDisableSaveCancel = false;
        }

        #region "Properties"
        private ObservableCollection<ContactPerson> _ContactPersonen;
        public ObservableCollection<ContactPerson> ContactPersonen
        {
            get { return _ContactPersonen; }
            set 
            { 
                _ContactPersonen = value;
                OnPropertyChanged("ContactPersonen");
            }
        }

        private ContactPerson _selectedContactPerson;
        public ContactPerson SelectedContactPerson
        {
            get { return _selectedContactPerson; }
            set 
            { 
                _selectedContactPerson = value;
                OnPropertyChanged("SelectedContactPerson");
            }
        }

        private Boolean _readOnlyProperty;
        public Boolean ReadOnlyProperty
        {
            get { return _readOnlyProperty; }
            set 
            { 
                _readOnlyProperty = value;
                OnPropertyChanged("ReadOnlyProperty");
            }
        }

        private Boolean _enableDisableControls;
        public Boolean EnableDisableControls
        {
            get { return _enableDisableControls; }
            set 
            { 
                _enableDisableControls = value;
                OnPropertyChanged("EnableDisableControls");
            }
        }

        private Boolean _enableDisableListView;
        public Boolean EnableDisableListView
        {
            get { return _enableDisableListView; }
            set 
            { 
                _enableDisableListView = value;
                OnPropertyChanged("EnableDisableListView");
            }
        }

        private Boolean _enableDisableSaveCancel;
        public Boolean EnableDisableSaveCancel
        {
            get { return _enableDisableSaveCancel; }
            set 
            { 
                _enableDisableSaveCancel = value;
                OnPropertyChanged("EnableDisableSaveCancel");
            }
        }
        
        
        #endregion

        public string Name
        {
            get { return "ContactPersonen"; } //unieke naam!
        }

        public ICommand AddNewContactPersonCommand
        {
            get
            {
                return new RelayCommand(AddNewContactPerson);
            }
        }

        public ICommand SaveNewContactPersonCommand
        {
            get
            {
                return new RelayCommand(InsertDatabase);
            }
        }
        private void AddNewContactPerson()
        {
            ContactPerson nieuw = new ContactPerson();
            SelectedContactPerson = nieuw;

            ReadOnlyProperty = false;
            EnableDisableControls = false;
            EnableDisableListView = false;
            EnableDisableSaveCancel = true;
        }

        private void InsertDatabase()
        {         
            //Console.WriteLine(SelectedContactPerson.FirstName);
            DbParameter[] parameters = new DbParameter[8];
            parameters[0] = new SqlParameter("param1", SelectedContactPerson.FirstName);
            parameters[1] = new SqlParameter("param2", SelectedContactPerson.LastName);
            parameters[2] = new SqlParameter("param3", SelectedContactPerson.Company);
            parameters[3] = new SqlParameter("param4", SelectedContactPerson.Address);
            parameters[4] = new SqlParameter("param5", SelectedContactPerson.City);
            parameters[5] = new SqlParameter("param6", SelectedContactPerson.Email);
            parameters[6] = new SqlParameter("param7", SelectedContactPerson.Phone);
            parameters[7] = new SqlParameter("param8", SelectedContactPerson.CellPhone);

            string SQL = "INSERT INTO ContactPerson (FirstName, LastName, Company, Address, City, Email, Phone, Cellphone) VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8);";
            Database.ModifyData(SQL, parameters);

            ContactPersonen = ContactPerson.GetContactPersons();
            EnableDisableSaveCancel = false;
            EnableDisableListView = true;
            EnableDisableControls = true;

            ContactPerson nieuw = new ContactPerson();
            SelectedContactPerson = nieuw;
        }
    }
}
