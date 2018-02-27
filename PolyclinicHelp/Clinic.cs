using System.Collections.Generic;
using System.Globalization;

namespace PolyclinicHelp
{
    public class Clinic
    {
        private static readonly NumberFormatInfo DecimalStyle = new NumberFormatInfo { NumberDecimalSeparator = "." };

        private int _rownum;
        private string _shortName;
        private int _postalCode;
        private Address _address;
        private string _chiefName;
        private string _chiefPosition;
        private string _chiefGender;
        private string _chiefPhone;
        private string _publicPhone;
        private string _fax;
        private string _email;
        private string _closeFlag;
        private string _closeReason;
        private string _closeDate;
        private string _reopenDate;
        private string _paidServicesInfo;
        private string _freeServicesInfo;
        private string _workingHours;
        private string _clarificationOfWorkingHours;
        private string _specialization;
        private string _beneficialDrugPrescriptions;
        private string _extraInfo;
        private string _addressUnom;
        private int _globalid;

        public Clinic(IReadOnlyList<string> row)
        {
            _rownum = int.Parse(row[(int)ClinicEnum.Rownum]);
            _shortName = row[1];
            _postalCode = int.Parse(row[4]);
            _address = new Address(row[2], row[3], row[5], double.Parse(row[25], DecimalStyle), double.Parse(row[26], DecimalStyle));
            _chiefName = row[6];
            _chiefPosition = row[7];
            _chiefGender = row[8];
            _chiefPhone = row[9];
            _publicPhone = row[10];
            _fax = row[11];
            _email = row[12];
            _closeFlag = row[13];
            _closeReason = row[14];
            _closeDate = row[15];
            _reopenDate = row[16];
            _paidServicesInfo = row[17];
            _freeServicesInfo = row[18];
            _workingHours = row[19];
            _clarificationOfWorkingHours = row[20];
            _specialization = row[21];
            _beneficialDrugPrescriptions = row[22];
            _extraInfo = row[23];
            _addressUnom = row[24];
            _globalid = int.Parse(row[(int)ClinicEnum.GlobalId]);
        }
    }

}
