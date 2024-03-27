using Microsoft.AspNetCore.Mvc;
using xls_Domain.Interfaces.Global;

namespace xls_Application.Controllers
{
    public class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        public MainController(INotifier notifier)
        {

            _notifier = notifier;

        }

        protected bool isValid()
        {
            return !_notifier.HasNotification();
        }

        protected ActionResult CustomResponse(object result = null, bool error = false)
        {
            if (error)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = new List<string>
                        {
                            result.ToString()
                        }
                });
            }

            if (isValid())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message).Distinct()
            }) ;
        }

        protected ActionResult CustomResponseError(object result = null)
        {
            return BadRequest(new
            {
                success = false,
                errors = new List<string>
                        {
                            result.ToString()
                        }
            });



        }
    }
}
