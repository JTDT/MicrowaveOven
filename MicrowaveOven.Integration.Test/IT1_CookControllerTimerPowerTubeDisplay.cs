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
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace MicrowaveOven.Integration.Test
{
    [TestFixture]
    public class IT1_CookControllerTimerPowerTubeDisplay
    {
        private ITimer _fakeTimer;
        private IPowerTube _fakePowerTube;
        private IDisplay _display;
        private ICookController _cookController;

        private IUserInterface _fakeUserInterface;
        private IOutput _fakeOutput;


        [SetUp]
        public void SetUp()
        {
            _fakeTimer = Substitute.For<ITimer>();
            _fakePowerTube = Substitute.For<IPowerTube>();
            //_display = new Display(_fakeOutput);
            _display = Substitute.For<IDisplay>();
            _fakeUserInterface = Substitute.For<IUserInterface>();

            _cookController = new CookController(_fakeTimer, _display, _fakePowerTube, _fakeUserInterface);

        }

        [TestCase(40, 50)]
        [TestCase(50, 60)]
        [TestCase(60, 70)]
        public void StartCookController_MethodsCalled(int power, int timer)
        {
            //Arrange
            _cookController.StartCooking(power,timer);

            //Assert
            _fakePowerTube.Received().TurnOn(power);
            _fakeTimer.Received().Start(timer);

        }

        [TestCase]
        public void StopCookController_MethodsCalled()
        {
            _cookController.Stop();

            _fakePowerTube.Received().TurnOff();
            _fakeTimer.Received().Stop();
        }

        //[TestCase(50, 10)]
        //public void OnTimerEvent_TimeRemaining_DisplayCalled(int power, int timer)
        //{
        //    _cookController.StartCooking(power,timer);

        //    _display.Received(1).ShowTime(Arg.Any<int>(),Arg.Any<int>());
        //}



    }
}
