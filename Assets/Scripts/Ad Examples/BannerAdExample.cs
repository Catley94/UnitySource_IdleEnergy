using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

 /// <summary>
 /// Provides methods to load, show, and hide banner ads as well as
 /// linking these functions to UI controls.
 /// </summary>
public class BannerAdExample : MonoBehaviour
{
    [SerializeField] Button m_LoadBannerButton;
    /// <summary> The button that loads the banner ad </summary>
    public Button LoadBannerButton => m_LoadBannerButton;
    
    [SerializeField] Button m_ShowBannerButton;
    /// <summary> The button that shows the banner ad </summary>
    public Button ShowBannerButton => m_ShowBannerButton;
    
    [SerializeField] Button m_HideBannerButton;
    /// <summary> The button that hides the banner ad </summary>
    public Button HideBannerButton => m_HideBannerButton;
 
    [SerializeField] BannerPosition m_BannerPosition = BannerPosition.BOTTOM_CENTER;
    /// <summary> The screen position of the banner ads </summary>
    public BannerPosition BannerPosition => m_BannerPosition;
 
    [SerializeField] string m_AndroidAdUnitId = "Banner_Android";
    /// <summary> The ad unit Id for Android </summary>
    public string AndroidAdUnitId => m_AndroidAdUnitId;
    
    [SerializeField] string m_iOSAdUnitId = "Banner_iOS";
    /// <summary> The ad unit Id for iOS </summary>
    public string iOSAdUnitId => m_iOSAdUnitId;
    
    private string m_AdUnitId;
 
    public void Awake()
    {
        // Android Ad Unit Ids are the default. If the platform is iOS, then apply the corresponding Ad Unit Id.
        m_AdUnitId = m_AndroidAdUnitId;
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            m_AdUnitId = m_iOSAdUnitId;
        }

        // Disable the button until an ad is ready to show
        m_LoadBannerButton.interactable = false;
        m_ShowBannerButton.interactable = false;
        m_HideBannerButton.interactable = false;
 
        // Set the banner position:
        Advertisement.Banner.SetPosition(m_BannerPosition);
    }
    
    /// <summary>
    /// Initialize the class
    /// </summary>
    public void Initialize()
    {
        m_LoadBannerButton.onClick.AddListener(LoadBanner);
        m_LoadBannerButton.interactable = true;
    }
    
    private void OnDestroy()
    {
        // Clean up the listeners
        m_LoadBannerButton.onClick.RemoveAllListeners();
        m_ShowBannerButton.onClick.RemoveAllListeners();
        m_HideBannerButton.onClick.RemoveAllListeners();
    }
 
    /// <summary>
    /// Load the banner ad
    /// </summary>
    private void LoadBanner()
    {
        // Define callbacks for when banner loading completes
        var options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
 
        // Load the Ad Unit with banner content
        Advertisement.Banner.Load(m_AdUnitId, options);
    }
 
    /// <summary>
    /// Handler for when a banner ad has finished loading
    /// </summary>
    private void OnBannerLoaded()
    {
        // Configure the Show Banner button to call the ShowBannerAd() method when clicked
        m_ShowBannerButton.onClick.AddListener(ShowBannerAd);
        // Configure the Hide Banner button to call the HideBannerAd() method when clicked
        m_HideBannerButton.onClick.AddListener(HideBannerAd);
 
        // Set button states
        m_ShowBannerButton.interactable = true;
        m_HideBannerButton.interactable = false;
        m_LoadBannerButton.interactable = false;
    }
 
    /// <summary>
    /// Handler for when there is an error loading a banner
    /// </summary>
    /// <param name="message"></param>
    private void OnBannerError(string message)
    {
        Debug.LogError($"Banner Error: {message}");
    }
 
    /// <summary>
    /// Show a banner ad on the screen
    /// </summary>
    private void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events
        var options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };
 
        // Show the loaded Banner Ad Unit
        Advertisement.Banner.Show(m_AdUnitId, options);
    }
 
    /// <summary>
    /// Hide the banner ad that is currently being shown on the screen
    /// </summary>
    private void HideBannerAd()
    {
        Advertisement.Banner.Hide();
    }

    /// <summary>
    /// Handler for when the user clicks on the banner
    /// </summary>
    private void OnBannerClicked()
    {
        Debug.Log($"Banner Clicked");
    }
    
    /// <summary>
    /// Handler for when the banner is successfully shown
    /// </summary>
    private void OnBannerShown() 
    {         
        m_ShowBannerButton.interactable = false;
        m_HideBannerButton.interactable = true;
    }

    /// <summary>
    /// Handler for when the banner is successfully hidden
    /// </summary>
    private void OnBannerHidden()
    {
        m_ShowBannerButton.interactable = true;
        m_HideBannerButton.interactable = false;
    }
}