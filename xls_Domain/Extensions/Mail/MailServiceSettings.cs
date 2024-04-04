using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xls_Domain.Extensions.Mail
{
    public class MailServiceSettings
    {
        public MailSettings System { get; set; }
        public MailSettings Commercial { get; set; }
    }
    public class MailSettings
    {
        public string STMP { get; set; }

        public int Port { get; set; }

        public bool UseSSL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string From { get; set; }

        public string FromEmail { get; set; }

        public string CcoDevelopment { get; set; }

        public string CcoContactForm { get; set; }
    }
}
