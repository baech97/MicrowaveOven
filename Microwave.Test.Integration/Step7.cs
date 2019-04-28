using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class Step7
    {
        private Door _door;
        private UserInterface _UI;
        private CookController _cookController;
        private Light _light;
        private Display _display;
        private PowerTube _powerTube;
        private Timer _timer;
        private IOutput _output;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        [SetUp]
        public void SetUp()
        {
            _output = NSubstitute.Substitute.For<IOutput>();
            _powerButton = NSubstitute.Substitute.For<IButton>();
            _timeButton = NSubstitute.Substitute.For<IButton>();
            _startCancelButton = NSubstitute.Substitute.For<IButton>();

            _door = new Door();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
            _UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }
        [Test]
        public void DoorOpened__Door_UserInterface_Light_on()
        {
            _door.Open();
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Light is turned on")));
        }

        [Test]
        public void DoorClosed__Door_UserInterface_Light_off()
        {
            _door.Open();
            _door.Close();
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Light is turned off")));
        }
    }
}
