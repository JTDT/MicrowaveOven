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

        [TestCase]
        public void ()
        {

        }



    }
}
