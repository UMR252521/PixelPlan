namespace pixelplan.puremvc {
    interface INotificationHandler
    {
        string[] ListNotificationInterests();
        void HandleNotification(INotification notification);
    }
}
