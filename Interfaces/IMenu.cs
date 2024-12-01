using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HavenHotel.InterfaceFolder
{
    public interface IMenu
    {
        void DisplayMenu(List<string> menue, Action option1, Action option2, Action option3, Action option4, Action option5, Action option6);
    }
}
