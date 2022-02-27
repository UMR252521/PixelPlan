namespace pixelplan.puremvc
{
    public class Notification : INotification
    {
        public Notification(string _name, params object[] _param)
        {
            name = _name;
            param = _param;
        }

        public string name { get; }
        public object[] param { get; set; }
    }
}
