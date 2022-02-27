using UnityEngine;
using System.Collections;

public partial class Util
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#endif

    public void Vibrate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            vibrator.Call("vibrate");
#else
            Handheld.Vibrate();
#endif
    }

    public void Vibrate(long milliseconds)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            vibrator.Call("vibrate", milliseconds);
#else
        Handheld.Vibrate();
#endif
    }

    public void Vibrate(long[] pattern, int repeat)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            vibrator.Call("vibrate", pattern, repeat);
#else
        Handheld.Vibrate();
#endif
    }

    public void Cancel()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
            vibrator.Call("cancel");
#endif
    }
}