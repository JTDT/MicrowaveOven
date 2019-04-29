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
        private ICookController _cookController;

        private IUserInterface _fakeUserInterface;
      


        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _fakePowerTube = Substitute.For<IPowerTube>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeUserInterface = Substitute.For<IUserInterface>();

            _cookController = new CookController(_timer, _fakeDisplay, _fakePowerTube, _fakeUserInterface);
            
        }

        [TestCase(20, 1000, 0, 19)]
        [TestCase(4, 2000, 0, 2)]// disse to virker ikke
        [TestCase(5, 2000, 0, 3)]
        public void TimerTickEvent_IsMethodCalledEverySecond_ShowTimeIsCalled(int timeSeconds, int sleepTimeMiliseconds, int showTimeMinute, int showtimeSeconds)
        {
            int power = 50;
            
            _cookController.StartCooking(power,timeSeconds);

          
            Thread.Sleep(sleepTimeMiliseconds+100);

            _fakeDisplay.Received().ShowTime(showTimeMinute, showtimeSeconds);


        }

        [TestCase(4, 2000)]
        [TestCase(2, 1000)]
        [TestCase(9, 8000)]
        public void TimerExpired_IsMethodCalledAfter_ShowTimeIsCalled(int timeSeconds, int sleepTimeMiliseconds)
        {
            int power = 50;

            _cookController.StartCooking(power, timeSeconds);

           
            Thread.Sleep(sleepTimeMiliseconds);

            _fakeUserInterface.DidNotReceive().CookingIsDone(); 


        }



    }
}
