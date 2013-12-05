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
            _contactPersonTypes = ContactPersonType.GetContactPersonTypes();
            _actualContactPersonTypes = GetActualContactPersonTypes();
            ReadOnlyProperty = true;
            EnableDisableControlsAdd = true;
            EnableDisableControlsEdit = false;
            EnableDisableControlsDelete = false;
            EnableDisableListView = true;
            EnableDisableSaveCancel = false;
            VisibilityCombobox = "hidden";
            VisibilityTextbox = "visible";
        }

        private int Teller = 0;

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

        private ObservableCollection<ContactPersonType> _contactPersonTypes;
        public ObservableCollection<ContactPersonType> ContactPersonTypes
        {
            get { return _contactPersonTypes; }
            set 
            { 
                _contactPersonTypes = value; 
                OnPropertyChanged("ContactPersonTypes");
            }
        }
        

        private ContactPerson _selectedContactPerson;
        public ContactPerson SelectedContactPerson
        {
            get { return _selectedContactPerson; }
            set 
            { 
                _selectedContactPerson = value;
                if(SelectedContactPerson != null)
                {
                    EnableDisableControlsEdit = true;
                    EnableDisableControlsDelete = true;
                }

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

        private Boolean _enableDisableControlsAdd;
        public Boolean EnableDisableControlsAdd
        {
            get { return _enableDisableControlsAdd; }
            set 
            { 
                _enableDisableControlsAdd = value;
                OnPropertyChanged("EnableDisableControlsAdd");
            }
        }

        private Boolean _enableDisableControlsDelete;
        public Boolean EnableDisableControlsDelete
        {
            get { return _enableDisableControlsDelete; }
            set
            {
                _enableDisableControlsDelete = value;
                OnPropertyChanged("EnableDisableControlsDelete");
            }
        }

        private Boolean _enableDisableControlsEdit;
        public Boolean EnableDisableControlsEdit
        {
            get { return _enableDisableControlsEdit; }
            set
            {
                _enableDisableControlsEdit = value;
                OnPropertyChanged("EnableDisableControlsEdit");
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

        private string _visibilityTextbox;
        public string VisibilityTextbox
        {
            get { return _visibilityTextbox; }
            set 
            { 
                _visibilityTextbox = value;
                OnPropertyChanged("VisibilityTextbox");
            }
        }

        private string _visibilityCombobox;
        public string VisibilityCombobox
        {
            get { return _visibilityCombobox; }
            set 
            { 
                _visibilityCombobox = value;
                OnPropertyChanged("VisibilityCombobox");
            }
        }

        private ObservableCollection<ContactPersonType> _actualContactPersonTypes;
        public ObservableCollection<ContactPersonType> ActualContactPersonTypes
        {
            get { return _actualContactPersonTypes; }
            set 
            { 
                _actualContactPersonTypes = value;
                OnPropertyChanged("ActualContactPersonTypes");
            }
        }

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
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

        public ICommand CancelNewContactPersonCommand
        {
            get
            {
                return new RelayCommand(CancelContact);
            }
        }

        public ICommand EditNewContactPersonCommand
        {
            get
            {
                return new RelayCommand(EditContact);
            }
        }

        public ICommand DeleteContactPersonCommand
        {
            get
            {
                return new RelayCommand(DeleteContact);
            }
        }

        private void AddNewContactPerson()
        {
            Teller = 1;

            ContactPerson nieuw = new ContactPerson();
            SelectedContactPerson = nieuw;

            ReadOnlyProperty = false;
            EnableDisableControlsAdd = false;
            EnableDisableControlsEdit = false;
            EnableDisableControlsDelete = false;
            EnableDisableListView = false;
            EnableDisableSaveCancel = true;
            VisibilityTextbox = "hidden";
            VisibilityCombobox = "visible";
        }

        private void InsertDatabase()
        {
            if (Teller == 1)
            {
                //Console.WriteLine(SelectedContactPerson.FirstName);
                DbParameter[] parameters = new DbParameter[9];
                parameters[0] = new SqlParameter("param1", SelectedContactPerson.FirstName);
                parameters[1] = new SqlParameter("param2", SelectedContactPerson.LastName);
                parameters[2] = new SqlParameter("param3", SelectedContactPerson.Company);
                parameters[3] = new SqlParameter("param4", SelectedContactPerson.ContactPersonType.id);
                parameters[4] = new SqlParameter("param5", SelectedContactPerson.Address);
                parameters[5] = new SqlParameter("param6", SelectedContactPerson.City);
                parameters[6] = new SqlParameter("param7", SelectedContactPerson.Email);
                parameters[7] = new SqlParameter("param8", SelectedContactPerson.Phone);
                parameters[8] = new SqlParameter("param9", SelectedContactPerson.CellPhone);

                string SQL = "INSERT INTO ContactPerson (FirstName, LastName, Company, JobRole, Address, City, Email, Phone, Cellphone) VALUES (@param1, @param2, @param3, @param4, @param5, @param6, @param7, @param8, @param9);";
                Database.ModifyData(SQL, parameters);

                ContactPersonen = ContactPerson.GetContactPersons();

                EnableDisableControl();

                ContactPerson nieuw = new ContactPerson();
                SelectedContactPerson = nieuw;
            }
            else if(Teller == 2)
            {
                DbParameter[] parameters = new DbParameter[10];
                parameters[0] = new SqlParameter("param1", SelectedContactPerson.FirstName);
                parameters[1] = new SqlParameter("param2", SelectedContactPerson.LastName);
                parameters[2] = new SqlParameter("param3", SelectedContactPerson.Company);
                parameters[3] = new SqlParameter("param4", SelectedContactPerson.ContactPersonType.id);
                parameters[4] = new SqlParameter("param5", SelectedContactPerson.Address);
                parameters[5] = new SqlParameter("param6", SelectedContactPerson.City);
                parameters[6] = new SqlParameter("param7", SelectedContactPerson.Email);
                parameters[7] = new SqlParameter("param8", SelectedContactPerson.Phone);
                parameters[8] = new SqlParameter("param9", SelectedContactPerson.CellPhone);
                parameters[9] = new SqlParameter("param10", SelectedContactPerson.ID);

                string SQL = "UPDATE ContactPerson SET FirstName=@param1, LastName=@param2, Company=@param3, JobRole=@param4, Address=@param5, City=@param6, Email=@param7, Phone=@param8, Cellphone=@param9  WHERE ID=@param10;";
                Database.ModifyData(SQL, parameters);

                ContactPersonen = ContactPerson.GetContactPersons();

                EnableDisableControl();
            }
        }

        private void CancelContact()
        {
            ContactPerson nieuw = new ContactPerson();
            SelectedContactPerson = nieuw;

            ContactPersonen = ContactPerson.GetContactPersons();

            EnableDisableControl();
        }

        private void EnableDisableControl()
        {
            ReadOnlyProperty = true;
            EnableDisableSaveCancel = false;
            EnableDisableListView = true;
            EnableDisableControlsAdd = true;
            VisibilityCombobox = "hidden";
            VisibilityTextbox = "visible";
            EnableDisableControlsEdit = false;
            EnableDisableControlsDelete = false;
        }

        private void EditContact()
        {
            Teller = 2;

            ReadOnlyProperty = false;
            EnableDisableControlsAdd = false;
            EnableDisableControlsEdit = false;
            EnableDisableControlsDelete = false;
            EnableDisableListView = false;
            EnableDisableSaveCancel = true;
            VisibilityTextbox = "hidden";
            VisibilityCombobox = "visible";
        }

        private void DeleteContact()
        {
            DbParameter[] parameters = new DbParameter[1];
            parameters[0] = new SqlParameter("param1", SelectedContactPerson.ID);

            string SQL = "DELETE FROM ContactPerson WHERE ID = @param1;";
            Database.ModifyData(SQL, parameters);

            ContactPersonen = ContactPerson.GetContactPersons();
        }
        
        private ObservableCollection<ContactPersonType> GetActualContactPersonTypes()
        {
            ObservableCollection<ContactPersonType> types = new ObservableCollection<ContactPersonType>();
            List<int> iTypes = new List<int>();

            foreach(ContactPerson person in ContactPersonen)
            {
                if(!iTypes.Contains(person.ContactPersonType.id))
                {
                    iTypes.Add(person.ContactPersonType.id);
                }
            }

            foreach(int i in iTypes)
            {
                foreach(ContactPersonType type in ContactPersonTypes)
                {
                    if(type.id.Equals(i))
                    {
                        types.Add(type);
                    }
                }
            }

            //foreach(ContactPerson person in ContactPersonen)
            //{
            //    if(!types.Contains(person.ContactPersonType))
            //    {
            //        types.Add(person.ContactPersonType);
            //    }
            //}

            return types;
        }
    }
}
