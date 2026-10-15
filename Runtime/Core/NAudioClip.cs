using System;
using UnityEngine;

namespace FairyGUI
{
    /// <summary>
    /// 
    /// </summary>
    public class NAudioClip
    {
        public static Action<AudioClip> CustomDestroyMethod;

        /// <summary>
        /// 
        /// </summary>
        public DestroyMethod destroyMethod;

        /// <summary>
        /// 
        /// </summary>
        public AudioClip nativeClip;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="audioClip"></param>
        [UnityEngine.Scripting.Preserve]
        public NAudioClip(AudioClip audioClip)
        {
            nativeClip = audioClip;
        }

        /// <summary>
        /// 
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public void Unload()
        {
            if (nativeClip == null)
                return;

            if (destroyMethod == DestroyMethod.Unload)
                Resources.UnloadAsset(nativeClip);
            else if (destroyMethod == DestroyMethod.Destroy)
                UnityEngine.Object.DestroyImmediate(nativeClip, true);
            else if (destroyMethod == DestroyMethod.AsyncResource)
            {
                UIPackage.AsyncLoadResource?.ReleaseResource(nativeClip);
            }
            else if (destroyMethod == DestroyMethod.Custom)
            {
                if (CustomDestroyMethod == null)
                    Debug.LogWarning("NAudioClip.CustomDestroyMethod must be set to handle DestroyMethod.Custom");
                else
                    CustomDestroyMethod(nativeClip);
            }

            nativeClip = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="audioClip"></param>
        [UnityEngine.Scripting.Preserve]
        public void Reload(AudioClip audioClip)
        {
            if (nativeClip != null && nativeClip != audioClip)
                Unload();

            nativeClip = audioClip;
        }
#if UNITY_2019_3_OR_NEWER
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void InitializeOnLoad()
        {
            CustomDestroyMethod = null;
        }
#endif
    }
}
