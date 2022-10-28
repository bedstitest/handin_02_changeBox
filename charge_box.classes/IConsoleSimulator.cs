namespace charge_box.classes;

public interface IConsoleSimulator
{
    void Clear();
    void Write(string text);
    int BufferWidth { get; set; }
    void SetCursorPosition(int Left, int Top);
    (int Left, int Top) GetCursorPosition();
}

public interface IConsoleWriter
{
    
}