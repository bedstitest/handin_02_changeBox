namespace charge_box.classes
{
    public class LogFileSimulator : ILogFile
    {        

        public void LogDoorLocked(int id, DateTime TimeOfEvent)
        {
            string message = "Door Has been Locked: " + TimeOfEvent;
            var task = WriteToLog(id, message, TimeOfEvent);
        }

        public void LogDoorUnlocked(int id, DateTime TimeOfEvent)
        {
            string message = "Door has been unlocked: ";
            var task = WriteToLog(id, message, TimeOfEvent);
        }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
        
        public static async Task WriteToLog(int id, string message, DateTime TimeOfEvent)
        {

            var workingDirectory = Environment.CurrentDirectory;
            var filename = "logfile.txt";
            var file = $"{workingDirectory}{filename}";
            

            if (!File.Exists(id+file))
            {
                //If file doesnt exist, create a file to write to.
                string[] lines =
                {
                    "New logging: ",
                    "Id: " + id.ToString(),
                    "Message: " + message,
                    "Time of event: " + TimeOfEvent.ToString()

                };
                await File.WriteAllLinesAsync("logfile.txt", lines);
            }
            else
            {
                using StreamWriter appendfile = new(id+"logfile.txt", append: true);
                await appendfile.WriteLineAsync(
                    "Id: " + id.ToString() + "\n Message: " + 
                    message + "\nTime of event: " + 
                    TimeOfEvent.ToString() + "\n");
            }
                
        }

    }


}