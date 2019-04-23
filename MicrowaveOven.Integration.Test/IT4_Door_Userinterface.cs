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
    class IT4_Door_Userinterface
    {
        private IUserInterface _userInterface;
        private IButton _fakePowerButton;
        private IButton _fakeTimeButton;
        private IButton _fakeStartCancelButton;
        private IDisplay _fakeDisplay;
        private ILight _fakeLight;
        private ICookController _fakeCookController;

        //private IOutput _fakeOutput;

        private IDoor _uut;

        [SetUp]
        public void SetUp()
        {
            _fakePowerButton = Substitute.For<IButton>();
            _fakeTimeButton = Substitute.For<IButton>();
            _fakeStartCancelButton = Substitute.For<IButton>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeLight = Substitute.For<ILight>();
            _fakeCookController = Substitute.For<ICookController>();

            //_fakeOutput = Substitute.For<IOutput>();


            _userInterface = new UserInterface(_fakePowerButton, _fakeTimeButton, _fakeStartCancelButton, _uut, _fakeDisplay, _fakeLight, _fakeCookController);
           
            _uut = new Door();

        }

        [Test]
        public void Open_WhenDoorOpens_OutputLightTurnsOn()
        {
            _uut.Open();
            _fakeLight.Received().TurnOn(); // virker ikke ?

            //_fakeOutput.Received().OutputLine("Light is turned on");
        }

        [Test]
        public void Close_WhenDoorCloses_OutputLightTurnedOff()
        {

            // act -> døren skal først åbnes før den kan lukkes ???
            //_uut.Open();
            
            //Act and assert
            _uut.Close();
            _fakeLight.Received().TurnOff();

        }


    }
}
