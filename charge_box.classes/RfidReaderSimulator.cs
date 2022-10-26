namespace charge_box.classes
{
    public class RfidReaderSimulator : IRfidReader
    {
        public event EventHandler<RfidDetectedEventArgs> RfidValueEvent;
        public int id;

        public RfidReaderSimulator()
        {
            id = 0;
        }

        public int SetId
        {
            get => id;
            set
            {
                if (id == value) return;
                OnRfidDetected(new RfidDetectedEventArgs(){Id = value});
                id = value;
            }
        }

        private void OnRfidDetected(RfidDetectedEventArgs e)
        {
            RfidValueEvent?.Invoke(this, e);
        }
    }
}