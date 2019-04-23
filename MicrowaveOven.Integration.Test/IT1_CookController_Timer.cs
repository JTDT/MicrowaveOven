using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace MicrowaveOven.Integration.Test
{
    [TestFixture]
    class IT1_CookController_Timer
    {
        private ITimer _timer;
        private IPowerTube _fakePowerTube;
        private IDisplay _fakeDisplay;
        private ICookController _uut;

        private IUserInterface _fakeUserInterface;
        //private IOutput _fakeOutput;


        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _fakePowerTube = Substitute.For<IPowerTube>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeUserInterface = Substitute.For<IUserInterface>();

            _uut = new CookController(_timer, _fakeDisplay, _fakePowerTube, _fakeUserInterface);
            
        }

        [TestCase(62, 1000, 1, 1)]
        [TestCase(4, 2000, 0, 2)]
        [TestCase(5, 3000, 0, 2)]
        public void TimerTickEvent_IsMethodCalledEverySecond_ShowTimeIsCalled(int timeSeconds, int sleepTimeMiliseconds, int showTimeMinute, int showtimeSeconds)
        {
            int power = 50;
            
            _uut.StartCooking(power,timeSeconds);

          
            Thread.Sleep(sleepTimeMiliseconds);

            _fakeDisplay.Received(1).ShowTime(showTimeMinute, showtimeSeconds);


        }

        [TestCase(17, 9000)]
        [TestCase(14, 2000)]
        [TestCase(19, 8000)]
        public void TimerExpired_IsMethodCalledAfter_ShowTimeIsCalled(int timeSeconds, int sleepTimeMiliseconds)
        {
            int power = 50;

            _uut.StartCooking(power, timeSeconds);

            //_timer.TimerTick += Raise.EventWith(new EventArgs());


            //_timer.Start(timeSeconds);
            Thread.Sleep(sleepTimeMiliseconds);

            _fakeUserInterface.DidNotReceive().CookingIsDone(); // OBS fejler lige nu


        }



    }
}
