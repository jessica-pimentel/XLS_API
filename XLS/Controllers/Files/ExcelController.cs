using Microsoft.AspNetCore.Mvc;
using xls_Domain.Interfaces.Global;

namespace xls_Application.Controllers.Files
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("files/[controller]")]
    public class ExcelController : MainController
    {
        private readonly INotifier _notifier;
        public ExcelController(INotifier notifier) : base(notifier) 
        {
            _notifier = notifier;   
        }
    }
}
