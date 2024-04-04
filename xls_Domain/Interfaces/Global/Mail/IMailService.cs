using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xls_Domain.Interfaces.Global.Mail
{
    public interface IMailService
    {
        Task<bool> SendFileEmailAsync(string recipientEmail, string subject, IEnumerable<Guid> simulationIds, Guid logId);

    }
}
