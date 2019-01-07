using System;
using UnityEngine;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Custom
{
    public class CustomRewardAd
    {

        // google ca-app-pub-3940256099942544/1712485313
        // mine ca-app-pub-7731812981209546/1121473812
#if UNITY_ANDROID
        private static string adUnitId = "ca-app-pub-7731812981209546/8512168231";
#elif UNITY_IPHONE
        private static string adUnitId = "ca-app-pub-7731812981209546/1121473812";
#endif

        private RewardBasedVideoAd rewardAd = null;

        private Action onAdOpening;
        private Action onAdClosed;
        private Action onAdLeavingApplication;
        private Action<double,string> onReward;

        private static CustomRewardAd _instance = null;



        public static CustomRewardAd Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomRewardAd();
                }
                return _instance;
            }
        }

        public void Init(Action onAdOpening, Action onAdClosed, Action onAdLeavingApplication)
        {
            rewardAd = RewardBasedVideoAd.Instance;
            this.onAdOpening = onAdOpening;
            this.onAdClosed = onAdClosed;
            this.onAdLeavingApplication = onAdLeavingApplication;
            rewardAd.OnAdOpening += HandleOnAdOpened;
            rewardAd.OnAdClosed += HandleOnAdClosed;
            rewardAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
            rewardAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            rewardAd.OnAdRewarded += HandleRewardBasedVideoRewarded;

            RequestRewardBasedVideo();
        }

        private void RequestRewardBasedVideo()
        {
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().AddTestDevice("ED7472ADCC079DC963B661595DCB4EEF")
                                             .Build();
            // Load the rewarded video ad with the request.
            rewardAd.LoadAd(request, adUnitId);
        }

        public void Show(Action<double, string> rewardCallBack = null)
        {
            if (rewardAd != null)
            {
                if(rewardCallBack != null){
                    this.onReward = rewardCallBack;
                }
                if (rewardAd.IsLoaded())
                {
                    rewardAd.Show();
                }
            }
        }

        public bool GetLoadedStatus(){
            return rewardAd != null && rewardAd.IsLoaded();
        }

        public void DestoryInterstitialAd()
        {

        }

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            MonoBehaviour.print("HandleAdLoaded event received");
        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.LogError("Reward Ad Load Failed"+args.Message);
            AdRequest request = new AdRequest.Builder().AddTestDevice("ED7472ADCC079DC963B661595DCB4EEF")
                                             .Build();
            rewardAd.LoadAd(request, adUnitId);
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
            this.RequestRewardBasedVideo();
        }

        public void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            if (this.onAdLeavingApplication != null)
            {
                this.onAdLeavingApplication();
            }
        }

        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            string type = args.Type;
            double amount = args.Amount;
            Debug.Log("HandleRewardBasedVideoRewarded event received for "
                      + amount.ToString() + " " + type);
            if(this.onReward !=null){
                this.onReward(amount,type);
            }
        }
    }
}
