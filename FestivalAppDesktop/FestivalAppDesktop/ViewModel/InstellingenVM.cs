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
            VisibilityContactPersonTypeListbox = "visible";
            VisibilityContactPersonTypeTextbox = "hidden";
            ButtonContentAddContactPersonType = "Voeg toe";
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

        private ContactPersonType _selectedType;
        public ContactPersonType SelectedType
        {
            get { return _selectedType; }
            set 
            { 
                _selectedType = value;
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

        private void AddContactPersonType()
        {
            if(ButtonContentAddContactPersonType == "Voeg toe")
            {
                ButtonContentAddContactPersonType = "Opslaan";
                VisibilityContactPersonTypeListbox = "hidden";
                VisibilityContactPersonTypeTextbox = "visible";

                ContactPersonType nieuw = new ContactPersonType();
                SelectedType = nieuw;
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
    }
}
