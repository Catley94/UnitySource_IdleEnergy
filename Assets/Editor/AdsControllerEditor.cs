using System;
using UnityEditor;
using UnityEditor.Advertisements;
using UnityEditor.Experimental;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// This retrieves the Game Id's for both iOS and Android from the Project Settings in the Unity Editor. Once the
/// Game Id's have been retrieved they are then applied to the Ads Controller automatically.
/// </summary>
[CustomEditor(typeof(AdsController))]
public class AdsControllerEditor : Editor
{
    private const string k_UnityConnectSettingsPath = "ProjectSettings/UnityConnectSettings.asset";
    private string m_AndroidGameId;
    private string m_IosGameId;
    private bool m_IsTestMode;
    private SerializedObject m_ConnectSettings;
    private AdsController m_AdsController;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (m_ConnectSettings != null && m_ConnectSettings.UpdateIfRequiredOrScript())
        {
            SetAdvertisementSettings();
        }
    }

    private void OnEnable()
    {
        var connectSettingsRes = EditorResources.Load<Object>(k_UnityConnectSettingsPath);

        if (connectSettingsRes == null)
        {
            throw new NullReferenceException(
                $"The Unity Connection Settings asset cannot be found at path {k_UnityConnectSettingsPath}. Please ensure that the Services is enabled correctly within the Project Settings.");
        }
        m_ConnectSettings = new SerializedObject(connectSettingsRes);
        m_AdsController = (AdsController)target;
        SetAdvertisementSettings();
    }

    private void SetAdvertisementSettings()
    {
        bool previousTestMode = m_AdsController.TestMode;
        string previousAndroidId = m_AdsController.AndroidGameId;
        string previousIosId = m_AdsController.IosGameId;
        
        m_AdsController.TestMode = AdvertisementSettings.testMode && AdvertisementSettings.enabled;
        m_AdsController.AndroidGameId = AdvertisementSettings.GetGameId(RuntimePlatform.Android);
        m_AdsController.IosGameId = AdvertisementSettings.GetGameId(RuntimePlatform.IPhonePlayer);

        if (previousTestMode != m_AdsController.TestMode || previousAndroidId != m_AdsController.AndroidGameId || previousIosId != m_AdsController.IosGameId)
        {
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
