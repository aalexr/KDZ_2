using System;
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

        public Clinic()
        {
            ShortName =
                ChiefName =
                    ChiefPosition =
                        ChiefGender =
                            ChiefPhone =
                                PublicPhone =
                                    Fax =
                                        Email =
                                            CloseFlag =
                                                CloseReason =
                                                    CloseDate =
                                                        ReopenDate =
                                                            PaidServicesInfo =
                                                                FreeServicesInfo =
                                                                    WorkingHours =
                                                                        ClarificationOfWorkingHours =
                                                                            Specialization =
                                                                                BeneficialDrugPrescriptions =
                                                                                    ExtraInfo =
                                                                                        AddressUnom = string.Empty;
            Address = new Address();
        }

        public Clinic(IReadOnlyList<string> row)
        {
            if (row.Count == 29)
            {
                Rownum = int.Parse(row[0]);
                ShortName = row[1];
                PostalCode = int.Parse(row[4]);
                Address = new Address(row[2], row[3], row[5], double.Parse(row[25], DecimalStyle), double.Parse(row[26], DecimalStyle));
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
                AddressUnom = row[24];
                Globalid = int.Parse(row[27]);
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

        public object this[int index]
        {
            set
            {
                switch (index)
                {
                    case 0:
                        Rownum = (int)value;
                        break;
                    case 1:
                        ShortName = (string)value;
                        break;
                    case 2:
                        PostalCode = (int)value;
                        break;
                    case 3:
                        // Не дожно никогда запускаться
                        var t = ((Address)value).ToString().Split(',');
                        Address = new Address(t[0], t[1], t[2], double.Parse(t[3]), double.Parse(t[3]));
                        break;
                    case 4:
                        ChiefName = (string)value;
                        break;
                    case 5:
                        ChiefPosition = (string)value;
                        break;
                    case 6:
                        ChiefGender = (string)value;
                        break;
                    case 7:
                        ChiefPhone = (string)value;
                        break;
                    case 8:
                        PublicPhone = (string)value;
                        break;
                    case 9:
                        Fax = (string)value;
                        break;
                    case 10:
                        Email = (string)value;
                        break;
                    case 11:
                        CloseFlag = (string)value;
                        break;
                    case 12:
                        CloseReason = (string)value;
                        break;
                    case 13:
                        CloseDate = (string)value;
                        break;
                    case 14:
                        ReopenDate = (string)value;
                        break;
                    case 15:
                        PaidServicesInfo = (string)value;
                        break;
                    case 16:
                        FreeServicesInfo = (string)value;
                        break;
                    case 17:
                        WorkingHours = (string)value;
                        break;
                    case 18:
                        ClarificationOfWorkingHours = (string)value;
                        break;
                    case 19:
                        Specialization = (string)value;
                        break;
                    case 20:
                        BeneficialDrugPrescriptions = (string)value;
                        break;
                    case 21:
                        ExtraInfo = (string)value;
                        break;
                    case 22:
                        ShortName = (string)value;
                        break;
                    case 23:
                        Globalid = (int)value;
                        break;
                    case 24:
                        AddressUnom = (string) value;
                        break;
                    default:
                        // Не дожно никогда запускаться
                        break;
                }
            }
        }

        public override string ToString()
        {
            return $"{Rownum};\"{ShortName ?? string.Empty}\";\"{Address.AdmArea}\";\"{Address.District}\";{PostalCode};\"{Address.AddressString}\";\"{ChiefName}\";\"{ChiefPosition}\";\"{ChiefGender}\";\"{ChiefPhone}\";\"{PublicPhone}\";\"{Fax}\";\"{Email}\";\"{CloseFlag}\";\"{CloseReason}\";\"{CloseDate}\";\"{ReopenDate}\";\"{PaidServicesInfo}\";\"{FreeServicesInfo}\";\"{WorkingHours}\";\"{ClarificationOfWorkingHours}\";\"{Specialization}\";\"{BeneficialDrugPrescriptions}\";\"{ExtraInfo}\";\"{AddressUnom}\";{Address.PointX};{Address.PointY};{Globalid};";
        }
    }

}
