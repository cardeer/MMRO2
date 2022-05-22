using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Sprites
{
    class Effect
    {
        public bool ShouldRemove = false;
        public float Width = 2f;
        public float Height;
        public Vector2 Position;
        public Vector2 FrameSize;

        public Controllers.Animation Animation;

        public Effect(Texture2D texture, int partsX, int partsY)
        {
            Animation = new Controllers.Animation(texture, partsX, partsY);
            FrameSize = new Vector2(Animation.FrameWidth, Animation.FrameHeight);
            Height = Width / (FrameSize.X / FrameSize.Y);
        }

        public void SetSize(float size)
        {
            Width = size;
            Height = Width / (FrameSize.X / FrameSize.Y);
        }

        public void Update()
        {
            if (Animation.CurrentFrame == Animation.Frames - 1)
            {
                ShouldRemove = true;
                return;
            }

            Animation.Update();
        }

        public void Draw()
        {
            Global.Instance.SpriteBatch.Draw(
                Animation.Texture,
                Position,
                new Microsoft.Xna.Framework.Rectangle(Animation.FrameX, Animation.FrameY, Animation.FrameWidth, Animation.FrameHeight),
                Color.White,
                0f,
                FrameSize / 2,
                new Vector2(Width, Height) / FrameSize,
                SpriteEffects.FlipVertically,
                0f
            );
        }
    }
}
