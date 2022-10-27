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
        private enum ChargeboxState
        {
            Available,
            Locked,
            DoorOpen
        };

        private ChargeboxState _state;
        private IChargeControl _charger;
        private IDoor _door;

        private IDisplay<string> _display;
        private ILogFile _logFile;
        private IRfidReader _rfidReader;

        public int OldId { get; private set; }
        public bool DoorOpen { get; private set; }

        public StationControl(IChargeControl charger, IDoor door, IDisplay<string> display, ILogFile logFile, IRfidReader rfidReader)
        {
            _state = ChargeboxState.Available;

            _charger = charger;
            _door = door;
            _display = display;
            _logFile = logFile;
            _rfidReader = rfidReader;

            _door.DoorOpenedEvent += HandleDoorOpenedEvent;
            _door.DoorClosedEvent += HandleDoorClosedEvent;
            _rfidReader.RfidValueEvent += RfidDetectedEvent;
        }

        private bool CheckId(int oldId, int id)
        {
            return oldId == id;
        }
        private void DoorOpened()
        {
            DoorOpen = true;
            _display.DisplayMessage("user", "Please connect ye olde phone");
            _state = ChargeboxState.DoorOpen;
        }
        private void DoorClosed()
        {
            DoorOpen = false;
            _display.DisplayMessage("user", "Please use ye taggy thingy");
            _state = ChargeboxState.Available;
        }


        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case ChargeboxState.Available:
                    // Check for charger connection
                    if (_charger.IsConnected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        OldId = id;
                        _logFile.LogDoorLocked(id);

                        _display.DisplayMessage("user", "Charge box is locked and phone is charging. Use ye taggy thingy to unlock");
                        _state = ChargeboxState.Locked;
                    }
                    else
                    {
                        _display.DisplayMessage("user", "Ye phone is not properly connected. Try once again");
                    }
                    break;

                case ChargeboxState.DoorOpen:
                    // Ignore
                    break;

                case ChargeboxState.Locked:
                    // Check for correct ID
                    if (CheckId(OldId, id))
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _logFile.LogDoorUnlocked(id);

                        _display.DisplayMessage("user", "Pick up ye phone and close the door");
                        _state = ChargeboxState.Available;
                    }
                    else
                    {
                        _display.DisplayMessage("user", "You have no power here!");
                    }
                    break;
            }
        }

        private void HandleDoorOpenedEvent(object s, DoorOpenedEventArgs e)
        {
            DoorOpened();
        }
        private void HandleDoorClosedEvent(object s, DoorClosedEventArgs e)
        {
            DoorClosed();
        }
        private void RfidDetectedEvent(object s, RfidDetectedEventArgs e)
        {
            RfidDetected(e.Id);
        }
    }
}
