namespace pixelplan.puremvc
{
    public class SimpleCommand : Notifier, ICommand, INotifier
    {
        public virtual void Execute(INotification notification)
        {
        }
    }
}
