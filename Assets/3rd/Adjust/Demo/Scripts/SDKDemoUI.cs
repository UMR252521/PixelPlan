using System;
using System.Collections;
using System.Collections.Generic;
using AdjustNS;
using AdjustNS.MiniJSON;
using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;

namespace AdjustDemo
{
    public class SDKDemoUI : MonoBehaviour
    {
        public static SDKDemoUI INSTANCE;
        public static string GAME_ENTRY = "game";

        public Text logText;
        public ToastTools toastTools;

        public Button btnClearLog;
        public Button btnLogEventNormal;
        public Button btnShowBanner;
        public Button btnHideBanner;
        public Button btnHasInterstitial;
        public Button btnShowInterstitial;
        public Button btnHashVideo;
        public Button btnShowVideo;
        public Button btnLogEvent;
        public Button btnLogEventStatus;
        

        private void Awake()
        {
            INSTANCE = this;
            // sdk init
            AdjustSDK.GetInstance()
                .Init(delegate(InitSuccessResult result) { LogPrint("init success"); },
                    delegate(InitFailedResult result) { LogPrint($"init failed msg: {result.message}"); });

            // init listener
            AdjustSDK.GetInstance().SetClientListener(new MyAdjustClientListener());
            AdjustSDK.GetInstance().SetBannerAdListener(new MyAdjustBannerAdListener());
            AdjustSDK.GetInstance().SetInterstitialAdListener(new MyAdjustInterstitialAdListener());
            AdjustSDK.GetInstance().SetRewardedVideoAdListener(new MyAdjustRewardedVideoAdListener());
            LogPrint(transform.name);
            this.btnLogEventNormal = transform.Find("Scroll View")
                .Find("Viewport")
                .Find("Content")
                .Find("btnLogEventNormal").GetComponent<Button>();
        }

        public void LogPrint(string s)
        {
            var rectTransform = logText.rectTransform;
            var size = rectTransform.rect;
            rectTransform.sizeDelta = new Vector2(size.width, size.height + 22);

            logText.text = logText.text + "\n" + s;
        }

        private void Start()
        {
            this.btnClearLog.onClick.AddListener(this.OnBtnClearLogClick);
            this.btnShowBanner.onClick.AddListener(this.OnBtnShowBannerClick);
            this.btnHideBanner.onClick.AddListener(this.OnBtnHideBannerClick);
            this.btnHasInterstitial.onClick.AddListener(this.OnBtnHasInterstitialClick);
            this.btnShowInterstitial.onClick.AddListener(this.OnBtnShowInterstitialClick);
            this.btnHashVideo.onClick.AddListener(this.OnBtnHashVideoClick);
            this.btnShowVideo.onClick.AddListener(this.OnBtnShowVideoClick);
            this.btnLogEvent.onClick.AddListener(this.OnBtnLogEventClick);
            this.btnLogEventStatus.onClick.AddListener(this.OnBtnLogEventStatusClick);
            this.btnLogEventNormal.onClick.AddListener(this.OnBtnLogEventNormalClick);
        }

        private void OnBtnClearLogClick()
        {
            this.logText.text = "";
            
            var rectTransform = logText.rectTransform;
            var size = rectTransform.rect;
            rectTransform.sizeDelta = new Vector2(size.width, 100);
        }

        private void OnBtnShowBannerClick()
        {
            Toast("show Banner");
            LogPrint("show banner");
            AdjustSDK.GetInstance().ShowBannerAd(BannerADPosition.bottomCenter);
        }

        private void OnBtnHideBannerClick()
        {
            Toast("hide Banner");
            LogPrint("hide banner");
            AdjustSDK.GetInstance().DismissBannerAd();
        }

        private void OnBtnHasInterstitialClick()
        {
            var isReady = AdjustSDK.GetInstance().HasInterstitialAd(GAME_ENTRY);
            LogPrint("HasInterstitialAd + " + isReady);
            Toast("" + isReady);
        }

        private void OnBtnShowInterstitialClick()
        {
            Toast("show InterstitialAd");
            LogPrint("show InterstitialAd");
            AdjustSDK.GetInstance().ShowInterstitialAd(GAME_ENTRY);
        }

        private void OnBtnHashVideoClick()
        {
            var isReady = AdjustSDK.GetInstance().HasRewardedVideoAd(GAME_ENTRY);
            LogPrint("Has Video + " + isReady);
            Toast("" + isReady);
        }

        private void OnBtnShowVideoClick()
        {
            Toast("show video");
            LogPrint("showRewardedVideoAd");
            AdjustSDK.GetInstance().ShowRewardedVideoAd(GAME_ENTRY);
        }

        private void OnBtnLogEventClick()
        {
            Toast("log event");
            AdjustSDK.GetInstance().LogEvent("adujust_sdk_test");
            LogPrint($"LogEvent name: adujust_sdk_test");
        }

        private void OnBtnLogEventStatusClick()
        {
            Toast("log status");
            AdjustSDK.GetInstance().LogEventStatus("adujust_sdk_test", "test");
            LogPrint($"LogEventStatus name: adujust_sdk_test value: test");
        }

        private void OnBtnLogEventNormalClick()
        {
            Toast("log event normal");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["aaa"] = "bbb";
            dic["ccc"] = "ddd";
            string jsonData = Json.Serialize(dic);
            AdjustSDK.GetInstance().LogEventNormal("adujust_sdk_test", dic);
            LogPrint($"logEventNormal name: adujust_sdk_test value: ${jsonData}");
        }

        private void Toast(string str)
        {
            toastTools.Toast(str);
        }

        private class MyAdjustClientListener : AdjustClientListener
        {
            public void onStuffTurnChanged(bool isOpen)
            {
                if (isOpen) {
                    AdjustSDKUtils.GetInstance().PpClientOpen();
                    SDKDemoUI.INSTANCE.LogPrint("LogEvent PpClientOpen");
                    AdjustSDKUtils.GetInstance().PpHomePageImpression();
                    SDKDemoUI.INSTANCE.LogPrint("LogEvent PpHomePageImpression");
                }
                SDKDemoUI.INSTANCE.LogPrint("[Listener] onStuffTurnChanged: " + isOpen);
            }

            // A或B等多版本和对应配置，后台没有配置将没有该回调
            public void versionConfig(string json)
            {
                //格式 {"config_key":"A","config_value":"{}"}，解析这串json拿到version和对应的配置
                SDKDemoUI.INSTANCE.LogPrint("versionConfig::" + json);
            }

        }

        private class MyAdjustBannerAdListener : AdjustBannerAdListener
        {
            public void onBannerShow(string gameEntry)
            {
                SDKDemoUI.INSTANCE.LogPrint("[BannerAdListener] onBannerShow: " + gameEntry);
            }

            public void onBannerClicked(string gameEntry)
            {
                SDKDemoUI.INSTANCE.LogPrint("[BannerAdListener] onBannerClicked: " + gameEntry);
            }

            public void onBannerClose(string gameEntry)
            {
                SDKDemoUI.INSTANCE.LogPrint("[BannerAdListener] onBannerClose: " + gameEntry);
            }
        }

        private class MyAdjustInterstitialAdListener : AdjustInterstitialAdListener
        {
            public void onInterstitialAdShow(string gameEntry)
            {
                SDKDemoUI.INSTANCE.LogPrint("[InterstitialAdListener] onInterstitialAdShow: " + gameEntry);
            }

            public void onInterstitialAdClicked(string gameEntry)
            {
                SDKDemoUI.INSTANCE.LogPrint("[InterstitialAdListener] onInterstitialAdClicked: " + gameEntry);
            }

            public void onInterstitialAdClose(string gameEntry)
            {
                SDKDemoUI.INSTANCE.LogPrint("[InterstitialAdListener] onInterstitialAdClose: " + gameEntry);
            }
        }

        private class MyAdjustRewardedVideoAdListener : AdjustRewardedVideoAdListener
        {
            public void onRewardedVideoAdPlayStart(string gameEntry)
            {
                SDKDemoUI.INSTANCE.LogPrint("[RewardedVideoAdListener] onRewardedVideoAdPlayStart: " + gameEntry);
            }

            public void onRewardedVideoAdClosed(string gameEntry)
            {
                SDKDemoUI.INSTANCE.LogPrint("[RewardedVideoAdListener] onRewardedVideoAdClosed: " + gameEntry);
            }

            public void onRewardedVideoAdPlayClicked(string gameEntry)
            {
                SDKDemoUI.INSTANCE.LogPrint("[RewardedVideoAdListener] onRewardedVideoAdPlayClicked: " + gameEntry);
            }

            public void onReward(string gameEntry)
            {
                SDKDemoUI.INSTANCE.LogPrint("[RewardedVideoAdListener] onReward: " + gameEntry);
            }
        }
    }
}