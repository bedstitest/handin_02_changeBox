using charge_box.classes;

namespace charge_box.test;

[TestFixture]
public class TestDisplaySimulatr
{
   
   private IDisplay<string> _uut;

   [SetUp]
   public void Setup()
   {
      _uut = new displaySimulator();
   }
   




}