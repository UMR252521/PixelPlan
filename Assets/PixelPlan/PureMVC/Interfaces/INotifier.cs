namespace pixelplan.puremvc
{
    public interface INotifier
    {
        void SendNotification(string notificationName, params object[] param);
    }
}
