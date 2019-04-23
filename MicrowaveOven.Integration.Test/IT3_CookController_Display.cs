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
using NSubstitute.Core.Arguments;
using Timer = MicrowaveOvenClasses.Boundary.Timer;


namespace MicrowaveOven.Integration.Test
{
    [TestFixture]
    class IT3_CookController_Display
    {
        private ICookController _uut;
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
            _uut = new CookController(_fakeTimer, _display, _powerTube);
        }

       // [TestCase(1000, 1,"Display: 01:00")]
       // [TestCase(8, 1, "Display: 00:08")]
        public void OnTimerTick_CookingInProgress_RemainingTimeIsDisplayed(int time, int eventReceived, string output)
        {
            int power = 50;
            _uut.StartCooking(power,time);
        
            _fakeOutput.Received(eventReceived).OutputLine(output);
        }


    }
}
