using System;

namespace DistanceCalculator.Domain.Core
{
    /// <summary>
    /// Геометрический угол
    /// </summary>
    public class Angle
    {
        public Angle(double degrees)
        {
            Degrees = degrees;
        }
        
        /// <summary>
        /// Значение угла в градусах
        /// </summary>
        public Double Degrees { get; set; }
    }
}