using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MMRO2.Scenes
{
	class Home : Main.GameScene
	{
		private Texture2D _backgroundTex;
		private Texture2D _logoTex;

		private Texture2D _buttonTex;

		private SpriteFont _font;

		private Sprites.Rectangle _background;
		private Sprites.Rectangle _logo;

		private Sprites.Buttons _playButton;
		private Sprites.Buttons _storyButton;
		private Sprites.Buttons _creditButton;
		private Sprites.Buttons _quitButton;

		private List<Sprites.Rectangle> _recList;
		private List<Sprites.Buttons> _buttonList;

		public override void Initialize()
		{
			base.Initialize();
			_recList = new List<Sprites.Rectangle>();
			_buttonList = new List<Sprites.Buttons>();

			//Load Font
			_font = Global.Instance.Content.Load<SpriteFont>("fonts/font20");

			//Load Texture
			_backgroundTex = Global.Instance.Content.Load<Texture2D>("images/backgrounds/main");
			_logoTex = Global.Instance.Content.Load<Texture2D>("images/logo/logo_2");

			_buttonTex = Global.Instance.Content.Load<Texture2D>("images/ui/main_button");

			//Create Object
			_background = new Sprites.Rectangle(_backgroundTex, _backgroundTex.Width, _backgroundTex.Height);
			_background.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight);

			_logo = new Sprites.Rectangle(_logoTex, (int)(_logoTex.Width / 1.25), (int)(_logoTex.Height / 1.25));
			_logo.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight - 150);

			_playButton = new Sprites.Buttons(_buttonTex, _font, "PLAY", _buttonTex.Width / 3, _buttonTex.Height / 3);
			_playButton.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight + 50);
			_playButton.setTextColor(Color.White);

			_storyButton = new Sprites.Buttons(_buttonTex, _font, "STORY", _buttonTex.Width / 3, _buttonTex.Height / 3);
			_storyButton.Position = new Vector2(Settings.Window.HalfWidth, _playButton.Position.Y + 80);
			_storyButton.setTextColor(Color.White);

			_creditButton = new Sprites.Buttons(_buttonTex, _font, "CREDIT", _buttonTex.Width / 3, _buttonTex.Height / 3);
			_creditButton.Position = new Vector2(Settings.Window.HalfWidth, _storyButton.Position.Y + 80);
			_creditButton.setTextColor(Color.White);

			_quitButton = new Sprites.Buttons(_buttonTex, _font, "QUIT", _buttonTex.Width / 3, _buttonTex.Height / 3);
			_quitButton.Position = new Vector2(Settings.Window.HalfWidth, _creditButton.Position.Y + 80);
			_quitButton.setTextColor(Color.White);

			_playButton.Click += _playButton_clicked;
			_storyButton.Click += _storyButton_clicked;
			_creditButton.Click += _creditButton_clicked;
			_quitButton.Click += _quitButton_clicked;

			_recList.Add(_background);
			_recList.Add(_logo);

			_buttonList.Add(_playButton);
			_buttonList.Add(_storyButton);
			_buttonList.Add(_creditButton);
			_buttonList.Add(_quitButton);
		}

		public override void Update()
		{
			foreach (var button in _buttonList)
			{
				button.Update();
			}
		}

		public override void Draw()
		{
			BeginSprite();

			foreach (var rectangle in _recList)
			{
				rectangle.Draw();
			}

			foreach (var button in _buttonList)
			{
				button.Draw();
			}

			EndSprite();
		}

		public void _playButton_clicked(object sender, EventArgs args)
		{
			Global.Instance.GameData = new GameData();
			Utils.Scene.Control.ChangeScene(Enums.Scenes.Playing);
		}

		public void _storyButton_clicked(object sender, EventArgs args)
		{
			Utils.Scene.Control.ChangeScene(Enums.Scenes.Story);
		}

		public void _creditButton_clicked(object sender, EventArgs args)
		{
			Utils.Scene.Control.ChangeScene(Enums.Scenes.Credit);
		}

		public void _quitButton_clicked(object sender, EventArgs args)
		{
			Game1.Quit();
		}
	}
}
