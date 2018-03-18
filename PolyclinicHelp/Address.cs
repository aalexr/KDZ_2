using System;

namespace PolyclinicHelp
{
    public class Address
    {
        /// <summary>
        /// Административный округ
        /// </summary>
        public string AdmArea { get; }

        /// <summary>
        /// Район
        /// </summary>
        public string District { get; }

        /// <summary>
        /// Адрес
        /// </summary>
        public string AddressString { get; }

        /// <summary>
        /// Координата Х
        /// </summary>
        public double PointX { get; }

        /// <summary>
        /// Координата Y
        /// </summary>
        public double PointY { get; }

        public Address()
        {
            AdmArea = District = AddressString = string.Empty;
        }

        public Address(string admArea, string district, string addressString, double pointX, double pointY)
        {
            AdmArea = admArea;
            District = district;
            AddressString = addressString;
            PointX = pointX;
            PointY = pointY;
        }

        /// <summary>
        /// Вычисление расстояния до места по координатам
        /// </summary>
        /// <param name="x">Координата x</param>
        /// <param name="y">Координата y</param>
        /// <returns></returns>
        public double Distance(double x, double y)
        {
            double dx = PointX - x, dy = PointY - y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString() => $"{AdmArea},{District},{AddressString},{PointX},{PointY}";
    }
}
