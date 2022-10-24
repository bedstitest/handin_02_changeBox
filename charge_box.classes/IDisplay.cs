using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charge_box.classes
{
    public interface IDisplay<in T>
    {
        void DisplayMessage(T typeOfMessage, string message);    
    }
}
