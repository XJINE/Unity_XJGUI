﻿using System.Reflection;

namespace XJGUI
{
    public abstract class FieldGUIComponent<T> : FieldGUIComponentBase<T>
    {
        #region Constructor

        public FieldGUIComponent(object data, FieldInfo fieldInfo, FieldGUIInfo guiInfo)
            :base(data, fieldInfo, guiInfo)
        {
            base.IsIListType = false;
            base.Type = typeof(T);
        }

        #endregion Constructor

        #region Method

        protected override void Load()
        {
            base.Load();
            this.previousValue = this.gui.Value;
        }

        protected override void ShowGUI()
        {
            T currentValue = this.gui.Show();

            base.UpdateIndex = CheckUpdate(currentValue, previousValue);

            this.previousValue = currentValue;
        }

        public override void SetValue(object value, int index = -1)
        {
            this.gui.Value = (T)value;
        }

        protected override int CheckUpdate(T value1, T value2)
        {
            return value1.Equals(value2) ? -1 : 0;
        }

        #endregion Method
    }
}