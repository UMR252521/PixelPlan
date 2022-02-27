using System;
using System.Collections.Generic;
using pixelplan.puremvc;

namespace pixelplan
{
    public class SDKManager : Singleton<SDKManager>, ILifeCycle, ISDK, IAD
    {
        public override string SingletonName() { return "SDKManager"; }
        public override void OnSingletonInit() { this.OnInit(); }

        private List<SDKBase> m_SDKList = new List<SDKBase>();
        private bool m_isInitialized = false;
        private ISDK m_ProxySDK = null;
        private IAD m_ProxyAD = null;

        /// <summary>
        /// 初始化游戏模块
        /// </summary>
        public void OnInit()
        {
            if (!m_isInitialized)
            {
                m_isInitialized = true;

                //初始化AdjustSDK
                var adjustSDK = new MyAdjustSDK();
                m_SDKList.Add(adjustSDK);
                m_ProxySDK = adjustSDK;

                //初始化广告SDK
                var adsSDK = new AdjustAdsSDK();
                m_SDKList.Add(adsSDK);
                m_ProxyAD = adsSDK;

                m_SDKList.ForEach(sdk =>
                {
                    sdk.OnInit();
                });
            }
        }

        public void OnPause() { m_SDKList.ForEach(sdk => sdk.OnPause()); }
        public void OnResume() { m_SDKList.ForEach(sdk => sdk.OnResume()); }
        public void OnDestroy() { m_SDKList.ForEach(sdk => sdk.OnDestroy()); }
        public void OnUpdate() { m_SDKList.ForEach(sdk => sdk.OnUpdate()); }

        #region 广告SDK相关接口

        /// <summary>
        /// 播放激励广告
        /// </summary>
        /// <param name="id">广告id</param>
        /// <param name="callback">回调事件</param>
        public void ShowRewardedVideoAd(string id, Action<RewardedVideoResult> callback)
        {
            if (m_ProxyAD != null)
            {
                m_ProxyAD.ShowRewardedVideoAd(id, callback);
            }
            else
            {
                MyDebug.LogError("广告SDK未初始化");
            }
        }

        /// <summary>
        /// 播放插页广告
        /// </summary>
        /// <param name="id">广告id</param>
        /// <param name="callback">回调事件</param>
        public void ShowInterstitialAd(string id, Action<InterstitialResult> callback)
        {
            if (m_ProxyAD != null)
            {
                m_ProxyAD.ShowInterstitialAd(id, callback);
            }
            else
            {
                MyDebug.LogError("广告SDK未初始化");
            }
        }

        /// <summary>
        /// 展示Banner广告
        /// </summary>
        /// <param name="bannerPos">位置</param>
        public void ShowBannerAd(BannerPos bannerPos)
        {
            if (m_ProxyAD != null)
            {
                m_ProxyAD.ShowBannerAd(bannerPos);
            }
            else
            {
                MyDebug.LogError("广告SDK未初始化");
            }
        }

        /// <summary>
        /// 隐藏Banner广告
        /// </summary>
        public void DismissBannerAd()
        {
            if (m_ProxyAD != null)
            {
                m_ProxyAD.DismissBannerAd();
            }
            else
            {
                MyDebug.LogError("广告SDK未初始化");
            }
        }
        #endregion

        #region AdjustSDK相关接口

        /// <summary>
        /// 无参数事件上报
        /// </summary>
        /// <param name="eventName">事件名</param>
        public void LogEvent(string eventName)
        {
            if (m_ProxySDK != null)
            {
                m_ProxySDK.LogEvent(eventName);
            }
            else
            {
                MyDebug.LogError("AdjustSDK未初始化");
            }
        }

        /// <summary>
        /// 有参事件上报
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="param">参数</param>
        public void LogEventStatus(string eventName, string param)
        {
            if (m_ProxySDK != null)
            {
                m_ProxySDK.LogEventStatus(eventName, param);
            }
            else
            {
                MyDebug.LogError("AdjustSDK未初始化");
            }
        }

        /// <summary>
        /// 多参数事件上报
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="param">参数</param>
        public void LogEventNormal(string eventName, Dictionary<string, string> param)
        {
            if (m_ProxySDK != null)
            {
                m_ProxySDK.LogEventNormal(eventName, param);
            }
            else
            {
                MyDebug.LogError("AdjustSDK未初始化");
            }
        }

        /// <summary>
        /// 客户端开启兑换功能(若onStuffTurnChanged回调的isOpen=true调用) （Paypal_client_open 事件）
        /// </summary>
        public void PpClientOpen()
        {
            if (m_ProxySDK != null)
            {
                m_ProxySDK.PpClientOpen();
            }
            else
            {
                MyDebug.LogError("AdjustSDK未初始化");
            }
        }

        /// <summary>
        /// 主页展示Pp卡，SDK通知兑换功能开启后，游戏开启成功时上报 （Paypal_homepage 事件）
        /// </summary>
        public void PpHomePageImpression()
        {
            if (m_ProxySDK != null)
            {
                m_ProxySDK.PpHomePageImpression();
            }
            else
            {
                MyDebug.LogError("AdjustSDK未初始化");
            }
        }

        /// <summary>
        /// Pp卡奖励页 展示，带获取奖励（请求广告）的按钮的弹窗展示时上报，包含签到页面 （PayPal_impression 事件）
        /// </summary>
        public void PpRewardPageImpression()
        {
            if (m_ProxySDK != null)
            {
                m_ProxySDK.PpRewardPageImpression();
            }
            else
            {
                MyDebug.LogError("AdjustSDK未初始化");
            }
        }

        /// <summary>
        /// 应用商店跳转（评论引导）
        /// </summary>
        public void ToStore()
        {
            if (m_ProxySDK != null)
            {
                m_ProxySDK.ToStore();
            }
            else
            {
                MyDebug.LogError("AdjustSDK未初始化");
            }
        }
        #endregion
    }
}
