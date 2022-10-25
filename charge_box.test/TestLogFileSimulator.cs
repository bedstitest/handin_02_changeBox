using charge_box.classes;

namespace charge_box.test;

[TestFixture]
public class TestLogFileSimulator
{
    private LogFileSimulator _uut;
    private int _idnumber;
    private System.DateTime _logDate;
    private bool _CheckIfFileExist = false;

    

    [SetUp]
    public void Setup()
    {
        _uut = new LogFileSimulator();
    }

    [Test]
    public void LogDoorUnlocked_assertFilePath_correct()
    {
        string FilePath_ = Environment.CurrentDirectory;
        _idnumber = 123;
        _logDate = DateTime.Now;
        _uut.LogDoorUnlocked(_idnumber, _logDate);
        if (File.Exists(FilePath_ + "/logfile.txt"))
        {
            _CheckIfFileExist = true;
        }
        else
        {
            _CheckIfFileExist = false;
        }

        Assert.That(_CheckIfFileExist, Is.True);
    }

    [Test]
    public void LogDoorUnlocked_assertFilePath_Notcorrect()
    {
        string FilePath_ = Environment.CurrentDirectory;
        _idnumber = 123;
        _CheckIfFileExist = true;
        _uut.LogDoorUnlocked(_idnumber, _logDate = _uut.GetCurrentTime());
        if (File.Exists(FilePath_ + "ShouldntExist.txt"))
        {
            _CheckIfFileExist = true;
        }
        else
        {
            _CheckIfFileExist = false;
        }

        Assert.That(_CheckIfFileExist, Is.False);
    }

    [Test]
    public void LogDoorlocked_assertFilePath_correct()
    {
        string FilePath_ = Environment.CurrentDirectory;
        _idnumber = 123;
        _uut.LogDoorLocked(_idnumber, _logDate = _uut.GetCurrentTime());
        if (File.Exists(FilePath_ + "/logfile.txt"))
        {
            _CheckIfFileExist = true;
        }
        else
        {
            _CheckIfFileExist = false;
        }

        Assert.That(_CheckIfFileExist, Is.True);
    }

    [Test]
    public void LogDoorlocked_assertFilePath_Notcorrect()
    {
        string FilePath_ = Environment.CurrentDirectory;
        _idnumber = 123;
        _CheckIfFileExist = true;
        _uut.LogDoorLocked(_idnumber, _logDate = _uut.GetCurrentTime());
        if (File.Exists(FilePath_ + "ShouldntExist.txt"))
        {
            _CheckIfFileExist = true;
        }
        else
        {
            _CheckIfFileExist = false;
        }

        Assert.That(_CheckIfFileExist, Is.False);
    }

    /*
    [Test]
    public void Test_timeIsCorrect()
    {
        _logDate = _uut.GetCurrentTime();

        Assert.That(_logDate.Minute, Is.Not.);
    }
    */

    [Test]
    public void CheckifFileisNotEmpty()
    {
        bool Fileisnotempty = true;
        string FilePath_ = Environment.CurrentDirectory;
        int id = 23;
        var testTime = new DateTime(2022, 10, 13);
        _uut.LogDoorUnlocked(id, testTime);

        if (new FileInfo(FilePath_+ "/logfile.txt").Length > 0)
        {
            
            Fileisnotempty = false;
        }
        Assert.That(Fileisnotempty, Is.EqualTo(false));

    }










}