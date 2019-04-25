using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;

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
        public void Start__CookController_Timer_Powertube__Power(int s1)
        {
            _cookController.StartCooking(s1, 10);

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains(Convert.ToString(s1))));

        }

        [Test]
        public void Stop_CookController_Timer_Powertube_Is_Stopped()
        { 
            _cookController.StartCooking(100,10);
            _cookController.Stop();
            
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains(Convert.ToString("PowerTube turned off"))));
        }

        [Test]
        public void OnTimerTick_CookController_Timer_Powertube__Power()
        {

        }

        [Test]
        public void OnTimerExpired_CookController_Timer_Powertube__Power()
        {

        }

        
    }
}
