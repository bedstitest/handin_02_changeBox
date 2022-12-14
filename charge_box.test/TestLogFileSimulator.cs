using System.Runtime.InteropServices;
using charge_box.classes;
using System.Threading;
using NSubstitute;

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

    [TearDown]
    public void cleanup()
    {
        string FilePath_ = Environment.CurrentDirectory;
        File.Delete(FilePath_+"/logfile.txt");

    }

    [Test]
    public void LogDoorUnlocked_assertFilePath_correct()
    {
        string FilePath_ = Environment.CurrentDirectory;
        _idnumber = 123;
        _logDate = DateTime.Now;
        _uut.LogDoorUnlocked(_idnumber);
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
        _uut.LogDoorUnlocked(_idnumber);
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
        _uut.LogDoorLocked(_idnumber);
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
        _uut.LogDoorLocked(_idnumber);
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
        _uut.LogDoorUnlocked(id);

        if (new FileInfo(FilePath_+ "/logfile.txt").Length > 0)
        {
            
            Fileisnotempty = false;
        }
        Assert.That(Fileisnotempty, Is.EqualTo(false));
    }


    [Test]

    public void multipleInscriptions()
    {
        string FilePath_ = Environment.CurrentDirectory;
        int id = 23;
        
        var testTime = DateTime.Now;

        _uut.LogDoorLocked(id);

        Thread.Sleep(50);
        var testTime2 = DateTime.Now;

        _uut.LogDoorLocked(id);
        Thread.Sleep(50);  
        
        string text = File.ReadAllText(FilePath_ + "/logfile.txt");
        var checkString = "";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            checkString =
                $"New logging: \nId: 23\nMessage: Door Has been Locked: {testTime}\nTime of event: {testTime}\nId: 23\nMessage: Door Has been Locked: {testTime2}\nTime of event: {testTime2}\n";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            checkString =
                $"New logging: \r\nId: 23\r\nMessage: Door Has been Locked: {testTime}\r\nTime of event: {testTime}\r\nId: 23\r\nMessage: Door Has been Locked: {testTime2}\r\nTime of event: {testTime2}\r\n";
        }
        Assert.That(text,Is.EqualTo(checkString));

    }










}