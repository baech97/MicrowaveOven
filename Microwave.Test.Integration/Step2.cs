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
    class Step2
    {
        private IUserInterface _UI;
        private CookController _cookController;
        private Display _display;
        private PowerTube _powerTube;
        private Timer _timer;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _UI = NSubstitute.Substitute.For<IUserInterface>();
            _output = NSubstitute.Substitute.For<IOutput>();

            _display = new Display(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube, _UI);
        }

        [TestCase(0,"00:00")]
        [TestCase(10000, "00:10")]
        [TestCase(100000, "01:40")]
        [TestCase(200000, "03:20")]
        [TestCase(500000, "08:20")]
        public void ShowTime__CookController_Display__OnTimerTick(int s1, string s2)
        {
            _cookController.StartCooking(100, s1);
            _cookController.OnTimerTick(this, EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains(Convert.ToString("Display shows: " + s2))));
        }
    }
}
