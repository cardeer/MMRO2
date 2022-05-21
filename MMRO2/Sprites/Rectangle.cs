using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using Rect = Microsoft.Xna.Framework.Rectangle;

namespace MMRO2.Sprites
{
    class Rectangle : Main.Sprite
    {
        public int Width;
        public int Height;
        public Color DrawColor = Color.White;

        public Rectangle(Texture2D texture, int width, int height) : base(texture)
        {
            Width = width;
            Height = height;

            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Scale = new Vector2(Width, Height) / TextureSize;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            Global.Instance.SpriteBatch.Draw(
                Texture,
                Position,
                null,
                DrawColor,
                Rotation,
                Origin,
                Scale,
                SpriteEffects.None,
                0f
            );

            base.Draw();
        }
    }
}
