﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xls_Domain.Extensions;
using xls_Domain.Interfaces.Services.Files;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using xls_Domain.Interfaces.Global.Mail;
using System.Net.Mail;

namespace xls_Domain.Service.Files
{
    public class ExcelService : IExcelService
    {
        private XlsTradeSettings _xlsTradeSettings;
        private IMailService _mailService;

        public ExcelService(IOptions<XlsTradeSettings> xlsTradeSettings,
                            IMailService mailService)
        {
            _xlsTradeSettings = xlsTradeSettings.Value;
            _mailService = mailService;
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

            if (sendMail)
            {
                var recipientEmail = "jppimentel96@hotmail.com";
                _mailService.SendFile(fileName, recipientEmail);
            }

            return $"{_xlsTradeSettings.EndPointOut}/tmp/{fileName}";
        }

        //estudar uma maneira de deixar otimizado a criação do header, body e footer por meio de metodos.
        public async Task<string> GenerateExcelAsync(IEnumerable<Guid> simulationIds, Guid logId)
        {
             var obj = new List<string>();
            obj.Add("Primeiro");
            obj.Add("Segundo");
            obj.Add("Terceiro");

            var dataToExport = (from _r in obj
                                select new
                                {
                                    tId = logId,
                                    Teste = "teste",
                                    Teste2 = "teste",
                                    Teste3 = "teste",
                                }).Distinct();

            var fileName = $"PreAgreement_{logId}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
            var file = new FileInfo($"{_xlsTradeSettings.DirectoryOut}\\tmp\\{fileName}");

            using (var package = new ExcelPackage(file))
            {
                var workSheet = package.Workbook.Worksheets.Add("PreAgreementData");
                workSheet.Cells.LoadFromCollection(dataToExport, true, OfficeOpenXml.Table.TableStyles.Medium6);
                workSheet.Cells["A1:H1"].Style.Font.Bold = true; // Exemplo simples de formatação
                package.Save();
            }

            return $"{_xlsTradeSettings.EndPointOut}/tmp/{fileName}";
        }

    }
}
