using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FestivalAppDesktop.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        public ApplicationVM()
        {
            _pages = new ObservableCollection<IPage>();

            //hieronder voegen we al een eerst IPage-object toe
            //bij nieuwe Pages moet deze lijst aangevuld worden met telkens de bijhorende ViewModel-klasse
            _pages.Add(new LineUpVM());
            _pages.Add(new ContactPersonenVM());
            _pages.Add(new TicketingVM());
            _pages.Add(new InstellingenVM());

            //default zetten we de CurrentPage in io eerste IPage (hier HomePage)
            _currentPage = Pages[0];
        }

        private IPage _currentPage;
        public IPage CurrentPage
        {
            get
            {
                return _currentPage; //huidige page (dat nu getoond wordt)
            }
            set
            {
                _currentPage = value;
                //ik maak aan de buitenwereld bekend dat er de property "CurrentPage" 
                //gewijzigd is. Eventuele controls die er aan gebind zijn, worden nu aangepast.
                OnPropertyChanged("CurrentPage");
            }
        }

        //bijhouden van de verschillende IPages
        private ObservableCollection<IPage> _pages;
        public ObservableCollection<IPage> Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                _pages = value;
                OnPropertyChanged("Pages");
            }
        }

        //onderstaande komt uit cursus
        //deze twee methodes worden gebruikt door de buttons (op mainwindow.xaml)
        //en gaan het juiste commando activeren. Hier is dat het wisselen van de Page
        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        private void ChangePage(IPage page)
        {
            CurrentPage = page;
        }
    }
}
