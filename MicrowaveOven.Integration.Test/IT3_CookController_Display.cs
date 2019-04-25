using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using NUnit.Framework;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;


namespace MicrowaveOven.Integration.Test
{
    [TestFixture]
    class IT3_CookController_Display
    {
        private ICookController _cookController;
        private IDisplay _display;
        private IOutput _fakeOutput;
        private ITimer _fakeTimer;
        private IPowerTube _powerTube;
        private IUserInterface _fakeUserInterface;


        [SetUp]
        public void SetUp()
        {
            _fakeOutput = Substitute.For<IOutput>();
            _fakeTimer = Substitute.For<ITimer>();
            _fakeUserInterface = Substitute.For<IUserInterface>();
            _display = new Display(_fakeOutput);
            _powerTube = new PowerTube(_fakeOutput);
            _cookController = new CookController(_fakeTimer, _display, _powerTube);
        }

         [TestCase(50, 1, 0, 1)]
         [TestCase(50, 2, 0, 2)]
        public void OnTimerTick_ShowTime_LogLineCalled(int power, int timer, int min, int sec)
        {            
            _cookController.StartCooking(power, timer);
            _display.ShowTime(min, sec);

            _fakeOutput.Received(1).OutputLine($"Display shows: {min}:{sec}"); 
        }
    }
}
