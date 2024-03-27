using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xls_Domain.Extensions;
using xls_Domain.Interfaces.Services.Files;
using Microsoft.Extensions.Options;
using OfficeOpenXml;

namespace xls_Domain.Service.Files
{
    public class ExcelService : IExcelService
    {
        private XlsTradeSettings _xlsTradeSettings;

        public ExcelService(IOptions<XlsTradeSettings> xlsTradeSettings)
        {
            _xlsTradeSettings = xlsTradeSettings.Value;       
        }

        public string Generate<T>(IEnumerable<T> dataSource, bool sendMail = false)
        {
            var fileName = $"{ExtensionMethod.GenerateFileName()}.xlsx";

            var file = new FileInfo($"{_xlsTradeSettings.DirectoryOut}\\tmp\\{fileName}");

            using (var package = new ExcelPackage(file))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                workSheet.Cells.LoadFromCollection(dataSource, true);

                package.Save();
            }

            //if (sendMail)
            //{
            //    //send mail here
            //}

            return $"{_xlsTradeSettings.EndPointOut}/tmp/{fileName}";
        }

    }
}
