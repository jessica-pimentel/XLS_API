using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout.Borders;
using iText.Layout.Properties;
using OfficeOpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xls_Domain.Extensions;
using xls_Domain.Interfaces.Services.Files;
using xls_Domain.Models.Files;
using static xls_Domain.Extensions.Files.ExtensionPDF;

namespace xls_Domain.Service.Files
{
    public class PDFService : IPDFService
    {
        private Color colorTitle;

        public async Task<string> GeneratePDF(IEnumerable<PDF> models, Guid logId, bool sendMail = false)
        {
            var font = PdfFontFactory.CreateFont("Helvetica");

            var networkLogoPath = @"C:\Users\jessi\source\image\logoPanutrir.png";

            var fileName = ExtensionMethod.GenerateFileName();
            var printFileName = $"{fileName}.pdf";
            var filePath = Path.Combine(@"C:\pdf", printFileName);

            var file = new FileInfo(filePath);

            var writer = new PdfWriter(file.FullName);
            var pdf = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdf);
            document.SetMargins(20, 20, 10, 20);

            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, new TextFooterEventHandler(document, font));

            // Header
            UnitValue[] headerColumnWidths = UnitValue.CreatePercentArray(new float[] { 33.33f, 33.33f, 33.33f });
            iText.Layout.Element.Table headerTable = new iText.Layout.Element.Table(headerColumnWidths);
            headerTable.SetWidth(pdf.GetDefaultPageSize().GetWidth() - 20);

            // Adiciona a logo à tabela de cabeçalho
            var logoImage = new iText.Layout.Element.Image(ImageDataFactory.Create(networkLogoPath));
            logoImage.SetAutoScale(true);
            var logoCell = new iText.Layout.Element.Cell(1, 1).SetBorder(Border.NO_BORDER);
            logoCell.Add(logoImage);
            headerTable.AddCell(logoCell);

            // Adiciona as informações ao cabeçalho
            headerTable.AddCell(new iText.Layout.Element.Cell(1, 1).SetBorder(Border.NO_BORDER).Add(new iText.Layout.Element.Paragraph("Panutrir").SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.LEFT)));
            headerTable.AddCell(new iText.Layout.Element.Cell(1, 1).SetBorder(Border.NO_BORDER).Add(new iText.Layout.Element.Paragraph(DateTime.Now.ToString("dd/MM/yyyy")).SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)));
            headerTable.AddCell(new iText.Layout.Element.Cell(1, 1).SetBorder(Border.NO_BORDER).Add(new iText.Layout.Element.Paragraph("Email para contato: SAC\nsac@panutrir.com.br\nFone: (43) 3548-1421").SetFont(font).SetFontSize(10).SetTextAlignment(TextAlignment.RIGHT)));

            // Adiciona o cabeçalho ao documento
            document.Add(headerTable);

            // Adiciona espaço entre o cabeçalho e os dados
            document.Add(new iText.Layout.Element.Paragraph("\n"));

            // Tabela de dados
            UnitValue[] columnWidths = UnitValue.CreatePercentArray(new float[] { 15f, 15f, 15f, 15f, 15f, 10f, 10f, 10f });
            iText.Layout.Element.Table dataTable = new iText.Layout.Element.Table(columnWidths);
            dataTable.SetBorder(Border.NO_BORDER);
            dataTable.SetWidth(pdf.GetDefaultPageSize().GetWidth() - 20);

            // Adiciona célula vazia para alinhar com as colunas de dados
            dataTable.AddHeaderCell(new iText.Layout.Element.Cell(1, columnWidths.Length).SetBorder(Border.NO_BORDER));

            // Adiciona os títulos das colunas
            dataTable.AddHeaderCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Teste").SetFont(font).SetFontSize(12).SetBackgroundColor(colorTitle).SetTextAlignment(TextAlignment.CENTER)));
            //dataTable.AddHeaderCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph("CodRede").SetFont(font).SetFontSize(12).SetBackgroundColor(colorTitle).SetTextAlignment(TextAlignment.CENTER)));
            //dataTable.AddHeaderCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Rede").SetFont(font).SetFontSize(12).SetBackgroundColor(colorTitle).SetTextAlignment(TextAlignment.CENTER)));
            //dataTable.AddHeaderCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Valor Simulado").SetFont(font).SetFontSize(12).SetBackgroundColor(colorTitle).SetTextAlignment(TextAlignment.CENTER)));
            //dataTable.AddHeaderCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Investimento Pan").SetFont(font).SetFontSize(12).SetBackgroundColor(colorTitle).SetTextAlignment(TextAlignment.CENTER)));
            //dataTable.AddHeaderCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Investimento Rede").SetFont(font).SetFontSize(12).SetBackgroundColor(colorTitle).SetTextAlignment(TextAlignment.CENTER)));
            //dataTable.AddHeaderCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Periodo Inicial").SetFont(font).SetFontSize(12).SetBackgroundColor(colorTitle).SetTextAlignment(TextAlignment.CENTER)));
            //dataTable.AddHeaderCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph("Periodo Final").SetFont(font).SetFontSize(12).SetBackgroundColor(colorTitle).SetTextAlignment(TextAlignment.CENTER)));

            // Adiciona dados às células
            foreach (var resultItem in models)
            {
                dataTable.AddCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph(resultItem.Teste.ToString()).SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER)));
                //dataTable.AddCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph(resultItem.CodNetwork.ToString()).SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER)));
                //dataTable.AddCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph(resultItem.Network.ToString()).SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER)));
                //dataTable.AddCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph(resultItem.ValueOffer.ToString()).SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER)));
                //dataTable.AddCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph(resultItem.MarginPan.ToString()).SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER)));
                //dataTable.AddCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph(resultItem.MarginNetwork.ToString()).SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER)));
                //dataTable.AddCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph(resultItem.PeriodInitial.ToString()).SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER)));
                //dataTable.AddCell(new iText.Layout.Element.Cell(1, 1).Add(new iText.Layout.Element.Paragraph(resultItem.PeriodFinal.ToString()).SetFont(font).SetFontSize(11).SetTextAlignment(TextAlignment.CENTER)));
            }

            document.Add(dataTable);

            //if (sendMail)
            //{
            //    await _mailService.Send("teste", "teste", "teste@mail", "teste", "teste");
            //}

            document.Close();
            return printFileName;
        }
    }
}
