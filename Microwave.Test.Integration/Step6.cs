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
    [TestFixture]
    public class Step6
    {
        private UserInterface _UI;
        private CookController _cookController;
        private Display _display;
        private PowerTube _powerTube;
        private Light _ligth;
        private Timer _timer;
        private Button _powerButton;
        private Button _timeButton;
        private Button _startCancelButton;

        private IOutput _output;
        private IDoor _door;

        [SetUp]
        public void SetUp()
        {
            _door = NSubstitute.Substitute.For<IDoor>();
            _output = NSubstitute.Substitute.For<IOutput>();

            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _ligth = new Light(_output);
            _timer = new Timer();
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _cookController = new CookController(_timer, _display, _powerTube);
            _UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _ligth, _cookController);
        }

        [Test]
        public void powerButton__UserInterface_Display_Output()
        {
            _powerButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Display shows: 50 W")));
        }

        [Test]
        public void startCancelButton__UserInterface_Light_Turned_on_Output()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Light is turned on")));
        }
        [Test]
        public void startCancelButton__UserInterface_Display_Output_Time_is_1Min()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("01:00")));
        }
        [Test]
        public void startCancelButton__UserInterface_Display_Output_Power_is_50()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("50")));
        }
    }
}
