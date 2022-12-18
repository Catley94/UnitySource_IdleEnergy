using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Automatically selects the <see cref="AdsController"/> class when the sample scene is open.
/// This is not necessary for a project in production using the <see cref="AdsController"/>.
/// It is provided here for convenience, as selecting <see cref="AdsController"/> in the hierarchy allows the Game IDs
/// to be automatically copied from the project settings into the <see cref="AdsController"/> inspector.
/// </summary>
[InitializeOnLoad]
static class AdsControllerAutoSelector
{
    static AdsControllerAutoSelector()
    {
        SelectAdsControllerInHierarchy();
        EditorSceneManager.sceneOpened += OnSceneLoaded; 
    }

    static void OnSceneLoaded(Scene current, OpenSceneMode mode)
    {
        SelectAdsControllerInHierarchy(); 
    }

    static void SelectAdsControllerInHierarchy()
    {
        if (EditorSceneManager.GetActiveScene().name == "SampleScene")
        {
            var adsController = GameObject.FindObjectOfType<AdsController>();
            if (adsController != null)
            {
                Selection.objects = new Object[] {adsController.gameObject};
            }
        }
        
    }
}
