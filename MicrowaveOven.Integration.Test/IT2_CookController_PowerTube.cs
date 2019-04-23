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
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace MicrowaveOven.Integration.Test
{
    [TestFixture]
    class IT2_CookController_PowerTube
    {
        private ICookController _uut;
        private IPowerTube _powerTube;

        private IUserInterface _fakeUserInterface;
        private ITimer _fakeTimer;
        private IDisplay _fakeDisplay;
        private IOutput _fakeOutput;

        [SetUp]
        public void SetUp()
        {
            _fakeOutput = Substitute.For<IOutput>();
            _powerTube = new PowerTube(_fakeOutput);
            _fakeUserInterface = Substitute.For<IUserInterface>();
            _fakeTimer = Substitute.For<ITimer>();
            _fakeDisplay = Substitute.For<IDisplay>();

            _uut = new CookController(_fakeTimer, _fakeDisplay, _powerTube, _fakeUserInterface);
        }

        [TestCase(50, 10)]
        [TestCase(60, 20)]
        [TestCase(70, 30)]
        public void StartCookController_TurnOnCalled(int power, int timer)
        {
            //Arrange
            _uut.StartCooking(power, timer);

            //Assert
            _fakeOutput.Received(1).OutputLine($"PowerTube works with {power} %");

        }

        [TestCase(110, 10)]
        public void CookControllerPowerTube_PowerTubeAboveLimit_ThrowsException(int power, int timer)
        {
            _uut.StartCooking(power, timer);

            //_fakeOutput.Received(1).OutputLine($"power {power} Must be between 1 and 100 % (incl.)");
            Assert.Throws<System.ArgumentOutOfRangeException>(() => _powerTube.TurnOn(power));
        }

    [TestCase]
    public void StopCookController_TurnOffCalled()
    {
        //_uut.StartCooking(50, 0);
        _powerTube.TurnOff();

        _fakeOutput.Received(1).OutputLine($"PowerTube turned off");
    }
}
}
