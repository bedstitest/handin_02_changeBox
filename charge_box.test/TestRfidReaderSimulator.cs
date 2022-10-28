using charge_box.classes;


namespace charge_box.test
{
    [TestFixture]
    public class TestRfidReaderSimulator 
    {
        private RfidReaderSimulator _uut;
        private RfidDetectedEventArgs _RfidDetectedEventArgs;

        [SetUp]
        public void Setup()
        {
            _uut = new RfidReaderSimulator();
            _uut.RfidValueEvent += (o, args) => _RfidDetectedEventArgs = args;
            
        }

        [Test]
        public void ctor_IdIsZero()
        {
            Assert.That(_uut.id, Is.EqualTo(0));
        }

        [Test]
        public void SetId_OnDetectEvent()
        {
          _uut.Id = 1;
          Assert.That(_RfidDetectedEventArgs.Id, Is.EqualTo(1));
        }

        [Test]
        public void GetId()
        {
            _uut.Id = 1;
            Assert.That(_uut.Id, Is.EqualTo(1));
        }

        [Test]
        public void SetId_OnDetectEventFired()
        {
            _uut.Id = 1;
            Assert.That(_RfidDetectedEventArgs, Is.Not.Null);
        }

        [Test]
        public void SetId_changeValue()
        {
            _uut.Id = 2;
            Assert.That(_uut.id, Is.EqualTo(2));
        }

    }
}
