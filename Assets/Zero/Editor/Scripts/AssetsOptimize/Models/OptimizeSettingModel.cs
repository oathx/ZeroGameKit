﻿using Jing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zero;

namespace ZeroEditor
{
    /// <summary>
    /// 优化配置模型
    /// </summary>
    class OptimizeSettingModel
    {
        PathTree<TextureOptimizeSettingVO> _pathTree = new PathTree<TextureOptimizeSettingVO>();

        string[] SplitFolderPath(string folder)
        {
            var path = FileUtility.StandardizeBackslashSeparator(folder);
            path = FileUtility.RemoveStartPathSeparator(path);
            var paths = path.Split('/');
            return paths;
        }

        /// <summary>
        /// 整理纹理优化配置，方便查找
        /// </summary>
        /// <param name="textureSettings"></param>
        public void TidySettings(List<TextureOptimizeSettingVO> settings)
        {
            _pathTree.Clear();
            foreach (var setting in settings)
            {
                var paths = SplitFolderPath(setting.folder);
                _pathTree.Create(paths).data = setting;
            }

            var info = _pathTree.ToMapString();
            if (!string.IsNullOrEmpty(info))
            {
                Debug.Log(info);
            }
        }

        /// <summary>
        /// 找到和路径匹配的配置
        /// </summary>
        /// <param name="path"></param>
        public TextureOptimizeSettingVO FindSetting(string path)
        {
            var paths = SplitFolderPath(path);
            var node = _pathTree.Find(paths, false);
            if (null == node)
            {
                return null;
            }

            var setting = PathTree<TextureOptimizeSettingVO>.FindLastNodeWithNonNullDataForward(node);
            if(null == setting)
            {
                return null;
            }
            return setting.data;
        }
    }
}
