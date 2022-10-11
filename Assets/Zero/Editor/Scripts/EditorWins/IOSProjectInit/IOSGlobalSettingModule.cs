﻿using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ZeroEditor
{
    class IOSGlobalSettingModule:AEditorModule
    {
        public const string CONFIG_NAME = "ios_project_config.json";

        IOSProjectInitConfigVO _cfg;

        public IOSGlobalSettingModule(EditorWindow editorWin) : base(editorWin)
        {
            _cfg = EditorConfigUtil.LoadConfig<IOSProjectInitConfigVO>(CONFIG_NAME);
            if (null == _cfg)
            {
                _cfg = new IOSProjectInitConfigVO();
            }

            isEnable = _cfg.isEnable;
        }

        [Title("全局配置", titleAlignment: TitleAlignments.Centered)]
        [LabelText("保存配置"), Button(size: ButtonSizes.Large), PropertyOrder(-1)]
        void SaveConfig()
        {
            _cfg.isEnable = isEnable;

            EditorConfigUtil.SaveConfig(_cfg, CONFIG_NAME);
            editorWin.ShowTip("保存成功!");
        }

        [Space(20)]
        [InfoBox("「启用」的情况下，发布「iOS」的「xcode」项目后，会按照本工具的配置对项目进行自动配置")]
        [LabelText("是否激活自动化配置功能")]
        public bool isEnable;
    }
}
