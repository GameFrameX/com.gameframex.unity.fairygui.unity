using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using FairyGUI;
using UnityEditor.Build;

namespace FairyGUIEditor
{
    /// <summary>
    /// 
    /// </summary>
    public class EditorToolSet
    {
        public static GUIContent[] packagesPopupContents;

        static bool _loaded;

        [RuntimeInitializeOnLoadMethod]
        static void InitializeOnLoad()
        {
            _loaded = false;
        }

        [InitializeOnLoadMethod]
        static void Startup()
        {
            EditorApplication.update += EditorApplication_Update;
        }

        [MenuItem("GameObject/FairyGUI/UI Panel", false, 0)]
        static void CreatePanel()
        {
            EditorApplication.update -= EditorApplication_Update;
            EditorApplication.update += EditorApplication_Update;

            StageCamera.CheckMainCamera();

            GameObject panelObject = new GameObject("UIPanel");
            if (Selection.activeGameObject != null)
            {
                panelObject.transform.parent = Selection.activeGameObject.transform;
                panelObject.layer = Selection.activeGameObject.layer;
            }
            else
            {
                int layer = LayerMask.NameToLayer(StageCamera.LayerName);
                panelObject.layer = layer;
            }

            panelObject.AddComponent<FairyGUI.UIPanel>();
            Selection.objects = new Object[] { panelObject };
        }

        [MenuItem("GameObject/FairyGUI/UI Camera", false, 0)]
        static void CreateCamera()
        {
            StageCamera.CheckMainCamera();
            Selection.objects = new Object[] { StageCamera.main.gameObject };
        }

        [MenuItem("Window/FairyGUI - Refresh Packages And Panels")]
        static void RefreshPanels()
        {
            ReloadPackages();
        }

        /// <summary>
        /// 开启微信小游戏的键盘适配
        /// </summary>
        [MenuItem("Tools/FairyGUI/Open WeChat MiniGame")]
        public static void OpenWeChatMiniGame()
        {
            AddDefine("ENABLE_WECHAT_MINI_GAME");
        }

        /// <summary>
        /// 关闭微信小游戏的键盘适配
        /// </summary>
        [MenuItem("Tools/FairyGUI/Close WeChat MiniGame")]
        public static void CloseWeChatMiniGame()
        {
            RemoveDefine("ENABLE_WECHAT_MINI_GAME");
        }

        /// <summary>
        /// 开启抖音小游戏的键盘适配
        /// </summary>
        [MenuItem("Tools/FairyGUI/Open DouYin MiniGame")]
        public static void OpenDouYinMiniGame()
        {
            AddDefine("ENABLE_DOUYIN_MINI_GAME");
        }

        /// <summary>
        /// 关闭抖音小游戏的键盘适配
        /// </summary>
        [MenuItem("Tools/FairyGUI/Close DouYin MiniGame")]
        public static void CloseDouYinMiniGame()
        {
            RemoveDefine("ENABLE_DOUYIN_MINI_GAME");
        }

        private static void AddDefine(string defineName)
        {
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL).Split(new string[] { ";" }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var define in defines)
            {
                if (define == defineName)
                {
                    return;
                }
            }

            var newDefines = defines.ToList();
            newDefines.Add(defineName);
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, string.Join(";", newDefines.ToArray()));
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }


        private static void RemoveDefine(string defineName)
        {
            var defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL).Split(new string[] { ";" }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var define in defines)
            {
                if (define == defineName)
                {
                    var newDefines = defines.ToList();
                    newDefines.Remove(defineName);
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.WebGL, string.Join(";", newDefines.ToArray()));
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    return;
                }
            }
        }

        static void EditorApplication_Update()
        {
            if (Application.isPlaying)
                return;

            if (_loaded || !EMRenderSupport.hasTarget)
                return;

            LoadPackages();
        }

        public static void ReloadPackages()
        {
            if (!Application.isPlaying)
            {
                _loaded = false;
                LoadPackages();
                Debug.Log("FairyGUI - Refresh Packages And Panels complete.");
            }
            else
                EditorUtility.DisplayDialog("FairyGUI", "Cannot run in play mode.", "OK");
        }

        public static void LoadPackages()
        {
            if (Application.isPlaying || _loaded)
                return;

            EditorApplication.update -= EditorApplication_Update;
            EditorApplication.update += EditorApplication_Update;

            _loaded = true;

            UIPackage.RemoveAllPackages();
            UIPackage.branch = null;
            FontManager.Clear();
            NTexture.DisposeEmpty();
            UIObjectFactory.Clear();

            string[] ids = AssetDatabase.FindAssets("_fui t:textAsset");
            int cnt = ids.Length;
            for (int i = 0; i < cnt; i++)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(ids[i]);
                int pos = assetPath.LastIndexOf("_fui");
                if (pos == -1)
                    continue;

                assetPath = assetPath.Substring(0, pos);
                if (AssetDatabase.AssetPathToGUID(assetPath) != null)
                    UIPackage.AddPackage(assetPath,
                        (string name, string extension, System.Type type, out DestroyMethod destroyMethod) =>
                        {
                            destroyMethod = DestroyMethod.Unload;
                            return AssetDatabase.LoadAssetAtPath(name + extension, type);
                        }
                    );
            }

            List<UIPackage> pkgs = UIPackage.GetPackages();
            pkgs.Sort(CompareUIPackage);

            cnt = pkgs.Count;
            packagesPopupContents = new GUIContent[cnt + 1];
            for (int i = 0; i < cnt; i++)
                packagesPopupContents[i] = new GUIContent(pkgs[i].name);
            packagesPopupContents[cnt] = new GUIContent("Please Select");

            EMRenderSupport.Reload();
        }

        static int CompareUIPackage(UIPackage u1, UIPackage u2)
        {
            return u1.name.CompareTo(u2.name);
        }
    }
}