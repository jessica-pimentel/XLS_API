using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xls_Domain.Models.Files;

namespace xls_Domain.Interfaces.Services.Files
{
    public interface IPDFService
    {
        Task<string> GeneratePDF(IEnumerable<PDF> models, Guid logId, bool sendMail = false);
        Task<string> GenerateCompletePDF(IEnumerable<object> obj, Guid logId, bool sendMail = false);
    }
}
