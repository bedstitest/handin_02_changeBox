namespace charge_box.classes
{
    public class LogFileSimulator : ILogFile
    {
        public string FilePath_ = Environment.CurrentDirectory;
        public void LogDoorLocked(int id, DateTime TimeOfEvent)
        {
            string message = "Door Has been Locked: " + TimeOfEvent;
            TimeOfEvent = GetCurrentTime();
            WriteToLog(id, message);
        }

        public void LogDoorUnlocked(int id, DateTime TimeOfEvent)
        {
            string message = "Door has been unlocked: " + TimeOfEvent;
            WriteToLog(id, message);
        }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
        
        public void WriteToLog(int id, string message)
        {

            if (!File.Exists(FilePath_ + id + ".txt"))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(FilePath_ + id + ".txt"))
                {
                    sw.WriteLine("Message: " + message);
                    sw.WriteLine(GetCurrentTime());
                }
            }
            else
            {
                Console.WriteLine("Rfid already saved");
            }

        }

    }


}