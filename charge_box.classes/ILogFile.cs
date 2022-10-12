namespace charge_box.classes
{


    public interface ILogFile
    {
        event LogDoorUnlocked<DoorOpenedEventArgs> DoorOpenedEvent;
        event LogDoorLocked<DoorClosedEventArgs> DoorClosedEvent;
    }

    public class DoorOpenedEventArgs : EventArgs
    {
        public string Time {get;set; }
    }

    public class DoorClosedEventArgs : EventArgs
    {
        public string Time {get;set; }
    }


    public class LogFile : ILogFile
    {
        private readonly string path;
        private string time;

        public event EventHandler<DoorOpenedEventArgs> DoorOpenedEvent;
        public event EventHandler<DoorClosedEventArgs> DoorClosedEvent;
        public void logfileOpened(object o, string DoorOpenedTime)
        {
            onDooropened(new DoorOpenedEventArgs {Time = DoorOpenedTime});

        }

        public void logfileClosed(object o, string DoorClosedTime)
        {
            onDoorClosed(new DoorClosedEventArgs {Time = DoorClosedTime});
        }


         protected virtual void onDoorOpened(DoorOpenedEventArgs e)
        {
            DoorOpenedEvent?.invoke(this.e);
        }

        protected virtual void onDoorClosed(DoorClosedEventArgs e)
        {
            DoorClosedEvent?.invoke(this.e);
        }


    }


}