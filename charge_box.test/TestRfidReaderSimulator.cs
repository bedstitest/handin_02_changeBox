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
            _uut.RfidValueEvent += (o, args) => _RfidDetectedEventArgs = args;
            
        }

        [Test]
        public void ctor_IdIsZero()
        {
            Assert.That(_uut.id, Is.EqualTo(0));
        }

        //Can't test event? Event is called by StationContrion?
        [Test]
        public void SetId_OnDetectEvent()
        {
          _uut.Id = 1;
          Assert.That(_RfidDetectedEventArgs.Id, Is.EqualTo(1));
        }

        [Test]
        public void SetId_changeValue()
        {
            _uut.Id = 2;
            Assert.That(_uut.id, Is.EqualTo(2));
        }

    }
}
