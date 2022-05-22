﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Rect = Microsoft.Xna.Framework.Rectangle;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Sprites.Monsters
{
    class Golem : Main.Monster
    {
        public float Speed = 2;

        private bool _slow = false;
        private float _slowTime = 0;

        private bool _fire = false;
        private float _fireCounter = 0;
        private float _fireTime = 0;

        private bool _lightning = false;
        private float _lightningTime = 0;

        private float _attackCooldown = 3f;

        public Golem(World world) : base(world)
        {
            Height = 10;

            IsBoss = true;

            HP = MaxHP = 2000;

            Texture2D animation1 = Global.Instance.Content.Load<Texture2D>("images/monsters/golem");
            Texture2D animation2 = Global.Instance.Content.Load<Texture2D>("images/monsters/golem_attack");

            Animations = new Dictionary<Enums.MonsterStates, Controllers.Animation>();
            Animations[Enums.MonsterStates.Walking] = new Controllers.Animation(animation1, 6, 1);
            Animations[Enums.MonsterStates.Attacking] = new Controllers.Animation(animation2, 5, 1);

            State = Enums.MonsterStates.Walking;

            Width = Height * ((float)Animations[State].FrameWidth / Animations[State].FrameHeight);

            Body = world.CreateBody(Vector2.Zero, 0f, BodyType.Kinematic);
            Body.Tag = Settings.Collision.Monster;
            Body.IgnoreGravity = true;
            Body.FixedRotation = true;

            ReduceHitbox = new Vector2(2, 2);

            Body.CreateRectangle(Width - ReduceHitbox.X, Height - ReduceHitbox.Y, 1f, Vector2.Zero);
            Body.OnCollision += Body_OnCollision;
        }

        private bool Body_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            if ((string)other.Body.Tag == Settings.Collision.Bullet)
            {
                TakeDamage(Settings.Gameplay.BaseBulletDamage + Global.Instance.GameData.Perks[Enums.Perks.IncreaseBulletDamage]);
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
                State = Enums.MonsterStates.Attacking;
                Body.LinearVelocity = Vector2.Zero;
            }

            if (State == Enums.MonsterStates.Attacking)
            {
                _attackCooldown -= (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;

                if (_attackCooldown <= 0)
                {
                    if (Animations[State].CurrentFrame == 4)
                    {
                        Global.Instance.GameData.PlayerHP -= Settings.Gameplay.Damages["golem"];
                        _attackCooldown = 8;
                    }
                }
                else
                {
                    Animations[State].CurrentFrame = 0;
                }
            }

            if (_slow)
            {
                _slowTime += (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;
                Speed = .3f;

                if (_slowTime >= 3)
                {
                    _slow = false;
                    _slowTime = 0;
                    Speed = 1;
                }
            }

            if (_fire)
            {
                _fireTime += (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;
                _fireCounter += (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;

                if (_fireCounter >= 1)
                {
                    TakeDamage(Settings.Gameplay.BaseFireDamage + Global.Instance.GameData.Perks[Enums.Perks.IncreaseBulletDamage]);
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
                    TakeDamage(Settings.Gameplay.BaseLightningDamage + Global.Instance.GameData.Perks[Enums.Perks.IncreaseBulletDamage]);
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
