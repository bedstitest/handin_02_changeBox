using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charge_box.classes
{
    public interface IDisplay<in T>
    {
        /// <summary>
        /// Renders a new message on the display.
        /// Each area of the display is declared with a string
        /// </summary>
        /// <param name="typeOfMessage"></param>
        /// <param name="message"></param>
        void DisplayMessage(T typeOfMessage, string message);    
    }
}
