﻿using System.Collections.Generic;
using UnityEngine;

namespace XJGUI
{
    public abstract class ElementsGUI<T> : ElementGUI<IList<T>>
    {
        #region Field

        protected ElementGUI<T>[] guis;
        protected FoldoutPanel foldOutPanel;

        #endregion Field

        #region Property

        public override IList<T> Value
        {
            get
            {
                return base.value;
            }

            set
            {
                base.value = value;

                if (base.value != null)
                {
                    CheckGUIsUpdate();
                }
            }
        }

        public override string Title
        {
            get
            {
                return base.title;
            }
            set
            {
                base.title = value;
                this.foldOutPanel.Title = value;
            }
        }

        public override bool BoldTitle
        {
            get
            {
                return base.boldTitle;
            }
            set
            {
                base.boldTitle = value;
                this.foldOutPanel.BoldTitle = value;
            }
        }

        #endregion Property

        #region Constructor

        public ElementsGUI() : base()
        {
            // NOTE:
            // base.title & base.boldTitle is initialized in base() constructor.

            this.foldOutPanel = new FoldoutPanel()
            {
                Title = base.title,
                BoldTitle = base.boldTitle,
                Value = false
            };
        }

        #endregion Consctructor

        #region Method

        public override IList<T> Show()
        {
            if (this.Value != null)
            {
                CheckGUIsUpdate();
            }

            XJGUILayout.VerticalLayout(() => 
            {
                if (this.Title != null)
                {
                    this.foldOutPanel.Show(delegate ()
                    {
                        ShowComponentGUI();
                    });
                }
                else
                {
                    ShowComponentGUI();
                }
            });

            return this.Value;
        }

        public void SetValue(int index, T value)
        {
            // NOTE:
            // Almost for FieldGUISync.

            this.guis[index].Value = value;
        }

        protected void ShowComponentGUI()
        {
            if (this.Value == null)
            {
                GUILayout.Label("Null");
                return;
            }

            int valueCount = this.Value.Count;

            if (valueCount == 0)
            {
                GUILayout.Label("No Element");
                return;
            }

            for (int i = 0; i < valueCount; i++)
            {
                this.Value[i] = this.guis[i].Show();
            }
        }

        protected virtual bool CheckGUIsUpdate()
        {
            // NOTE:
            // We have not to check if base.value is changed.
            // When "Value" property is changed, call this function.

            int valueCount = this.Value.Count;

            if (this.guis != null && this.guis.Length == this.Value.Count)
            {
                return false;
            }

            this.guis = new ElementGUI<T>[valueCount];

            for (int i = 0; i < valueCount; i++)
            {
                ElementGUI<T> gui = GenerateValueGUI();
                gui.Value = this.Value[i];
                gui.BoldTitle = false;
                this.guis[i] = gui;
            }

            return true;
        }

        protected abstract ElementGUI<T> GenerateValueGUI();

        #endregion Method
    }
}