using System;
namespace charge_box.classes;

public class DisplaySimulator : IDisplay<string>
{
    /// <summary>
    /// The _width variable saves the width of the buffer at start up.
    /// This display does not support  variable size buffers
    /// </summary>
    private readonly int _width; 
    /// <summary>
    /// The constructor crates the 4 areas available to print in.
    /// It then clears the buffer and saves the width.
    /// </summary>
    public DisplaySimulator()
    {
        _messageTypes.Add("menu", 1);
        _messageTypes.Add("user", 2);
        _messageTypes.Add("status", 3);
        _messageTypes.Add("systemInfo", 0);
        _width = Console.BufferWidth;
    }
    /// <summary>
    /// This prints the message its correct area, while clearing the content already there.
    /// </summary>
    /// <param name="typeOfMessage">"SystemInfo", "menu", "user" or "status" as valid options</param>
    /// <param name="message">anything goes, along as its a string</param>
    public void DisplayMessage(string typeOfMessage, string message)
    {
        var curPos = Console.GetCursorPosition();
        Console.SetCursorPosition(0, _messageTypes[typeOfMessage]);
        Console.Write(message.PadRight(_width-2) + "\n");
        Console.SetCursorPosition(curPos.Left, curPos.Top);
    }

    private readonly IDictionary<string, int> _messageTypes = new Dictionary<string, int>();

}