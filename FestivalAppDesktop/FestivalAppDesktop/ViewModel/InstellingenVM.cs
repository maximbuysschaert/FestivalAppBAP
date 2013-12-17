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
            #region "ContactPersonType"
            ContactPersonTypes = DALContactPersonTypes.GetContactPersonTypes();
            ContactPersons = DALContactPerson.GetContactPersons();

            VisibilityContactPersonTypeListbox = "visible";
            VisibilityContactPersonTypeTextbox = "hidden";

            ButtonContentAddContactPersonType = "Voeg toe";
            ButtonContentEditContactPersonType = "Wijzig";

            EnableDisableContactPersonTypeDeleteButton = false;
            EnableDisableContactPersonTypeEditButton = false;
            EnableDisableContactPersonTypeAddButton = true;
            #endregion
            #region "Genres"
            Genres = DALGenres.GetGenres();
            //nog te implementeren!
            //ContactPersons = DALContactPerson.GetContactPersons();

            VisibilityGenreListbox = "visible";
            VisibilityGenreTextbox = "hidden";

            ButtonContentAddGenre = "Voeg toe";
            ButtonContentEditGenre = "Wijzig";

            EnableDisableGenreDeleteButton = false;
            EnableDisableGenreEditButton = false;
            EnableDisableGenreAddButton = true;
            #endregion
            #region "Stages"
            Stages = DALStages.GetStages();
            //nog te implementeren!
            //ContactPersons = DALContactPerson.GetContactPersons();

            VisibilityStageListbox = "visible";
            VisibilityStageTextbox = "hidden";

            ButtonContentAddStage = "Voeg toe";
            ButtonContentEditStage = "Wijzig";

            EnableDisableStageDeleteButton = false;
            EnableDisableStageEditButton = false;
            EnableDisableStageAddButton = true;
            #endregion
        }

        #region "ContactPersonTypes"
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

        #endregion
        #region "Genres"
        private ObservableCollection<ContactPerson> Bands = new ObservableCollection<ContactPerson>();
        private ObservableCollection<Genre> _genres;
        public ObservableCollection<Genre> Genres
        {
            get { return _genres; }
            set 
            { 
                _genres = value;
                OnPropertyChanged("Genres");
            }
        }

        private Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get { return _selectedGenre; }
            set
            {
                _selectedGenre = value;
                if (SelectedGenre != null)
                {
                    //nog te implementeren! 
                    //ChangeGenreDeleteButton();
                    EnableDisableGenreEditButton = true;
                    Console.WriteLine("test");
                }
                OnPropertyChanged("SelectedGenre");
            }
        }

        private string _visibilityGenreTextbox;
        public string VisibilityGenreTextbox
        {
            get { return _visibilityGenreTextbox; }
            set
            {
                _visibilityGenreTextbox = value;
                OnPropertyChanged("VisibilityGenreTextbox");
            }
        }

        private string _visibilityGenreListbox;
        public string VisibilityGenreListbox
        {
            get { return _visibilityGenreListbox; }
            set
            {
                _visibilityGenreListbox = value;
                OnPropertyChanged("VisibilityGenreListbox");
            }
        }

        private Boolean _enableDisableGenreDeleteButton;
        public Boolean EnableDisableGenreDeleteButton
        {
            get { return _enableDisableGenreDeleteButton; }
            set
            {
                _enableDisableGenreDeleteButton = value;
                OnPropertyChanged("EnableDisableGenreDeleteButton");
            }
        }

        private Boolean _enablaDisableGenreAddButton;
        public Boolean EnableDisableGenreAddButton
        {
            get { return _enablaDisableGenreAddButton; }
            set
            {
                _enablaDisableGenreAddButton = value;
                OnPropertyChanged("EnableDisableGenreAddButton");
            }
        }

        private Boolean _enableDisableGenreEditButton;
        public Boolean EnableDisableGenreEditButton
        {
            get { return _enableDisableGenreEditButton; }
            set
            {
                _enableDisableGenreEditButton = value;
                OnPropertyChanged("EnableDisableGenreEditButton");
            }
        }


        private string _buttonContentAddGenre;
        public string ButtonContentAddGenre
        {
            get { return _buttonContentAddGenre; }
            set
            {
                _buttonContentAddGenre = value;
                OnPropertyChanged("ButtonContentAddGenre");
            }
        }

        private string _buttonContentEditGenre;
        public string ButtonContentEditGenre
        {
            get { return _buttonContentEditGenre; }
            set
            {
                _buttonContentEditGenre = value;
                OnPropertyChanged("ButtonContentEditGenre");
            }
        }

        private string _contentTextboxGenre;
        public string ContentTextboxGenre
        {
            get { return _contentTextboxGenre; }
            set
            {
                _contentTextboxGenre = value;
                OnPropertyChanged("ContentTextboxGenre");
            }
        }

        public ICommand AddGenreCommand
        {
            get
            {
                return new RelayCommand(AddGenre);
            }
        }

        public ICommand EditGenreCommand
        {
            get
            {
                return new RelayCommand(EditGenre);
            }
        }

        public ICommand DeleteGenreCommand
        {
            get
            {
                return new RelayCommand(DeleteGenre);
            }
        }

        private void AddGenre()
        {
            if (ButtonContentAddGenre == "Voeg toe")
            {
                Genre nieuw = new Genre();
                SelectedGenre = nieuw;

                ButtonContentAddGenre = "Opslaan";
                VisibilityGenreListbox = "hidden";
                VisibilityGenreTextbox = "visible";
                EnableDisableGenreDeleteButton = false;
                EnableDisableGenreEditButton = false;
            }
            else
            {
                DALGenres.InsertGenre(SelectedGenre);
                Genres = DALGenres.GetGenres();

                ButtonContentAddGenre = "Voeg toe";
                VisibilityGenreListbox = "visible";
                VisibilityGenreTextbox = "hidden";
            }
        }

        private void EditGenre()
        {
            if (ButtonContentEditGenre == "Wijzig")
            {
                ButtonContentEditGenre = "Opslaan";
                VisibilityGenreListbox = "hidden";
                VisibilityGenreTextbox = "visible";

                EnableDisableGenreAddButton = false;
                EnableDisableGenreDeleteButton = false;
            }
            else
            {
                DALGenres.UpdataGenre(SelectedGenre);
                Genres = DALGenres.GetGenres();
                //Nog implementeren
                //ContactPersons = DALContactPerson.GetContactPersons();
                //Bands = DALBans.GetBands();

                ButtonContentEditGenre = "Wijzig";
                VisibilityGenreListbox = "visible";
                VisibilityGenreTextbox = "hidden";

                EnableDisableGenreAddButton = true;
                EnableDisableGenreEditButton = false;
            }
        }

        private void DeleteGenre()
        {
            DALGenres.DeleteGenre(SelectedGenre);
            Genres = DALGenres.GetGenres();

            EnableDisableGenreDeleteButton = false;
            EnableDisableGenreEditButton = false;
        }

        //nog te implementeren! 
        //private void ChangeGenreDeleteButton()
        //{
        //    foreach (ContactPerson person in ContactPersons)
        //    {
        //        if (person.ContactPersonType.id.Equals(SelectedType.id))
        //        {
        //            EnableDisableContactPersonTypeDeleteButton = false;
        //            return;
        //        }
        //        else
        //        {
        //            EnableDisableContactPersonTypeDeleteButton = true;
        //            Console.WriteLine("Geen contactpersonen gevonden");
        //        }
        //    }
        //}
        #endregion 
        #region "Stages"
        //private ObservableCollection<ContactPerson> Bands = new ObservableCollection<ContactPerson>();
        private ObservableCollection<Stage> _stages;
        public ObservableCollection<Stage> Stages
        {
            get { return _stages; }
            set
            {
                _stages = value;
                OnPropertyChanged("Stages");
            }
        }

        private Stage _selectedStage;
        public Stage SelectedStage
        {
            get { return _selectedStage; }
            set
            {
                _selectedStage = value;
                if (SelectedStage != null)
                {
                    //nog te implementeren! 
                    //ChangeGenreDeleteButton();
                    EnableDisableStageEditButton = true;
                    //Console.WriteLine("test");
                }
                OnPropertyChanged("SelectedStage");
            }
        }

        private string _visibilityStageTextbox;
        public string VisibilityStageTextbox
        {
            get { return _visibilityStageTextbox; }
            set
            {
                _visibilityStageTextbox = value;
                OnPropertyChanged("VisibilityStageTextbox");
            }
        }

        private string _visibilityStageListbox;
        public string VisibilityStageListbox
        {
            get { return _visibilityStageListbox; }
            set
            {
                _visibilityStageListbox = value;
                OnPropertyChanged("VisibilityStageListbox");
            }
        }

        private Boolean _enableDisableStageDeleteButton;
        public Boolean EnableDisableStageDeleteButton
        {
            get { return _enableDisableStageDeleteButton; }
            set
            {
                _enableDisableStageDeleteButton = value;
                OnPropertyChanged("EnableDisableStageDeleteButton");
            }
        }

        private Boolean _enablaDisableStageAddButton;
        public Boolean EnableDisableStageAddButton
        {
            get { return _enablaDisableStageAddButton; }
            set
            {
                _enablaDisableStageAddButton = value;
                OnPropertyChanged("EnableDisableStageAddButton");
            }
        }

        private Boolean _enableDisableStageEditButton;
        public Boolean EnableDisableStageEditButton
        {
            get { return _enableDisableStageEditButton; }
            set
            {
                _enableDisableStageEditButton = value;
                OnPropertyChanged("EnableDisableStageEditButton");
            }
        }


        private string _buttonContentAddStage;
        public string ButtonContentAddStage
        {
            get { return _buttonContentAddStage; }
            set
            {
                _buttonContentAddStage = value;
                OnPropertyChanged("ButtonContentAddStage");
            }
        }

        private string _buttonContentEditStage;
        public string ButtonContentEditStage
        {
            get { return _buttonContentEditStage; }
            set
            {
                _buttonContentEditStage = value;
                OnPropertyChanged("ButtonContentEditStage");
            }
        }

        private string _contentTextboxStage;
        public string ContentTextboxStage
        {
            get { return _contentTextboxStage; }
            set
            {
                _contentTextboxStage = value;
                OnPropertyChanged("ContentTextboxStage");
            }
        }

        public ICommand AddStageCommand
        {
            get
            {
                return new RelayCommand(AddStage);
            }
        }

        public ICommand EditStageCommand
        {
            get
            {
                return new RelayCommand(EditStage);
            }
        }

        public ICommand DeleteStageCommand
        {
            get
            {
                return new RelayCommand(DeleteStage);
            }
        }

        private void AddStage()
        {
            if (ButtonContentAddStage == "Voeg toe")
            {
                Stage nieuw = new Stage();
                SelectedStage = nieuw;

                ButtonContentAddStage = "Opslaan";
                VisibilityStageListbox = "hidden";
                VisibilityStageTextbox = "visible";
                EnableDisableStageDeleteButton = false;
                EnableDisableStageEditButton = false;
            }
            else
            {
                DALStages.InsertStage(SelectedStage);
                Stages = DALStages.GetStages();

                ButtonContentAddStage = "Voeg toe";
                VisibilityStageListbox = "visible";
                VisibilityStageTextbox = "hidden";
            }
        }

        private void EditStage()
        {
            if (ButtonContentEditStage == "Wijzig")
            {
                ButtonContentEditStage = "Opslaan";
                VisibilityStageListbox = "hidden";
                VisibilityStageTextbox = "visible";

                EnableDisableStageAddButton = false;
                EnableDisableStageDeleteButton = false;
            }
            else
            {
                DALStages.UpdataStage(SelectedStage);
                Stages = DALStages.GetStages();
                //Nog implementeren
                //ContactPersons = DALContactPerson.GetContactPersons();
                //Bands = DALBans.GetBands();

                ButtonContentEditStage = "Wijzig";
                VisibilityStageListbox = "visible";
                VisibilityStageTextbox = "hidden";

                EnableDisableStageAddButton = true;
                EnableDisableStageEditButton = false;
            }
        }

        private void DeleteStage()
        {
            DALStages.DeleteStage(SelectedStage);
            Stages = DALStages.GetStages();

            EnableDisableStageDeleteButton = false;
            EnableDisableStageEditButton = false;
        }

        //nog te implementeren! 
        //private void ChangeGenreDeleteButton()
        //{
        //    foreach (ContactPerson person in ContactPersons)
        //    {
        //        if (person.ContactPersonType.id.Equals(SelectedType.id))
        //        {
        //            EnableDisableContactPersonTypeDeleteButton = false;
        //            return;
        //        }
        //        else
        //        {
        //            EnableDisableContactPersonTypeDeleteButton = true;
        //            Console.WriteLine("Geen contactpersonen gevonden");
        //        }
        //    }
        //}
        #endregion 
        public string Name
        {
            get { return "Instellingen"; }
        }
    }
}
