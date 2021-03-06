using System;

namespace pixelplan.puremvc
{
    public interface IObserver
    {
        Action<INotification> notifyMethod { set; }
        object notifyContext { set; }
        void NotifyObserver(INotification notification);
        bool CompareNotifyContext(object obj);
    }
}
