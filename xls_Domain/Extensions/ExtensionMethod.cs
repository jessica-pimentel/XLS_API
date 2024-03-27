using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xls_Domain.Extensions
{
    public static class ExtensionMethod
    {
        public static string GenerateFileName()
        {
            return DateTime.Now.GenerateFileNames();
        }
        public static string GenerateFileNames(this DateTime value)
        {
            return value.ToString("ddMMyyHHmmssfffff"); //with miliseconds
        }

        public static string FormatDatePtBR(this DateTime value, bool hours = false, string year = "yyyy")
        {
            var d = hours == true ? $"{{0:dd/MM/{year} HH:mm:ss}}" : $"{{0:dd/MM/{year}}}";

            return string.Format(new CultureInfo("pt-BR"), d, value);
        }
    }
}
