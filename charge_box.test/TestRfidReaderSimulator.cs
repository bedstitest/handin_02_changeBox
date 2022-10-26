using charge_box.classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            _uut.RfidValueEvent += (sender, args) =>
            {
                _RfidDetectedEventArgs = args;
            };
        }

        [Test]
        public void ctor_IdIsZero()
        {
            Assert.That(_uut.id, Is.EqualTo(0));
        }

        //Can't test event? Event is called by StationContrion?
        [Test]
        public void OnTestReadEvent()
        {
          _uut.SetId = 1;
          Assert.That(_RfidDetectedEventArgs.Id, Is.EqualTo(1));
        }

    }
}
