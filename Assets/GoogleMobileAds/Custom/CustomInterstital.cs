using System;
using UnityEngine;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Custom
{
    internal class CustomInterstital:CustomAdBase
    {

        // google ca-app-pub-3940256099942544/4411468910
        // mine ca-app-pub-7731812981209546/4690905931
#if UNITY_ANDROID
        private static string adUnitId = "ca-app-pub-7731812981209546/1532537356";
#elif UNITY_IPHONE
        private static string adUnitId = "ca-app-pub-7731812981209546/4690905931";
#endif

        private static InterstitialAd interstitialAd = null;


        private static CustomInterstital _instance = null;

        public static CustomInterstital Instance
        {
            get
            {
                if(_instance == null){
                    _instance = new CustomInterstital();
                }
                return _instance;
            }
        }

        public void Init(Action onAdOpening,Action onAdClosed,Action onAdLeavingApplication){
            interstitialAd = new InterstitialAd(adUnitId);
            this.onAdOpening = onAdOpening;
            this.onAdClosed = onAdClosed;
            this.onAdLeavingApplication = onAdLeavingApplication;
            interstitialAd.OnAdOpening += HandleOnAdOpened;
            interstitialAd.OnAdClosed += HandleOnAdClosed;
            interstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
            interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;

            RequestAd();

        }

        public void Show(){
            if(interstitialAd != null){
                if(interstitialAd.IsLoaded()){
                    interstitialAd.Show();
                }
            }
        }

        public void DestoryInterstitialAd(){
            if(interstitialAd != null){
                interstitialAd.Destroy();
            }
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.LogError("Interstitial load Ad failed. "+args.Message);
            RequestAd();
        }


        public override void HandleOnAdClosed(object sender, EventArgs args)
        {
            base.HandleOnAdClosed(sender,args);
            interstitialAd.Destroy();
            Init(onAdOpening, onAdClosed, onAdLeavingApplication);
        }

        protected override void RequestAd()
        {
            interstitialAd.LoadAd(GetAdRequest());
        }

        public override bool IsLoaded()
        {
            return interstitialAd != null && interstitialAd.IsLoaded();
        }
    }
}
