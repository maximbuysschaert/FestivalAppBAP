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
            //ReadOnlyProperty = true;
            //VoegToeButtonContent = "Voeg toe";
            //EnableDisableControls = true;
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

        private string _voegToeButtonContent;
        public string VoegToeButtonContent
        {
            get { return _voegToeButtonContent; }
            set 
            { 
                _voegToeButtonContent = value;
                OnPropertyChanged("VoegToeButtonContent");
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
        }

        private void InsertDatabase()
        {         
            //Console.WriteLine(SelectedContactPerson.FirstName);
            DbParameter[] parameters = new DbParameter[2];
            parameters[0] = new SqlParameter("param1", SelectedContactPerson.FirstName);
            parameters[1] = new SqlParameter("param2", SelectedContactPerson.LastName);

            string SQL = "INSERT INTO ContactPerson (Firstname) VALUES (@param2);";
            Database.ModifyData(SQL, parameters);

            ContactPersonen = ContactPerson.GetContactPersons();
        }
    }
}
