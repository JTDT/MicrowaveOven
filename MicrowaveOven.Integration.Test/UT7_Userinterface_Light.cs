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

namespace MicrowaveOven.Integration.Test
{
    [TestFixture]
    class UT7_Userinterface_Light
    {

        private IDoor _door;
        private IButton _timeButton;
        private IButton _starCancelButton;
        private IButton _powerButton;
        private ICookController _fakeCookController;
        private IDisplay _display;
        private IOutput _fakeOutput;
       
        private ILight _light;
        private IUserInterface _userInterface;
        

        [SetUp]
        public void SetUp()
        {
            _door = new Door();
            _timeButton = new Button();
            _powerButton = new Button();
            _starCancelButton = new Button();
            _fakeOutput = Substitute.For<IOutput>();
            _light = new Light(_fakeOutput);
            _display = new Display(_fakeOutput);
            _fakeCookController = Substitute.For<ICookController>();
            

            _userInterface = new UserInterface(_powerButton, _timeButton , _starCancelButton , _door, _display, _light, _fakeCookController);

            

        }

        [Test]
        public void OnDoorOpened_WhenDoorIsOpen_LightIsOn()
        {
            //act
            _door.Open();
            
            // assert
            _fakeOutput.Received(1).OutputLine("Light is turned on");


        }
        [Test]
        public void OnDoorClosed_WhenDoorIsClosed_LightIsOff()
        {
            // act
            _door.Open();
            _door.Close();

            // assert 
            _fakeOutput.Received(1).OutputLine("Light is turned off");
            


        }

        [TestCase]
        public void CookingIsDone_WhenCookingIsDone_LightIsOff()
        {
            // act
            _powerButton.Press();
            _timeButton.Press();
            _starCancelButton.Press();
            _userInterface.CookingIsDone();

            // Assert
            _fakeOutput.Received(1).OutputLine("Light is turned off");

            

        }

        [TestCase]
        public void OnStartCancelPressed_WhenSCBIsPressed_LightIsOn()
        {
            // act
            _powerButton.Press();
            _timeButton.Press();
            _starCancelButton.Press();

            // Assert
            _fakeOutput.Received(1).OutputLine("Light is turned on"); 

        }


        [TestCase]
        public void OnStartCancelPressed_WhenSCBIsPressedx2_LightIsOff()
        {
            // act
            _powerButton.Press();
            _timeButton.Press();
            _starCancelButton.Press();
            _starCancelButton.Press();

            // Assert
            _fakeOutput.Received(1).OutputLine("Light is turned off");

        }





    }
}
