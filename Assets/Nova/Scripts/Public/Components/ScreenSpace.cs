// Copyright (c) Supernova Technologies LLC
using Nova.Compat;
using Nova.Internal.Core;
using Nova.Internal.Rendering;
using Nova.Internal.Utilities;
using System;
using System.Reflection;
using UnityEngine;

namespace Nova
{
    /// <summary>
    /// Configures a <see cref="UIBlock"/> hierarchy to render in screen-space.
    /// </summary>
    [RequireComponent(typeof(UIBlock))]
    [RequireComponent(typeof(SortGroup))]
    [DisallowMultipleComponent]
    [AddComponentMenu("Nova/Screen Space")]
    [ExecuteAlways]
    [HelpURL("https://novaui.io/manual/ScreenSpace.html")]
    public class ScreenSpace : MonoBehaviour, IScreenSpace
    {
        #region Public
        /// <summary>
        /// Configures how to resize a <see cref="Nova.UIBlock"/> to fill a camera's viewport.
        /// </summary>
        public enum FillMode
        {
            /// <summary>
            /// Maintain the width of the <see cref="UIBlock"/> at the <see cref="ReferenceResolution">ReferenceResolution</see> width, 
            /// adjusting the height to match the camera's aspect ratio.
            /// </summary>
            FixedWidth,
            /// <summary>
            /// Maintain the height of the <see cref="UIBlock"/> at the <see cref="ReferenceResolution">ReferenceResolution</see> height, 
            /// adjusting the width to match the camera's aspect ratio.
            /// </summary>
            FixedHeight,
            /// <summary>
            /// Set the <see cref="UIBlock">UIBlock's</see> <see cref="UIBlock.Size">Size</see> to the pixel-dimensions of the camera.
            /// </summary>
            MatchCameraResolution,
            /// <summary>
            /// Do not modify the <see cref="UIBlock">UIBlock's</see> <see cref="UIBlock.Size">Size</see> or scale.
            /// This can be used if you want to implement a custom resize behavior.
            /// </summary>
            Manual
        }

        /// <summary>
        /// The <see cref="Nova.UIBlock"/> on <c>this.gameObject</c> that will be positioned and sized to
        /// fill the <see cref="TargetCamera">TargetCamera</see>.
        /// </summary>
        public UIBlock UIBlock
        {
            get
            {
                if (uiBlock == null)
                {
                    uiBlock = GetComponent<UIBlock>();
                }

                return uiBlock;
            }
        }

        /// <summary>
        /// The resolution to use as a reference when resizing the root UIBlock to match the camera's aspect ratio
        /// when <see cref="Mode">Mode</see> is set to <see cref="FillMode.FixedHeight"/> or <see cref="FillMode.FixedWidth"/>.
        /// </summary>
        public Vector2 ReferenceResolution
        {
            get => referenceResolution;
            set => referenceResolution = value;
        }

        /// <summary>
        /// The camera to render to.
        /// </summary>
        public Camera TargetCamera
        {
            get => targetCamera;
            set
            {
                if (targetCamera == value)
                {
                    return;
                }

                targetCamera = value;
                RegisterOrUpdate();
            }
        }

        /// <summary>
        /// The <see cref="FillMode">FillMode</see> used for resizing <see cref="UIBlock">UIBlock</see> to fill
        /// <see cref="TargetCamera">TargetCamera</see>.
        /// </summary>
        public FillMode Mode
        {
            get => fillMode;
            set
            {
                fillMode = value;
            }
        }

        /// <summary>
        /// The distance in front of the camera at which to render the Nova content.
        /// </summary>
        public float PlaneDistance
        {
            get => planeDistance;
            set => planeDistance = value;
        }
        #endregion

        #region Internal
        [SerializeField]
        private Camera targetCamera = null;
        [SerializeField]
        private Vector2 referenceResolution = new Vector2(1920, 1080);
        [SerializeField]
        private FillMode fillMode = FillMode.FixedWidth;
        [SerializeField]
        private float planeDistance = 1f;
        [NonSerialized]
        private UIBlock uiBlock = null;
        [NonSerialized]
        private SortGroup sortGroup = null;
        [SerializeField]
        [HideInInspector]
        private bool haveConfiguredSortGroup = false;

        int IScreenSpace.CameraID => targetCamera.GetInstanceID();

        void IScreenSpace.Update() => UpdatePositionAndScale();

        private void OnEnable()
        {
            UIBlock.EnsureRegistration();

            if (sortGroup == null)
            {
                sortGroup = GetComponent<SortGroup>();
            }

            if (!haveConfiguredSortGroup)
            {
                haveConfiguredSortGroup = true;
                sortGroup.RenderOverOpaqueGeometry = true;
                sortGroup.RenderQueue = ConditionalConstants.DefaultOverlayRenderQueue;
            }

            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }

            RegisterOrUpdate();
        }

        private void OnDisable()
        {
            Unregister(uiBlock.ID);

            if (NovaApplication.IsEditor)
            {
                DataStoreID id = UIBlock.ID;
                NovaApplication.EditorDelayCall += () =>
                {
                    if (this == null)
                    {
                        Unregister(id);
                    }
                };
            }
        }

        internal void RegisterOrUpdate()
        {
            if (!UIBlock.Activated || !enabled)
            {
                return;
            }

            if (targetCamera != null)
            {
                RenderingDataStore.Instance.RegisterOrUpdateScreenSpace(UIBlock.ID, this);
            }
            else
            {
                Unregister(UIBlock.ID);
            }
        }

        private void Unregister(DataStoreID id)
        {
            if (RenderingDataStore.Instance == null)
            {
                return;
            }

            RenderingDataStore.Instance.UnregisterScreenSpaceRoot(id, this);
        }

        private void UpdatePositionAndScale(bool updatePositionUsingLayouts = false)
        {
            if (UIBlock == null || targetCamera == null)
            {
                return;
            }

            ref Layout layout = ref uiBlock.Layout;

            // Positioning below assumes center alignment, so just ensure that's the configuration here
            layout.Alignment = Alignment.Center;
            // Disable auto size
            layout.AutoSize = AutoSize.None;
            // Disable aspect ratio
            layout.AspectRatioAxis = Axis.None;
            layout.SizeMinMax = MinMax3.Unclamped;
            layout.PositionMinMax = MinMax3.Unclamped;

            // Position UIBlock in front of camera
            if (updatePositionUsingLayouts)
            {
                uiBlock.TrySetWorldPosition(targetCamera.transform.position + targetCamera.transform.forward * planeDistance);
            }
            else
            {
                uiBlock.transform.position = targetCamera.transform.position + targetCamera.transform.forward * planeDistance;
            }

            // Match camera rotation so the two are axis-aligned
            uiBlock.transform.rotation = targetCamera.transform.rotation;

            if (fillMode == FillMode.Manual)
            {
                return;
            }

            Vector2 cameraDimensions = new Vector2(targetCamera.pixelWidth, targetCamera.pixelHeight);

            // Adjust the size
            ref Length3 uiBlockSize = ref uiBlock.Size;
            switch (fillMode)
            {
                case FillMode.MatchCameraResolution:
                {
                    uiBlockSize.XY.Value = cameraDimensions;
                    break;
                }
                case FillMode.FixedHeight:
                {
                    uiBlockSize.Y.Value = ReferenceResolution.y;

                    float aspectRatio = cameraDimensions.x / cameraDimensions.y;
                    uiBlockSize.X.Value = aspectRatio * uiBlockSize.Y.Value;
                    break;
                }
                case FillMode.FixedWidth:
                {
                    uiBlockSize.X.Value = ReferenceResolution.x;

                    float aspectRatio = cameraDimensions.y / cameraDimensions.x;
                    uiBlockSize.Y.Value = aspectRatio * uiBlockSize.X.Value;
                    break;
                }
            }

            if (targetCamera.orthographic)
            {
                float scale = 2f * targetCamera.orthographicSize / uiBlockSize.Y.Value;
                uiBlock.transform.localScale = new Vector3(scale, scale, scale);
            }
            else
            {
                // Adjust scale to match screen size
                float scale = 2f * planeDistance * Mathf.Tan(.5f * targetCamera.fieldOfView * Mathf.Deg2Rad) / uiBlockSize.Y.Value;
                uiBlock.transform.localScale = new Vector3(scale, scale, scale);
            }
        }

        private void Reset()
        {
            // Use layouts here otherwise the editor will overwrite the position we set
            UpdatePositionAndScale(updatePositionUsingLayouts: true);
        }

        [Obfuscation]
        private void OnDidApplyAnimationProperties()
        {
            RegisterOrUpdate();
        }
#endregion
    }
}
