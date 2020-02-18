using System;
using System.Collections.Generic;
using System.Linq;

namespace DistanceCalculator.Domain.Core
{
    /// <summary>
    /// Геометрический треугольник
    /// </summary>
    public class Triangle
    {
        public Triangle(IEnumerable<Angle> angles, IEnumerable<Side> sides)
        {
            if (sides.Count() != 3)
                throw new ArgumentException("Количество сторон у треугольника может быть равно только 3");

            if (angles.Count() != 3)
                throw new ArgumentException("Количество углов у треугольника может быть равно только 3");
            
            Angles = angles;
            Sides = sides;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException">При попытке установить больше чем 3 угла будет выброшено исключение</exception>
        public IEnumerable<Angle> Angles { get; }

        /// <summary>
        /// Стороны треугольника
        /// </summary>
        /// <exception cref="ArgumentException">При попытке установить больше чем 3 стороны будет выброшено исключение</exception>
        public IEnumerable<Side> Sides { get; }
    }
}