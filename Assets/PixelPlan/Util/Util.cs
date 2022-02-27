namespace pixelplan {
    // Util implemented in partial class
    public partial class Util: puremvc.Singleton<Util> {
        // implemented in other files.
        public override void OnSingletonInit(){
            
        }
        public override string SingletonName(){
            return "Util";
        }
    }

    public static class MSG_LOADING{
        public const string ON = "MSG_LOADING_ON";
        public const string OFF = "MSG_LOADING_OFF";
    }
    
}
