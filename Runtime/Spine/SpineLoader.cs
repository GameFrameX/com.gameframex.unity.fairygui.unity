#if FAIRYGUI_SPINE

using System;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

namespace FairyGUI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GLoader3D : GObject
    {
        public static Action<SkeletonAnimation> CustomSpineDestroyMethod;

        SkeletonRenderer _spineRenderer;
        SkeletonAnimation _spineAnimation;

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public SkeletonAnimation spineAnimation
        {
            get { return _spineAnimation; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asset"></param>
        public void SetSpine(SkeletonDataAsset asset)
        {
            SetSpine(asset, _contentItem.width, _contentItem.height, _contentItem.skeletonAnchor);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="anchor"></param>
        public void SetSpine(SkeletonDataAsset asset, int width, int height, Vector2 anchor)
        {
            SetSpine(asset, width, height, anchor, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="anchor"></param>
        /// <param name="cloneMaterial"></param>
        public void SetSpine(SkeletonDataAsset asset, int width, int height, Vector2 anchor, bool cloneMaterial)
        {
            if (_spineAnimation != null)
                FreeSpine();

            _content.customCloneMaterials = MaterialOverride;
            _content.customRecoverMaterials = CleanMaterialOverride;

            var spineComponents = SkeletonAnimation.NewSkeletonAnimationGameObject(asset);
            _spineRenderer = spineComponents.skeletonRenderer;
            _spineAnimation = spineComponents.skeletonAnimation;
            _spineAnimation.gameObject.name = asset.name;
            _spineAnimation.gameObject.transform.localScale = new Vector3(1 / asset.scale, 1 / asset.scale, 1);
            _spineAnimation.gameObject.transform.localPosition = new Vector3(anchor.x, -anchor.y, 0);
            SetWrapTarget(_spineAnimation.gameObject, cloneMaterial, width, height);

            var skeletonColor = _spineAnimation.skeleton.GetColor();
            skeletonColor.r = _color.r;
            skeletonColor.g = _color.g;
            skeletonColor.b = _color.b;
            _spineAnimation.skeleton.SetColor(skeletonColor);

            OnChangeSpine(null);
        }

        protected void LoadSpine()
        {
            SkeletonDataAsset asset = (SkeletonDataAsset)_contentItem.skeletonAsset;
            if (asset == null)
                return;

            SetSpine(asset, _contentItem.width, _contentItem.height, _contentItem.skeletonAnchor);
        }

        protected void OnChangeSpine(string propertyName)
        {
            if (_spineAnimation == null)
                return;

            if (propertyName == "color")
            {
                var skeletonColor = _spineAnimation.skeleton.GetColor();
                skeletonColor.r = _color.r;
                skeletonColor.g = _color.g;
                skeletonColor.b = _color.b;
                _spineAnimation.skeleton.SetColor(skeletonColor);
                return;
            }

            var skeletonData = _spineAnimation.skeleton.Data;

            var state = _spineAnimation.AnimationState;
            Spine.Animation animationToUse = !string.IsNullOrEmpty(_animationName) ? skeletonData.FindAnimation(_animationName) : null;
            if (animationToUse != null)
            {
                var trackEntry = state.GetTrack(0);
                if (trackEntry == null || trackEntry.Animation.Name != _animationName || trackEntry.IsComplete && !trackEntry.Loop)
                    trackEntry = state.SetAnimation(0, animationToUse, _loop);
                else
                    trackEntry.Loop = _loop;

                if (_playing)
                    trackEntry.TimeScale = 1;
                else
                {
                    trackEntry.TimeScale = 0;
                    trackEntry.TrackTime = Mathf.Lerp(0, trackEntry.AnimationEnd - trackEntry.AnimationStart, _frame / 100f);
                }
            }
            else
                state.ClearTrack(0);

            var skin = !string.IsNullOrEmpty(skinName) ? skeletonData.FindSkin(skinName) : skeletonData.DefaultSkin;
            if (skin == null && skeletonData.Skins.Count > 0)
                skin = skeletonData.Skins.Items[0];
            if (_spineAnimation.skeleton.Skin != skin)
            {
                _spineAnimation.skeleton.SetSkin(skin);
                _spineAnimation.skeleton.SetupPoseSlots();
            }
        }

        protected void FreeSpine()
        {
            if (_spineAnimation != null)
            {
                if (CustomSpineDestroyMethod != null)
                {
                    CustomSpineDestroyMethod(_spineAnimation);
                }
                else
                {
                    if (Application.isPlaying)
                        GameObject.Destroy(_spineAnimation.gameObject);
                    else
                        GameObject.DestroyImmediate(_spineAnimation.gameObject);
                }

                _content.customCloneMaterials = null;
                _content.customRecoverMaterials = null;
                _spineRenderer = null;
                _spineAnimation = null;
            }
        }

        protected void OnUpdateSpine(UpdateContext context)
        {
            if (_spineAnimation != null)
            {
                var skeletonColor = _spineAnimation.skeleton.GetColor();
                skeletonColor.a = context.alpha * _content.alpha;
                _spineAnimation.skeleton.SetColor(skeletonColor);
            }
        }

        private void MaterialOverride(Dictionary<Material, Material> materials)
        {
            if (_spineRenderer != null)
            {
                foreach (var kv in materials)
                {
                    _spineRenderer.CustomMaterialOverride[kv.Key] = kv.Value;
                }
            }
        }

        private void CleanMaterialOverride()
        {
            if (_spineRenderer != null)
                _spineRenderer.CustomMaterialOverride.Clear();
        }
#if UNITY_2019_3_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InitializeOnLoad()
        {
            CustomSpineDestroyMethod = null;
        }
#endif
    }
}

#endif