using System;

namespace pixelplan {
    public interface IAD
    {
        void ShowRewardedVideoAd(string id,Action<RewardedVideoResult> callback);
        void ShowInterstitialAd(string id, Action<InterstitialResult> callback);
        void ShowBannerAd(BannerPos bannerPos);
        void DismissBannerAd();
    }
}
