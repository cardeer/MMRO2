using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Sprites.Player
{
    class StaffBall
    {
        public Controllers.Animation _animation;
        public float Width = 1.5f;
        public float Height;
        public Vector2 Position;
        private Vector2 _textureSize;

        public int CurrentFrame
        {
            get { return _animation.CurrentFrame; }
        }

        public void Update()
        {
            if (_animation != null)
            {
                _animation.Update();
            }
        }

        public void Draw()
        {
            if (_animation != null)
            {
                Global.Instance.SpriteBatch.Draw(
                    _animation.Texture,
                    Position,
                    new Microsoft.Xna.Framework.Rectangle(_animation.FrameX, _animation.FrameY, _animation.FrameWidth, _animation.FrameHeight),
                    Color.White,
                    0f,
                    new Vector2(_animation.FrameWidth / 2, _animation.FrameHeight / 2),
                    new Vector2(Width, Height) / _textureSize,
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public void SetTexture(Texture2D texture, int partsX = 1, int partsY = 1)
        {
            _animation = new Controllers.Animation(texture, partsX, partsY);
            Height = Width / ((float)_animation.FrameWidth / (float)_animation.FrameHeight);
            _textureSize = new Vector2(_animation.FrameWidth, _animation.FrameHeight);
        }
    }
}
