using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Main
{
    class Monster
    {
        protected Dictionary<Enums.MonsterStates, Controllers.Animation> Animations;

        public Enums.MonsterStates State = Enums.MonsterStates.Walking;

        public Body Body;
        public World World;
        public float Width;
        public float Height;
        public bool ShouldRemove = false;
        public float Offset = 0;

        public float HP = 100f;
        public float MaxHP = 100f;

        private Texture2D _hpTexture;
        private Vector2 _hpTextureSize = new Vector2(1, 1);

        private Vector2 _hpSize = new Vector2(2, 0.3f);
        private Vector2 _hpOrigin;

        public Monster(World world)
        {
            World = world;

            _hpTexture = Utils.Sprite.Factory.CreateRectangle((int)_hpTextureSize.X, (int)_hpTextureSize.Y, Color.Red);

            _hpOrigin = new Vector2(.5f, 1);
        }

        public virtual void Update()
        {
            if (HP <= 0)
            {
                HP = 0;
                ShouldRemove = true;
            }

            Animations[State].Update();
        }

        public virtual void Draw()
        {
            Vector2 _hpPosition = Body.Position + new Vector2(0, Height / 2 + .5f);

            Global.Instance.SpriteBatch.Draw(
                _hpTexture,
                _hpPosition,
                null,
                Color.White,
                0f,
                _hpOrigin,
                _hpSize * new Vector2(HP / MaxHP, 1) / _hpTextureSize,
                SpriteEffects.FlipVertically,
                0f
            );
        }

        public virtual void TakeDamage(float amount)
        {
            HP -= amount;

            if (HP <= 0)
            {
                HP = 0;
                ShouldRemove = true;
            }
        }
    }
}
