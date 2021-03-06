﻿using System;
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
    class Step4
    {
        private IOutput _output;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;

        private Light _light;
        private Display _display;
        private PowerTube _powerTube;
        private Timer _timer;
        private CookController _cookController;
        private UserInterface _UI;

        [SetUp]
        public void SetUp()
        {
            _output = NSubstitute.Substitute.For<IOutput>();
            _powerButton = NSubstitute.Substitute.For<IButton>();
            _timeButton = NSubstitute.Substitute.For<IButton>();
            _startCancelButton = NSubstitute.Substitute.For<IButton>();
            _door = NSubstitute.Substitute.For<IDoor>();

            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = new Timer();
            _cookController = new CookController(_timer, _display, _powerTube);
            _UI = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);
        }

        [Test]
        public void ShowPower__UserInterface_Display_DisplayShows50()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Display shows: 50 W")));
        }

        [Test]
        public void ShowPower__UserInterface_Display_DisplayShows100()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Display shows: 100 W")));
        }

        [Test]
        public void ShowPower__UserInterface_Display_DisplayShows700()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnPowerPressed(this, EventArgs.Empty);

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Display shows: 700 W")));
        }

        [Test]
        public void ShowTime__UserInterface_Display_DisplayShows0100()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty); //power skal indstilles inden man kan indstille tiden
            _UI.OnTimePressed(this, EventArgs.Empty);

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Display shows: 01:00")));
        }

        [Test]
        public void ShowTime__UserInterface_Display_DisplayShows0200()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty); 
            _UI.OnTimePressed(this, EventArgs.Empty);
            _UI.OnTimePressed(this, EventArgs.Empty);

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Display shows: 02:00")));
        }

        [Test]
        public void CookingIsDone__UserInterface_Display__DisplayCleared()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty); 
            _UI.OnTimePressed(this, EventArgs.Empty);
            _UI.OnStartCancelPressed(this, EventArgs.Empty);
            
            _UI.CookingIsDone();

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Display cleared")));
        }

        [Test]
        public void Cooking_CancelPressed__UserInterface_Display__DisplayCleared()
        {
            _UI.OnPowerPressed(this, EventArgs.Empty);
            _UI.OnTimePressed(this, EventArgs.Empty);
            _UI.OnStartCancelPressed(this, EventArgs.Empty);
            _UI.OnStartCancelPressed(this, EventArgs.Empty);

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains("Display cleared")));
        }

    }
}
