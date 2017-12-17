﻿using System.Reflection;
using UnityEngine;

namespace XJGUI.FieldGUIs
{
    public class ColorGUI : FieldGUIComponent<Color>
    {
        #region Constructor

        public ColorGUI(object data, FieldInfo fieldInfo, FieldGUIInfo guiInfo)
            : base(data, fieldInfo, guiInfo)
        {
        }

        #endregion Constructor

        #region Method

        protected override void InitializeGUI()
        {
            base.gui = new XJGUI.ColorGUI()
            {
                Value     = (Color)base.fieldInfo.GetValue(base.data),
                Title     = base.guiInfo.Title,
                BoldTitle = base.guiInfo.BoldTitle,
                MinValue  = base.guiInfo.MinColor,
                MaxValue  = base.guiInfo.MaxColor,
                Decimals  = base.guiInfo.Decimals,
                HSV       = base.guiInfo.HSV
            };
        }

        public override void SetSyncValue(int index, string value)
        {
            string[] values = value.Split(',');
            base.gui.Value = new Color()
            {
                r = float.Parse(values[0]),
                g = float.Parse(values[1]),
                b = float.Parse(values[2]),
                a = float.Parse(values[3]),
            };
        }

        public override void GetSyncValue(out int index, out string value)
        {
            index = base.updateIndex;
            value = base.gui.Value.r.ToString("R") + ","
                  + base.gui.Value.g.ToString("G") + ","
                  + base.gui.Value.b.ToString("B") + ","
                  + base.gui.Value.a.ToString("A");
        }

        #endregion Method
    }
}