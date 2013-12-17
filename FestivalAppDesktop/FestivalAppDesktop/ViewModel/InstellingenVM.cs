using Models.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace FestivalAppDesktop.ViewModel
{
    class InstellingenVM : ObservableObject, IPage
    {
        public InstellingenVM()
        {
            ContactPersonTypes = DALContactPersonTypes.GetContactPersonTypes();
            ContactPersons = DALContactPerson.GetContactPersons();

            VisibilityContactPersonTypeListbox = "visible";
            VisibilityContactPersonTypeTextbox = "hidden";

            ButtonContentAddContactPersonType = "Voeg toe";
            ButtonContentEditContactPersonType = "Wijzig";

            EnableDisableContactPersonTypeDeleteButton = false;
            EnableDisableContactPersonTypeEditButton = false;
            EnableDisableContactPersonTypeAddButton = true;
        }

        private ObservableCollection<ContactPerson> ContactPersons = new ObservableCollection<ContactPerson>();

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

        private ContactPersonType _selectedType;
        public ContactPersonType SelectedType
        {
            get { return _selectedType; }
            set 
            { 
                _selectedType = value;
                if(SelectedType != null)
                {
                    ChangeContactPersonTypeDeleteButton();
                    EnableDisableContactPersonTypeEditButton = true;
                    Console.WriteLine("test");
                }               
                OnPropertyChanged("SelectedType");
            }
        }

        private string _visibilityContactPersonTypeTextbox;
        public string VisibilityContactPersonTypeTextbox
        {
            get { return _visibilityContactPersonTypeTextbox; }
            set 
            { 
                _visibilityContactPersonTypeTextbox = value;
                OnPropertyChanged("VisibilityContactPersonTypeTextbox");
            }
        }

        private string _visibilityContactPersonTypeListbox;
        public string VisibilityContactPersonTypeListbox
        {
            get { return _visibilityContactPersonTypeListbox; }
            set 
            { 
                _visibilityContactPersonTypeListbox = value;
                OnPropertyChanged("VisibilityContactPersonTypeListbox");
            }
        }

        private Boolean _enableDisableContactPersonTypeDeleteButton;
        public Boolean EnableDisableContactPersonTypeDeleteButton
        {
            get { return _enableDisableContactPersonTypeDeleteButton; }
            set 
            { 
                _enableDisableContactPersonTypeDeleteButton = value;
                OnPropertyChanged("EnableDisableContactPersonTypeDeleteButton");
            }
        }

        private Boolean _enablaDisableContactPersonTypeAddButton;
        public Boolean EnableDisableContactPersonTypeAddButton
        {
            get { return _enablaDisableContactPersonTypeAddButton; }
            set 
            { 
                _enablaDisableContactPersonTypeAddButton = value;
                OnPropertyChanged("EnableDisableContactPersonTypeAddButton");
            }
        }

        private Boolean _enableDisableContactPersonTypeEditButton;
        public Boolean EnableDisableContactPersonTypeEditButton
        {
            get { return _enableDisableContactPersonTypeEditButton; }
            set 
            { 
                _enableDisableContactPersonTypeEditButton = value;
                OnPropertyChanged("EnableDisableContactPersonTypeEditButton");
            }
        }
        

        private string _buttonContentAddContactPersonType;
        public string ButtonContentAddContactPersonType
        {
            get { return _buttonContentAddContactPersonType; }
            set 
            { 
                _buttonContentAddContactPersonType = value;
                OnPropertyChanged("ButtonContentAddContactPersonType");
            }
        }

        private string _buttonContentEditContactPersonType;
        public string ButtonContentEditContactPersonType
        {
            get { return _buttonContentEditContactPersonType; }
            set
            {
                _buttonContentEditContactPersonType = value;
                OnPropertyChanged("ButtonContentEditContactPersonType");
            }
        }

        private string _contentTextboxContactPersonType;
        public string ContentTextboxContactPersonType
        {
            get { return _contentTextboxContactPersonType; }
            set 
            { 
                _contentTextboxContactPersonType = value;
                OnPropertyChanged("ContentTextboxContactPersonType");
            }
        }     
        
        public string Name
        {
            get { return "Instellingen"; }
        }

        public ICommand AddContactPersonTypeCommand
        {
            get
            {
                return new RelayCommand(AddContactPersonType);
            }
        }

        public ICommand EditContactPersonTypeCommand
        {
            get
            {
                return new RelayCommand(EditContactPersonType);
            }
        }

        public ICommand DeleteContactPersonTypeCommand
        {
            get
            {
                return new RelayCommand(DeleteContactPersonType);
            }
        }

        private void AddContactPersonType()
        {
            if(ButtonContentAddContactPersonType == "Voeg toe")
            {
                ContactPersonType nieuw = new ContactPersonType();
                SelectedType = nieuw;

                ButtonContentAddContactPersonType = "Opslaan";
                VisibilityContactPersonTypeListbox = "hidden";
                VisibilityContactPersonTypeTextbox = "visible";
                EnableDisableContactPersonTypeDeleteButton = false;
                EnableDisableContactPersonTypeEditButton = false;
            }
            else
            {
                DALContactPersonTypes.InsertContactPersonType(SelectedType);
                ContactPersonTypes = DALContactPersonTypes.GetContactPersonTypes();

                ButtonContentAddContactPersonType = "Voeg toe";
                VisibilityContactPersonTypeListbox = "visible";
                VisibilityContactPersonTypeTextbox = "hidden";
            }
        }

        private void EditContactPersonType()
        {
            if(ButtonContentEditContactPersonType == "Wijzig")
            {
                ButtonContentEditContactPersonType = "Opslaan";
                VisibilityContactPersonTypeListbox = "hidden";
                VisibilityContactPersonTypeTextbox = "visible";

                EnableDisableContactPersonTypeAddButton = false;
                EnableDisableContactPersonTypeDeleteButton = false;
            }
            else
            {
                DALContactPersonTypes.UpdataContactPerson(SelectedType);
                ContactPersonTypes = DALContactPersonTypes.GetContactPersonTypes();
                ContactPersons = DALContactPerson.GetContactPersons();

                ButtonContentEditContactPersonType = "Wijzig";
                VisibilityContactPersonTypeListbox = "visible";
                VisibilityContactPersonTypeTextbox = "hidden";

                EnableDisableContactPersonTypeAddButton = true;
                EnableDisableContactPersonTypeEditButton = false;
            }
        }

        private void DeleteContactPersonType()
        {
            DALContactPersonTypes.DeleteContactPerson(SelectedType);
            ContactPersonTypes = DALContactPersonTypes.GetContactPersonTypes();

            EnableDisableContactPersonTypeDeleteButton = false;
            EnableDisableContactPersonTypeEditButton = false;
        }

        private void ChangeContactPersonTypeDeleteButton()
        {
            foreach(ContactPerson person in ContactPersons)
            {
                if(person.ContactPersonType.id.Equals(SelectedType.id))
                {
                    EnableDisableContactPersonTypeDeleteButton = false;
                    return;
                }
                else
                {
                    EnableDisableContactPersonTypeDeleteButton = true;
                    Console.WriteLine("Geen contactpersonen gevonden");
                }
            }
        }
    }
}
