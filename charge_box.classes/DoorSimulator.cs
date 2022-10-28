namespace charge_box.classes
{
    public class DoorSimulator : IDoor
    {
        private bool _locked;
        public event EventHandler<DoorOpenedEventArgs>? DoorOpenedEvent;
        public event EventHandler<DoorClosedEventArgs>? DoorClosedEvent;
        public void UnlockDoor()
        {
            _locked = false;
        }

        public void LockDoor()
        {
            _locked = true;
        }

        public void OnDoorOpen()
        {
            if (!_locked)
            {
                OnDoorOpenedEvent(new DoorOpenedEventArgs());
            }
        }

        public void OnDoorClose()
        {
            OnDoorClosedEvent(new DoorClosedEventArgs());
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
