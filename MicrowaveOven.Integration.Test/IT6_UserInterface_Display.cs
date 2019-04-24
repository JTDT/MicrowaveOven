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
using NUnit.Framework.Internal;

namespace MicrowaveOven.Integration.Test
{
    [TestFixture()]
    class IT6_UserInterface_Display
    {
        private IDoor _door;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IButton _powerButton;
        private ICookController _fakeCookController;
        private IUserInterface _userInterface;
        private ILight _fakeLight;
        private IDisplay _display;
        private IOutput _fakeOutput;

        [SetUp]
        public void SetUp()
        {
            _door = new Door();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _powerButton = new Button();
            _fakeCookController = Substitute.For<ICookController>();
            _fakeLight = Substitute.For<ILight>();
            _fakeOutput = Substitute.For<IOutput>();
            _display = new Display(_fakeOutput);
            _userInterface = new UserInterface(_powerButton,_timeButton,_startCancelButton,_door,_display,_fakeLight, _fakeCookController);
            
        }

        [TestCase(00, 01)]
        public void PressTimeButton_DisplayShowsPower(int minute, int sec)
        {
            // Act
            _timeButton.Press();
            string expectedOutput = minute + ":" + sec;
            
            //Assert
            _fakeOutput.Received(1).OutputLine(Arg.Is<string>(s => s.Contains(expectedOutput)));
            // virker ikke, da der er en fejl i timer

        }

        [TestCase(50)]
        public void PressPowerButton_ShowPower(int power)
        {
            _powerButton.Press();
            string exspectedOutout = "" + power;
            _fakeOutput.Received(1).OutputLine(Arg.Is<string>(s=> s.Contains(exspectedOutout)));

        }

        [TestCase]
        public void DisplayIsCleared()
        {
            _userInterface.CookingIsDone();
            string exspectedOutout = "cleared";

            _fakeOutput.Received(1).OutputLine(Arg.Is<string>(s=> s.Contains(exspectedOutout)));
        }

    

    }
}
