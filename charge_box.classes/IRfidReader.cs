using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace charge_box.classes
{
    public class RfidDetectedEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
    public interface IRfidReader
    {
        event EventHandler<RfidDetectedEventArgs> RfidValueEvent;
    }
}