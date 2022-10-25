using charge_box.classes;
using NSubstitute;
using NUnit.Framework;

namespace charge_box.test;

[TestFixture]
public class TestStationController
{
    private StationControl _uut;

    private IChargeControl _chargeControl;
    private IDoor _door;
    private IDisplay _display;
    private IRfidReader _rfidReader;
    private ILogFile _logFile;

    [SetUp]
    public void Setup()
    {
        _chargeControl = Substitute.For<IChargeControl>();
        _door = Substitute.For<IDoor>();
        _display = Substitute.For<IDisplay>();
        _rfidReader = Substitute.For<IRfidReader>();
        _logFile = Substitute.For<ILogFile>();
        _uut = new StationControl(_chargeControl, _door, _display, _logFile, _rfidReader);
    }

    [Test]
    public void DoorOpenedEvent_DoorClosedEvent_DoorStatusIsCorrect()
    {
        _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs());
        Assert.That(_uut.DoorOpen, Is.True);
        _door.DoorClosedEvent += Raise.EventWith(new DoorClosedEventArgs());
        Assert.That(_uut.DoorOpen, Is.False);
    }

    [Test]
    public void RfidDetectedEvent_IdIsCorrect()
    {
        _chargeControl.IsConnected.Returns(true);
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = 5 });
        Assert.That(_uut.OldId, Is.EqualTo(5));
    }
    [Test]
    public void RfidDetectedEvent_PhoneNotConnected()
    {
        _chargeControl.IsConnected.Returns(false);
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = 5 });
        Assert.That(_uut.OldId, Is.EqualTo(0));
    }
    [Test]
    public void LockDoor_GoodRfidDetected_DoorUnlocked()
    {
        _chargeControl.IsConnected.Returns(true);
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = 5 });
        _door.Received(1).LockDoor();
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = 5 });
        _door.Received(1).UnlockDoor();
    }
    [Test]
    public void LockDoor_BadRfidDetected_DoorStillLocked()
    {
        _chargeControl.IsConnected.Returns(true);
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = 5 });
        _door.Received(1).LockDoor();
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = 99 });
        _door.Received(0).UnlockDoor();
    }
}