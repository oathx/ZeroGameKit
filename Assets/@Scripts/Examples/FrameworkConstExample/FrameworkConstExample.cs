﻿using System.Text;
using UnityEngine;
using Zero;
using ZeroGameKit;
using ZeroHot;

namespace Example
{
    class FrameworkConstExample
    {
        public static void Show()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("------------------网络下载的更新资源存储的目录------------------");
            sb.AppendLine($"定义：WWW_RES_PERSISTENT_DATA_PATH");
            sb.AppendLine($"值：{ZeroConst.WWW_RES_PERSISTENT_DATA_PATH}");
            sb.AppendLine();

            sb.AppendLine("------------------框架生成文件存放地址------------------");
            sb.AppendLine($"定义：GENERATES_PERSISTENT_DATA_PATH");
            sb.AppendLine($"值：{ZeroConst.GENERATES_PERSISTENT_DATA_PATH}");
            sb.AppendLine();

            sb.AppendLine("------------------当前平台可读写目录地址（每个平台值不同）------------------");
            sb.AppendLine($"定义：PERSISTENT_DATA_PATH");
            sb.AppendLine($"值：{ZeroConst.PERSISTENT_DATA_PATH}");
            sb.AppendLine();

            sb.AppendLine("------------------当前平台可用WWW加载资源的streamingAssets目录地址（每个平台值不同）------------------");
            sb.AppendLine($"定义：STREAMING_ASSETS_PATH");
            sb.AppendLine($"值：{ZeroConst.STREAMING_ASSETS_PATH}");
            sb.AppendLine();

            var content = sb.ToString();

            var msg = MsgWin.Show("Zero框架常量「ZeroConst.cs」", content);
            msg.Resize((int)(Screen.width * 0.9), (int)(Screen.height * 0.9));
        }
    }
}
