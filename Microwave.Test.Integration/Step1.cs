﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NUnit.Framework;
using NSubstitute;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

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

        [TestCase(100)]
        [TestCase(200)]
        [TestCase(550)]
        public void StartCooking__CookController_Timer_Powertube__CorrectPower(int s1)
        {
            _cookController.StartCooking(s1, 10);

            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains(Convert.ToString(s1))));
        }

        //[TestCase(49)]
        //[TestCase(701)]
        //public void Start_CookController_Timer_Powertube__OutOfRangeException(int s1)
        //{
        //    _cookController.StartCooking(s1, 10);

        //    Assert.That(_output.Received(),Is.Null);
        //}

        [Test]
        public void Stop__CookController_Timer_Powertube_Is_Stopped()
        { 
            _cookController.StartCooking(100,10);
            _cookController.Stop();
            
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains(Convert.ToString("PowerTube turned off"))));
        }

        [TestCase(500, 8, 15)]
        [TestCase(10, 0, 5)]
        [TestCase(200, 3, 15)]
        [TestCase(100, 1, 35)]
        public void StartCooking__CookController_Timer__Display_ShowsCorretTime(int s1, int s2, int s3)
        {
            ManualResetEvent _pause = new ManualResetEvent(false);

            _cookController.StartCooking(500, s1);
            _pause.WaitOne(5100);

            _display.Received(1).ShowTime(s2, s3);
        }


        [Test]
        public void OnTimerTick_CookController_Timer_Powertube__navn()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            _cookController.StartCooking(100,10);


            

        }


        [Test]
        public void OnTimerExpired_CookController_Timer_Powertube__navn()
        {
            _cookController.StartCooking(500, 0);
            _output.Received().OutputLine(Arg.Is<string>(t => t.Contains(Convert.ToString("PowerTube turned off"))));

        }

        
        


    }
}
