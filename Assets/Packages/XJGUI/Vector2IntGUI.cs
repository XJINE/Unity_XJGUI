﻿using UnityEngine;

namespace XJGUI
{
    public class Vector2IntGUI : VectorGUI<Vector2Int>
    {
        #region Field

        private readonly IntGUI intGUIX = new IntGUI() { Title = "X" };
        private readonly IntGUI intGUIY = new IntGUI() { Title = "Y" };

        #endregion Field

        #region Property

        public override Vector2Int MinValue
        {
            get
            {
                return new Vector2Int()
                {
                    x = this.intGUIX.MinValue,
                    y = this.intGUIY.MinValue
                };
            }
            set
            {
                this.intGUIX.MinValue = value.x;
                this.intGUIY.MinValue = value.y;
            }
        }

        public override Vector2Int MaxValue
        {
            get
            {
                return new Vector2Int()
                {
                    x = this.intGUIX.MaxValue,
                    y = this.intGUIY.MaxValue
                };
            }
            set
            {
                this.intGUIX.MaxValue = value.x;
                this.intGUIY.MaxValue = value.y;
            }
        }

        public override float FieldWidth
        {
            get
            {
                return this.intGUIX.FieldWidth;
            }
            set
            {
                this.intGUIX.FieldWidth = value;
                this.intGUIY.FieldWidth = value;
            }
        }

        public override bool WithSlider
        {
            get
            {
                return this.intGUIX.WithSlider;
            }
            set
            {
                this.intGUIX.WithSlider = value;
                this.intGUIY.WithSlider = value;
            }
        }

        #endregion Property

        #region Constructor

        public Vector2IntGUI() : base() { }

        public Vector2IntGUI(string title) : base(title) { }

        public Vector2IntGUI(string title, Vector2Int min, Vector2Int max) : base(title, min, max) { }

        #endregion Constructor

        #region Method

        protected override void Initialize()
        {
            base.Initialize();

            this.MinValue = XJGUILayout.DefaultMinValueVector2Int;
            this.MaxValue = XJGUILayout.DefaultMaxValueVector2Int;
        }

        protected override Vector2Int ShowComponents(Vector2Int value)
        {
            return new Vector2Int()
            {
                x = this.intGUIX.Show(value.x),
                y = this.intGUIY.Show(value.y)
            };
        }

        #endregion Method
    }
}