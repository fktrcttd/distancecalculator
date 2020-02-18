using System;
using DistanceCalculator.Domain.Interfaces;

namespace DistanceCalculator.Domain.Core
{
    /// <summary>
    /// Запись о проведении расчета 
    /// </summary>
    public class CalculationEntry
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата проведения расчета
        /// </summary>
        public DateTime CreationDateTime { get; set; }

        /// <summary>
        /// Высота установки устройства (камеры видеонаблюдения), см
        /// </summary>
        public int Height { get; set; }
        
        /// <summary>
        /// Дистанция до объекта, см
        /// </summary>
        /// <returns></returns>
        public int DistanceToObject { get; set; }

        /// <summary>
        /// Искомый угол наклона Альфа между объектом и устройством  
        /// </summary>
        public double AlfaAngle { get; set; }
        
        /// <summary>
        /// Разница между высотой объекта и высотой установки устройства
        /// </summary>
        public int DistanceAboveObject { get; set; }
    }
}