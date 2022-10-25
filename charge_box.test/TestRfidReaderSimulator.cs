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

        [SetUp]
        public void Setup()
        {
            _uut = new RfidReaderSimulator();
        }

        [Test]
        public void ctor_IdIsZero()
        {
            Assert.That(_uut.id, Is.EqualTo(1));
        }

    }
}
