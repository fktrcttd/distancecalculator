using DistanceCalculator.Infrastructure.Data;
using DistanceCalculator.Services.Core;
using DistanceCalculator.Services.Interfaces;
using FakeItEasy;
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
            var repo = new CalculationEntryRepository(new DataContext());
            _distanceCalculationService = new DistanceCalculationService(repo);
        }
        
        [Test]
        public void One()
        {
            var res = _distanceCalculationService.CalculateCameraParameters(new DistanceToDevice(350), new DistanceToObject(700),
                CalculationMode.CalculateOnly);
            
            Assert.AreEqual(15.19,res.AlfaAngle);
        }
        
        [Test]
        public void Two()
        {
            var res = _distanceCalculationService.CalculateCameraParameters(new DistanceToDevice(350), new DistanceToObject(700),
                CalculationMode.WithSave);
            
            Assert.AreEqual(15.19,res.AlfaAngle);
        }
    }
}