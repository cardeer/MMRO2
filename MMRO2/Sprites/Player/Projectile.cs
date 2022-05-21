using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Sprites.Player
{
    class Projectile
    {
        public Body Body;
        public Vector2 Size = new Vector2(.1f, .1f);

        private Texture2D _texture;
        private Vector2 _textureSize;

        public Projectile(World world, Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _textureSize = new Vector2(texture.Width, texture.Height);

            Body = world.CreateBody(position, 0, BodyType.Static);
            var fixture = Body.CreateRectangle(Size.X, Size.Y, 1f, Vector2.Zero);

            Body.Tag = new Tags.Projectile();
            Body.OnCollision += Body_OnCollision;
        }

        private bool Body_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            return false;
        }

        public void Draw()
        {
            Global.Instance.SpriteBatch.Draw(
                _texture,
                Body.Position,
                null,
                Color.White,
                0f,
                _textureSize / 2,
                Size / _textureSize,
                SpriteEffects.FlipVertically,
                0f
            );
        }
    }
}
