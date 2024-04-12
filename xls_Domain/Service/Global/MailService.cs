using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xls_Domain.Extensions.Mail;
using xls_Domain.Interfaces.Services.Files;
using xls_Domain.Extensions;
using Microsoft.Extensions.Options;
using System.Collections;
using Microsoft.Extensions.WebEncoders.Testing;
using xls_Domain.Interfaces.Global.Mail;

namespace xls_Domain.Service.Global
{
    public class MailService : IMailService
    {
        private MailServiceSettings _mailSettings { get; }
        private Microsoft.Extensions.Hosting.IHostingEnvironment _env;
        private readonly IPDFService _pdfService;
        private readonly IExcelService _excelService;

        private XlsTradeSettings _xlsSystemTradeSettings { get; }

        public MailService(IOptions<MailServiceSettings> mailSettings,
                           Microsoft.Extensions.Hosting.IHostingEnvironment env,
                           IPDFService pdfService,
                           IExcelService excelService)
        {
            _mailSettings = mailSettings.Value;
            _env = env;
            _pdfService = pdfService;
            _excelService = excelService;
        }

        public async Task<bool> SendFileEmailAsync(string recipientEmail, string subject, IEnumerable<Guid> simulationIds, Guid logId)
        {
            try
            {
                // Supondo que você possa obter os detalhes do para preencher o email aqui dentro do MailService
                var obj = new List<string>();
                obj.Add("Primeiro");
                obj.Add("Segundo");
                obj.Add("Terceiro");

                var objectValues = (from _r in obj
                                          select new 
                                          {
                                              tId = logId,
                                              Teste = "teste",
                                              Teste2 = "teste",
                                              Teste3 = "teste",
                                          });

                var body = new StringBuilder();

                // Adicionando cabeçalho e informações gerais
                body.Append($@"<strong>{obj}</strong> Segue em anexo resumo do pré-acordo
                            <br /><br />Data de realização: <strong>{obj}</strong><br />");

                // Adicionando detalhes específicos do pré-acordo
                body.Append("<strong>Dados Oferta:<br /></strong>");
                foreach (var simulationId in simulationIds)
                {
                    var simulationDetail = "teste";
                    body.AppendLine("<br />");
                    body.AppendLine($"<strong style='font-size: 12px;'>Rede:</strong> {simulationDetail}<br />");
                    body.AppendLine($"<strong style='font-size: 12px;'>CodEAN:</strong> {simulationDetail}<br />");
                    body.AppendLine($"<strong style='font-size: 12px;'>Data Inicial:</strong> {simulationDetail}<br />");
                    body.AppendLine($"<strong style='font-size: 12px;'>Data Final:</strong> {simulationDetail}<br />");
                    body.AppendLine($"<strong style='font-size: 12px;'>Valor PDV:</strong> {simulationDetail}<br />");
                    body.AppendLine($"<strong style='font-size: 12px;'>Margem Rede:</strong> {simulationDetail}<br />");
                    body.AppendLine($"<strong style='font-size: 12px;'>Margem Pan:</strong> {simulationDetail}<br />");
                    body.AppendLine("<br />");
                }

                // Adicionando rodapé
                body.Append(@"<br /><br /><br />Dúvidas acione o suporte (xx) xx xxxx-xxxx
                            <br /><br />E-mail automático, não responda para esse remetente</strong>
                            <br /><strong> EMPRESA CNPJ:xx.xxx.xxx/000x-0x</strong>");

                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(_mailSettings.System.FromEmail, "EMPRESA", Encoding.UTF8);
                    mail.To.Add(new MailAddress(recipientEmail));
                    mail.Subject = subject;
                    mail.Body = body.ToString();
                    mail.IsBodyHtml = true;

                    // Anexar os arquivos gerados
                    var excelFilePath = await _excelService.GenerateExcelAsync(simulationIds, logId);
                    var pdfFilePath = await _pdfService.GenerateCompletePDF(objectValues, logId, false);
                    mail.Attachments.Add(new Attachment(excelFilePath));
                    mail.Attachments.Add(new Attachment(pdfFilePath));

                    using (var smtp = new SmtpClient(_mailSettings.System.STMP, _mailSettings.System.Port))
                    {
                        smtp.Credentials = new NetworkCredential(_mailSettings.System.UserName, _mailSettings.System.Password);
                        smtp.EnableSsl = _mailSettings.System.UseSSL;
                        await smtp.SendMailAsync(mail);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SendFile(string fileName, string recipientEmail)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(_mailSettings.System.FromEmail, "EMPRESA", Encoding.UTF8);
                    mail.To.Add(new MailAddress(recipientEmail));
                    mail.IsBodyHtml = true;

                    // Anexar os arquivos gerados
                    mail.Attachments.Add(new Attachment(fileName));

                    using (var smtp = new SmtpClient(_mailSettings.System.STMP, _mailSettings.System.Port))
                    {
                        smtp.Credentials = new NetworkCredential(_mailSettings.System.UserName, _mailSettings.System.Password);
                        smtp.EnableSsl = _mailSettings.System.UseSSL;
                        await smtp.SendMailAsync(mail);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
