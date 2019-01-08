using System;
using GoogleMobileAds.Api;
namespace GoogleMobileAds.Custom
{
    internal abstract class CustomAdBase
    {

        protected Action onLoaded;
        protected Action onAdOpening;
        protected Action onAdClosed;
        protected Action onAdLeavingApplication;

        public virtual void HandleOnAdLoaded(object sender, EventArgs args)
        {
            if(onLoaded != null)
            {
                onLoaded();
            }
        }

        public virtual void HandleOnAdOpened(object sender,EventArgs args)
        {
            if(onAdOpening != null)
            {
                onAdOpening();
            }
        }

        public virtual void HandleOnAdClosed(object sender, EventArgs args)
        {
            if(onAdClosed != null)
            {
                onAdClosed();
            }
        }

        public virtual void HandleOnAdLeavingApplication(object sender, EventArgs args)
        {
            if (onAdLeavingApplication != null)
            {
                onAdLeavingApplication();
            }
        }

        public AdRequest GetAdRequest()
        {
            AdRequest adRequest = new AdRequest.Builder()
                                                .AddTestDevice("ED7472ADCC079DC963B661595DCB4EEF").AddTestDevice("44ec14876f1d407677689663b6a5ca27")
                                                .Build();
            return adRequest;
        }

        protected abstract void RequestAd();

        public abstract bool IsLoaded();
    }
}
