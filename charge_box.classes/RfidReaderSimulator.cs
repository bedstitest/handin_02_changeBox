namespace charge_box.classes
{
    public class RfidReaderSimulator : IRfidReader
    {
        public event EventHandler<RfidDetectedEventArgs> RfidValueEvent;
        public int id { get; set; }

        public RfidReaderSimulator()
        {
            id = 0;
        }
        private void OnRfidDetected(RfidDetectedEventArgs e)
        {
            RfidValueEvent?.Invoke(this, e);
        }
    }
}