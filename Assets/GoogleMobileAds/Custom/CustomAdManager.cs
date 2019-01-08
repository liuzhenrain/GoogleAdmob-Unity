using System;
using GoogleMobileAds.Api;
using UnityEngine;
using System.Collections.Generic;

namespace GoogleMobileAds.Custom
{
    public enum AdmobType
    {
        BannerView = 1,
        Interstitial = 2,
        RewardAd = 3
    }

    public class CustomAdManager
    {
        // Google ca-app-pub-3940256099942544~1458002511
        // mine ca-app-pub-7731812981209546~4693053165
#if UNITY_ANDROID
        private static string APPID = "ca-app-pub-7731812981209546~1368885824";
#elif UNITY_IPHONE
        private static string APPID = "ca-app-pub-7731812981209546~4693053165";
#else
        private static string APPID = "unexpected_platform";
#endif
        static bool hasInit = false;
        private static CustomAdManager _instance = null;

        public static CustomAdManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomAdManager();
                }
                return _instance;
            }
        }

        private AdSize bannerSize;
        private AdPosition bannerPosition;

        /// <summary>
        /// 初始化横幅广告
        /// </summary>
        /// <param name="size">Size.</param>
        /// <param name="position">Position.</param>
        public void InitAdmob(AdSize size, AdPosition position)
        {
            if (hasInit == false)
            {
                hasInit = true;
                MobileAds.Initialize(APPID);
            }
            this.bannerSize = size;
            this.bannerPosition = position;
            CustomBannerView.Instance.Init(this.bannerSize, this.bannerPosition);
        }

        /// <summary>
        /// 初始化非BannerView 的广告
        /// </summary>
        /// <param name="admobType">Admob type.</param>
        /// <param name="onAdOpening">On ad opening.</param>
        /// <param name="onAdClosed">On ad closed.</param>
        /// <param name="onAdLeavingApp">On ad leaving app.</param>
        public void InitAdmob(AdmobType admobType, Action onAdOpening, Action onAdClosed, Action onAdLeavingApp)
        {
            if (hasInit == false)
            {
                hasInit = true;
                MobileAds.Initialize(APPID);
            }

            switch (admobType)
            {
                case AdmobType.Interstitial:
                     CustomInterstital.Instance.Init(onAdOpening, onAdClosed, onAdLeavingApp);
                    break;
                case AdmobType.RewardAd:
                    CustomRewardAd.Instance.Init(onAdOpening, onAdClosed, onAdLeavingApp);
                    break;
            }
        }

        /// <summary>
        /// 获取广告是否已经加载的状态,BannerView 不需要查询
        /// </summary>
        /// <returns><c>true</c>, if loaded status was gotten, <c>false</c> otherwise.</returns>
        /// <param name="type">Type.</param>
        public bool GetLoadedStatus(AdmobType type){
            bool result = false;
            switch(type){
                case AdmobType.Interstitial:
                    result = CustomInterstital.Instance.IsLoaded();
                    break;
                case AdmobType.RewardAd:
                    result = CustomRewardAd.Instance.IsLoaded();
                    break;
            }
            return result;
        }

        /// <summary>
        /// 需要先判定是否已经加载（BannerView 除外） 如果是激励视频广告，则需要传入 rewardCallBack 回调
        /// </summary>
        /// <param name="admobType">Admob type.</param>
        /// <param name="rewardCallBack">Reward call back.</param>
        public void ShowAd(AdmobType admobType, Action<double, string> rewardCallBack = null)
        {
            switch (admobType)
            {
                case AdmobType.BannerView:
                    CustomBannerView.Instance.Show();
                    break;
                case AdmobType.Interstitial:
                    CustomInterstital.Instance.Show();
                    break;
                case AdmobType.RewardAd:
                    CustomRewardAd.Instance.Show(rewardCallBack);
                    break;
            }
        }

        public void CloseAd(AdmobType admobType)
        {
            switch (admobType)
            {
                case AdmobType.BannerView:
                    CustomBannerView.Instance.DestoryInterstitialAd();
                    CustomBannerView.Instance.Init(this.bannerSize, this.bannerPosition);
                    break;
            }

        }
    }
}