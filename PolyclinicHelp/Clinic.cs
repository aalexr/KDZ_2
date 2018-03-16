using System.Collections.Generic;
using System.Globalization;
// ReSharper disable MemberCanBePrivate.Global

namespace PolyclinicHelp
{
    public class Clinic
    {
        /// <summary>
        /// Формат числа с плавающей точкой в файле
        /// </summary>
        private static readonly NumberFormatInfo DecimalStyle = new NumberFormatInfo { NumberDecimalSeparator = "." };

        public Clinic() { }

        public Clinic(IReadOnlyList<string> row)
        {
            if (row.Count == 29)
            {
                Rownum = int.Parse(row[(int) ClinicEnum.Rownum]);
                ShortName = row[(int) ClinicEnum.ShortName];
                PostalCode = int.Parse(row[(int) ClinicEnum.PostalCode]);
                Address = new Address(row[(int) ClinicEnum.AdmAream],
                    row[(int) ClinicEnum.District],
                    row[(int) ClinicEnum.Address],
                    double.Parse(row[(int) ClinicEnum.PointX],
                        DecimalStyle),
                    double.Parse(row[(int) ClinicEnum.PointY],
                        DecimalStyle));
                ChiefName = row[(int) ClinicEnum.ChiefName];
                ChiefPosition = row[(int) ClinicEnum.ChiefPosition];
                ChiefGender = row[(int) ClinicEnum.ChiefGender];
                ChiefPhone = row[(int) ClinicEnum.ChiefPhone];
                PublicPhone = row[(int) ClinicEnum.PublicPhone];
                Fax = row[(int) ClinicEnum.Fax];
                Email = row[(int) ClinicEnum.Email];
                CloseFlag = row[(int) ClinicEnum.CloseFlag];
                CloseReason = row[(int) ClinicEnum.CloseReason];
                CloseDate = row[(int) ClinicEnum.CloseDate];
                ReopenDate = row[(int) ClinicEnum.ReopenDate];
                PaidServicesInfo = row[(int) ClinicEnum.PaidServicesInfo];
                FreeServicesInfo = row[(int) ClinicEnum.FreeServicesInfo];
                WorkingHours = row[(int) ClinicEnum.WorkingHours];
                ClarificationOfWorkingHours = row[(int) ClinicEnum.ClarificationOfWorkingHours];
                Specialization = row[(int) ClinicEnum.Specialization];
                BeneficialDrugPrescriptions = row[(int) ClinicEnum.BeneficialDrugPrescriptions];
                ExtraInfo = row[(int) ClinicEnum.ExtraInfo];
                AddressUnom = row[(int) ClinicEnum.AddressUnom];
                Globalid = int.Parse(row[(int) ClinicEnum.GlobalId]);
            }
            else
            {
                throw new Microsoft.VisualBasic.FileIO.MalformedLineException();
            }
        }

        public int Rownum { get; set; }
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
        public string AddressUnom { get; set; }
        public int Globalid { get; set; }

        public override string ToString() =>
            $"{Rownum};\"{ShortName ?? string.Empty}\";\"{Address.AdmArea}\";\"{Address.District}\";{PostalCode};\"{Address.AddressString}\";\"{ChiefName}\";\"{ChiefPosition}\";\"{ChiefGender}\";\"{ChiefPhone}\";\"{PublicPhone}\";\"{Fax}\";\"{Email}\";\"{CloseFlag}\";\"{CloseReason}\";\"{CloseDate}\";\"{ReopenDate}\";\"{PaidServicesInfo}\";\"{FreeServicesInfo}\";\"{WorkingHours}\";\"{ClarificationOfWorkingHours}\";\"{Specialization}\";\"{BeneficialDrugPrescriptions}\";\"{ExtraInfo}\";\"{AddressUnom}\";{Address.PointX};{Address.PointY};{Globalid};";
    }

}
