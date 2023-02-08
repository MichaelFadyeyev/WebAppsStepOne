using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppStepOne.Models;

namespace WebAppStepOne.ViewModels
{
    public class LocationVM
    {
        public Location? Location {get;set;}

        public List<Location>? Forecast { get;set;}
    }
}
