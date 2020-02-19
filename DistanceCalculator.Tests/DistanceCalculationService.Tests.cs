using System;
using System.Linq;
using DistanceCalculator.Infrastructure.Data;
using DistanceCalculator.Services.Core;
using DistanceCalculator.Services.Interfaces;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace DistanceCalculator.Tests
{
    [TestFixture]
    public class DistanceCalculationService_Tests
    {
        private IDistanceCalculationService _distanceCalculationService;
        
        [SetUp]
        public void Init()
        {
            var options =  new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var unitOfWork = new UnitOfWork(new DataContext(options));
            _distanceCalculationService = new DistanceCalculationService(unitOfWork);
        }
        
        
        [Test]
        public void CalculateCameraParameters_ThrowArgumentEx_When_DistanceToObject_IsNull()
        {
            Assert.Throws(typeof(ArgumentException), () =>
            {
                _distanceCalculationService.CalculateCameraParameters(new DistanceToDevice(350), null,
                    CalculationMode.CalculateOnly);
            });
        }
        
        [Test]
        public void CalculateCameraParameters_ThrowArgumentEx_When_DistanceToDevice_IsNull()
        {
            Assert.Throws(typeof(ArgumentException), () =>
            {
                _distanceCalculationService.CalculateCameraParameters(null, new DistanceToObject(700),
                    CalculationMode.CalculateOnly);
            });
        }
        
        [Test]
        public void CalculateCameraParameters_Success_When_Params_Is_Correct()
        {
            var res = _distanceCalculationService.CalculateCameraParameters(new DistanceToDevice(350), new DistanceToObject(700),
                CalculationMode.CalculateOnly);
            
            Assert.AreEqual(15.19,res.AlfaAngle);
        }
        
        [Test]
        public void CalculateCameraParameters_DontSaveToDB_When_Mode_Is_CalculateOnly()
        {
            var res = _distanceCalculationService.CalculateCameraParameters(new DistanceToDevice(350), new DistanceToObject(700),
                CalculationMode.CalculateOnly);
            
            Assert.That(!_distanceCalculationService.GetAllBeforeDate(DateTime.Now).Any());
        }
        
        [Test]
        public void CalculateCameraParameters_SaveToDB_When_Mode_Is_WithSave()
        {
            var res = _distanceCalculationService.CalculateCameraParameters(new DistanceToDevice(350), new DistanceToObject(700),
                CalculationMode.WithSave);
            
            Assert.That(_distanceCalculationService.GetAllBeforeDate(DateTime.Now).Any());
        }
    }
}