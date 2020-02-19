using System;
using System.Collections.Generic;
using System.Linq;
using DistanceCalculator.Domain.Core;
using DistanceCalculator.Services.Core;
using DistanceCalculator.Services.Interfaces;
using DistanceCalculator.Web.Models.RequestDto.Calculation;
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
        public CalculationEntry Post([FromBody] CalculationRequestDto dto)
        {
            var calculationMethod = dto.SaveToDatabase ? CalculationMode.WithSave : CalculationMode.CalculateOnly;
            return _distanceCalculationService.CalculateCameraParameters(new DistanceToDevice(dto.DistanceToCamera),
                new DistanceToObject(dto.DistanceToObject), calculationMethod);
        }
        
        [HttpGet]
        [Route("all")]
        public IEnumerable<CalculationEntry> Get()
        {
            //TODO: Do actual date filter from query
            return _distanceCalculationService.GetAllBeforeDate(DateTime.Now);
        }
    }
}