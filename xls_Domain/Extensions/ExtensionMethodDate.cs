using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xls_Domain.Extensions
{
    public static class ExtensionMethodDate
    {
        public static string FormatDatePtBR(this DateTime value, bool hours = false, string year = "yyyy")
        {
            var d = hours == true ? $"{{0:dd/MM/{year} HH:mm:ss}}" : $"{{0:dd/MM/{year}}}";

            return string.Format(new CultureInfo("pt-BR"), d, value);
        }

        public static string FormatDatePtBR(this DateTime? value, bool hours = false, string year = "yyyy")
        {
            if (value.HasValue)
            {
                var d = hours == true ? $"{{0:dd/MM/{year} HH:mm:ss}}" : $"{{0:dd/MM/{year}}}";

                return string.Format(new CultureInfo("pt-BR"), d, value);
            }
            else
            {
                return "";
            }
        }

        public static string GetNameMonth(this DateTime dateTime)
        {
            //return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);

            return dateTime.ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-BR"));
        }

        public static string GetNameMonth(this int month)
        {
            //return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mes);

            var m = new DateTime(1900, month, 1);

            return m.ToString("MMMM", CultureInfo.CreateSpecificCulture("pt-BR"));
        }

        public static DateTime GetMinimalDate(this DateTime dateTime)
        {
            //return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(mes);

            return new DateTime(1900, 1, 1);
        }

        public static string GetNameMonthAbbreviate(this DateTime dateTime, bool capitalize = false)
        {
            //return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);

            var sD = dateTime.ToString("MMM", CultureInfo.CreateSpecificCulture("pt-BR"));

            return capitalize ? sD.Capitalize() : sD;
        }

        public static string GetNameMonthAbbreviate(this int month)
        {
            var m = new DateTime(1900, month, 1);

            //return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName().Month);

            return m.ToString("MMM", CultureInfo.CreateSpecificCulture("pt-BR"));
        }

        public static string GetNameDay(this DateTime dateTime)
        {
            //return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);

            return dateTime.ToString("dddd", CultureInfo.CreateSpecificCulture("pt-BR"));
        }

        public static string GetNameDayAbbreviate(this DateTime dateTime)
        {
            //return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(dateTime.DayOfWeek);

            return dateTime.ToString("ddd", CultureInfo.CreateSpecificCulture("pt-BR"));
        }

        public static DateTime StringToDateTime(this string value)
        {
            return Convert.ToDateTime(value);
        }

        public static DateTime GetFirstDayWeek(this DateTime datetime, bool startSunday = true)
        {
            var d = startSunday ? DayOfWeek.Sunday : DayOfWeek.Monday;

            var rs = datetime.AddDays(d - datetime.DayOfWeek);

            return rs;
        }

        public static DateTime GetLastDayWeek(this DateTime datetime, bool startSunday = true)
        {
            var d = startSunday ? DayOfWeek.Sunday : DayOfWeek.Monday;

            var monday = datetime.AddDays(d - DateTime.Now.DayOfWeek);

            return monday.AddDays(6);
        }

        public static DateTime GetLastDayMonth(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, DateTime.DaysInMonth(datetime.Year, datetime.Month));
        }

        public static DateTime GetFirstDayMonth(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, 1);
        }

        public static string GenerateFileName(this DateTime value)
        {
            return value.ToString("ddMMyyHHmmssfffff"); //with miliseconds
        }

        public static string GenerateNumberDocument(this DateTime value)
        {
            return value.ToString("ssfffff");
        }

        public static string FormatToSQL(this DateTime value)
        {
            return $"{value:yyyy-MM-dd HH:mm:ss}";
        }

        public static bool IsDateValid(this DateTime? value, DateTime minDate)
        {
            if (value.HasValue)
            {
                return value.Value.Date <= minDate.Date;
            }
            else
            {
                return false;
            }
        }
    }
}
