using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Rect = Microsoft.Xna.Framework.Rectangle;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Sprites.Monsters
{
    class Cabbage : Main.Monster
    {
        public Cabbage(World world) : base(world)
        {
            Width = Height = 2;

            Texture2D animation1 = Global.Instance.Content.Load<Texture2D>("images/monsters/cabbage");

            Animations = new Dictionary<Enums.MonsterStates, Controllers.Animation>();
            Animations[Enums.MonsterStates.Idle] = new Controllers.Animation(animation1, 4, 1);
            Animations[Enums.MonsterStates.Walking] = new Controllers.Animation(animation1, 4, 1);
            Animations[Enums.MonsterStates.Attacking] = new Controllers.Animation(animation1, 4, 1);

            Tag = new Tags.Monster();

            Body = world.CreateBody(Vector2.Zero, 0f, BodyType.Kinematic);
            Body.Tag = Tag;
            Body.IgnoreGravity = true;
            Body.FixedRotation = true;

            Body.CreateCircle(Width, 1f);
            Body.OnCollision += Body_OnCollision;
        }

        private bool Body_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            return true;
        }

        public override void Update()
        {
            Body.LinearVelocity = new Vector2(-Tag.Speed, 0);

            if (Body.Position.X <= Settings.Gameplay.PlayerBasePosition + Width / 2)
            {
                Body.LinearVelocity = Vector2.Zero;
                Global.Instance.GameData.PlayerHP -= Settings.Gameplay.Damages["cabbage"];
                ShouldRemove = true;
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
                new Vector2(Width / 2 + 1f) / Animations[State].FrameSize,
                SpriteEffects.FlipVertically,
                0f
            );

            base.Draw();
        }
    }
}
