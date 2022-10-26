using charge_box.classes;

class Program
{
    static void Main(string[] args)
    {
        var door = new DoorSimulator();
        IDisplay display = new displaySimulator();
        //var rfidReader = new RfidReaderSimulator();
        ILogFile logFile = new LogFileSimulator();
        IUsbCharger usbCharger = new UsbChargerSimulator();
        //IChargeControl chargeControl = new ChargeControlSimulator(display, usbCharger);


        //var stationControl = new StationControl(chargeControl, door, display, logFile, rfidReader);

        bool finish = false;

        do
        {
            string input;
            System.Console.WriteLine("Indtast E, O, C, R: ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    door.OnDoorOpen();
                    break;

                case 'C':
                    door.OnDoorClose();
                    break;

                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    //rfidReader.OnRfidRead(id);
                    break;

                default:
                    break;
            }

        } while (!finish);
    }
}