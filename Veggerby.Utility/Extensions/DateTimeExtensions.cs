using System;
using Veggerby.Utility.Resources;

namespace Veggerby.Utility.Extensions
{
    /// <summary>
    /// DateTime Extensions
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Elapseds the time.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns>TimeSpan</returns>
        public static TimeSpan Elapsed(this DateTime datetime)
        {
            return DateTime.Now - datetime;
        }
        
        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="weekrule">The weekrule.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime, System.Globalization.CalendarWeekRule weekrule, DayOfWeek firstDayOfWeek)
        {
            System.Globalization.CultureInfo ciCurr = System.Globalization.CultureInfo.CurrentCulture;
            return ciCurr.Calendar.GetWeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime, DayOfWeek firstDayOfWeek)
        {
            var dateinf = new System.Globalization.DateTimeFormatInfo();
            System.Globalization.CalendarWeekRule weekrule = dateinf.CalendarWeekRule;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="weekrule">The weekrule.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime, System.Globalization.CalendarWeekRule weekrule)
        {
            var dateinf = new System.Globalization.DateTimeFormatInfo();
            DayOfWeek firstDayOfWeek = dateinf.FirstDayOfWeek;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        /// Weeks the of year.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime datetime)
        {
            var dateinf = new System.Globalization.DateTimeFormatInfo();
            System.Globalization.CalendarWeekRule weekrule = dateinf.CalendarWeekRule;
            DayOfWeek firstDayOfWeek = dateinf.FirstDayOfWeek;
            return WeekOfYear(datetime, weekrule, firstDayOfWeek);
        }

        /// <summary>
        /// Gets the date time for day of week.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="day">The day.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static DateTime GetDateTimeForDayOfWeek(this DateTime datetime, DayOfWeek day, DayOfWeek firstDayOfWeek)
        {
            int current = DaysFromFirstDayOfWeek(datetime.DayOfWeek, firstDayOfWeek);
            int resultday = DaysFromFirstDayOfWeek(day, firstDayOfWeek);
            return datetime.AddDays(resultday - current);
        }

        public static DateTime GetDateTimeForDayOfWeek(this DateTime datetime, DayOfWeek day)
        {
            var dateinf = new System.Globalization.DateTimeFormatInfo();
            DayOfWeek firstDayOfWeek = dateinf.FirstDayOfWeek;
            return GetDateTimeForDayOfWeek(datetime, day, firstDayOfWeek);
        }

        /// <summary>
        /// Firsts the date time of week.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        public static DateTime FirstDateTimeOfWeek(this DateTime datetime)
        {
            var dateinf = new System.Globalization.DateTimeFormatInfo();
            DayOfWeek firstDayOfWeek = dateinf.FirstDayOfWeek;
            return FirstDateTimeOfWeek(datetime, firstDayOfWeek);
        }

        /// <summary>
        /// Firsts the date time of week.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        public static DateTime FirstDateTimeOfWeek(this DateTime datetime, DayOfWeek firstDayOfWeek)
        {
            return datetime.AddDays(-DaysFromFirstDayOfWeek(datetime.DayOfWeek, firstDayOfWeek));
        }

        /// <summary>
        /// Dayses from first day of week.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns></returns>
        private static int DaysFromFirstDayOfWeek(DayOfWeek current, DayOfWeek firstDayOfWeek)
        {
            //Sunday = 0,Monday = 1,...,Saturday = 6
            int daysbetween = current - firstDayOfWeek;
            if (daysbetween < 0) daysbetween = 7 + daysbetween;
            return daysbetween;
        }

        public static string GetValueOrDefaultToString(this DateTime? datetime, string defaultvalue)
        {
            if (datetime == null) return defaultvalue;
            return datetime.Value.ToString();
        }

        public static string GetValueOrDefaultToString(this DateTime? datetime, string format, string defaultvalue)
        {
            if (datetime == null) return defaultvalue;
            return datetime.Value.ToString(format);
        }

        public static string Period(DateTime? min, DateTime? max)
        {
            if ((min != null) && (max != null))
            {
                if (max.Value.Year > min.Value.Year)
                {
                    if (max.Value.Year == min.Value.Year + 1)
                    {
                        return String.Format("{0}/{1}", min.Value.ToString("yyyy"), max.Value.ToString("yyyy"));
                    }

                    return String.Format("{0} - {1}", min.Value.ToString("yyyy"), max.Value.ToString("yyyy"));
                }

                if (max.Value.Month > min.Value.Month)
                {
                    return String.Format("{0}-{1}", min.Value.ToString("MMM"), max.Value.ToString("MMM yyyy"));
                }

                return min.Value.ToString("MMM yyyy");
            }

            return "n/a";
        }

        public static string TimeDifference(this DateTime from, DateTime to)
        {
            return TimeDifference(@from, to, Strings.Year, Strings.Years, Strings.Month, Strings.Months, Strings.TimeDifferenceFormat, Strings.TimeDifferenceFormatNoMonths);
        }

        public static string TimeDifference(this DateTime from, DateTime to, string year, string years, string month, string months, string format1, string format2)
        {
            double fromValue = @from.Year * 12 + @from.Month + @from.Day / 31.0;
            double toValue = to.Year * 12 + to.Month + to.Day / 31.0;
            var y = (int)Math.Floor((toValue - fromValue) / 12);
            var m = (int)Math.Floor((toValue - fromValue) - y * 12);
            if (m != 0)
            {
                return String.Format(format1, y, y == 1 ? year : years, m, m == 1 ? month : months);
            }

            return String.Format(format2, y, y == 1 ? year : years);
        }
    }
}
