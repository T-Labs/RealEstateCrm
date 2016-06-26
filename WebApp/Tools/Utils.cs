using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp
{
    public static class Utils
    {
        public static bool IsSet(this object obj)
        {
            return obj != null;
        }

        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static string ToLocalizedString(this bool b)
        {
            return b ? "Да" : "Нет";
        }
                
        public static bool NormalizePhoneNumber(string phone, out string normalized)
        {
            Regex rgx = new Regex("[^0-9]");
            var numbers = rgx.Replace(phone, "");
            // 81234567890 11
            // 71234567890 11
            // +71234567890 12
            if (numbers.Length == 11 && (numbers[0] == '7' || numbers[0] == '8'))
            {
                normalized =  numbers.Substring(1);
                return true;
            }
            else if (numbers.Length == 12 && numbers.StartsWith("+7"))
            {                
                normalized = numbers.Substring(2);
                return true;

            }

            normalized = "";
            return false;
        }
    }

    internal static class HousingCallTypeExtensions
    {
        private static readonly Dictionary<HousingCallType, string> _texts = new Dictionary<HousingCallType, string>()
        {
            {HousingCallType.Verified,  "Проверено" },
            {HousingCallType.DontAnswer,  "Не отвечает" },
            {HousingCallType.NotAvailable,  "Недоступен" },
            {HousingCallType.CorrectedWithoutCall,  "Корректировка без прозвона" },
        };
        public static string ToLocalizedString(this HousingCallType callType)
        {
            if (_texts.ContainsKey(callType))
            {
                return _texts[callType];
            }
            throw new ArgumentOutOfRangeException(nameof(callType), callType, null);
        }
    }
}
