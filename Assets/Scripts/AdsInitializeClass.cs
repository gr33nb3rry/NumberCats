using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializeClass : MonoBehaviour, IUnityAdsInitializationListener
{
    public bool testMode = false;
    void Awake()
    {
        Advertisement.Initialize("5272511", testMode, this);
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Initialize success");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Initialize failed");
    }

}
