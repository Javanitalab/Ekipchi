using System;
using DNTPersianUtils.Core;

namespace Hastnama.Ekipchi.Common.Util
{
    public class PersianDateUtil
    {
        public static string ChangeDateTime(DateTime? dateTime, string lang)
        {
            if (dateTime.HasValue)
                return dateTime.Value.ToString("O");


            return string.Empty;
        }

        public static string PersianDateTime(DateTime dateTime)
        {
            return dateTime.ToLongPersianDateString();
        }

        public static string PersianDate(DateTime? dateTime)
        {
            if (dateTime.HasValue)
                return dateTime.Value.ToPersianDateTextify();

            return "";
        }

        public static int PersianDay(DateTime dateTime)
        {
            return dateTime.GetPersianDayOfMonth();
        }

        public static string PersianMonth(DateTime dateTime)
        {
            string[] month = dateTime.ToLongPersianDateString().Split(" ");
            return month[1];
        }

        public static string LongPersianDate(DateTime dateTime)
        {
            return dateTime.ToPersianDateTextify();
        }

        public static PersianYear GetLastPersianYearDate(DateTime dateTime)
        {
            return dateTime.GetPersianYearStartAndEndDates();
        }
    }
}