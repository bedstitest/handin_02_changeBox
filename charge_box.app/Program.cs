using charge_box.classes;

class Program
{
    static void Main(string[] args)
    {
        var door = new DoorSimulator();
        var rfidReader = new RfidReaderSimulator();
        IDisplay<string> display = new DisplaySimulator();
        ILogFile logFile = new LogFileSimulator();
        var usbCharger = new UsbChargerSimulator();
        IChargeControl chargeControl = new ChargeControl(display, usbCharger);


        var stationControl = new StationControl(chargeControl, door, display, logFile, rfidReader);
        // Assemble your system here from all the classes

        var finish = false;
        do
        {
            display.DisplayMessage("systemInfo", 
                $"width: {Console.BufferWidth} & {Console.WindowWidth} Height: {Console.BufferHeight} & {Console.WindowHeight} ({Console.GetCursorPosition()})".PadLeft(Console.BufferWidth));
            display.DisplayMessage("menu","Enter O, S, C, R, F, E: ");
            var input = Console.ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.E:
                    finish = true;
                    break;

                case ConsoleKey.O:
                    door.OnDoorOpen();
                    break;
                case ConsoleKey.S:
                    usbCharger.SimulateConnected(true);
                    break;
                case ConsoleKey.C:
                    door.OnDoorClose();
                    break;
                case ConsoleKey.F:
                    usbCharger.SimulateOverload(true);
                    break;
                case ConsoleKey.U:
                    usbCharger.SimulateOverload(false);
                    break;
                case ConsoleKey.R:
                    display.DisplayMessage("user","Indtast RFID id: ");
                    string idString = System.Console.ReadLine();
                    int id = Convert.ToInt32(idString);
                    rfidReader.Id = id;
                    display.DisplayMessage("status", $"registered RFid: {id}"); 
                    break;

               case ConsoleKey.A :
                // vim movement for testing stuff... 
                    break;
                case ConsoleKey.H:
                    Console.CursorLeft += Console.CursorLeft > 0 ? -1 : 0;
                    break;
                case ConsoleKey.L:
                    Console.CursorLeft += Console.CursorLeft < Console.BufferWidth-1 ? 1 : 0;
                    break;
                case ConsoleKey.J:
                    Console.CursorTop += Console.CursorTop < Console.BufferHeight-1 ? 1 : 0;
                    break;
                case ConsoleKey.K:
                    Console.CursorTop += Console.CursorTop > 0 ? -1 : 0;
                    break;

                default:
                    break;
            }

        } while (!finish);
    }
}
