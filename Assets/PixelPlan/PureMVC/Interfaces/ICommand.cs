namespace pixelplan.puremvc 
{
    public interface ICommand: INotifier
    {
        void Execute(INotification Notification);
    }
}
