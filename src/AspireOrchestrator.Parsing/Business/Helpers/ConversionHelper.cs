using System.Globalization;

namespace AspireOrchestrator.Parsing.Business.Helpers
{
    internal static class ConversionHelper
    {
        public static decimal GetDecimal100(string inputString)
        {
            var success = decimal.TryParse(inputString, out var result);
            if (!success)
                return 0M;
            if (result == 0M)
                return result;
            return result / 100;
        }

        public static decimal GetDecimal100(string inputString, string sign)
        {
            var success = decimal.TryParse(inputString, out var result);
            if (!success)
                return 0M;
            if (result == 0M)
                return result;
            return sign == "-" || sign == "2" ? -result / 100 : result / 100;
        }

        public static DateTime ParseDate(string dateString)
        {
            if (dateString == "00000000")
                return GetMinDateTime();
            try
            {
                return DateTime.ParseExact(dateString, "yyyyMMdd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch
            {
                return GetMinDateTime();
            }
        }

        public static bool IsMinDateTime(DateTime date)
        {
            return date == GetMinDateTime();
        }

        private static DateTime GetMinDateTime()
        {
            return new DateTime(1900, 1, 1);
        }

        public static string CprHelper(string textCpr) 
        {
            if (textCpr != null && textCpr.Length > 9)
                textCpr = textCpr.Substring(0, 6) + "-" + textCpr.Substring(6, 4);
            return textCpr;
        }

        public static string Concatenate(string s1, string s2)
        {
            return s1.Trim() + s2.Trim(); ;
        }

        public static decimal GetDecimalUs(string amount, string sign)
        {
            const NumberStyles style = NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign;
            var success = decimal.TryParse(amount, style, CultureInfo.GetCultureInfo("en-US"), out var result);
            if (!success)
                return 0M;
            if (result == 0M)
                return result;
            return sign is "-" or "2" ? -result : result;
        }
    }
}
