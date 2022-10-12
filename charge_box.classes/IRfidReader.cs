using System;

namespace charge_box.classes
{
    public class RfidDetectedEventArgs : EventArgs
    {
        public int Id { get; set; }
    }
    public interface IRfidReader
    {
        event EventHandler<RfidDetectedEventArgs> RfidValueEvent;

        void ReadTag();
    }
}