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
        public Enums.BallTypes Type;

        public Bullet(World world, Vector2 position, float dirX, float dirY, float forceX, float forceY, Enums.BallTypes type) : base(world)
        {
            Type = type;

            string tag = Settings.Collision.Bullet;

            switch (type)
            {
                case Enums.BallTypes.Ice:
                    tag = Settings.Collision.IceBullet;
                    break;
                case Enums.BallTypes.Fire:
                    tag = Settings.Collision.FireBullet;
                    break;
                case Enums.BallTypes.Lightning:
                    tag = Settings.Collision.LightningBullet;
                    break;
                case Enums.BallTypes.Explosion:
                    tag = Settings.Collision.ExplosionBullet;
                    break;
            }

            Body = World.CreateBody(position + new Vector2(dirX * 2, dirY + .7f), 0, BodyType.Dynamic);
            Body.Tag = tag;

            var bullet = Body.CreateCircle(0.2f, 1);
            bullet.Restitution = 0f;

            Body.Mass = 1;

            Body.ApplyLinearImpulse(new Vector2(dirX * forceX, dirY * forceY));
            Body.OnCollision += Body_OnCollision;
        }

        private bool Body_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            if (Array.IndexOf(Settings.CollisionList.Bullet, (string)other.Body.Tag) != -1)
            {
                ShouldRemove = true;

                if ((string)other.Body.Tag == Settings.Collision.Tower) {
                    Global.Instance.GameData.PlayerHP -= 10;
                    return true;
                }

                if (Type == Enums.BallTypes.Ice)
                {
                    var area = new EffectArea(World, Settings.Collision.IceArea, Body.Position, 3);
                    Global.Instance.GameData.BulletEffects.Add(area);
                }
                else if (Type == Enums.BallTypes.Lightning)
                {
                    var area = new EffectArea(World, Settings.Collision.LightningArea, Body.Position, 3);
                    Global.Instance.GameData.BulletEffects.Add(area);
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
