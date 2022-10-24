namespace charge_box.classes;

public class displaySimulator : IDisplay<string>
{
    public displaySimulator()
    {
        messageTypes.Add("menu", 0);
        messageTypes.Add("user", 1);
        messageTypes.Add("status", 2);
    }
    public void DisplayMessage(string typeOfMessage, string message)
    {
        var type = messageTypes[typeOfMessage];
        printMessage(type, message);
    }
    private void printMessage(int typeOfMessage, string message)
    {
        var curPos = Console.GetCursorPosition();
        Console.SetCursorPosition(0, typeOfMessage);
        Console.Write(message.PadLeft(Console.BufferWidth), ' ');
        Console.SetCursorPosition(curPos.Left, curPos.Top);
    }

    public readonly IDictionary<string, int> messageTypes = new Dictionary<string, int>();
}