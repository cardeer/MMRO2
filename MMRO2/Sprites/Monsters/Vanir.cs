using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Rect = Microsoft.Xna.Framework.Rectangle;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Sprites.Monsters
{
    class Vanir : Main.Monster
    {
        private float _originSpeed = 3;
        public float Speed = 3;

        private bool _slow = false;
        private float _slowTime = 0;

        private bool _fire = false;
        private float _fireCounter = 0;
        private float _fireTime = 0;

        private bool _lightning = false;
        private float _lightningTime = 0;

        public Vanir(World world) : base(world)
        {
            Width = 1.5f;

            Texture2D animation1 = Global.Instance.Content.Load<Texture2D>("images/monsters/vanir");

            Animations = new Dictionary<Enums.MonsterStates, Controllers.Animation>();
            Animations[Enums.MonsterStates.Walking] = new Controllers.Animation(animation1, 4, 1);
            Animations[Enums.MonsterStates.Attacking] = new Controllers.Animation(animation1, 4, 1);

            Height = Width / ((float)Animations[0].FrameWidth / Animations[0].FrameHeight);

            Body = world.CreateBody(Vector2.Zero, 0f, BodyType.Kinematic);
            Body.Tag = Settings.Collision.Monster;
            Body.IgnoreGravity = true;
            Body.FixedRotation = true;

            Body.CreateCircle(Width, 1f);
            Body.OnCollision += Body_OnCollision;
        }

        private bool Body_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            if ((string)other.Body.Tag == Settings.Collision.Bullet)
            {
                TakeDamage(Settings.Gameplay.BaseBulletDamage * Utils.Stats.BulletDamage());
            }

            if ((string)other.Body.Tag == Settings.Collision.IceArea)
            {
                _slow = true;
                _slowTime = 0;
            }
            else if ((string)other.Body.Tag == Settings.Collision.FireBullet)
            {
                _fire = true;
                _fireTime = 0;
            }
            else if ((string)other.Body.Tag == Settings.Collision.LightningArea && !_lightning)
            {
                _lightning = true;
            }

            return true;
        }

        public override void Update()
        {
            Body.LinearVelocity = new Vector2(-Speed, 0);

            if (Body.Position.X <= Settings.Gameplay.PlayerBasePosition + Width / 2)
            {
                Body.LinearVelocity = Vector2.Zero;
                Global.Instance.GameData.PlayerHP -= Settings.Gameplay.Damages["vanir"];
                ShouldRemove = true;
            }

            if (_slow)
            {
                _slowTime += (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;
                Speed = .3f;

                if (_slowTime >= 3 + Utils.Stats.IceBullet())
                {
                    _slow = false;
                    _slowTime = 0;
                    Speed = _originSpeed;
                }
            }

            if (_fire)
            {
                _fireTime += (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;
                _fireCounter += (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;

                if (_fireCounter >= 1)
                {
                    TakeDamage(Settings.Gameplay.BaseFireDamage + Utils.Stats.FireBullet());
                    _fireCounter = 0;
                }

                if (_fireTime >= 5)
                {
                    _fire = false;
                    _fireTime = 0;
                    _fireCounter = 0;
                }
            }

            if (_lightning)
            {
                _lightningTime += (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;

                if (_lightningTime >= .1)
                {
                    TakeDamage(Settings.Gameplay.BaseLightningDamage * Utils.Stats.LightningBullet());
                    _lightning = false;
                }

            }

            base.Update();
        }

        public override void Draw()
        {
            Global.Instance.SpriteBatch.Draw(
                Animations[State].Texture,
                Body.Position,
                new Rect(Animations[State].FrameX, Animations[State].FrameY, Animations[State].FrameWidth, Animations[State].FrameHeight),
                Color.White,
                0f,
                Animations[State].FrameSize / 2,
                new Vector2(Width, Height) / Animations[State].FrameSize,
                SpriteEffects.FlipVertically,
                0f
            );

            base.Draw();
        }
    }
}
