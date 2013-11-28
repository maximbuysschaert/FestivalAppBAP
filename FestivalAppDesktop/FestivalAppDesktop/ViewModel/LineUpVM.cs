using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAppDesktop.ViewModel
{
    //dit is de ViewModel klasse dat hoort bij de view 'HomePage'
    class LineUpVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "LineUp"; } //unieke naam!
        }
    }
}
