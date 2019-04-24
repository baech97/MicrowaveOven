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

        [Test]
        public void Start__CookController_Timer_Powertube__50procent()
        {
            _cookController.StartCooking(50, 10);

            _output.Received().OutputLine("50");

        }
    }
}
