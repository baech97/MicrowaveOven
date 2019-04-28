using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class Step1
    {
        private IUserInterface _UI;
        private CookController _cookController;
        private IDisplay _display;
        private PowerTube _powerTube;
        private Timer _timer;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _UI = NSubstitute.Substitute.For<IUserInterface>();
            _display = NSubstitute.Substitute.For<IDisplay>();
            _output = NSubstitute.Substitute.For<IOutput>();

            _timer = new Timer();
            _powerTube = new PowerTube(_output); 
            _cookController = new CookController(_timer, _display, _powerTube, _UI);
        }

        [TestCase(100)]
        [TestCase(200)]
        [TestCase(550)]
        public void StartCooking__CookController_Timer_Powertube__CorrectPower(int s1)
        {
            _cookController.StartCooking(s1, 10);

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains(Convert.ToString(s1))));
        }

        [Test]
        public void Stop__CookController_Timer_Powertube_Is_TurnedOff()
        {
            ManualResetEvent _pause = new ManualResetEvent(false);
            _cookController.StartCooking(100,5);
            _pause.WaitOne(5100);
            
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains(Convert.ToString("PowerTube turned off"))));
        }

        [Test]
        public void Stop__CookController_Timer_Powertube_IsNot_TurnedOff()
        {
            ManualResetEvent _pause = new ManualResetEvent(false);
            _cookController.StartCooking(100,5);
            _pause.WaitOne(3100);
            
            _output.DidNotReceive().OutputLine(Arg.Is<string>(t => t.Contains(Convert.ToString("PowerTube turned off"))));
        }

        [TestCase(500, 8, 14)]
        [TestCase(10, 0, 4)]
        [TestCase(200, 3, 14)]
        [TestCase(100, 1, 34)]
        public void StartCooking__CookController_Timer__Wait6Seconds__Display_ShowsCorrectTime(int s1, int s2, int s3)
        {
            ManualResetEvent _pause = new ManualResetEvent(false);

            _cookController.StartCooking(500, s1);
            _pause.WaitOne(6100);

            _display.Received().ShowTime(s2, s3);
        }


        [Test]
        public void OnTimerTick_CookController_Timer_Powertube__UI_CookingIsDone()
        {
            ManualResetEvent _pause = new ManualResetEvent(false);
            _cookController.StartCooking(100,2);

            _pause.WaitOne(2100);

            _UI.Received().CookingIsDone();
            
        }

        [Test]
        public void OnTimerTick_CookController_Timer_Powertube__UI_CookingIsNotDone()
        {
            ManualResetEvent _pause = new ManualResetEvent(false);
            _cookController.StartCooking(100,2);

            _pause.WaitOne(1100);

            _UI.DidNotReceive().CookingIsDone();
            
        }


        
        


    }
}
