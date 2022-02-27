namespace pixelplan.puremvc
{
    public interface INotification
    {
        string name { get; }
        object[] param { get; set; }
        string ToString();
    }
}
