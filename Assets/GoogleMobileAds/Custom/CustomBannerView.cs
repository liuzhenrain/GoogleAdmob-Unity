using System;
using UnityEngine;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Custom
{
    internal class CustomBannerView
    {
        // google ca-app-pub-3940256099942544/2934735716
        // ca-app-pub-7731812981209546/4507907257
#if UNITY_ANDROID
        private static string adUnitId = "ca-app-pub-7731812981209546/9222614673";
#elif UNITY_IPHONE
        private static string adUnitId = "ca-app-pub-7731812981209546/4507907257";
#endif

        private BannerView bannerAd = null;

        private Action onAdOpening;
        private Action onAdClosed;
        private Action onAdLeavingApplication;

        private AdSize bannerSize;
        private AdPosition bannerPosition;

        private static CustomBannerView _instance = null;

        public static CustomBannerView Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomBannerView();
                }
                return _instance;
            }
        }

        public void Init(AdSize size,AdPosition position)
        {

            this.bannerSize = size;
            this.bannerPosition = position;
        }

        private void RequestAd(){
            bannerAd = new BannerView(adUnitId, this.bannerSize, this.bannerPosition);
            bannerAd.OnAdFailedToLoad += BannerAd_OnAdFailedToLoad;
            AdRequest request = new AdRequest.Builder().AddTestDevice("ED7472ADCC079DC963B661595DCB4EEF")
                                                 .Build();
            bannerAd.LoadAd(request);
        }

        void BannerAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
        {
            Debug.LogError("BannerView load Ad Failed. " + e.Message);
        }


        public void Show()
        {
            if(bannerAd == null){
                RequestAd();
            }
        }

        public void DestoryInterstitialAd()
        {
            if (bannerAd != null)
            {
                bannerAd.Destroy();
            }
            bannerAd = null;
        }

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            RequestAd();
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            if (this.onAdOpening != null)
            {
                this.onAdOpening();
            }
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            if (this.onAdClosed != null)
            {
                this.onAdClosed();
            }
        }

        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            if (this.onAdLeavingApplication != null)
            {
                this.onAdLeavingApplication();
            }
        }
    }
}
