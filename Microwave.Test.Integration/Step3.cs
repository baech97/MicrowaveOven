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
    class Step3
    {
        private UserInterface _UI;
        private CookController _cookController;
        private Light _light;
        private Display _display;
        private PowerTube _powerTube;
        private Timer _timer;
        
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IOutput _output;
        //private IDoor _door;
        private Door _door;

        [SetUp]
        public void SetUp()
        {
            _output = NSubstitute.Substitute.For<IOutput>();
            //_door = NSubstitute.Substitute.For<IDoor>();
            _powerButton = NSubstitute.Substitute.For<IButton>();
            _timeButton = NSubstitute.Substitute.For<IButton>();
            _startCancelButton = NSubstitute.Substitute.For<IButton>();

            _door = new Door();
            

            _display = new Display(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _light = new Light(_output);
            _UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void DoorOpened__UserInterface_Light_on()
        {
            _door.Open();
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Light is turned on")));
        }



    }
}
