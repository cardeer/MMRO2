using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Main
{
    class PhysicsSprite : Sprite
    {
        protected World World;
        public Body Body;

        public PhysicsSprite(World world, Texture2D texture) : base(texture)
        {
            World = world;
            Origin = TextureSize / 2;
        }

        public PhysicsSprite(World world) : base()
        {
            World = world;
        }

        public new Vector2 Position
        {
            get { return Body.Position; }
        }

        public new float Rotation
        {
            get { return Body.Rotation; }
        }
    }
}
