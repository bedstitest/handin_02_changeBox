using System;
using System.Text;
using charge_box.classes;

namespace charge_box.test;

[TestFixture]
public class TestDisplaySimulator
{
   private readonly string _testOutputString = new(@"./displayTest.txt");
   private IDisplay<string> _uut;

   [SetUp]
   public void Setup()
   {
      _uut = new DisplaySimulator();
      if (!File.Exists(_testOutputString))
      {
         File.Create(_testOutputString);
      }
   }

   [TearDown]
   public void TearDown()
   {
       File.Delete(_testOutputString);
   }

   [Test]
   public void TestingNotExistingDisplayArea()
   {
      _uut = new DisplaySimulator();
      
      Assert.Throws<KeyNotFoundException>(
         () => _uut.DisplayMessage("notAKey","message"));

    }

    [Test]
   public void TestingOutputs()
   {

      var sw = new StringWriter();
      Console.SetOut(sw);
      _uut.DisplayMessage("systemInfo", "test");
      _uut.DisplayMessage("menu","test");
      _uut.DisplayMessage("user", "test");
      _uut.DisplayMessage("status", "test");
      
      
      var correct = "";
      for (int i = 0; i < 4; i++)
      {
         correct += "test".PadRight(Console.BufferWidth - 2) + "\n";
      }

      var output = sw.ToString();
      Assert.That(output, Is.EqualTo(correct));

   }

}
