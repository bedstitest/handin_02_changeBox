using charge_box.classes;

namespace charge_box.test;

public class FakeConsoleSimulator : IConsoleSimulator
{
    public void Clear()
    {
    }

    public void Write(string text)
    {
        Console.Write(text);
    }

    public int BufferWidth
    {
        get => 80;
        set { throw new NotImplementedException(); }
    }

    public void SetCursorPosition(int Left, int Top)
    {
    }

    public (int Left, int Top) GetCursorPosition()
    {
        return (0, 0);
    }
}