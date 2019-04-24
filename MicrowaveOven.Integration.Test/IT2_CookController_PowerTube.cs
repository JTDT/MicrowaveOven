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
        private ICookController _cookController;
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

            _cookController = new CookController(_fakeTimer, _fakeDisplay, _powerTube, _fakeUserInterface);
        }

        [TestCase(50, 10)]
        [TestCase(60, 20)]
        [TestCase(70, 30)]
        public void StartCookController_TurnOnCalled(int power, int timer)
        {
            //Arrange
            _cookController.StartCooking(power, timer);

            //Assert
            _fakeOutput.Received(1).OutputLine($"PowerTube works with {power} %");

        }

        [TestCase(2000, 10)]
        [TestCase(-1, 10)]
        [TestCase(0, 10)]
        public void CookControllerPowerTube_PowerTubeAboveLimit_ThrowsException(int power, int timer)
        {
           Assert.Throws<System.ArgumentOutOfRangeException>(() => _powerTube.TurnOn(power));
        }

    [TestCase]
    public void StopCookController_TurnOffCalled_NoOutput()
    {
        _cookController.Stop();

        _fakeOutput.DidNotReceive().OutputLine("PowerTube turned off");
        
    }
}
}
