using UsbSimulator;

namespace charge_box.classes
{
    public interface IChargeControl
    {
        public bool IsConnected();
        public void StartCharge();
        public void StopCharge();
        //private void HandleCurrentEvent(object s, CurrentEventArgs e);
    }
}