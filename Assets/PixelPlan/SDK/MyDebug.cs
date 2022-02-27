using UnityEngine;

namespace pixelplan
{
    public static class MyDebug
    {
        public static bool _canShowLog;

        public static void Log(string msg)
        {
            if (_canShowLog)
                Debug.Log("SDKLog>>>>" + msg);
        }

        public static void LogError(string msg)
        {
            if (_canShowLog)
                Debug.LogError("SDKLog>>>>" + msg);
        }
    }
}
