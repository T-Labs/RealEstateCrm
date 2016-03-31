using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp
{
    internal static class Utils
    {

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
