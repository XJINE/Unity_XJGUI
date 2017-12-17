﻿using UnityEngine;

namespace XJGUI
{
    public class ColorsGUI : ValuesGUI<Color>
    {
        #region Field

        protected bool hsvMode;

        #endregion Field

        #region Property

        public bool HSVMode
        {
            get
            {
                return this.hsvMode;
            }

            set
            {
                this.hsvMode = value;

                if (base.value == null || CheckGUIsUpdate())
                {
                    return;
                }

                for (int i = 0; i < base.value.Count; i++)
                {
                    ((ColorGUI)base.guis[i]).HSV = value;
                }
            }
        }

        #endregion Property

        #region Constructor

        public ColorsGUI()
        {
            base.minValue = XJGUILayout.DefaultMinValueColor;
            base.maxValue = XJGUILayout.DefaultMaxValueColor;
        }

        #endregion Constructor

        #region Method

        protected override ElementGUI<Color> GenerateGUI()
        {
            return new ColorGUI();
        }

        #endregion Method
    }
}