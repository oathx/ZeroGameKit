﻿using System.Collections;
using UnityEngine;
using Zero;
using ZeroGameKit;
using ZeroHot;

namespace Example
{
    public class MsgWinExample
    {
        public static void Start()
        {
            var text = ConfigMgr.Ins.LoadTextConfig("隐私政策.txt");
            var win = MsgWin.Show("隐私政策", text);
            win.SetContentAlignment(TextAnchor.UpperLeft);
        }
    }
}