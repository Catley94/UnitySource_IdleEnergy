// Copyright (c) Supernova Technologies LLC
using Nova.Editor.Utilities;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

namespace Nova.Editor
{
    internal static class AddObjectMenus
    {
        [MenuItem("GameObject/Nova/UIBlock 2D", false, 8)]
        private static void AddUIBlock2D(MenuCommand menuCommand)
        {
            Add<UIBlock2D, Tools.UIBlockTool>(menuCommand, "UIBlock2D").CopyToDataStore();
        }

        [MenuItem("GameObject/Nova/TextBlock", false, 9)]
        private static void AddTextBlock(MenuCommand menuCommand)
        {
            Add<TextBlock, Tools.UIBlockTool>(menuCommand, "TextBlock").CopyToDataStore();
        }

        [MenuItem("GameObject/Nova/UIBlock 3D", false, 10)]
        private static void AddUIBlock3D(MenuCommand menuCommand)
        {
            Add<UIBlock3D, Tools.UIBlockTool>(menuCommand, "UIBlock3D").CopyToDataStore();
        }

        [MenuItem("GameObject/Nova/UIBlock", false, 11)]
        private static void AddUIBlock(MenuCommand menuCommand)
        {
            Add<UIBlock, Tools.UIBlockTool>(menuCommand, "UIBlock").CopyToDataStore();
        }

        private static T Add<T, TTool>(MenuCommand menuCommand, string name = "GameObject") where T : Component where TTool : UnityEditor.EditorTools.EditorTool
        {
            // Create a custom game object
            GameObject go = new GameObject(name);

            if (menuCommand.context is GameObject parent)
            {
                GameObjectUtility.SetParentAndAlign(go, parent);
            }
            else
            {
                UnityEditor.SceneManagement.StageUtility.PlaceGameObjectInCurrentStage(go);
            }

            T addedComponent = go.AddComponent<T>();

            Preset[] presets = Preset.GetDefaultPresetsForObject(addedComponent);

            if (presets != null && presets.Length > 0 && presets[0] != null)
            {
                presets[0].ApplyTo(addedComponent);
            }

            // Register the creation in the undo system
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            Selection.activeGameObject = go;

            EditorApplication.delayCall += () =>
            {
                if (!ActiveEditorUtils.TryGetActiveEditorTargetType<T>(out _))
                {
                    // editor window not active, which can happen if the inspector
                    // is locked to another object or if the single-frame delay
                    // is insufficient (thanks Unity), so the tool can't be set active.
                    return;
                }

                UnityEditor.EditorTools.ToolManager.SetActiveTool<TTool>();
            };

            return addedComponent;
        }
    }
}

