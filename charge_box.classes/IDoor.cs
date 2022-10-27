namespace charge_box.classes
{
    public class DoorOpenedEventArgs : EventArgs
    {
    }
    public class DoorClosedEventArgs : EventArgs
    {
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