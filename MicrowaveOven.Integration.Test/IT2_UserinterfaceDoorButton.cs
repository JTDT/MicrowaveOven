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
    class IT2_UserinterfaceDoorButton
    {
        private IDoor _door;
        private IButton _Powerbutton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        private IDisplay _display;
        private ILight _light;
        private ICookController _cookController;

        private UserInterface _uut;

        [SetUp]
        public void SetUp()
        {
            _door = new Door();
            _Powerbutton = new Button();
            _timeButton = new Button();

            _display = Substitute.For<IDisplay>();
            _light = Substitute.For<ILight>();
            _cookController = Substitute.For<ICookController>();

            _uut = new UserInterface(_Powerbutton, _timeButton , _startCancelButton, _door, _display,_light, _cookController);
        }

        [Test]
        public void Userinterface_DoorIsOpen_LightIsOn()
        {
            _door.Opened += Raise.EventWith(this, EventArgs.Empty);
            _uut.OnDoorOpened(this,EventArgs.Empty);
            _light.Received().TurnOn();
            

            //_uut.OnDoorClosed(e); // den skal tage et event - hvordan gør man det?
            //Assert.That();
            //Assert.That(_evenOddBet.WonAmount(_winField), Is.EqualTo(2 * BetAmount));
        }

        [Test]
        public void Userinterface_DoorIsClosed_LightIsOff()
        {
            _door.Closed += Raise.EventWith(this, EventArgs.Empty);
            _uut.OnDoorOpened(this, EventArgs.Empty);
            _light.Received().TurnOff();


            //_uut.OnDoorClosed(e); // den skal tage et event - hvordan gør man det?
            //Assert.That();
            //Assert.That(_evenOddBet.WonAmount(_winField), Is.EqualTo(2 * BetAmount));
        }


    }
}
