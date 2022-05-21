using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Sprites
{
    class EffectArea
    {
        public World World;
        public Body Body;
        public Texture2D Texture = Global.Instance.InvisibleRect;
        public float Time = 0;

        public EffectArea(World world, string tag, Vector2 position, float radius)
        {
            World = world;

            Body = World.CreateBody(position, 0f, BodyType.Dynamic);
            Body.IgnoreGravity = true;
            Body.FixedRotation = true;
            Body.Tag = tag;

            Body.OnCollision += Body_OnCollision;

            var area = Body.CreateCircle(radius, 1f);
        }

        private bool Body_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            return false;
        }

        public void Update()
        {
            Time += (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;

            if (Time >= .1)
            {
                World.Remove(Body);
            }
        }
    }
}
