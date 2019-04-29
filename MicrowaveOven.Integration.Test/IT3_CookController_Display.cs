using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            _cookController = new CookController(_fakeTimer, _display, _powerTube, _fakeUserInterface);
        }

         [TestCase]
        public void OnTimerTick_ShowTime_LogLineCalled()
        {  
            //_fakeUserInterface.OnStartCancelPressed(null,null);
            _fakeTimer.TimerTick += Raise.Event();
           
            _fakeOutput.Received().OutputLine(Arg.Is<string>(s => s.Contains("Display shows")));
 
        }
    }
}
