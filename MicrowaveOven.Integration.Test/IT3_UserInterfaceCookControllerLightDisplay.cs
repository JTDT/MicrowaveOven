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
    class IT3_UserInterfaceCookControllerLightDisplay
    {
        private IButton _startCancelButton;
        private IButton _timeButton;
        private IButton _powerButton;
        private ITimer _timer;
        private IPowerTube _powerTube;
        private IOutput _output;
        private IDoor _door;

        private UserInterface _uut; 
        private ICookController _cookController;
        private ILight _light;
        private IDisplay _display;

        [SetUp]
        public void SetUp()
        {
            _startCancelButton = new Button();
            _timeButton = new Button();
            _powerButton = new Button();
            _timer = new Timer();
            _output =new Output();
            _powerTube = new PowerTube(_output);
            _door = new Door();

            _light = Substitute.For<ILight>();
            _display = new Display(_output);
            _cookController = new CookController(_timer, _display, _powerTube);
            _uut = new UserInterface(_powerButton, _timeButton,_startCancelButton, _door, _display, _light,_cookController);
          
        }

        //I tvivl om hvad det er der skal/kan testes her 


    }
}
