namespace charge_box.classes;

public class DisplaySimulator : IDisplay<string>
{
    private int _width; 
    public DisplaySimulator()
    {
        _messageTypes.Add("menu", 1);
        _messageTypes.Add("user", 2);
        _messageTypes.Add("status", 3);
        _messageTypes.Add("systemInfo", 0);
        Console.Clear();
        _width = Console.BufferWidth;
    }
    public void DisplayMessage(string typeOfMessage, string message)
    {
        var type = _messageTypes[typeOfMessage];
        PrintMessage(type, message);
    }
    private void PrintMessage(int typeOfMessage, string message)
    {
        var curPos = Console.GetCursorPosition();
        Console.SetCursorPosition(0, typeOfMessage);
        Console.Write(message.PadRight(_width-2) + "\n");
        Console.SetCursorPosition(curPos.Left, curPos.Top);
    }

    private readonly IDictionary<string, int> _messageTypes = new Dictionary<string, int>();

}