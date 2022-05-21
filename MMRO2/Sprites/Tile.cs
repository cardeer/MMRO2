using System;
using System.Collections.Generic;
using System.Text;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Sprites
{
    class Tile : Main.PhysicsSprite
    {
        public float Width;
        public float Height;
        private string _tag;

        private int _blocksX;
        private int _blocksY;

        public float TotalWidth;
        public float TotalHeight;

        public Vector2 InitialPosition = Vector2.Zero;

        public Tile(World world, Texture2D texture, string tag, float width, float height, int blocksX = 1, int blocksY = 1) : base(world, texture)
        {
            Width = width;
            Height = height;

            _tag = tag;

            _blocksX = blocksX;
            _blocksY = blocksY;

            TotalWidth = blocksX * width;
            TotalHeight = blocksY * height;

            Body = world.CreateBody(Vector2.Zero, 0f, BodyType.Static);
            Body.Tag = tag;

            var tile = Body.CreateRectangle(TotalWidth, TotalHeight, 1f, Vector2.Zero);
            tile.Restitution = 0f;
            tile.Friction = .5f;

            Body.OnCollision += Body_OnCollision;

            Scale = new Vector2(Width, Height) / TextureSize;
            Origin = Vector2.Zero;

            SetPosition(Vector2.Zero);
        }

        private bool Body_OnCollision(Fixture sender, Fixture other, tainicom.Aether.Physics2D.Dynamics.Contacts.Contact contact)
        {
            return true;
        }

        private void SetPosition(Vector2 position)
        {
            Body.Position = position;
            InitialPosition = new Vector2(Body.Position.X - TotalWidth / 2, Body.Position.Y - TotalHeight / 2);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
            for (int y = 0; y < _blocksY; y++)
            {
                for (int x = 0; x < _blocksX; x++)
                {
                    Global.Instance.SpriteBatch.Draw(
                        Texture,
                        InitialPosition + new Vector2(Width * x, Height * y),
                        null,
                        Color.White,
                        0f,
                        Origin,
                        Scale,
                        SpriteEffects.FlipVertically,
                        0f
                    );
                }
            }

            base.Draw();
        }

        public new Vector2 Position
        {
            get { return Body.Position; }
            set
            {
                SetPosition(value);
            }
        }

        public Tile Clone()
        {
            var tile = new Tile(World, Texture, _tag, Width, Height, _blocksX, _blocksY);
            tile.SetPosition(Body.Position);
            return tile;
        }
    }
}
