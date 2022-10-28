using System;
using System.Text;
using charge_box.classes;
using NSubstitute;
using NSubstitute.ClearExtensions;
using NSubstitute.Extensions;
using NSubstitute.Routing.Handlers;

namespace charge_box.test;

[TestFixture]
public class TestDisplaySimulator
{
   private readonly string _testOutputString = new(@"./displayTest.txt");
   private IDisplay<string> _uut;
   private IConsoleSimulator _console;

   [SetUp]
   public void Setup()
   {
      _console = Substitute.ForPartsOf<ConsoleSimulator>();
      _uut = new DisplaySimulator(_console);
      
   }

   [TearDown]
   public void TearDown()
   {
   }

   [Test]
   public void TestingNotExistingDisplayArea()
   {
      
      Assert.Throws<KeyNotFoundException>(
         () => _uut.DisplayMessage("notAKey","message"));
   }

   [Test]
   public void TestingOutputs()
   {
      _console.When(x => x.SetCursorPosition(default, default)).DoNotCallBase();
      _console.When(x => x.GetCursorPosition()).DoNotCallBase();
      _console.When(x => x.Clear()).DoNotCallBase();
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
