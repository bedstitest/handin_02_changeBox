using charge_box.classes;

namespace charge_box.test;

[TestFixture]
public class TestDoorSimulator
{
    private DoorSimulator _uut;
    private DoorOpenedEventArgs _receivedDoorOpenedEventArgs;
    private DoorClosedEventArgs _receivedDoorClosedEventArgs;

    [SetUp]
    public void Setup()
    {
        _receivedDoorOpenedEventArgs = null;
        _receivedDoorClosedEventArgs = null;

        _uut = new DoorSimulator();

        _uut.DoorOpenedEvent += (sender, args) =>
        {
            _receivedDoorOpenedEventArgs = args;
        };
       
        _uut.DoorClosedEvent += (sender, args) =>
        {
            _receivedDoorClosedEventArgs = args;
        };
    }

    [Test]
    public void DoorUnlocked_DoorLocked_DoorOpened_NoEventFired()
    {
        _uut.LockDoor();
        _uut.OnDoorOpen();
        Assert.That(_receivedDoorOpenedEventArgs, Is.Null);
    }
    [Test]
    public void DoorUnlocked_DoorOpened_EventFired()
    {
        _uut.UnlockDoor();
        _uut.OnDoorOpen();
        Assert.That(_receivedDoorOpenedEventArgs, Is.Not.Null);
    }
    [Test]
    public void DoorClosed_EventFired()
    {
        _uut.OnDoorClose();
        Assert.That(_receivedDoorClosedEventArgs, Is.Not.Null);
    }
}