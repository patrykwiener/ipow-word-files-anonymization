using System;
using System.Collections.Generic;
using System.Text;

namespace IPOW
{
    class PatternsModel
    {
        public const string PESEL = @"[0-9]{4}[0-3]{1}[0-9]{6}";
        public const string PHONE_NUMBER = @"(^|\s)(\+48\s?)?([0-9][0-9][0-9]\s?){3}(\s|$)";
        public const string BIRTH_DATE = @"(0?[1-9]|[12][0-9]|3[01])\.(0?[1-9]|1[012])\.\d{4}";
        public const string EMAIL = @"[a-zA-Z0-9_\-\.]+@([a-zA-Z0-9]+\.)+[a-zA-Z0-9]+";
        public const string VEHICLE_REGISTRATION_NUMBER = @"^[A-Z][A-Z]([0-9](([0-9][0-9][0-9][0-9])|([0-9][0-9][0-9][A-Z])|([0-9][0-9][A-Z][A-Z])|([A-Z][0-9][0-9][0-9])|([A-Z][A-Z][0-9][0-9])))|([A-Z](([A-Z][0-9][0-9][0-9])|([0-9][0-9][A-Z][A-Z])|([1-9][A-Z][0-9][0-9])|([0-9][0-9][A-Z][1-9])|([1-9][A-Z][A-Z][1-9])|([A-Z][A-Z][0-9][0-9])|([0-9][0-9][0-9][0-9][0-9])|([0-9][0-9][0-9][0-9][A-Z])|([0-9][0-9][0-9][A-Z][A-Z])|([A-Z][0-9][0-9][A-Z])|([A-Z][0-9][A-Z][A-Z])))$";
    }
}
