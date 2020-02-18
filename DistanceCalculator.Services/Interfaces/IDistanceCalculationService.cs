using System;
using System.Collections;
using System.Collections.Generic;
using DistanceCalculator.Domain.Core;
using DistanceCalculator.Services.Core;

namespace DistanceCalculator.Services.Interfaces
{
    public interface IDistanceCalculationService
    {
        CalculationEntry CalculateCameraParameters(DistanceToDevice distanceToDevice,
            DistanceToObject distanceToObject, CalculationMode calculationMode);

        IEnumerable<CalculationEntry> GetAllBeforeDate(DateTime dateTime);
    }
}