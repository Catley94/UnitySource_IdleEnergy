using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

/// <summary>
/// Provides methods to load and show interstitial ads as well as
/// linking these functions to UI controls
/// </summary>
public class InterstitialAdExample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private Button m_LoadInterstitialAdButton;
    /// <summary> The button that loads the ad </summary>
    public Button LoadInterstitialAdButton => m_LoadInterstitialAdButton;
    
    [SerializeField] private Button m_ShowInterstitialAdButton;
    /// <summary> The button that shows the ad </summary>
    public Button ShowInterstitialAdButton => m_ShowInterstitialAdButton;
    
    [SerializeField] string m_AndroidAdUnitId = "Interstitial_Android";
    /// <summary> The ad unit Id for Android </summary>
    public string AndroidAdUnitId => m_AndroidAdUnitId;
    
    [SerializeField] string m_iOSAdUnitId = "Interstitial_iOS";
    /// <summary> The ad unit Id for iOS </summary>
    public string iOSAdUnitId => m_iOSAdUnitId;
    string m_AdUnitId;

    private void Awake()
    {
        // Android Ad Unit Ids are the default. If the platform is iOS, then apply the corresponding Ad Unit Id.
        m_AdUnitId = m_AndroidAdUnitId;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            m_AdUnitId = m_iOSAdUnitId;
        }
        
        // Disable buttons
        m_LoadInterstitialAdButton.interactable = false;
        m_ShowInterstitialAdButton.interactable = false;
    }

    /// <summary>
    /// Initialize the class
    /// </summary>
    public void Initialize()
    {
        m_LoadInterstitialAdButton.onClick.AddListener(LoadAd);
        m_ShowInterstitialAdButton.onClick.AddListener(ShowAd);

        m_LoadInterstitialAdButton.interactable = true;
    }

    private void OnDestroy()
    {
        m_LoadInterstitialAdButton.onClick.RemoveListener(LoadAd);
        m_ShowInterstitialAdButton.onClick.RemoveListener(ShowAd);
    }

    private void LoadAd()
    {
        Debug.Log("Loading Ad: " + m_AdUnitId);
        Advertisement.Load(m_AdUnitId, this);
    }
    
    #region IUnityAdsLoadListener

    /// <summary>
    /// Handler for when an ad is successfully loaded
    /// </summary>
    /// <param name="adUnitId">The ad unit ID for the loaded ad</param>
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
        m_LoadInterstitialAdButton.interactable = false;
        m_ShowInterstitialAdButton.interactable = true;
    }
 
    /// <summary>
    /// Handler for when a Unity ad fails to load
    /// </summary>
    /// <param name="adUnitId">The ad unit ID for the ad</param>
    /// <param name="error">The error that prevented the ad from loading</param>
    /// <param name="message">The message accompanying the error</param>
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    #endregion

    /// <summary>
    /// Show an ad on the screen
    /// </summary>
    private void ShowAd()
    {
        Debug.Log("Showing Ad: " + m_AdUnitId);
        Advertisement.Show(m_AdUnitId, this);
    }
    #region IUnityAdsShowListener

    /// <summary>
    /// Handler for when an add finishes showing
    /// </summary>
    /// <param name="adUnitId"></param>
    /// <param name="showCompletionState"></param>
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState) { }

    /// <summary>
    /// Handler for when showing an ad fails
    /// </summary>
    /// <param name="adUnitId">The ad unit ID for the ad</param>
    /// <param name="error">The error that prevented the ad from loading</param>
    /// <param name="message">The message accompanying the error</param>
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }
 
    /// <summary>
    /// Handler for when an ad starts showing
    /// </summary>
    /// <param name="adUnitId"></param>
    public void OnUnityAdsShowStart(string adUnitId) { }
    
    /// <summary>
    /// Handler for when the user clicks/taps on an ad
    /// </summary>
    /// <param name="adUnitId"></param>
    public void OnUnityAdsShowClick(string adUnitId) { }
    
    #endregion
}