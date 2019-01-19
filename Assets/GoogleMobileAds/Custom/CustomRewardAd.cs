using System;
using UnityEngine;
using GoogleMobileAds.Api;

namespace GoogleMobileAds.Custom
{
    internal class CustomRewardAd:CustomAdBase
    {

        // google ca-app-pub-3940256099942544/1712485313
        // mine ca-app-pub-7731812981209546/1121473812
#if UNITY_ANDROID
        private static string adUnitId = "ca-app-pub-7731812981209546/8512168231";
#elif UNITY_IPHONE
        private static string adUnitId = "ca-app-pub-7731812981209546/1121473812";
#endif

        private RewardBasedVideoAd rewardAd = null;

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
            base.onAdClosed = onAdClosed;
            base.onAdLeavingApplication = onAdLeavingApplication;
            rewardAd.OnAdOpening += HandleOnAdOpened;
            rewardAd.OnAdClosed += HandleOnAdClosed;
            rewardAd.OnAdLeavingApplication += HandleOnAdLeavingApplication;
            rewardAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            rewardAd.OnAdRewarded += HandleRewardBasedVideoRewarded;

            RequestAd();
        }

        public void Show(Action<double, string> rewardCallBack)
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

        public void DestoryInterstitialAd()
        {

        }

        public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.LogError("Reward Ad Load Failed"+args.Message);
            rewardAd.LoadAd(GetAdRequest(), adUnitId);
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

        public override void HandleOnAdClosed(object sender, EventArgs args)
        {
            base.HandleOnAdClosed(sender, args);
            RequestAd();
        }

        public override bool IsLoaded()
        {
            return rewardAd != null && rewardAd.IsLoaded();
        }

        protected override void RequestAd()
        {
            rewardAd.LoadAd(GetAdRequest(), adUnitId);
        }
    }
}
