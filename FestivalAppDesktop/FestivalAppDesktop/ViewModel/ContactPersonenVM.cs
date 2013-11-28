using FestivalAppDesktop.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
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
            VoegToeButtonContent = "Voeg toe";
            EnableDisableControls = true;
        }

        #region "Properties"
        private ObservableCollection<ContactPerson> _ContactPersonen;
        public ObservableCollection<ContactPerson> ContactPersonen
        {
            get { return _ContactPersonen; }
            set { _ContactPersonen = value; }
        }

        private ContactPerson _selectedContactPerson;

        public ContactPerson SelectedContactPerson
        {
            get { return _selectedContactPerson; }
            set 
            { 
                _selectedContactPerson = value;
                OnPropertyChanged("SelectedContactPerson");
                if(SelectedContactPerson != null)
                    Console.WriteLine(SelectedContactPerson.FirstName);
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

        private void AddNewContactPerson()
        {
            //Attraction Nieuw = new Attraction();

            ////met onderstaande lijn worden alle tekstvelden gewist
            ////deze zijn immers aan deze property gebind
            //SelectedAttraction = Nieuw;

            SelectedContactPerson = null;
            ReadOnlyProperty = false;
            VoegToeButtonContent = "Opslaan";
            EnableDisableControls = false;

            InsertDatabase();
        }

        private void InsertDatabase()
        {
            //DbParameter[] parameters = new DbParameter[9];
            //parameters[0] = SelectedContactPerson.FirstName;

            //string SQL = "INSERT INTO ContactPerson VALUES ";
        }
    }
}
