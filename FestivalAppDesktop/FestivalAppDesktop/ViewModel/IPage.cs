using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalAppDesktop.ViewModel
{
    //elke ViewModel klasse zal deze interface moeten implementeren. 
    //Zo kan ik later een lijst van objecten van klassen gaan bijhouden waarvan de klasse deze interface implementeert. 
    //Deze lijst zit in de klasse ApplicationVM
    interface IPage
    {
        //één property insteken. Elke klasse moet deze property gaan uitwerken.
        string Name { get; }
    }
}
