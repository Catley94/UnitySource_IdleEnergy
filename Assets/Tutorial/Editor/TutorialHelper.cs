using UnityEditor;
using UnityEngine;

/// <summary>
/// Methods to support the Ads Template Tutorial 
/// </summary>
[CreateAssetMenu(fileName = nameof(TutorialHelper), menuName = "Tutorials/Tutorial Helper")]
public class TutorialHelper : ScriptableObject
{
    /// <summary>
    /// Open the Project Settings window, open to the Ads section
    /// </summary>
    public void OpenProjectSettings()
    {
        SettingsService.OpenProjectSettings("Project/Services/Ads");
    }

    /// <summary>
    /// Open the Unity Ads Home page in a web browser
    /// </summary>
    public void OpenAdsHomePageInBrowser()
    {
        Application.OpenURL("https://docs.unity.com/ads/UnityAdsHome.html");
    }
}
