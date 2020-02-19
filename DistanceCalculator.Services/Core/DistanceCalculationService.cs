using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DistanceCalculator.Domain.Core;
using DistanceCalculator.Domain.Interfaces;
using DistanceCalculator.Infrastructure.Data;
using DistanceCalculator.Services.Interfaces;

namespace DistanceCalculator.Services.Core
{
    public class DistanceCalculationService: IDistanceCalculationService
    {
        private ICalculationEntryRepository _repository;

        public DistanceCalculationService(UnitOfWork unitOfWork)
        {
            _repository = unitOfWork.CalculationEntryRepository;
        }
        
        public CalculationEntry CalculateCameraParameters(DistanceToDevice distanceToDevice, DistanceToObject distanceToObject, CalculationMode calculation)
        {
            if (distanceToDevice is null )
            {
                throw new ArgumentException(nameof(distanceToDevice));
            }

            if (distanceToObject is null)
            {
                throw new ArgumentException(nameof(distanceToObject));    
            }
            var aCathet = new Side(distanceToObject.Val, TriangleSideTypes.Cathet);
            var bCathet = new Side(distanceToDevice.Val - Constant.AverageHumanHeight, TriangleSideTypes.Cathet);
            var hypotenuse = new Side(GetHypotenusesLength(aCathet.Length, bCathet.Length), TriangleSideTypes.Hypotenuse);

            var tangentAlpha = (double)bCathet.Length / (double)aCathet.Length;
            var alphaDegrees = Math.Round(Math.Atan(tangentAlpha) * Constant.DegreesInRadian, 2);
            var alphaAngle = new Angle(alphaDegrees);
            var bAngle = new Angle(Constant.RightAngleDegrees);
            var cAngle = new Angle(Constant.FullTriangleDegrees - bAngle.Degrees - alphaAngle.Degrees);
            
            var calculationEntry = new CalculationEntry()
            {
                Height = distanceToDevice.Val,
                AlfaAngle = alphaAngle.Degrees,
                CreationDateTime = DateTime.Now,
                DistanceAboveObject = bCathet.Length,
                DistanceToObject = distanceToObject.Val
            };

            if (calculation == CalculationMode.WithSave) 
                Save(calculationEntry);

            return calculationEntry;
        }

        public IEnumerable<CalculationEntry> GetAllBeforeDate(DateTime dateTime)
        {
            return _repository.GetList().Where(ce => ce.CreationDateTime < dateTime);
        }

        private int GetHypotenusesLength(int aSide, int bSide)
        {
            return Convert.ToInt32(Math.Sqrt(aSide * aSide + bSide * bSide));
        }

        private void Save(CalculationEntry entry)
        {
            this._repository.Create(entry);
            this._repository.Save();
        }
    }
}