﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using NUnit.Framework;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MicrowaveOven.Integration.Test
{
    [TestFixture]
    class IT5_Button_UserInterface
    {
        private IButton _timeButton;
        private IButton _powerButton;
        private IButton _startCancelButton;
        private IUserInterface _userInterface;
        private IDoor _fakeDoor;
        private IDisplay _fakeDisplay;
        private ILight _fakeLight;
        private IOutput _fakeOutput;
        private ICookController _fakeCookController;


        [SetUp]
        public void SetUp()
        {
            _timeButton = new Button();
            _powerButton = new Button();
            _startCancelButton = new Button();
            _fakeDoor = Substitute.For<IDoor>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeLight = Substitute.For<ILight>();
            _fakeOutput = Substitute.For<IOutput>(); 
            _fakeCookController = Substitute.For<ICookController>();
            _userInterface = new UserInterface(_powerButton,_timeButton,_startCancelButton,_fakeDoor,_fakeDisplay,_fakeLight,_fakeCookController);
        }

        [Test] //kan den laves til en testcase?
        public void OnPowerEvent_IsMethodCalled_ShowPowerIsCalled()
        {
            int power = 50;
            _powerButton.Press(); 
            _fakeDisplay.Received(1).ShowPower(power);
        }

       // [TestCase(1,1)]
        public void OnTimeEvent_IsMethodCalled_ShowTimeIsCalled(int sec, int min)
        {
            _timeButton.Press();
            _userInterface.OnPowerPressed();
            _fakeDisplay.Received(1).ShowTime(1,1);
        }

        [TestCase()]
        public void StartCancelEvent_IsMethodCalled_TurnOnIsCalled()
        {
            _startCancelButton.Press();
            _fakeLight.Received(1).TurnOn();
        }

        //Se IT6

    }
}
