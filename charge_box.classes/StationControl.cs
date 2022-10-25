using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace charge_box.classes
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IRfidReader _rfidReader;
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        /// <summary>
        /// The constructor of the class StationControl
        /// </summary>
        /// <param name="rfidReader"> parameter for initiating the private field _rfidReader</param>
        /// <param name="charger"> parameter for initiating the private field _charger</param>
        /// <param name="door"> parameter for initiating the private field _door</param>
        StationControl(IRfidReader rfidReader, IChargeControl charger, IDoor door)
        {
            _door = door;
            _charger = charger;
            _rfidReader = rfidReader;
            _rfidReader.RfidValueEvent += RfidDetectedEvent;
        }
        /// <summary>
        /// This function serves as a glue between the event source and the RfidDetected function
        /// </summary>
        /// <param name="sender">The sender of the event - probably not needed at the moment</param>
        /// <param name="e">The values from the event - here the Id is passed to the RfidDetected function</param>
        private void RfidDetectedEvent(object sender, RfidDetectedEventArgs e)
        {
            RfidDetected(e.Id);
        }
        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.IsConnected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
    }
}
