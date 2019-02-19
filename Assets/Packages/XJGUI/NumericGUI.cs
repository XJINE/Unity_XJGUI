﻿using System;
using UnityEngine;

namespace XJGUI
{
    // NOTE:
    // T must be 'int' or 'float'.

    public abstract class NumericGUI<T> : ValueGUI<T> where T : struct, IComparable
    {
        #region Field

        protected string text;

        #endregion Field

        #region Property

        public override T Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = CorrectValue(value);
                this.text = base.Value.ToString();
            }
        }

        public override T MinValue
        {
            get
            {
                return base.MinValue;
            }
            set
            {
                base.MinValue = value;
                this.Value = base.Value;
            }
        }

        public override T MaxValue
        {
            get
            {
                return base.MaxValue;
            }
            set
            {
                base.MaxValue = value;
                this.Value = base.Value;
            }
        }

        protected override GUIStyle FieldStyle
        {
            get
            {
                GUIStyle style = base.FieldStyle;

                bool textIsCorrect = TextIsCorrect;

                style.normal.textColor
                = textIsCorrect ? GUI.skin.textField.normal.textColor
                                : XJGUILayout.DefaultInvalidValueColor;

                style.active.textColor
                = textIsCorrect ? GUI.skin.textField.active.textColor
                                : XJGUILayout.DefaultInvalidValueColor;

                style.focused.textColor
                = textIsCorrect ? GUI.skin.textField.focused.textColor
                                : XJGUILayout.DefaultInvalidValueColor;

                return style;
            }
        }

        protected abstract bool TextIsCorrect { get; }

        #endregion Property

        #region Method

        public override T Show()
        {
            XJGUILayout.VerticalLayout(() =>
            {
                XJGUILayout.HorizontalLayout(() =>
                {
                    base.ShowTitle();

                    this.text = base.ShowTextField(this.text);

                    // NOTE:
                    // float.TryParse("0.") return true.
                    // But need to keep user input text, because the user may input "0.x~".

                    float textValue;

                    if (float.TryParse(this.text, out textValue))
                    {
                        base.Value = (T)(object)textValue;
                    }
                });

                if (!base.WithSlider)
                {
                    return;
                }

                // NOTE:
                // Need to update text when the value is updated with Slider.

                T sliderValue = (T)(object)GUILayout.HorizontalSlider((float)(object)base.Value,
                                                                      (float)(object)base.MinValue,
                                                                      (float)(object)base.MaxValue);

                if (!sliderValue.Equals(base.Value))
                {
                    this.Value = sliderValue;
                }
            });

            return base.Value;
        }

        protected virtual T CorrectValue(T value)
        {
            if (0 < value.CompareTo(base.MaxValue))
            {
                value = base.MaxValue;
            }

            if (value.CompareTo(base.MinValue) < 0)
            {
                value = base.MinValue;
            }

            return value;
        }

        #endregion Method
    }
}