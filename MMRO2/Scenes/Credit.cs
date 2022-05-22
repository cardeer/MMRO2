using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MMRO2.Scenes
{
	class Credit : Main.GameScene
	{
		private Texture2D _backgroundTex;

		private Texture2D _buttonTex;

		private SpriteFont _font;

		private Sprites.Buttons _backButton;

		private Sprites.Rectangle _background;

		public override void Initialize()
		{
			base.Initialize();

			//Load Font
			_font = Global.Instance.Content.Load<SpriteFont>("fonts/font20");

			//Load Texture
			_backgroundTex = Global.Instance.Content.Load<Texture2D>("images/backgrounds/main");

			_buttonTex = Global.Instance.Content.Load<Texture2D>("images/ui/main_button");

			//Create Object
			_background = new Sprites.Rectangle(_backgroundTex, _backgroundTex.Width, _backgroundTex.Height);
			_background.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight);

			_backButton = new Sprites.Buttons(_buttonTex, _font, "  BACK", _buttonTex.Width / 3, _buttonTex.Height / 3);
			_backButton.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.Height - 100);
			_backButton.setTextColor(Color.White);

			_backButton.Click += _backButton_clicked;
		}

		public override void Update()
		{
			_backButton.Update();
		}

		public override void Draw()
		{
			BeginSprite();
			_background.Draw();
			_backButton.Draw();
			EndSprite();
		}

		public void _backButton_clicked(object sender, EventArgs args)
		{
			Utils.Scene.Control.ChangeScene(Enums.Scenes.Home);
		}
	}
}
