using System.Globalization;

namespace PolyclinicHelp
{
    public class Polyclinic
    {
        static NumberFormatInfo decimalStyle = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public int ROWNUM { get; set; }
        public string ShortName { get; set; }
        public int PostalCode { get; set; }
        public Address Address { get; set; }
        public string ChiefName { get; set; }
        public string ChiefPosition { get; set; }
        public string ChiefGender { get; set; }
        public string ChiefPhone { get; set; }
        public string PublicPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string CloseFlag { get; set; }
        public string CloseReason { get; set; }
        public string CloseDate { get; set; }
        public string ReopenDate { get; set; }
        public string PaidServicesInfo { get; set; }
        public string FreeServicesInfo { get; set; }
        public string WorkingHours { get; set; }
        public string ClarificationOfWorkingHours { get; set; }
        public string Specialization { get; set; }
        public string BeneficialDrugPrescriptions { get; set; }
        public string ExtraInfo { get; set; }
        public string AddressUNOM { get; set; }
        public int GLOBALID { get; set; }

        public Polyclinic(string[] row)
        {
            ROWNUM = int.Parse(row[0]);
            ShortName = row[1];
            PostalCode = int.Parse(row[4]);
            Address = new Address(row[2], row[3], row[5], double.Parse(row[25], decimalStyle), double.Parse(row[26], decimalStyle));
            ChiefName = row[6];
            ChiefPosition = row[7];
            ChiefGender = row[8];
            ChiefPhone = row[9];
            PublicPhone = row[10];
            Fax = row[11];
            Email = row[12];
            CloseFlag = row[13];
            CloseReason = row[14];
            CloseDate = row[15];
            ReopenDate = row[16];
            PaidServicesInfo = row[17];
            FreeServicesInfo = row[18];
            WorkingHours = row[19];
            ClarificationOfWorkingHours = row[20];
            Specialization = row[21];
            BeneficialDrugPrescriptions = row[22];
            ExtraInfo = row[23];
            AddressUNOM = row[24];
            GLOBALID = int.Parse(row[27]);
        }
    }

}
