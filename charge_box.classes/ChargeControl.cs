using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charge_box.classes
{
    public class ChargeControl : IChargeControl
    {
        private bool isConnected;
        public bool IsConnected
        {
            get => _charger.Connected;
        }

        private IDisplay<string> _display;
        private IUsbCharger _charger;

        public ChargeControl(IDisplay<string> display, IUsbCharger charger)
        {
            _display = display;
            _charger = charger;
            _charger.CurrentValueEvent += HandleCurrentEvent;
        }

        public void StartCharge()
        {
            if (IsConnected)
            {
                _display.DisplayMessage("status", "Charging has begun");
                _charger.StartCharge();
            }
        }

        public void StopCharge()
        {
            _display.DisplayMessage("status","Charging has ended");
            _charger.StopCharge();
        }

        public void HandleCurrentEvent(object s, CurrentEventArgs e)
        {
            switch (e.Current)
            {
                case > 0 and <= 5:
                    StopCharge();
                    break;
                case > 5 and <= 500:
                    _display.DisplayMessage("status","Charging is going well👍");
                    break;
                case > 500:
                    _display.DisplayMessage("status", "Charging stopped -" +
                                                      " Fatal error occurred, current is brutally high");
                    _charger.StopCharge();
                    break;
            }
        }
    }
}
