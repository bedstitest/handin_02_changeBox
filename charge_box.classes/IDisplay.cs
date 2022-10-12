using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charge_box.classes
{
    public interface IDisplay
    {
        void DisplayMessage<T>(T typeOfMessage, string message);    
    }
}
