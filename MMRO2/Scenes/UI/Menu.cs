using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Scenes.UI
{
	class Menu : Main.SceneComponent
	{
		public static Sprites.Rectangle Canvas;

		private bool _isMute = false;

		//Texture
		private Texture2D _canvasTex;

		private Texture2D _musicTex;
		private Texture2D _muteTex;
		private Texture2D _resumeTex;
		private Texture2D _replayTex;
		private Texture2D _TButtonTex;

		//Buttons
		private Sprites.Buttons _musicButton;
		private Sprites.Buttons _resumeButton;
		private Sprites.Buttons _replayButton;
		private Sprites.Buttons _homeButton;
		private Sprites.Buttons _quitButton;

		//Text Font
		private SpriteFont _font14;
		private SpriteFont _font20;

		private List<Sprites.Buttons> _buttonList;

		public Menu()
		{
			_buttonList = new List<Sprites.Buttons>();

			//Load Texture
			_font14 = _font14 = Global.Instance.Content.Load<SpriteFont>("fonts/font14");
			_font14 = _font20 = Global.Instance.Content.Load<SpriteFont>("fonts/font20");
			_canvasTex = Global.Instance.Content.Load<Texture2D>("images/ui/menu");

			_musicTex = Global.Instance.Content.Load<Texture2D>("images/icons/audio");
			_muteTex = Global.Instance.Content.Load<Texture2D>("images/icons/audio_mute");
			_resumeTex = Global.Instance.Content.Load<Texture2D>("images/icons/play");
			_replayTex = Global.Instance.Content.Load<Texture2D>("images/icons/restart");
			_TButtonTex = Global.Instance.Content.Load<Texture2D>("images/ui/menu_button");

			//Create Buttons
			Canvas = new Sprites.Rectangle(_canvasTex, 350, 400);
			Canvas.Origin = new Vector2(_canvasTex.Width / 2, _canvasTex.Height / 2);
			Canvas.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight);

			_musicButton = new Sprites.Buttons(_musicTex, _font14, "", 70, 70);
			_musicButton.Origin = new Vector2(_musicTex.Width / 2, _musicTex.Height / 2);
			_musicButton.Position = new Vector2(Settings.Window.HalfWidth - 100, Settings.Window.HalfHeight - 70);

			_resumeButton = new Sprites.Buttons(_resumeTex, _font14, "", 70, 70);
			_resumeButton.Origin = new Vector2(_resumeTex.Width / 2, _resumeTex.Height / 2);
			_resumeButton.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight - 70);

			_replayButton = new Sprites.Buttons(_replayTex, _font14, "", 70, 70);
			_replayButton.Origin = new Vector2(_replayTex.Width / 2, _replayTex.Height / 2);
			_replayButton.Position = new Vector2(Settings.Window.HalfWidth + 100, Settings.Window.HalfHeight - 70);

			_homeButton = new Sprites.Buttons(_TButtonTex, _font14, "HOME", 200, 50);
			_homeButton.Origin = new Vector2(_TButtonTex.Width / 2, _TButtonTex.Height / 2);
			_homeButton.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight + 40);

			_quitButton = new Sprites.Buttons(_TButtonTex, _font14, "QUIT", 200, 50);
			_quitButton.Origin = new Vector2(_TButtonTex.Width / 2, _TButtonTex.Height / 2);
			_quitButton.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight + 120);

			//Call Function
			_musicButton.Click += _musicButton_clicked;
			_resumeButton.Click += _resumeButton_clicked;
			_replayButton.Click += _replayButton_clicked;
			_homeButton.Click += _homeButton_clicked;
			_quitButton.Click += _quitButton_clicked;

			//Add to List
			_buttonList.Add(_musicButton);
			_buttonList.Add(_resumeButton);
			_buttonList.Add(_replayButton);
			_buttonList.Add(_homeButton);
			_buttonList.Add(_quitButton);
		}

		public override void Update()
		{
			if (Global.Instance.GameData.Paused)
			{
				foreach (var button in _buttonList)
				{
					button.Update();
				}
			}
		}

		public override void Draw()
		{
			if (Global.Instance.GameData.Paused)
			{
				Canvas.Draw();

				foreach (var button in _buttonList)
				{
					button.Draw();
				}

				Global.Instance.SpriteBatch.DrawString
				(
					_font20,
					"OPTIONS",
					new Vector2(Settings.Window.HalfWidth  - 65, Settings.Window.HalfHeight - 170),
					Color.White,
					0f,
					Vector2.Zero,
					1f,
					SpriteEffects.None,
					0f
				); ;
			}
		}

		public void _musicButton_clicked(object sender, EventArgs args)
		{
			if (!_isMute)
			{
				_isMute = true;
				_musicButton.setTexture(_muteTex);
			}
			else
			{
				_isMute = false;
				_musicButton.setTexture(_musicTex);
			}
		}

		public void _resumeButton_clicked(object sender, EventArgs args)
		{
			Global.Instance.GameData.Paused = false;
		}

		public void _replayButton_clicked(object sender, EventArgs args)
		{
			System.Diagnostics.Debug.WriteLine("replay");
			Global.Instance.GameData.Reset();
		}

		public void _homeButton_clicked(object sender, EventArgs args)
		{
			Utils.Scene.Control.ChangeScene(Enums.Scenes.Home);
		}

		public void _quitButton_clicked(object sender, EventArgs args)
		{
			Game1.Quit();
		}
	}
}
