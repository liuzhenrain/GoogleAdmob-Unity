using System;
namespace Application
{
    internal abstract class CustomAdBase
    {

        protected Action onLoaded;
        protected Action onOpening;
        protected Action onClosed;

        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            if(onLoaded != null)
            {
                onLoaded();
            }
        }

        public void HandleOnAdOpening(object sender,EventArgs args)
        {
            if(onOpening != null)
            {
                onOpening();
            }
        }

        public void HandleOnCloseed(object sender, EventArgs args)
        {
            if(onClosed != null)
            {
                onClosed();
            }
        }

        public abstract bool IsLoaded();
    }
}
