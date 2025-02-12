﻿using Jing;
using LitJson;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using Zero;

namespace Zero
{
    /// <summary>
    /// 配置管理工具(对应@Resources中的文件)
    /// </summary>
    public class ConfigMgr : ASingleton<ConfigMgr>
    {                
        /// <summary>
        /// 加载JSON配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public T LoadJsonConfig<T>(string assetPath)
        {            
            string json = LoadTextConfig(assetPath);
            var vo = JsonMapper.ToObject<T>(json);
            return vo;
        }

        /// <summary>
        /// 加载文本配置
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public string LoadTextConfig(string assetPath)
        {
            var ta = ResMgr.Ins.Load<TextAsset>(assetPath);    
            
            if(null == ta)
            {
                //配置不存在
                throw new Exception(string.Format("[{0}] 文件不存在", assetPath));
            }
            
            return ta.text;
        }

        /// <summary>
        /// 加载二进制数据
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public byte[] LoadBytesConfig(string assetPath)
        {
            var ta = ResMgr.Ins.Load<TextAsset>(assetPath);

            if (null == ta)
            {
                //配置不存在
                throw new Exception(string.Format("[{0}] 文件不存在", assetPath));
            }

            return ta.bytes;
        }

        /// <summary>
        /// 加载自动化工具的配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadZeroHotConfig<T>()
        {
            var type = typeof(T);

            var atts = type.GetCustomAttributes(typeof(ZeroHotConfigAttribute), false);
            var att = atts[0] as ZeroHotConfigAttribute;

            if (null == att)
            {
                //加载的配置不存在
                throw new Exception(string.Format("[{0}] 并不是一个配置数据对象", type.FullName));
            }

            return LoadJsonConfig<T>(att.assetPath);
        }

        public override void Destroy()
        {
            
        }

        protected override void Init()
        {
            
        }
    }
}
