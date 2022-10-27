using charge_box.classes;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute.ReceivedExtensions;

namespace charge_box.test
{
    [TestFixture]
    public class TestChargeControl
    {
        private ChargeControl _uut;

        private IUsbCharger _charger;
        private IDisplay<string> _display;

        [SetUp]
        public void Setup()
        {
            _charger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay<string>>();
            _uut = new ChargeControl(_display, _charger);
        }

        #region EventTests
        [Test]
        public void CurrentEvent_StopChargeNotCalled_NegativeCurrent()
        {
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = -1 });
            _charger.Received(0).StopCharge();
        }

        [TestCase (1)]
        [TestCase (4)]
        [TestCase (5)]
        public void CurrentEvent_StopCharge_LowCurrent(int param)
        {
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = param });
            _charger.Received(1).StopCharge();
        }


        [TestCase(6)]
        [TestCase(499)]
        [TestCase(500)]
        public void CurrentEvent_StopCharge_MediumCurrent(int param)
        {
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = param });
            _display.Received(1).DisplayMessage("status", "Charging is going well👍");
        }


        [TestCase(501)]
        [TestCase(600)]
        public void CurrentEvent_StopCharge_HighCurrent(int param)
        {
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = param });
            _charger.Received(1).StopCharge();
        }
        #endregion
        #region StopStartChargeTests

        [Test]
        public void StopCharge_DelegatingMethodCalls()
        {
            _uut.StopCharge();
            _charger.Received(1).StopCharge();
            _display.Received(1).DisplayMessage("status","Charging has ended");
        }

        [Test]
        public void StartCharge_DelegatingMethodCalls()
        {
            _uut.StartCharge();
            _charger.Received(1).StartCharge();
            _display.DisplayMessage("status","Charging has begun");
        }
        #endregion

    }
}
