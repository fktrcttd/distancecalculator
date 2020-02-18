using System;
using System.Collections.Generic;
using System.Linq;
using DistanceCalculator.Domain.Core;
using DistanceCalculator.Services.Core;
using DistanceCalculator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DistanceCalculator.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationController : ControllerBase
    {
        private IDistanceCalculationService _distanceCalculationService;

        public CalculationController(IDistanceCalculationService distanceCalculationService)
        {
            _distanceCalculationService = distanceCalculationService;
        }
        
        [HttpPost]
        public CalculationEntry Post(int a, int d, bool withSave = false)
        {
            var calculationMethod = withSave ? CalculationMode.WithSave : CalculationMode.CalculateOnly;
            return _distanceCalculationService.CalculateCameraParameters(new DistanceToDevice(d),
                new DistanceToObject(a), calculationMethod);
        }
    }
}