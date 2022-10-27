using charge_box.classes;
using NSubstitute;
using NUnit.Framework.Internal;

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
        var testId = 5;
        _chargeControl.IsConnected.Returns(true);
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = testId });
        _chargeControl.Received(1).StartCharge();
        Assert.That(_uut.OldId, Is.EqualTo(testId));
        _logFile.Received(1).LogDoorLocked(testId);
        _display.Received(1).DisplayMessage("user", "Charge box is locked and phone is charging. Use ye taggy thingy to unlock");
    }
    [Test]
    public void RfidDetectedEvent_PhoneNotConnected()
    {
        var testId = 5;
        _chargeControl.IsConnected.Returns(false);
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = testId });
        Assert.That(_uut.OldId, Is.EqualTo(0));
        _display.Received(1).DisplayMessage("user", "Ye phone is not properly connected. Try once again");
    }
    [Test]
    public void LockDoor_GoodRfidDetected_DoorUnlocked()
    {
        var testId = 5;
        _chargeControl.IsConnected.Returns(true);
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = testId });
        _door.Received(1).LockDoor();
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = testId });
        _door.Received(1).UnlockDoor();
        _logFile.Received(1).LogDoorUnlocked(testId);
        _display.Received(1).DisplayMessage("user", "Pick up ye phone and close the door");

    }
    [Test]
    public void LockDoor_BadRfidDetected_DoorStillLocked()
    {
        _chargeControl.IsConnected.Returns(true);
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = 5 });
        _door.Received(1).LockDoor();
        _rfidReader.RfidValueEvent += Raise.EventWith(new RfidDetectedEventArgs { Id = 99 });
        _door.Received(0).UnlockDoor();
        _display.Received(1).DisplayMessage("user", "You have no power here!");

    }
}