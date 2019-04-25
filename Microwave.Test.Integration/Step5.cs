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
        private ICookController _cookController;
        private Light _light;
        private IDisplay _display;
        private IOutput _output;
        private IDoor _door;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        [SetUp]
        public void SetUp()
        {
            _output = NSubstitute.Substitute.For<IOutput>();
            _cookController = NSubstitute.Substitute.For<ICookController>();
            _display = NSubstitute.Substitute.For<IDisplay>();
            _door = NSubstitute.Substitute.For<IDoor>();
            _powerButton = NSubstitute.Substitute.For<IButton>();
            _powerButton = NSubstitute.Substitute.For<IButton>();
            _timeButton = NSubstitute.Substitute.For<IButton>();
            _startCancelButton = NSubstitute.Substitute.For<IButton>();


            _UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
            _light = new Light(_output);
        }
    }
}
