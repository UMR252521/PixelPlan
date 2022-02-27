using System.Collections;
using System.Collections.Generic;
using AdjustNS;
using UnityEngine;

namespace pixelplan
{
    public class MyAdjustSDK : SDKBase, ISDK
    {
        private bool _isInit = false;

        public override void OnInit()
        {
            base.OnInit();

            AdjustSDK.GetInstance().Init((success) =>
            {
                _isInit = true;

                // 客户端监听，可以监听到兑换开关
                // 注意，这里的回调是实时刷新的。（涉及到刷新逻辑不能只处理一次，需要动态实时更改）
                AdjustSDK.GetInstance().SetClientListener(new MyAdjustClientListener());

                MyDebug.Log("AdjustSDK初始化成功");
            }, (fail) =>
             {
                 MyDebug.LogError("AdjustSDK初始化失败");
             });
        }

        public void LogEvent(string eventName)
        {
            MyDebug.Log("无参事件上报:" + eventName);
            AdjustSDK.GetInstance().LogEvent(eventName);
        }

        public void LogEventNormal(string eventName, Dictionary<string, string> param)
        {
            string msg = "多参数事件上报:" + eventName;
            foreach (var v in param)
            {
                msg += "\nKey:" + v.Key + "****Value:" + param;
            }
            MyDebug.Log(msg);
            AdjustSDK.GetInstance().LogEventNormal(eventName, param);
        }

        public void LogEventStatus(string eventName, string param)
        {
            MyDebug.Log("有参事件上报:" + eventName + "****参数:" + param);
            AdjustSDK.GetInstance().LogEventStatus(eventName, param);
        }

        public void PpClientOpen()
        {
            AdjustSDKUtils.GetInstance().PpClientOpen();
        }

        public void PpHomePageImpression()
        {
            AdjustSDKUtils.GetInstance().PpHomePageImpression();
        }

        public void PpRewardPageImpression()
        {
            AdjustSDKUtils.GetInstance().PpRewardPageImpression();
        }

        public void ToStore()
        {
            MyDebug.LogError("接口暂未提供,请联系AdjustSDK更新");
        }
    }

    public class MyAdjustClientListener : AdjustClientListener
    {
        public void onStuffTurnChanged(bool isOpen)
        {
            if (isOpen)
            {
                // 上报客户端开启兑换功能
                AdjustSDKUtils.GetInstance().PpClientOpen();
            }
            // called when StuffTurn changed
            // 兑换开关，isOpen为true表示开启，false表示未开启。
            // 这个接口会动态刷新，实时变化，需要开发者自行测试。
        }

        // A或B等多版本和对应配置，后台没有配置将没有该回调
        public void versionConfig(string json)
        {
            MyDebug.Log("版本信息:" + json);
        }

        // 用户账户信息
        public void loginSuccess(string userInfo)
        {
            MyDebug.Log("用户信息:" + userInfo);
        }
    }
}
