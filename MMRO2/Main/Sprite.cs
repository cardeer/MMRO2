using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Main
{
    class Sprite
    {
        protected Texture2D Texture;
        protected Vector2 TextureSize;

        public Vector2 Origin = Vector2.Zero;
        public Vector2 Position = Vector2.Zero;
        public float Rotation = 0f;
        public Vector2 Scale;

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            TextureSize = new Vector2(texture.Width, texture.Height);

            Scale = new Vector2(1, 1);
        }

        public Sprite()
        {
        
        }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }
    }
}
