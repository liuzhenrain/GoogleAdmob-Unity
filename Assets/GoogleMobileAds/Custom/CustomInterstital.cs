using System;
using UnityEngine;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Custom
{
    internal class CustomInterstital
    {

        // google ca-app-pub-3940256099942544/4411468910
        // mine ca-app-pub-7731812981209546/4690905931
#if UNITY_ANDROID
        private static string adUnitId = "ca-app-pub-7731812981209546/1532537356";
#elif UNITY_IPHONE
        private static string adUnitId = "ca-app-pub-7731812981209546/4690905931";
#endif

        private static InterstitialAd interstitialAd = null;

        private Action onAdOpening;
        private Action onAdClosed;
        private Action onAdLeavingApplication;

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

            this.RequestInterstitial();

        }

        private void RequestInterstitial(){
            AdRequest request = new AdRequest.Builder()
                                            .AddTestDevice("ED7472ADCC079DC963B661595DCB4EEF")
                                             .Build();
            interstitialAd.LoadAd(request);
        }

        public void Show(){
            if(interstitialAd != null){
                if(interstitialAd.IsLoaded()){
                    interstitialAd.Show();
                }
            }
        }

        public bool GetLoadedStatus()
        {
            return interstitialAd != null && interstitialAd.IsLoaded();
        }

        public void DestoryInterstitialAd(){
            if(interstitialAd != null){
                interstitialAd.Destroy();
            }
        }

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.LogError("Interstitial load Ad failed. "+args.Message);
            RequestInterstitial();
        }

        public void HandleOnAdOpened(object sender, EventArgs args)
        {
            if(this.onAdOpening != null){
                this.onAdOpening();
            }
        }

        public void HandleOnAdClosed(object sender, EventArgs args)
        {
            if(this.onAdClosed !=null){
                this.onAdClosed();
            }
            interstitialAd.Destroy();
            Init(this.onAdOpening, this.onAdClosed, this.onAdLeavingApplication);
        }

        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            if(this.onAdLeavingApplication != null){
                this.onAdLeavingApplication();
            }
        }
    }
}
