using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using tainicom.Aether.Physics2D.Dynamics;
using Microsoft.Xna.Framework.Media;

namespace MMRO2.Scenes.UI
{
    class ResultBoard : Main.SceneComponent
    {
        private Texture2D _winTex;
        private Texture2D _loseTex;
        private Texture2D _buttonTex;

        private Sprites.Rectangle _winboard;
        private Sprites.Rectangle _loseboard;

        private Sprites.Buttons _replayButton;
        private Sprites.Buttons _quitButton;
        private Sprites.Buttons _continueButton;
        private Sprites.Buttons _homeButton;
        private Sprites.Buttons _winHomeButton;

        private SpriteFont _font;

        public ResultBoard()
        {
            //Load Content
            _font = Global.Instance.Content.Load<SpriteFont>("fonts/font20");

            _winTex = Global.Instance.Content.Load<Texture2D>("images/ui/Board_Win");
            _loseTex = Global.Instance.Content.Load<Texture2D>("images/ui/Board_Lose");

            _buttonTex = Global.Instance.Content.Load<Texture2D>("images/ui/main_button");


            //Create Object
            _winboard = new Sprites.Rectangle(_winTex, _winTex.Width / 2, _winTex.Height / 2);
            _winboard.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight - 60);

            _loseboard = new Sprites.Rectangle(_loseTex, _loseTex.Width / 2, _loseTex.Height / 2);
            _loseboard.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight - 60);

            _replayButton = new Sprites.Buttons(_buttonTex, _font, "REPLAY", _buttonTex.Width / 4, _buttonTex.Height / 4);
            _replayButton.Position = new Vector2(Settings.Window.HalfWidth - 120, Settings.Window.HalfHeight);
            _replayButton.setTextColor(Color.White);

            _homeButton = new Sprites.Buttons(_buttonTex, _font, "HOME", _buttonTex.Width / 4, _buttonTex.Height / 4);
            _homeButton.Position = new Vector2(Settings.Window.HalfWidth + 120, Settings.Window.HalfHeight);
            _homeButton.setTextColor(Color.White);

            _quitButton = new Sprites.Buttons(_buttonTex, _font, "QUIT", _buttonTex.Width / 4, _buttonTex.Height / 4);
            _quitButton.Position = new Vector2(Settings.Window.HalfWidth + 120, Settings.Window.HalfHeight);
            _quitButton.setTextColor(Color.White);

            _continueButton = new Sprites.Buttons(_buttonTex, _font, "CONTINUE", _buttonTex.Width / 4, _buttonTex.Height / 4);
            _continueButton.Position = new Vector2(Settings.Window.HalfWidth - 120, Settings.Window.HalfHeight);
            _continueButton.setTextColor(Color.White);

            _winHomeButton = new Sprites.Buttons(_buttonTex, _font, "HOME", _buttonTex.Width / 4, _buttonTex.Height / 4);
            _winHomeButton.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight);
            _winHomeButton.setTextColor(Color.White);

            _replayButton.Click += _replayButton_clicked;
            _homeButton.Click += _homeButton_clicked;

            _continueButton.Click += _continueButton_clicked;
            _quitButton.Click += _quitButton_clicked;

            _winHomeButton.Click += _homeButton_clicked;
        }

        public override void Update()
        {
            if (Global.Instance.GameData.Failed)
            {
                _replayButton.Update();
                _quitButton.Update();
            }
            else if (Global.Instance.GameData.BossDied)
            {
                if (Global.Instance.GameData.Wave == 9)
                {
                    _winHomeButton.Update();
                }
                else
                {
                     _homeButton.Update();
                    _continueButton.Update();
                }
            }
        }

        public override void Draw()
        {
            if (Global.Instance.GameData.Failed)
            {
                _loseboard.Draw();

                _replayButton.Draw(); 
                _quitButton.Draw();
                
            }
            else if (Global.Instance.GameData.BossDied)
            {
                _winboard.Draw();

                if (Global.Instance.GameData.Wave == 9)
                {
                    _winHomeButton.Draw();
                }
                else
                {
                    _continueButton.Draw();
                    _homeButton.Draw();
                }
            }
        }

        public void _replayButton_clicked(object sender, EventArgs args)
        {
            Global.Instance.GameData = Global.Instance.GameData.GetNew();
        }

        public void _homeButton_clicked(object sender, EventArgs args)
        {
            Utils.Scene.Control.ChangeScene(Enums.Scenes.Home);
            MediaPlayer.Volume = 0f;
        }

        public void _quitButton_clicked(object sender, EventArgs args)
        {
            Game1.Quit();
        }

        public void _continueButton_clicked(object sender, EventArgs args)
        {
            Global.Instance.GameData.Reset();
            Global.Instance.GameData.Wave++;
            Utils.Scene.Control.ChangeScene(Enums.Scenes.Gacha);
        }
    }
}
