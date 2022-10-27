using System.IO;

namespace charge_box.classes
{
    public interface ILogFile
    {
        public void LogDoorUnlocked(int id);
        public void LogDoorLocked(int id);
    }
}