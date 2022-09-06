﻿using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Zero;
using ZeroEditor;

/// <summary>
/// 自定义右键菜单
/// </summary>
public class RightClickEditorMenu
{
    [MenuItem("Assets/Zero/生成DLL", false, 0)]
    static void GenerateDll()
    {
        var now = DateTime.Now;
        var cmd = new DllBuildCommand(ZeroEditorConst.HOT_SCRIPT_ROOT_DIR, ZeroEditorConst.DLL_PUBLISH_DIR);
        cmd.onFinished += (DllBuildCommand self, bool isSuccess) => {
            var tip = isSuccess ? "Dll生成成功!" : "Dll生成失败!";
            Debug.Log(Log.Zero1(tip));
            Debug.Log(Log.Zero1("耗时:{0}秒", (DateTime.Now - now).TotalSeconds));
        };
        cmd.Execute();        
    }

    [MenuItem("Assets/Zero/工具/创建SpriteAtlas", false, 0)]
    static void CreateSpriteAtlas()
    {
        
        if (Selection.objects.Length != 1)
        {
            EditorUtility.DisplayDialog("错误", "仅支持[单选]的[文件夹]", "OK");
            return;
        }
        
        var obj = Selection.objects[0];
        var assetPath = AssetDatabase.GetAssetPath(obj);        
        if (false == Directory.Exists(assetPath))
        {
            EditorUtility.DisplayDialog("错误", "仅支持[单选]的[文件夹]", "OK");
            return;
        }

        SpriteAtlasToolsCreateEditorWin.Open(assetPath);
    }


    [MenuItem("Assets/Zero/位图字体/直接创建/使用Png图片的名称作为字符源", false, 1)]
    static void CreateBitmapFontUsePNGFileName()
    {
        BitmapFontCreaterMenu.CreateBitmapFontUsePNGFileName();
    }

    [MenuItem("Assets/Zero/位图字体/直接创建/使用「chars.txt」作为字符源", false, 2)]
    static void CreateBitmapFontUseCharsTxt()
    {
        BitmapFontCreaterMenu.CreateBitmapFontUseCharsTxt();
    }

    [MenuItem("Assets/Zero/位图字体/使用GUI创建", false, 3)]
    static void CreateBitmapFontGUI()
    {
        BitmapFontCreaterMenu.CreateBitmapFontGUI();
    }

    [MenuItem("Assets/Zero/资源名生成", false, 100)]
    static void GenerateAssetNames()
    {
        new GenerateHotFileClassCommand().Excute();
        var findCmd = new FindAssetBundlesCommand(false);
        findCmd.onFinished += (cmd, list) =>
        {
            new GenerateABClassCommand(list).Excute();
            new GenerateAutoViewRegisterClassCommand(list, cmd.cfg.viewClassNamespaceList).Excute();
            Debug.Log(Log.Zero1("生成完毕!"));
            AssetDatabase.Refresh();
        };
        findCmd.Excute();
    }
}
