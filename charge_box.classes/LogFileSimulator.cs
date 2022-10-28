using System.IO;

namespace charge_box.classes
{
    public class LogFileSimulator : ILogFile
    {        

        public void LogDoorLocked(int id)
        {
            var TimeOfEvent = DateTime.Now;
            var message = "Door Has been Locked: " + TimeOfEvent;
            var task = WriteToLog(id, message, TimeOfEvent);
        }

        public void LogDoorUnlocked(int id)
        {
            var TimeOfEvent = DateTime.Now;
            var message = "Door has been unlocked: ";
            var task = WriteToLog(id, message, TimeOfEvent);
        }

        private static async Task WriteToLog(int id, string message, DateTime TimeOfEvent)
        {

            var workingDirectory = Environment.CurrentDirectory;
            var filename = "logfile.txt";
            var file = $"{workingDirectory}/{filename}";
            

            if (!(File.Exists(file)))
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
                await using var sw = File.AppendText(file);
                await sw.WriteLineAsync("Id: " + id.ToString());
                await sw.WriteLineAsync("Message: " + message);
                await sw.WriteLineAsync("Time of event: " + TimeOfEvent.ToString());
            }
            
                
        }

    }


}