namespace charge_box.classes
{
    public class DoorSimulator : IDoor
    {
        public DateTime TestTime { get; set; }

        private bool _open;
        public event EventHandler<DoorOpenedEventArgs>? DoorOpenedEvent;
        public event EventHandler<DoorClosedEventArgs>? DoorClosedEvent;
        public void UnlockDoor()
        {
            _open = true;
        }

        public void LockDoor()
        {
            _open = false;
        }

        public void OnDoorOpen()
        {
            if (_open)
            {
                OnDoorOpenedEvent(new DoorOpenedEventArgs
                {
                    DoorOpenedTime = TestTime
                });
            }
        }

        public void OnDoorClose()
        {
            OnDoorClosedEvent(new DoorClosedEventArgs
            {
                DoorClosedTime = TestTime
            });
        }

        private void OnDoorOpenedEvent(DoorOpenedEventArgs e)
        {
            DoorOpenedEvent?.Invoke(this, e);
        }

        private void OnDoorClosedEvent(DoorClosedEventArgs e)
        {
            DoorClosedEvent?.Invoke(this, e);
        }
    }
}
