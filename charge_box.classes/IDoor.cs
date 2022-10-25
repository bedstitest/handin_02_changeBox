namespace charge_box.classes
{
    public class DoorOpenedEventArgs : EventArgs
    {
        public DateTime DoorOpenedTime { get; set; }
    }
    public class DoorClosedEventArgs : EventArgs
    {
        public DateTime DoorClosedTime { get; set; }
    }
    public interface IDoor
    {
        // Event triggered on door opened
        event EventHandler<DoorOpenedEventArgs> DoorOpenedEvent;
        // Event triggered on door closed
        event EventHandler<DoorClosedEventArgs> DoorClosedEvent;

        void UnlockDoor();
        void LockDoor();
    }
}