using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tainicom.Aether.Physics2D.Dynamics;

namespace MMRO2.Scenes
{
    class Playing : Main.PhysicsScene
    {
        private SpriteFont _font = Global.Instance.Content.Load<SpriteFont>("fonts/font14");
        private Sprites.Rectangle _BG;
        private List<Main.SceneComponent> _ui;

        private Texture2D _bgForestTexture;
        private Texture2D _bgHalloweenTexture;
        private Texture2D _bgWinterTexture;

        private Texture2D _groundTex;
        private Texture2D _towerTex;
        private Texture2D _standTex;

        private Sprites.Tile _stand;

        private Sprites.Player.Player _player;
        private List<Sprites.Tile> _map;
        private List<Body> _edges;
        private Texture2D _pauseBackgroundTexture;

        private UI.Hamburger _hamburger = new UI.Hamburger();

        private float _borderWidth;
        private float _abilityTop;

        private float _accumulatedSeconds = 0f;

        private Random _random = new Random();
        private int _spawnSeconds;

        public override void Initialize()
        {
            base.Initialize();

            Global.Instance.GameData.World = World;
            Global.Instance.GameData.Camera = Camera;

            _ui = new List<Main.SceneComponent>();
            _ui.Add(new UI.Frame());
            _ui.Add(new UI.PlayerStats());

            //Load BG
            _bgForestTexture = Global.Instance.Content.Load<Texture2D>("images/backgrounds/forest");
            _bgHalloweenTexture = Global.Instance.Content.Load<Texture2D>("images/backgrounds/halloween");
            _bgWinterTexture = Global.Instance.Content.Load<Texture2D>("images/backgrounds/winter");

            _groundTex = Utils.Sprite.Factory.CreateRectangle(1, 1, new Color(0, 0, 0, 0));
            _towerTex = Global.Instance.Content.Load<Texture2D>("images/map_objects/tower");
            _standTex = Global.Instance.Content.Load<Texture2D>("images/map_objects/base");

            _BG = new Sprites.Rectangle(_bgForestTexture, Settings.Window.Width, Settings.Window.Height);
            _BG.Position = new Vector2(Settings.Window.Width / 2, _bgForestTexture.Height / 2);

            _borderWidth = Camera.ConvertWidthToWorld(30);
            _abilityTop = Camera.ConvertHeightToWorld(Math.Abs(Settings.Window.Height / 2 - Settings.UI.AbilityTop));

            Camera.Position = new Vector3(Camera.Width / 2 - _borderWidth, _abilityTop, 0);

            _edges = new List<Body>()
            {
                World.CreateEdge(new Vector2(0, 0), new Vector2(0, Camera.Height)),
                World.CreateEdge(new Vector2(Settings.Gameplay.PlayerBasePosition, 0), new Vector2(Settings.Gameplay.PlayerBasePosition, Camera.Height))
            };

            foreach (var edge in _edges)
            {
                edge.Tag = Settings.Collision.Edge;
                edge.BodyType = BodyType.Static;
            }

            _player = new Sprites.Player.Player(World);

            _map = new List<Sprites.Tile>();

            var ground = new Sprites.Tile(World, _groundTex, Settings.Collision.Ground, Camera.Width - _borderWidth * 2, 1f, 1, 1);
            ground.Position = new Vector2(ground.TotalWidth / 2, ground.TotalHeight / 2);
            _map.Add(ground);

            var tower = new Sprites.Tile(World, _towerTex, Settings.Collision.Tower, 3f, 7.5f, 1, 1);
            tower.Position = new Vector2(Settings.Gameplay.PlayerBasePosition - tower.TotalWidth / 2, tower.TotalHeight / 2);
            _map.Add(tower);

            _player.Body.Position = new Vector2(tower.Position.X / 2, tower.Position.Y + 2);

            _stand = new Sprites.Tile(World, _standTex, Settings.Collision.Tower, 3f, 4f, 1, 1);
            _stand.Position = _player.Position + new Vector2(0, -_player.Height / 2 - _stand.Height / 2);
            _map.Add(_stand);


            _pauseBackgroundTexture = Utils.Sprite.Factory.CreateRectangle(Settings.Window.Width, Settings.Window.Height, Color.Black);

            _spawnSeconds = _random.Next(2, 5);
        }

        public void HandleCamera()
        {
            KeyboardState state = Keyboard.GetState();

            float totalX = 0;

            if (state.IsKeyDown(Keys.A))
            {
                totalX -= 20;
            }
            if (state.IsKeyDown(Keys.D))
            {
                totalX += 20;
            }

            Camera.Position.X += totalX * (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;

            if (Camera.Position.X < Camera.Width / 2 - _borderWidth)
            {
                Camera.Position.X = Camera.Width / 2 - _borderWidth;
            }
        }

        public override void Update()
        {
            //HandleCamera();

            if (Global.Instance.GameData.Failed || Global.Instance.GameData.BossDied) return;

            _hamburger.Update();

            if (Global.Instance.GameData.Paused) return;

            foreach (var ui in _ui)
            {
                ui.Update();
            }

            _player.Update();

            foreach (var monster in Global.Instance.GameData.Monsters)
            {
                monster.Update();

                if (monster.ShouldRemove)
                {
                    World.Remove(monster.Body);
                }
            }

            Global.Instance.GameData.Monsters.RemoveAll(e => e.ShouldRemove);

            if (_accumulatedSeconds >= _spawnSeconds)
            {
                _accumulatedSeconds = 0;
                _spawnSeconds = _random.Next(5, 10);

                int monsterCount = Settings.Gameplay.MonsterCount[Global.Instance.GameData.Wave - 1];

                if (Global.Instance.GameData.EnemiesSpawned >= monsterCount)
                {
                    if (Global.Instance.GameData.Wave % 3 == 0 && !Global.Instance.GameData.BossSpawned && Global.Instance.GameData.Monsters.Count == 0)
                    {
                        var boss = Utils.Gameplay.GetBoss(World, Global.Instance.GameData.Wave);
                        boss.Body.Position = new Vector2(Camera.Width - _borderWidth - 2f, boss.Height / 2 + boss.Offset);
                        Global.Instance.GameData.BossSpawned = true;
                        Global.Instance.GameData.Monsters.Add(boss);
                    }
                    else if (Global.Instance.GameData.Wave % 3 != 0 && Global.Instance.GameData.Monsters.Count == 0)
                    {
                        Global.Instance.GameData.Reset();
                        Global.Instance.GameData.BossDied = true;
                    }
                }
                else if (Global.Instance.GameData.EnemiesSpawned < monsterCount)
                {
                    var m1 = Utils.Gameplay.RandomMonster(World);
                    m1.Body.Position = new Vector2(Camera.Width - _borderWidth - 2f, m1.Height / 2 + m1.Offset);
                    Global.Instance.GameData.Monsters.Add(m1);

                    Global.Instance.GameData.EnemiesSpawned++;
                }
            }
            else
            {
                _accumulatedSeconds += (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;
            }

            foreach (var effect in Global.Instance.GameData.Effects)
            {
                effect.Update();
            }

            Global.Instance.GameData.Effects.RemoveAll(e => e.Time >= .1);

            if (Global.Instance.GameData.PlayerHP <= 0)
            {
                Global.Instance.GameData.Failed = true;
            }
            else if (Global.Instance.GameData.BossDied)
            {

            }

            base.Update();
        }

        public override void Draw()
        {
            BeginSprite();
            _BG.Draw();
            EndSprite();

            BeginSprite(true);

            foreach (var obj in _map)
            {
                obj.Draw();
            }

            _player.Draw();

            foreach (var monster in Global.Instance.GameData.Monsters)
            {
                monster.Draw();
            }
            EndSprite();

            BeginSprite();

            foreach (var ui in _ui)
            {
                ui.Draw();
            }

            if (Global.Instance.GameData.Paused)
            {
                Global.Instance.SpriteBatch.Draw(
                    _pauseBackgroundTexture,
                    Vector2.Zero,
                    null,
                    new Color(255, 255, 255, 150),
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f
                );
            }

            _hamburger.Draw();

            if (Global.Instance.GameData.Failed)
            {
                Vector2 size = _font.MeasureString("YOU LOSE");
                Global.Instance.SpriteBatch.DrawString(
                    _font,
                    "YOU LOSE",
                    new Vector2(Settings.Window.Width / 2, Settings.Window.Height / 2),
                    Color.White,
                    0f,
                    size / 2,
                    1f,
                    SpriteEffects.None,
                    0f
                );
            }
            else if (Global.Instance.GameData.BossDied)
            {
                Vector2 size = _font.MeasureString("YOU WIN");
                Global.Instance.SpriteBatch.DrawString(
                    _font,
                    "YOU WIN",
                    new Vector2(Settings.Window.Width / 2, Settings.Window.Height / 2),
                    Color.White,
                    0f,
                    size / 2,
                    1f,
                    SpriteEffects.None,
                    0f
                );
            }
            EndSprite();

            base.Draw();
        }


    }
}
