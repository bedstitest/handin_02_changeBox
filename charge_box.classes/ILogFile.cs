namespace charge_box.classes
{


    public interface ILogFile
    {
        void LogDoorLocked(int Id, DateTime TimeOfEvent);
        void LogDoorUnlocked(int Id, DateTime TimeOfEvent);
    }

}