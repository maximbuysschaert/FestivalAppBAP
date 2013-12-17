using Models.Model;
using DAL;
using GalaSoft.MvvmLight.Command;
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
            _ContactPersonen = DALContactPerson.GetContactPersons();
            _contactPersonTypes = DALContactPersonTypes.GetContactPersonTypes();
            _actualContactPersonTypes = GetActualContactPersonTypes();
            GlobalContactPersonen = DALContactPerson.GetContactPersons();
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
        private ObservableCollection<ContactPerson> GlobalContactPersonen = new ObservableCollection<ContactPerson>();
        private ContactPerson CacheContactPerson = new ContactPerson();

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

        private ContactPersonType _selectedContactPersonType;

        public ContactPersonType SelectedContactPersonType
        {
            get { return _selectedContactPersonType; }
            set 
            { 
                _selectedContactPersonType = value;
                ShowSelectedContactType();
                OnPropertyChanged("SelectedContactPersonType");
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

        public ICommand SearchCommand
        {
            get { return new RelayCommand(UpdateObservableCollections); }
        }

        private void AddNewContactPerson()
        {
            Teller = 1;

            CacheContactPerson = SelectedContactPerson;

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
                CacheContactPerson = SelectedContactPerson;

                DALContactPerson.InsertContactPerson(SelectedContactPerson);

                ContactPersonen = DALContactPerson.GetContactPersons();
                GlobalContactPersonen = DALContactPerson.GetContactPersons();
                    
                //ContactPersonType nieuwType = new ContactPersonType();
                //SelectedContactPersonType = nieuwType; 

                //ContactPerson nieuw = new ContactPerson();
                //SelectedContactPerson = nieuw;

                int aantal = ContactPersonen.Count();
                SelectedContactPerson = ContactPersonen[aantal - 1];

                EnableDisableControl();
            }
            else if(Teller == 2)
            {
                CacheContactPerson = SelectedContactPerson;

                ContactPerson SelectedPerson = SelectedContactPerson;
                //Database.ModifyData(SQL, new SqlParameter[]{
                //new SqlParameter("param1", SelectedContactPerson.FirstName),
                //new SqlParameter("param1", SelectedContactPerson.FirstName),
                //new SqlParameter("param1", SelectedContactPerson.FirstName);
                //});

                DALContactPerson.UpdataContactPerson(SelectedPerson);

                ContactPersonen = DALContactPerson.GetContactPersons();
                GlobalContactPersonen = DALContactPerson.GetContactPersons();

                int i = 0;
                foreach(ContactPerson person in ContactPersonen)
                {                    
                    if(person.ID == SelectedPerson.ID)
                    {
                        SelectedContactPerson = ContactPersonen[i];
                    }
                    else
                    {
                        i++;
                    }
                }

                EnableDisableControl();
            }
        }

        private void CancelContact()
        {
            //ContactPerson nieuw = new ContactPerson();
            //SelectedContactPerson = nieuw;

            ContactPersonen = DALContactPerson.GetContactPersons();
            GlobalContactPersonen = DALContactPerson.GetContactPersons();
           
            SelectedContactPerson = CacheContactPerson;
            

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
            EnableDisableControlsEdit = true;
            EnableDisableControlsDelete = true;
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
            DALContactPerson.DeleteContactPerson(SelectedContactPerson);

            ContactPersonen = DALContactPerson.GetContactPersons();
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
                foreach(ContactPersonType type in ContactPersonTypes )
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

        private void ShowSelectedContactType()
        {
            ObservableCollection<ContactPerson> persons = new ObservableCollection<ContactPerson>();

            foreach(ContactPerson person in GlobalContactPersonen)
            {
                if(person.ContactPersonType.name.Equals(SelectedContactPersonType.name))
                {
                    persons.Add(person);
                }
            }

            ContactPersonen = persons;
        }

        private void UpdateObservableCollections()
        {
            ContactPersonen = DALContactPerson.GetContactPersons();
            ContactPersonTypes = DALContactPersonTypes.GetContactPersonTypes();
        }
    }
}
