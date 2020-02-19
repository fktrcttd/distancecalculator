using System;
using Newtonsoft.Json;

namespace DistanceCalculator.Web.Models.RequestDto.Calculation
{
    public class CalculationRequestDto
    {
        public int DistanceToObject { get; set; }
        
        public int DistanceToCamera { get; set; }
        
        public Boolean SaveToDatabase { get; set; }
    }
}