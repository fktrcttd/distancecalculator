namespace DistanceCalculator.Domain.Core
{
    /// <summary>
    /// Сторона треугольника
    /// </summary>
    public class Side
    {
        public Side(int length, TriangleSideTypes type)
        {
            Length = length;
            Type = type;
        }
        
        /// <summary>
        /// Длина в сантиметрах
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Тип стороны треугольника
        /// </summary>
        public TriangleSideTypes Type { get; set; }
    }
}