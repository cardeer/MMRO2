using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Scenes.UI
{
    class PlayerStats : Main.SceneComponent
    {
        //Player State Texture
        private Texture2D _playerTex;
        private Texture2D _hpTex;
        private Texture2D _barTex;
        private Texture2D _manaTex;

        private Texture2D _stateBGTex;

        //Skill icon Texture
        private Texture2D _iceBallTex;
        private Texture2D _fireBallTex;
        private Texture2D _lightningBallTex;
        private Texture2D _explosionTex;

        private List<Sprites.Rectangle> _stateBarList;
        private List<Sprites.Buttons> _buttonList;

        //State Bar
        private Sprites.Rectangle _hpBar;
        private Sprites.Rectangle _hpBarValue;

        private Sprites.Rectangle _manaBar;
        private Sprites.Rectangle _manaBarValue;

        //Skill Buttons
        private Sprites.Buttons _iceButton;
        private Sprites.Buttons _fireButton;
        private Sprites.Buttons _lightningButton;
        private Sprites.Buttons _explosionButton;

        //Font
        private SpriteFont _font;

        public PlayerStats()
        {
            //Load Texture
            _stateBGTex = Global.Instance.Content.Load<Texture2D>("images/ui/stats_background");
            _playerTex = Global.Instance.Content.Load<Texture2D>("images/ui/player");
            _hpTex = Global.Instance.Content.Load<Texture2D>("images/ui/heart");
            _manaTex = Global.Instance.Content.Load<Texture2D>("images/ui/mana_stroke");

            _iceBallTex = Global.Instance.Content.Load<Texture2D>("images/skills/ice");
            _fireBallTex = Global.Instance.Content.Load<Texture2D>("images/skills/fire");
            _lightningBallTex = Global.Instance.Content.Load<Texture2D>("images/skills/lightning");
            _explosionTex = Global.Instance.Content.Load<Texture2D>("images/skills/explosion");

            //Load Font
            _font = Global.Instance.Content.Load<SpriteFont>("fonts/font14");

            _stateBarList = new List<Sprites.Rectangle>();
            _buttonList = new List<Sprites.Buttons>();

            var BG = new Sprites.Rectangle(_stateBGTex, _stateBGTex.Width, _stateBGTex.Height);
            BG.Origin = new Vector2(0, _stateBGTex.Height);
            BG.Position = new Vector2(30, Settings.Window.Height - 30);

            var player = new Sprites.Rectangle(_playerTex, _playerTex.Width, _playerTex.Height);
            player.Origin = new Vector2(_playerTex.Width / 2, _playerTex.Height / 2);
            player.Position = new Vector2(150, Settings.Window.Height - 120);

            var hp = new Sprites.Rectangle(_hpTex, _hpTex.Width - 50, _hpTex.Height - 50);
            hp.Origin = new Vector2(_hpTex.Width / 2, _hpTex.Height / 2);
            hp.Position = new Vector2(player.Position.X + 100, player.Position.Y - 20);

            var mana = new Sprites.Rectangle(_manaTex, _manaTex.Width / 5, _manaTex.Height / 5);
            mana.Origin = new Vector2(_manaTex.Width / 2, _manaTex.Height / 2);
            mana.Position = new Vector2(player.Position.X + 100, player.Position.Y + 30);

            _barTex = Utils.Sprite.Factory.CreateRectangle(150, 25, Color.White);

            _hpBar = new Sprites.Rectangle(_barTex, _barTex.Width, _barTex.Height);
            _hpBar.DrawColor = Color.Gray;
            _hpBar.Origin = new Vector2(0, _barTex.Height / 2);
            _hpBar.Position = new Vector2(hp.Position.X + 30, hp.Position.Y);

            _hpBarValue = new Sprites.Rectangle(_barTex, _barTex.Width, _barTex.Height);
            _hpBarValue.DrawColor = Color.Red;
            _hpBarValue.Origin = new Vector2(0, _barTex.Height / 2);
            _hpBarValue.Position = new Vector2(_hpBar.Position.X, _hpBar.Position.Y);

            _manaBar = new Sprites.Rectangle(_barTex, _barTex.Width, _barTex.Height);
            _manaBar.DrawColor = Color.Gray;
            _manaBar.Origin = new Vector2(0, _barTex.Height / 2);
            _manaBar.Position = new Vector2(mana.Position.X + 30, mana.Position.Y);

            _manaBarValue = new Sprites.Rectangle(_barTex, _barTex.Width, _barTex.Height);
            _manaBarValue.DrawColor = Color.Blue;
            _manaBarValue.Origin = new Vector2(0, _barTex.Height / 2);
            _manaBarValue.Position = new Vector2(_manaBar.Position.X, _manaBar.Position.Y);

            //Skill Button
            _iceButton = new Sprites.Buttons(_iceBallTex, _font, "", _iceBallTex.Width / 25, _iceBallTex.Height / 25);
            _iceButton.Origin = new Vector2(_iceBallTex.Width / 2, _iceBallTex.Height / 2);
            _iceButton.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.Height - 120);

            _fireButton = new Sprites.Buttons(_fireBallTex, _font, "", _fireBallTex.Width / 25, _fireBallTex.Height / 25);
            _fireButton.Origin = new Vector2(_fireBallTex.Width / 2, _fireBallTex.Height / 2);
            _fireButton.Position = new Vector2(_iceButton.Position.X + 70, _iceButton.Position.Y);

            _lightningButton = new Sprites.Buttons(_lightningBallTex, _font, "", _lightningBallTex.Width / 25, _lightningBallTex.Height / 25);
            _lightningButton.Origin = new Vector2(_lightningBallTex.Width / 2, _lightningBallTex.Height / 2);
            _lightningButton.Position = new Vector2(_fireButton.Position.X + 70, _fireButton.Position.Y);

            _explosionButton = new Sprites.Buttons(_explosionTex, _font, "", _explosionTex.Width / 25, _explosionTex.Height / 25);
            _explosionButton.Origin = new Vector2(_explosionTex.Width / 2, _explosionTex.Height / 2);
            _explosionButton.Position = new Vector2(_lightningButton.Position.X + 70, _lightningButton.Position.Y);

            //Call Function
            _iceButton.Click += iceButton_clicked;
            _fireButton.Click += fireButton_clicked;
            _lightningButton.Click += lightningButton_clicked;
            _explosionButton.Click += explosionButton_clicked;


            _stateBarList.Add(BG);
            _stateBarList.Add(player);
            _stateBarList.Add(hp);
            _stateBarList.Add(mana);
            _stateBarList.Add(_hpBar);
            _stateBarList.Add(_hpBarValue);
            _stateBarList.Add(_manaBar);
            _stateBarList.Add(_manaBarValue);

            _buttonList.Add(_iceButton);
            _buttonList.Add(_fireButton);
            _buttonList.Add(_lightningButton);
            _buttonList.Add(_explosionButton);

        }

        public override void Update()
        {
            _hpBarValue.Scale = new Vector2(Global.Instance.GameData.PlayerHP / Global.Instance.GameData.PlayerMaxHP, 1);
            _manaBarValue.Scale = new Vector2(Global.Instance.GameData.PlayerMana / Global.Instance.GameData.PlayerMaxMana, 1);

            foreach (var button in _buttonList)
            {
                button.Update();
            }
        }

        public override void Draw()
        {
            foreach (var ability in _stateBarList)
            {
                ability.Draw();
            }

            foreach (var button in _buttonList)
            {
                button.Draw();
            }

            string str = $"{Global.Instance.GameData.PlayerHP} / {Global.Instance.GameData.PlayerMaxHP}";
            Vector2 stringSize = _font.MeasureString(str);

            Global.Instance.SpriteBatch.DrawString(
                _font,
                str,
                _hpBar.Position + new Vector2(_hpBar.Width + 10, 0),
                Color.Black,
                0f,
                new Vector2(0, stringSize.Y / 2),
                1f,
                SpriteEffects.None,
                0f
            );

            str = $"{Global.Instance.GameData.PlayerMana} / {Global.Instance.GameData.PlayerMaxMana}";
            stringSize = _font.MeasureString(str);

            Global.Instance.SpriteBatch.DrawString(
                _font,
                str,
                _manaBar.Position + new Vector2(_manaBar.Width + 10, 0),
                Color.Black,
                0f,
                new Vector2(0, stringSize.Y / 2),
                1f,
                SpriteEffects.None,
                0f
            );
        }

        public void iceButton_clicked(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("iceBall");
        }

        public void fireButton_clicked(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("fireBall");
        }

        public void lightningButton_clicked(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("lightningBall");
        }

        public void explosionButton_clicked(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("explosion");
        }
    }
}
