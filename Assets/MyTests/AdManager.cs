using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AdManager : MonoBehaviour, INonSkippableVideoAdListener, IInterstitialAdListener, IRewardedVideoAdListener{
#if UNITY_EDITOR && !UNITY_ANDROID && !UNITY_IPHONE
		string appKey = "";
#elif UNITY_ANDROID
    string appKey = "fee50c333ff3825fd6ad6d38cff78154de3025546d47a84f";
#elif UNITY_IPHONE
		string appKey = "4b46ef930cd37cf11da84ae4d41019abb7234d5bbce3f000";
#else
		string appKey = "";
#endif
    // Use this for initialization
    public Toggle testingToggle;
    public Button interstitioalBtn;
    public Button rewardBtn;
    public Button nonSkippableBtn;

    void Awake()
    {
        init();
    }
    void init () {
        Appodeal.setAutoCache(Appodeal.INTERSTITIAL, true);
        Appodeal.setAutoCache(Appodeal.REWARDED_VIDEO, true);
        Appodeal.setAutoCache(Appodeal.NON_SKIPPABLE_VIDEO, true);
        Appodeal.initialize(appKey, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO | Appodeal.NON_SKIPPABLE_VIDEO);

        Appodeal.setInterstitialCallbacks(this);
        Appodeal.setRewardedVideoCallbacks(this);
        Appodeal.setNonSkippableVideoCallbacks(this);

        testingToggle.onValueChanged.AddListener(ChangeTestingToggle);
        interstitioalBtn.onClick.AddListener(showInterstitial);
        rewardBtn.onClick.AddListener(showRewardedVideo);
        nonSkippableBtn.onClick.AddListener(showNonSkippableVideo);
    }
	
    public void ChangeTestingToggle(bool isOn)
    {
        Debug.Log(isOn);
        Appodeal.setTesting(isOn);
        init();
    }
    public void showInterstitial()
    {
        if (Appodeal.isLoaded(Appodeal.INTERSTITIAL))
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
        else
        {
            interstitioalBtn.interactable = false;
        }
    }
    public void showRewardedVideo()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        else
        {
            rewardBtn.interactable = false;
        }
    }
    public void showNonSkippableVideo()
    {
        if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO))
        {
            Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
        }
        else
        {
            nonSkippableBtn.interactable = false;
        }
    }
    void CheckBtnAfterShowing(Button btn, int videoType)
    {
        if (Appodeal.canShow(videoType))
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
        }
    }
    public void onNonSkippableVideoLoaded()    { CheckBtnAfterShowing(nonSkippableBtn, Appodeal.NON_SKIPPABLE_VIDEO); }
    public void onNonSkippableVideoFailedToLoad()    { CheckBtnAfterShowing(nonSkippableBtn, Appodeal.NON_SKIPPABLE_VIDEO); }
    public void onNonSkippableVideoShown()    { CheckBtnAfterShowing(nonSkippableBtn, Appodeal.NON_SKIPPABLE_VIDEO); }
    public void onNonSkippableVideoFinished()    { CheckBtnAfterShowing(nonSkippableBtn, Appodeal.NON_SKIPPABLE_VIDEO); }
    public void onNonSkippableVideoClosed(bool finished)    { CheckBtnAfterShowing(nonSkippableBtn, Appodeal.NON_SKIPPABLE_VIDEO); }

    public void onInterstitialLoaded(bool isPrecache)    {        CheckBtnAfterShowing(interstitioalBtn, Appodeal.INTERSTITIAL);    }
    public void onInterstitialFailedToLoad() { CheckBtnAfterShowing(interstitioalBtn, Appodeal.INTERSTITIAL); }
    public void onInterstitialShown()    {        CheckBtnAfterShowing(interstitioalBtn, Appodeal.INTERSTITIAL);    }
    public void onInterstitialClicked() { CheckBtnAfterShowing(interstitioalBtn, Appodeal.INTERSTITIAL); }
    public void onInterstitialClosed() { CheckBtnAfterShowing(interstitioalBtn, Appodeal.INTERSTITIAL); }

    public void onRewardedVideoLoaded(bool isPrecache) { CheckBtnAfterShowing(rewardBtn, Appodeal.REWARDED_VIDEO); }
    public void onRewardedVideoFailedToLoad() { CheckBtnAfterShowing(rewardBtn, Appodeal.REWARDED_VIDEO); }
    public void onRewardedVideoShown() { CheckBtnAfterShowing(rewardBtn, Appodeal.REWARDED_VIDEO); }
    public void onRewardedVideoClosed(bool isFinished) { CheckBtnAfterShowing(rewardBtn, Appodeal.REWARDED_VIDEO); }
    public void onRewardedVideoFinished(int amount, string name) { CheckBtnAfterShowing(rewardBtn, Appodeal.REWARDED_VIDEO); }
}
