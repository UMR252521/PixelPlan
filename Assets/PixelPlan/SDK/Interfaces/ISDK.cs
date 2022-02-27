using System.Collections.Generic;

namespace pixelplan
{
    public interface ISDK
    {
        void OnInit();
        void LogEvent(string eventName);
        void LogEventStatus(string eventName,string param);
        void LogEventNormal(string eventName, Dictionary<string,string> param);
        void PpClientOpen();
        void PpHomePageImpression();
        void PpRewardPageImpression();
        void ToStore();
    }
}
