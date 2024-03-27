using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xls_Domain.Notification;

namespace xls_Domain.Interfaces.Global
{
    public interface INotifier
    {
        bool HasNotification();

        void ClearNotifications();

        List<Notify> GetNotifications();

        void Handle(Notify notification);
    }
}
