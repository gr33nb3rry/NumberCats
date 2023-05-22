using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    static public bool isRewardWatched;
    void Awake()
    {
        LoadAd();
    }

    public void LoadAd()
    {
        Debug.Log("Load ad");
        Advertisement.Load("Rewarded_Android", this);
    }
    public void ShowAd()
    {
        Debug.Log("Show ad");
        Advertisement.Show("Rewarded_Android", this);
        //Time.timeScale = 0;
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("AdLoaded");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("AdShowStart");
        Time.timeScale = 0;
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("AdShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd();
        //isRewardWatched = true;
        Debug.Log("Ad complete");
        Time.timeScale = 1;
    }
}
