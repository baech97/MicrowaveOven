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
        private Door _door;

        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IOutput _output;

        [SetUp]
        public void SetUp()
        {
            _output = NSubstitute.Substitute.For<IOutput>();
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
            _UI.OnDoorOpened(this, EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Light is turned on")));
        }

        [Test]
        public void DoorClosed__UserInterface_Light_Off()
        {
            _UI.OnDoorOpened(this, EventArgs.Empty);
            _UI.OnDoorClosed(this, EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Light is turned off")));
        }

        [Test]
        public void StartPressed__UserInterface_Light__TurnedOn()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnTimePressed(this, EventArgs.Empty);
            _UI.OnStartCancelPressed(this, EventArgs.Empty);

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Light is turned on")));
        }

        [Test]
        public void Cooking_CancelPressed__UserInterface_Light__TurnedOff()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnTimePressed(this, EventArgs.Empty);
            _UI.OnStartCancelPressed(this, EventArgs.Empty);
            _UI.OnStartCancelPressed(this, EventArgs.Empty);

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Light is turned off")));

            // ifølge sekvensdiagrammet for extension 3, burde der ikke komme en logline når der trykkes cancel imens den tilbereder, men det gør der.

        }

        [Test]
        public void CookingIsDone__UserInterface_Light__TurnedOff()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty); 
            _UI.OnTimePressed(this, EventArgs.Empty);
            _UI.OnStartCancelPressed(this, EventArgs.Empty);
            
            _UI.CookingIsDone();

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Light is turned off")));

        }
    }
}
