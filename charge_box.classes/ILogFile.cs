using System.IO;

namespace charge_box.classes
{
    public interface ILogFile
    {
        public void LogDoorUnlocked(int id, DateTime TimeOfEvent);
        public void LogDoorLocked(int id, DateTime TimeOfEvent);
    }
}