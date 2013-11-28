using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAppDesktop.ViewModel
{
    class InstellingenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Instellingen"; }
        }
    }
}
