
namespace charge_box.classes
{
    public interface IChargeControl
    {
        public bool IsConnected { get; }
        public void StartCharge();
        public void StopCharge();
    }
}