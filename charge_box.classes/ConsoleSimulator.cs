namespace charge_box.classes;

public class ConsoleSimulator : IConsoleSimulator
{
    public void Clear()
    {
        Console.Clear();
    }

    public void Write(string text)
    {
        Console.Write(text);
    }

    public int BufferWidth { get => Console.BufferWidth; set => Console.BufferWidth = value; }
   
    
    public void SetCursorPosition(int left, int top)
    {
        Console.SetCursorPosition(left, top);
    }

    public (int Left, int Top) GetCursorPosition()
    {
        return Console.GetCursorPosition();
    }
}