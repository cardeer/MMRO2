using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Scenes.UI
{
    class Frame : Main.SceneComponent
    {
        private Texture2D _FrameTexture;
        private Sprites.Rectangle _frame;

        public Frame()
        {
            _FrameTexture = Global.Instance.Content.Load<Texture2D>("images/UI/frame");

            _frame = new Sprites.Rectangle(_FrameTexture, _FrameTexture.Width, _FrameTexture.Height);
            _frame.Origin = Vector2.Zero;
            _frame.Position = new Vector2(0, 0);
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            _frame.Draw(); 
        }
    }
}
