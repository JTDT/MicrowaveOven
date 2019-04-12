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
    class IT1_CookControllerTimerPowerTubeDisplay
    {
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IDisplay _display;
        private ICookController _uut;

        private IUserInterface _fakeUserInterface;
        private IOutput _fakeOutput;


        [SetUp]
        public void SetUp()
        {
            _timer = new Timer();
            _powerTube = new PowerTube(_fakeOutput);
            _display = new Display(_fakeOutput);
            _uut = new CookController(_timer, _display, _powerTube);

            _fakeUserInterface = Substitute.For<IUserInterface>();
            _fakeOutput = Substitute.For<IOutput>();
        }

        [TestCase]
        public void StartCookController()
        {
            int time = 5;
            int powerTube = 120;
            _uut.StartCooking(120, 5);

            Assert.That() => ;

        }


    }
}
