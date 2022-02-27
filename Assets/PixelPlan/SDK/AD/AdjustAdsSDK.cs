using System;
using AdjustNS;
using UnityEngine;

namespace pixelplan
{
    public class AdjustAdsSDK : SDKBase, IAD
    {
        public Action<RewardedVideoResult> RewardCallback;
        public Action<InterstitialResult> InterstitialCallback;

        public override void OnInit()
        {
            base.OnInit();
            AdjustSDK.GetInstance().SetRewardedVideoAdListener(new MyAdjustRewardedVideoAdListener(RewardCallback));
            AdjustSDK.GetInstance().SetInterstitialAdListener(new MyAdjustInterstitialAdListener(InterstitialCallback));
        }

        public void DismissBannerAd()
        {
            AdjustSDK.GetInstance().DismissBannerAd();
        }

        private bool HasInterstitialAd(string id)
        {
            return AdjustSDK.GetInstance().HasInterstitialAd(id);
        }

        private bool HasRewardedVideoAd(string id)
        {
            return AdjustSDK.GetInstance().HasRewardedVideoAd(id);
        }

        public void ShowBannerAd(BannerPos bannerPos)
        {
            AdjustSDK.GetInstance().ShowBannerAd(bannerPos == BannerPos.Bottom ? BannerADPosition.bottomCenter : BannerADPosition.topCenter);
        }

        public void ShowInterstitialAd(string id, Action<InterstitialResult> callback)
        {
            if (!Util.Instance.IsNetworkReachability() || !HasInterstitialAd(id))
            {
                MyDebug.LogError("插页广告播放失败");
                callback?.Invoke(InterstitialResult.Fail);
            }
            else
            {
                InterstitialCallback += callback;
                AdjustSDK.GetInstance().ShowInterstitialAd(id);
            }
        }

        public void ShowRewardedVideoAd(string id, Action<RewardedVideoResult> callback)
        {
            if (!Util.Instance.IsNetworkReachability() || !HasRewardedVideoAd(id))
            {
                MyDebug.LogError("激励广告播放失败");
                callback?.Invoke(RewardedVideoResult.Fail);
            }
            else
            {
                RewardCallback += callback;
                AdjustSDK.GetInstance().ShowRewardedVideoAd(id);
            }
        }
    }

    /// <summary>
    /// 激励广告回调
    /// </summary>
    public class MyAdjustRewardedVideoAdListener : AdjustRewardedVideoAdListener
    {
        private Action<RewardedVideoResult> _callback;
        private bool _isReward = false;
        public MyAdjustRewardedVideoAdListener(Action<RewardedVideoResult> callback)
        {
            _callback = callback;
        }

        public void onReward(string gameEntry)
        {
            MyDebug.Log("广告:" + gameEntry + "播放完成");
            _isReward = true;
            _callback?.Invoke(RewardedVideoResult.Complete);
        }

        public void onRewardedVideoAdClosed(string gameEntry)
        {
            MyDebug.Log("关闭激励广告:" + gameEntry);
            AudioListener.pause = false;
            _callback?.Invoke(RewardedVideoResult.Close);
            if (_isReward)
            {
                _callback?.Invoke(RewardedVideoResult.Reward);
            }
            _callback = null;
        }

        public void onRewardedVideoAdPlayClicked(string gameEntry)
        {
            MyDebug.Log("点击激励广告:" + gameEntry);
            _callback?.Invoke(RewardedVideoResult.Click);
        }

        public void onRewardedVideoAdPlayStart(string gameEntry)
        {
            MyDebug.Log("播放激励广告:" + gameEntry);
            _isReward = false;
            AudioListener.pause = true;
            _callback?.Invoke(RewardedVideoResult.Show);
        }
    }

    /// <summary>
    /// 插页广告回调
    /// </summary>
    public class MyAdjustInterstitialAdListener : AdjustInterstitialAdListener
    {
        private Action<InterstitialResult> _callback;
        public MyAdjustInterstitialAdListener(Action<InterstitialResult> callback)
        {
            _callback = callback;
        }

        public void onInterstitialAdShow(string gameEntry)
        {
            MyDebug.Log("播放插页广告:" + gameEntry);
            AudioListener.pause = true;
            _callback?.Invoke(InterstitialResult.Show);
        }

        public void onInterstitialAdClicked(string gameEntry)
        {
            MyDebug.Log("点击插页广告:" + gameEntry);
            _callback?.Invoke(InterstitialResult.Click);
        }

        public void onInterstitialAdClose(string gameEntry)
        {
            MyDebug.Log("关闭插页广告:" + gameEntry);
            AudioListener.pause = false;
            _callback?.Invoke(InterstitialResult.Close);
            _callback = null;
        }
    }
}
