using System;

namespace PolyclinicHelp
{
    public class Address
    {
        public string AdmArea { get; set; }

        public string District { get; set; }

        public string AddressString { get; set; }

        public double PointX { get; set; }

        public double PointY { get; set; }

        public Address(string admArea, string district, string addressString, double pointX, double pointY)
        {
            AdmArea = admArea;
            District = district;
            AddressString = addressString;
            PointX = pointX;
            PointY = pointY;
        }

        public override string ToString() => $"{PointX}, {PointY}, {AddressString} {AdmArea}";
    }
}
