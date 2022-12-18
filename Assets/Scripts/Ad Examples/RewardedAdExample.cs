using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

/// <summary>
/// Provides methods to load and show rewarded ads as well as
/// linking these functions to UI controls
/// </summary>
public class RewardedAdExample : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button _loadAdButton;
    /// <summary> The button to load an ad </summary>
    public Button LoadAdButton => _loadAdButton;
    
    [SerializeField] Button m_ShowAdButton;
    /// <summary> The button to show an ad </summary>
    public Button ShowAdButton => m_ShowAdButton;
    
    [SerializeField] TextMeshProUGUI m_RewardText;
    /// <summary> The text that displays the user's current reward count  </summary>
    public TextMeshProUGUI RewardText => m_RewardText;
    
    [SerializeField] string m_AndroidAdUnitId = "Rewarded_Android";
    /// <summary> The ad unit Id for Android </summary>
    public string AndroidAdUnitId => m_AndroidAdUnitId;
    
    [SerializeField] string m_iOSAdUnitId = "Rewarded_iOS";
    /// <summary> The ad unit Id for iOS </summary>
    public string iOSAdUnitId => m_iOSAdUnitId;

    private string m_AdUnitId;
    private int m_RewardAmount;
    
    private void Awake()
    {
        // Android Ad Unit Ids are the default. If the platform is iOS, then apply the corresponding Ad Unit Id.
        m_AdUnitId = m_AndroidAdUnitId;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            m_AdUnitId = m_iOSAdUnitId;
        }

        //Disable the button until the ad is ready to show:
        m_ShowAdButton.interactable = false;
        _loadAdButton.interactable = false;
    }

    /// <summary>
    /// Initialize the class
    /// </summary>
    public void Initialize()
    {   
        _loadAdButton.onClick.AddListener(LoadAd);
        m_ShowAdButton.onClick.AddListener(ShowAd);

        _loadAdButton.interactable = true;
        m_RewardAmount = 0;
    }
    
    void OnDestroy()
    {
        // Clean up the button listeners:
        m_ShowAdButton.onClick.RemoveAllListeners();
        _loadAdButton.onClick.RemoveAllListeners();
    }
 
    /// <summary>
    /// Load a rewarded ad
    /// </summary>
    private void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
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
        _loadAdButton.interactable = false;
        m_ShowAdButton.interactable = true;
    }
    
    /// <summary>
    /// Handler for when a Unity ad fails to load
    /// </summary>
    /// <param name="adUnitId">The ad unit ID for the ad</param>
    /// <param name="error">The error that prevented the ad from loading</param>
    /// <param name="message">The message accompanying the error</param>
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    #endregion
 
    /// <summary>
    /// Shows a rewarded ad
    /// </summary>
    private void ShowAd()
    {
        // Disable the button:
        m_ShowAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(m_AdUnitId, this);
    }

    #region IUnityAdsShowListener

    /// <summary>
    /// Handler for when an add finishes showing
    /// </summary>
    /// <param name="adUnitId"></param>
    /// <param name="showCompletionState"></param>
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(m_AdUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            m_RewardAmount += 1;
            m_RewardText.text = $"x{m_RewardAmount}";
            // Load another ad:
            Advertisement.Load(m_AdUnitId, this);
        }
    }
 
    /// <summary>
    /// Handler for when showing an ad fails
    /// </summary>
    /// <param name="adUnitId">The ad unit ID for the ad</param>
    /// <param name="error">The error that prevented the ad from loading</param>
    /// <param name="message">The message accompanying the error</param>
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
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