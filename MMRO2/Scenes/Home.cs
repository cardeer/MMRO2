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

		private Texture2D _playTex;
		private Texture2D _quitTex;

		private Sprites.Rectangle _background;
		private Sprites.Rectangle _logo;

		private Sprites.Buttons _playButton;
		private Sprites.Buttons _quitButton;

		private List<Sprites.Rectangle> _recList;
		private List<Sprites.Buttons> _buttonList;

		public override void Initialize()
		{
			base.Initialize();
			_recList = new List<Sprites.Rectangle>();
			_buttonList = new List<Sprites.Buttons>();

			//Load Texture
			_backgroundTex = Global.Instance.Content.Load<Texture2D>("images/backgrounds/main");
			_logoTex = Global.Instance.Content.Load<Texture2D>("images/logo/logo_2");

			//_playTex = Global.Instance.Content.Load<Texture2D>("images/ui/");
			//_quitTex = Global.Instance.Content.Load<Texture2D>("images/ui/");

			_background = new Sprites.Rectangle(_backgroundTex, _backgroundTex.Width, _backgroundTex.Height);
			_background.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight);

			_logo = new Sprites.Rectangle(_logoTex, (int)(_logoTex.Width / 1.25), (int)(_logoTex.Height / 1.25));
			_logo.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight - 150);

			_recList.Add(_background);
			_recList.Add(_logo);


		}

		public override void Update()
		{/*
			foreach (var button in _buttonList)
			{
				button.Update();
			}*/
		}

		public override void Draw()
		{
			BeginSprite();
			foreach (var rectangle in _recList)
			{
				rectangle.Draw();
			}
			EndSprite();
			/*
			foreach (var button in _buttonList)
			{
				button.Draw();
			}*/
		}
	}
}
