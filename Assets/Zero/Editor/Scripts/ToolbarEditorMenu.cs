﻿using System.IO;
using UnityEditor;
using UnityEngine;
using Zero;

namespace ZeroEditor
{
    /// <summary>
    /// Zero框架编辑器菜单
    /// </summary>
    public class ToolbarEditorMenu
    {
        [MenuItem("Zero/热更资源构建", false, 0)]
        public static void HotResBuild()
        {            
            BuildHotResEditorWin.Open();
        }

        [MenuItem("Zero/配置文件编辑", false, 50)]
        public static void Configs()
        {
            HotConfigEditorWin.Open("配置文件编辑");
        }

        [MenuItem("Zero/自动生成代码", false, 100)]
        public static void GenerateCode()
        {
            GenerateCodeEditorWin.Open();            
        }

        [MenuItem("Zero/发布构建/iOS构建自动化配置", false, 150)]
        public static void IosProjectInit()
        {
            IOS.IOSProjectInitEditorWin.Open();
        }
        
        [MenuItem("Zero/调试/清理[Caches]目录", false, 250)]
        public static void ClearCachesDir()
        {
            var cacheDir = new DirectoryInfo(ZeroConst.PERSISTENT_DATA_PATH);
            if (cacheDir.Exists)
            {
                cacheDir.Delete(true);
            }            
        }

        [MenuItem("Zero/调试/打开[Caches]目录", false, 300)]
        public static void OpenCachesDir()
        {
            var cacheDir = new DirectoryInfo(ZeroConst.PERSISTENT_DATA_PATH);
            if (cacheDir.Exists)
            {
                ZeroEditorUtility.OpenDirectory(cacheDir.FullName);
            }
        }

        [MenuItem("Zero/调试/打开[Application.temporaryCachePath]目录", false, 310)]
        public static void OpenTemporaryCacheDir()
        {
            Application.OpenURL(Application.temporaryCachePath);
        }

        [MenuItem("Zero/调试/GC", false, 350)]
        public static void GC()
        {
            ResMgr.Ins.DoGC();            
        }

        [MenuItem("Zero/工具/位图字体创建", false, 400)]
        public static void CreateBitmapFontGUITools()
        {
            BitmapFontCreaterMenu.CreateBitmapFontGUITools();            
        }

        [MenuItem("Zero/工具/SpriteAtlas 管理", false, 401)]
        public static void SpriteAtlasTools()
        {
            SpriteAtlasToolsEditorWin.Open();
        }

        [MenuItem("Zero/工具/AssetImporter 管理", false, 402)]
        public static void AssetsOptimizeTools()
        {
            AssetsOptimizeEditorWindow.Open();
        }

        [MenuItem("Zero/工具/Sprite Packing Tag 管理", false, 403)]
        public static void SpritePackingTagTools()
        {
            SpritePackingTagToolsEditorWin.Open();
        }

        [MenuItem("Zero/工具/冗余资源管理", false, 403)]
        public static void RedundancyResourcesCleanTools()
        {
            RedundancyResourcesCleanToolsEditorWin.Open();
        }

        [MenuItem("Zero/热更代码框架/ILRuntime", false, 450)]
        public static void OpenILRuntimeEditorWindow()
        {
            ILRuntimeEditorWin.Open();
        }

        [MenuItem("Zero/热更代码框架/HybridCLR", false, 450)]
        public static void OpenHybridCLREditorWindow()
        {
            HybridCLREditorWindow.Open();
        }

        [MenuItem("Zero/About", false, 500)]
        public static void Document()
        {
            AboutEditorWin.Open();            
        }


    }
}