namespace pixelplan.puremvc
{
    public class Notifier : INotifier
    {
        public virtual void SendNotification(string notificationName, params object[] param)
        {
            this.facade.SendNotification(notificationName, param);
        }

        protected IFacade facade
        {
            get
            {
                return Facade.getInstance();
            }
        }
    }
}
