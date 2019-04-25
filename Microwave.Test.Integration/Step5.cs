using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class Step5
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
        private IDoor _door;

        [SetUp]
        public void SetUp()
        {
            _output = NSubstitute.Substitute.For<IOutput>();
            _door = NSubstitute.Substitute.For<IDoor>();
            _powerButton = NSubstitute.Substitute.For<IButton>();
            _powerButton = NSubstitute.Substitute.For<IButton>();
            _timeButton = NSubstitute.Substitute.For<IButton>();
            _startCancelButton = NSubstitute.Substitute.For<IButton>();


            _cookController = new CookController(_timer, _display, _powerTube);
            _display = new Display(_output);
            _timer = new Timer();
            _powerTube = new PowerTube(_output);
            _UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _light = new Light(_output);
        }
    }
}
