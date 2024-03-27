using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xls_Domain.Interfaces.Services.Files
{
    public interface IExcelService
    {
        string Generate<T>(IEnumerable<T> dataSource, bool sendMail = false);
    }
}
