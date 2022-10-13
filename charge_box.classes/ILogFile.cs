namespace charge_box.classes
{

    
    public interface ILogFile
    {
        public void LogDoorUnlocked(int id, DateTime TimeOfEvent);
        public void LogDoorLocked(int id, DateTime TimeOfEvent);
    }


    public class LogFile : ILogFile
    {

        private int idnum;

        public void LogDoorLocked(int id, DateTime TimeOfEvent)
        {
            string message = "Door Has been Locked: " + DoorClosedTime;
            TimeOfEvent = GetCurrentTime();
            WriteToLog(id, message);

        }

        public void LogDoorUnlocked(int id, DateTime TimeOfEvent)
        {
            OnDoorOpened();
            string message = "Door has been unlocked" + DoorOpenedTime;
            WriteToLog(id, message);
        }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        public static async Task WriteToLog(int id, string message)
        {
            string text =
                $"Id number: " + id + "Message: " + message;
            
            await File.WriteAllTextAsync("Logfile.txt", text);
        }

        private void OnDoorOpened()
        {
            DoorOpenedEvent?.Invoke(this, new DoorClosedEventArgs() {DoorOpenedTime = this.GetCurrentTime()});
        }

        private void OnDoorClosed()
        {
            DoorClosedEvent?.Invoke(this, new DoorOpenedEventArgs() {DoorClosedTime = this.GetCurrentTime()});
        }

    }


}