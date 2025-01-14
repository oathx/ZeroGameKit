﻿using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zero;
using ZeroGameKit;
using ZeroHot;

namespace Roushan
{
    class GamePanel : AView
    {
        GameStage stage;

        public Button btnExit;

        protected override void OnInit(object data)
        {
            stage = data as GameStage;
        }

        protected override void OnDisable()
        {
            UIEventListener.Get(GetChild("TouchPad").gameObject).onClick -= OnTouchPadClick;
            GetChild("BtnReset").GetComponent<Button>().onClick.RemoveListener(OnResetClick);
            GetChild("BtnHelp").GetComponent<Button>().onClick.RemoveListener(OnBtnHelpClick);

            btnExit.onClick.RemoveListener(Exit);
        }

        private void OnBtnHelpClick()
        {
            AudioDevice.GC();
            UIWinMgr.Ins.Open<HelpWin>(null, true, false);            
        }

        protected override void OnEnable()
        {
            UIEventListener.Get(GetChild("TouchPad").gameObject).onClick += OnTouchPadClick;
            GetChild("BtnReset").GetComponent<Button>().onClick.AddListener(OnResetClick);
            GetChild("BtnHelp").GetComponent<Button>().onClick.AddListener(OnBtnHelpClick);

            btnExit.onClick.AddListener(Exit);
        }

        private void Exit()
        {
            UIPanelMgr.Ins.Switch<MenuPanel>();
        }

        void OnTouchPadClick(PointerEventData e)
        {
            stage.CreateBlock();
        }

        void OnResetClick()
        {                     
            StageMgr.Ins.SwitchASync<GameStage>();
        }
    }
}
