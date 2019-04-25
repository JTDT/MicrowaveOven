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

namespace MicrowaveOven.Integration.Test
{
    [TestFixture]
    class IT8_Userinterface_CookController
    {
        private IDoor _door;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IButton _powerButton;
        private ITimer _fakeTimer;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private IOutput _fakeOutput;
        private ILight _light;

        private ICookController _cookController;
        private IUserInterface _userInterface;


        [SetUp]
        public void SetUp()
        {
            _door = new Door();
            _timeButton = new Button();
            _powerButton = new Button();
            _startCancelButton = new Button();
            _fakeTimer = Substitute.For<ITimer>();
            _fakeOutput = Substitute.For<IOutput>();
            _display = new Display(_fakeOutput);
            _powerTube = new PowerTube(_fakeOutput);
            _light = new Light(_fakeOutput );

            _cookController = new CookController(_fakeTimer, _display, _powerTube, _userInterface);
            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _cookController);



        }

        [TestCase]
        public void OnStartCancelPressed_WhenStartCooing_StartCookingIsCalled()
        {
            //act
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            

            //assert
            _fakeOutput.Received().OutputLine(Arg.Is<string>(s => s.Contains("50"))); 

            
        }

        [TestCase]
        public void OnDoorOpened_WhenDoorOpens_StopIsCalled()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _door.Open();
        

            // assert
            _fakeTimer.Received(1).Stop();
            
        }

    }
}
