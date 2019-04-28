using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class Step5
    {
        private UserInterface _UI;
        private CookController _cookController;
        //private Light _light;
        //private Display _display;
        private Timer _timer;
        private Button _startCancelButton;

        private ILight _light;
        private IDisplay _display;
        private IButton _powerButton;
        private IButton _timeButton;
        private IOutput _output;
        private IDoor _door;
        private IPowerTube _powerTube;

        [SetUp]
        public void SetUp()
        {
            _output = NSubstitute.Substitute.For<IOutput>();
            _powerButton = NSubstitute.Substitute.For<IButton>();
            _timeButton = NSubstitute.Substitute.For<IButton>();
            _powerTube = NSubstitute.Substitute.For<IPowerTube>();
            _door = NSubstitute.Substitute.For<IDoor>();
            _display = NSubstitute.Substitute.For<IDisplay>();
            _light = NSubstitute.Substitute.For<ILight>();

            //_display = new Display(_output);
            _timer = new Timer();
            //_light = new Light(_output);
            _startCancelButton = new Button();
            _cookController = new CookController(_timer, _display, _powerTube);
            _UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void StartCooking__UI_CookController__navn()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnTimePressed(this, EventArgs.Empty);
            _UI.OnStartCancelPressed(this, EventArgs.Empty);
            _powerTube.Received().TurnOn(50);
            //_output.Received().OutputLine(Arg.Is<string>(t => t.Contains("PowerTube works with 50 watt")));
        }

        [Test]
        public void CookingIsDone__UI_CookController__ClearDisplay() 
        {
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnTimePressed(this, EventArgs.Empty);
            _UI.OnStartCancelPressed(this, EventArgs.Empty);
            ManualResetEvent _pause = new ManualResetEvent(false);
            //_cookController.StartCooking(50,1);
            _pause.WaitOne(60100);
            //_cookController.OnTimerExpired(this, EventArgs.Empty);
            //_powerTube.Received().TurnOff();
            //_display.Clear();
            //_light.TurnOff();
            _display.Received().Clear();
            //_output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Display cleared")));
        }

        [Test]
        public void CookingIsDone__UI_CookController__TurnOffLight()
        {
            //_UI.OnPowerPressed(this, EventArgs.Empty);
            //_UI.OnTimePressed(this, EventArgs.Empty);
            //_UI.OnStartCancelPressed(this, EventArgs.Empty);
            ManualResetEvent _pause = new ManualResetEvent(false);
            _cookController.StartCooking(50,60);
            _pause.WaitOne(60100);
            //_cookController.OnTimerExpired(this, EventArgs.Empty);
            //_powerTube.Received().TurnOff();
            //_display.Clear();
            //_light.TurnOff();
            _light.Received().TurnOff();
            //_output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Light is turned off")));
        }

    }
}
