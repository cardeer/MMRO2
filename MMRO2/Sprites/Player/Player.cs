using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Sprites.Player
{
    class Player : Main.PhysicsSprite
    {
        public float Width = 1.7f;
        public float Height;

        private float _wandWidth = 1.7f;
        private float _wandHeight;
        private Vector2 _wandScale;
        private Vector2 _wandOrigin;
        private float _unitX = 0;
        private float _unitY = 0;

        private Texture2D _playerBodyTexture = Global.Instance.Content.Load<Texture2D>("images/player/body");
        private Texture2D _playerWandTexture = Global.Instance.Content.Load<Texture2D>("images/player/wand");

        private Texture2D _ball = Global.Instance.Content.Load<Texture2D>("images/ball");
        private Texture2D _iceBall = Global.Instance.Content.Load<Texture2D>("images/ball");
        private Texture2D _fireBall = Global.Instance.Content.Load<Texture2D>("images/ball");
        private Texture2D _lightningBall = Global.Instance.Content.Load<Texture2D>("images/ball");

        private float _wandRotation = 0f;

        private Texture2D _projectileTexture;
        private List<Projectile> _projectiles;

        private event RayCastReportFixtureDelegate _rayCastCallback;
        private bool _rayCastHit = false;

        private StaffBall _staffBall;

        private Texture2D _fireBallTexture = Global.Instance.Content.Load<Texture2D>("images/staff_balls/fire");
        private Texture2D _lightningBallTexture = Global.Instance.Content.Load<Texture2D>("images/staff_balls/lightning");
        private Texture2D _iceBallTexture = Global.Instance.Content.Load<Texture2D>("images/staff_balls/ice");

        private Enums.BallTypes _ballType = Enums.BallTypes.Normal;

        public Player(World world) : base(world)
        {
            Texture = _playerBodyTexture;
            TextureSize = new Vector2(Texture.Width, Texture.Height);
            Origin = TextureSize / 2;

            // calculate player body texture aspect ratio
            Height = Width / (TextureSize.X / TextureSize.Y);

            // calculate player wand texture aspect ratio
            _wandHeight = _wandWidth / (_playerWandTexture.Width / _playerWandTexture.Height);
            _wandScale = new Vector2(_wandWidth, _wandHeight) / new Vector2(_playerWandTexture.Width, _playerWandTexture.Height);
            _wandOrigin = new Vector2(50, 159);

            Body = world.CreateBody(Vector2.Zero, 0f, BodyType.Static);
            Body.Tag = new Tags.Player();

            var player = Body.CreateRectangle(Width, Height, 1, Vector2.Zero);
            player.Restitution = 0f;
            player.Friction = 0.5f;

            Scale = new Vector2(Width, Height) / TextureSize;

            _projectileTexture = Utils.Sprite.Factory.CreateRectangle(10, 10, Color.White);
            _projectiles = new List<Projectile>();

            _rayCastCallback += Player__rayCastCallback;

            _staffBall = new StaffBall() { Position = Body.Position, Width = 1 };
            SetBallType(Enums.BallTypes.Normal);
        }

        public void setBallTexture(Texture2D texture)
        {
            _ball = texture;
        }

        public override void Update()
        {
            HandleKeyboard();

            _staffBall.Position = Body.Position + new Vector2(0, Height / 2 + _staffBall.Height / 2 + .3f);
            _staffBall.Update();

            foreach (var projectile in _projectiles)
            {
                World.Remove(projectile.Body);
            }

            _projectiles.Clear();

            MouseState mouseState = Mouse.GetState();
            Vector2 relativeMousePos = Global.Instance.GameData.Camera.ConvertScreenToWorld(mouseState.Position);

            _wandRotation = (float)Math.Atan2(relativeMousePos.Y - Position.Y, relativeMousePos.X - Position.X);

            if (_wandRotation > Math.PI / 2)
            {
                _wandRotation = (float)Math.PI / 2;
            }
            else if (_wandRotation < -Math.PI / 2)
            {
                _wandRotation = (float)Math.PI / 2;
            }
            else if (_wandRotation < 0)
            {
                _wandRotation = 0;
            }

            _unitX = (float)Math.Cos(_wandRotation);
            _unitY = (float)Math.Sin(_wandRotation);

            Types.Camera camera = Global.Instance.GameData.Camera;
            Vector2 mousePos = camera.ConvertScreenToWorld(Global.Instance.CurrentMouseState.Position);

            float distX = (mousePos.X - Body.Position.X);
            if (distX > Settings.Gameplay.ProjectileSensitivityX) distX = Settings.Gameplay.ProjectileSensitivityX;

            float distY = (mousePos.Y - Body.Position.Y);
            if (distY > Settings.Gameplay.ProjectileSensitivityY) distY = Settings.Gameplay.ProjectileSensitivityY;

            Vector2 p0 = Position + new Vector2(_unitX * 2, _unitY + .7f);
            Vector2 velocity = new Vector2(
                _unitX * distX / Settings.Gameplay.ProjectileSensitivityX * Settings.Gameplay.MaxProjectileForce,
                _unitY * distY / Settings.Gameplay.ProjectileSensitivityY * Settings.Gameplay.MaxProjectileForce
            );
            Vector2 lastPos = p0;

            _rayCastHit = false;

            for (int i = 1; i < 50; i += 3)
            {
                Vector2 pos = GetProjectilePosition(p0, velocity, i);

                World.RayCast(_rayCastCallback, lastPos, pos);

                if (_rayCastHit)
                {
                    break;
                }

                _projectiles.Add(new Projectile(World, _projectileTexture, pos));
                lastPos = pos;
            }

            if (!Global.Instance.GameData.PrevPaused && Utils.Input.IsLeftMouseClicked())
            {
                Bullet bullet = new Bullet(
                    World,
                    Body.Position,
                    _unitX,
                    _unitY,
                    distX / Settings.Gameplay.ProjectileSensitivityX * Settings.Gameplay.MaxProjectileForce,
                    distY / Settings.Gameplay.ProjectileSensitivityY * Settings.Gameplay.MaxProjectileForce,
                    _ballType
                );

                Global.Instance.GameData.Bullets.Add(bullet);

                SetBallType(Enums.BallTypes.Normal);
            }

            foreach (var bullet in Global.Instance.GameData.Bullets)
            {
                if (bullet.ShouldRemove)
                {
                    World.Remove(bullet.Body);
                }
            }

            Global.Instance.GameData.Bullets.RemoveAll(e => e.ShouldRemove);

            base.Update();
        }

        private float Player__rayCastCallback(Fixture fixture, Vector2 point, Vector2 normal, float fraction)
        {
            Main.Tag tag = (Main.Tag)fixture.Body.Tag;

            if (tag.Name == Settings.Collision.Tower || tag.Name == Settings.Collision.Ground)
            {
                _rayCastHit = true;
            }

            return 0;
        }

        private Vector2 GetProjectilePosition(Vector2 start, Vector2 velocity, float n)
        {
            float t = (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;
            Vector2 stepVelocity = t * velocity;
            Vector2 stepGravity = t * t * World.Gravity;

            return start + n * stepVelocity + 0.5f * (n * n + n) * stepGravity;
        }

        public override void Draw()
        {
            foreach (var projectile in _projectiles)
            {
                projectile.Draw();
            }

            Global.Instance.SpriteBatch.Draw(
                _playerWandTexture,
                Position + new Vector2(Width / 2 - 0.5f, 0.2f),
                null,
                Color.White,
                _wandRotation,
                _wandOrigin,
                _wandScale,
                SpriteEffects.FlipVertically,
                0f
            );

            Global.Instance.SpriteBatch.Draw(
                Texture,
                Position,
                null,
                Color.White,
                Rotation,
                Origin,
                Scale,
                SpriteEffects.FlipVertically,
                0f
            );

            foreach (var bullet in Global.Instance.GameData.Bullets)
            {
                Global.Instance.SpriteBatch.Draw(
                    _ball,
                    bullet.Position,
                    null,
                    Color.White,
                    bullet.Rotation,
                    new Vector2(_ball.Width, _ball.Height) / 2,
                    new Vector2(.4f, .4f) / new Vector2(_ball.Width, _ball.Height),
                    SpriteEffects.FlipVertically,
                    0f
                );
            }

            _staffBall.Draw();

            base.Draw();
        }

        private void SetBallType(Enums.BallTypes type)
        {
            _ballType = type;

            switch (type)
            {
                case Enums.BallTypes.Normal:
                    _staffBall.SetTexture(_fireBallTexture, 6, 1);
                    break;
                case Enums.BallTypes.Fire:
                    _staffBall.SetTexture(_fireBallTexture, 6, 1);
                    break;
                case Enums.BallTypes.Ice:
                    _staffBall.SetTexture(_iceBallTexture, 6, 1);
                    break;
                case Enums.BallTypes.Lightning:
                    _staffBall.SetTexture(_lightningBallTexture, 6, 1);
                    break;
                case Enums.BallTypes.Explosion:
                    _staffBall.SetTexture(_fireBallTexture, 6, 1);
                    break;
                default:
                    _staffBall.SetTexture(_fireBallTexture, 6, 1);
                    break;
            }
        }

        private void HandleKeyboard()
        {
            if (Utils.Input.IsKeyPressed(Keys.Q))
            {
                SetBallType(Enums.BallTypes.Ice);
            }
            else if (Utils.Input.IsKeyPressed(Keys.W))
            {
                SetBallType(Enums.BallTypes.Fire);
            }
            else if (Utils.Input.IsKeyPressed(Keys.E))
            {
                SetBallType(Enums.BallTypes.Lightning);
            }
            else if (Utils.Input.IsKeyPressed(Keys.R))
            {
                SetBallType(Enums.BallTypes.Explosion);
            }
        }
    }
}
