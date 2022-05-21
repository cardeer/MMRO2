using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Sprites.Player
{
    class Bullet : Main.PhysicsSprite
    {
        public bool ShouldRemove = false;
        private Tags.Bullet _tag;

        public Bullet(World world, Vector2 position, float dirX, float dirY, float forceX, float forceY, Enums.BallTypes type) : base(world)
        {
            _tag = new Tags.Bullet() { Type = type };

            Body = World.CreateBody(position + new Vector2(dirX * 2, dirY + .7f), 0, BodyType.Dynamic);
            Body.Tag = _tag;

            var bullet = Body.CreateCircle(0.2f, 1);
            bullet.Restitution = 0f;

            Body.Mass = 1;

            Body.ApplyLinearImpulse(new Vector2(dirX * forceX, dirY * forceY));
            Body.OnCollision += Body_OnCollision;
        }

        private bool Body_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            Main.Tag tag = (Main.Tag)other.Body.Tag;

            if (Array.IndexOf(Settings.CollisionList.Bullet, tag.Name) != -1)
            {
                ShouldRemove = true;

                if (tag.Name == Settings.Collision.Tower)
                {
                    Global.Instance.GameData.PlayerHP -= 10;
                }
                else if (tag.Name == Settings.Collision.Monster)
                {
                    Tags.Monster monster = (Tags.Monster)tag;

                    if (_tag.Type == Enums.BallTypes.Normal)
                    {
                        monster.Health -= 50;
                    }
                    else if (_tag.Type == Enums.BallTypes.Ice)
                    {
                        monster.Speed = .5f;
                    }
                }
            }
            else
            {
                return false;
            }


            return true;
        }
    }
}
