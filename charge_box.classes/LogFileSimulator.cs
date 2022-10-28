using System.IO;

namespace charge_box.classes
{
    public class LogFileSimulator : ILogFile
    {        

        public void LogDoorLocked(int id)
        {
            var TimeOfEvent = DateTime.Now;
            var message = "Door Has been Locked: " + TimeOfEvent;
            WriteToLog(id, message, TimeOfEvent);
        }

        public void LogDoorUnlocked(int id)
        {
            var TimeOfEvent = DateTime.Now;
            var message = "Door has been unlocked: "; 
            WriteToLog(id, message, TimeOfEvent);
        }

        private void WriteToLog(int id, string message, DateTime TimeOfEvent)
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
                File.WriteAllLines("logfile.txt", lines);
                
            }
            else

            {
                using var sw = File.AppendText(file);
                sw.WriteLine("Id: " + id.ToString());
                sw.WriteLine("Message: " + message);
                sw.WriteLine("Time of event: " + TimeOfEvent.ToString());
            }
            
                
        }

    }


}