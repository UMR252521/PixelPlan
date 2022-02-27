namespace pixelplan.puremvc
{
    public interface IMediator: INotifier
    {
        string mediatorName { get; }
        object viewComponent { get; set; }
        string[] ListNotificationInterests();
        void HandleNotification(INotification notification);
        void OnRegister();
        void OnRemove();
    }
}
