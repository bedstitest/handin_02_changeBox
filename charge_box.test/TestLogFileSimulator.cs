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
        _uut.FilePath_ = Environment.CurrentDirectory;
        _idnumber = 123;
        _uut.LogDoorUnlocked(_idnumber, _logDate = _uut.GetCurrentTime());
        if (File.Exists(_uut.FilePath_ + _idnumber))
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
        _uut.FilePath_ = Environment.CurrentDirectory;
        _idnumber = 123;
        _CheckIfFileExist = true;
        _uut.LogDoorUnlocked(_idnumber, _logDate = _uut.GetCurrentTime());
        if (File.Exists(_uut.FilePath_ + "ShouldntExist.txt"))
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
        _uut.FilePath_ = Environment.CurrentDirectory;
        _idnumber = 123;
        _uut.LogDoorLocked(_idnumber, _logDate = _uut.GetCurrentTime());
        if (File.Exists(_uut.FilePath_ + _idnumber))
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
        _uut.FilePath_ = Environment.CurrentDirectory;
        _idnumber = 123;
        _CheckIfFileExist = true;
        _uut.LogDoorLocked(_idnumber, _logDate = _uut.GetCurrentTime());
        if (File.Exists(_uut.FilePath_ + "ShouldntExist.txt"))
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
    public void Test_timeIsCorrect()
    {
        _logDate = _uut.GetCurrentTime();

        Assert.That(_logDate.Minute, Is.Not.Empty);
    }

    [Test]
    public void Test_StringisNotNull()
    {
        Assert.That(_uut.FilePath_, Is.Not.Empty);
    }











}