using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Scenes.UI
{
    class Frame : Main.SceneComponent
    {
        private Texture2D _horizontalFrameTexture;
        private Texture2D _verticalFrameTexture;

        private List<Sprites.Rectangle> _frames;

        public Frame()
        {
            _horizontalFrameTexture = Global.Instance.Content.Load<Texture2D>("images/UI/horizontal_frame");
            _verticalFrameTexture = Global.Instance.Content.Load<Texture2D>("images/UI/vertical_frame");

            _frames = new List<Sprites.Rectangle>();

            var topFrame = new Sprites.Rectangle(_horizontalFrameTexture, _horizontalFrameTexture.Width, _horizontalFrameTexture.Height);
            topFrame.Origin = Vector2.Zero;
            topFrame.Position = new Vector2(0, 0);

            var bottomFrame = new Sprites.Rectangle(_horizontalFrameTexture, _horizontalFrameTexture.Width, _horizontalFrameTexture.Height);
            bottomFrame.Origin = new Vector2(0, _horizontalFrameTexture.Height);
            bottomFrame.Position = new Vector2(0, Settings.Window.Height);

            var leftFrame = new Sprites.Rectangle(_verticalFrameTexture, _verticalFrameTexture.Width, _verticalFrameTexture.Height);
            leftFrame.Origin = Vector2.Zero;
            leftFrame.Position = new Vector2(0, 0);

            var rightFrame = new Sprites.Rectangle(_verticalFrameTexture, _verticalFrameTexture.Width, _verticalFrameTexture.Height);
            rightFrame.Origin = new Vector2(_verticalFrameTexture.Width, 0);
            rightFrame.Position = new Vector2(Settings.Window.Width, 0);

            var centerFrame = new Sprites.Rectangle(_horizontalFrameTexture, _horizontalFrameTexture.Width, _horizontalFrameTexture.Height);
            centerFrame.Origin = Vector2.Zero;
            centerFrame.Position = new Vector2(0, Settings.UI.AbilityTop);

            _frames.Add(topFrame);
            _frames.Add(bottomFrame);
            _frames.Add(leftFrame);
            _frames.Add(rightFrame);
            _frames.Add(centerFrame);
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            foreach (var frame in _frames)
            {
                frame.Draw();
            }
        }
    }
}
