using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Custom;
using GoogleMobileAds.Api;

public class CustomAdTest : MonoBehaviour {

    public GameObject banner;
    public GameObject interstitial;
    public GameObject reward;

    // Banner
    private Button openBanner;
    private Button closeBanner;

    // Interstitial
    private Button openInterstitial;
    private Button closeInterstitial;

    // Reward
    private Button openReward;
    private Button closeReward;
    private Text rewardTips;


	// Use this for initialization
	void Start () {
        if(banner!=null){
            CustomAdManager.Instance.InitAdmob(AdSize.Banner, AdPosition.Bottom);
            openBanner = banner.transform.Find("Open").GetComponent<Button>();
            closeBanner = banner.transform.Find("Close").GetComponent<Button>();
            openBanner.onClick.AddListener(() => {
                    CustomAdManager.Instance.ShowAd(AdmobType.BannerView);
            });
            closeBanner.onClick.AddListener(() =>
            {
                CustomAdManager.Instance.CloseAd(AdmobType.BannerView);
            });
        }
        if (interstitial != null)
        {
            CustomAdManager.Instance.InitAdmob(AdmobType.Interstitial, () => { Debug.Log("Interstitial Opening"); }, ()=> { Debug.Log("InterstitialAd Closeed"); }, null);
            openInterstitial = interstitial.transform.Find("Open").GetComponent<Button>();
            closeInterstitial = interstitial.transform.Find("Close").GetComponent<Button>();
            openInterstitial.onClick.AddListener(() => {
                if (CustomAdManager.Instance.GetLoadedStatus(AdmobType.Interstitial))
                    CustomAdManager.Instance.ShowAd(AdmobType.Interstitial);
                else
                    Debug.Log("插屏广告还未加载完成");
            });
            closeInterstitial.onClick.AddListener(() =>
            {
                CustomAdManager.Instance.CloseAd(AdmobType.Interstitial);
            });
        }
        if (reward != null)
        {
            openReward = reward.transform.Find("Open").GetComponent<Button>();
            closeReward = reward.transform.Find("Close").GetComponent<Button>();
            openReward.onClick.AddListener(() => {
                if (CustomAdManager.Instance.GetLoadedStatus(AdmobType.RewardAd))
                    CustomAdManager.Instance.ShowAd(AdmobType.RewardAd,(double amount,string type)=>{
                        rewardTips.text += string.Format("Ammount:{0},type:{1}\n", amount, type);
                });
                else
                    Debug.Log("激励视频广告还未加载完成");
            });
            closeReward.onClick.AddListener(() =>
            {
                CustomAdManager.Instance.CloseAd(AdmobType.RewardAd);
            });
            rewardTips = reward.transform.Find("RewardTip").GetComponent<Text>();
            CustomAdManager.Instance.InitAdmob(AdmobType.RewardAd, () => { Debug.Log("RewardAd Opening"); }, () => { Debug.Log("RewardAd Closeed"); }, null);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
