using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Scenes.UI
{
	class Hamburger : Main.SceneComponent
	{
		public UI.Menu Menu;

		private Texture2D _menuButtonTex;

		private Sprites.Buttons _menuButton;

		private SpriteFont _font;

		public Hamburger() 
		{
			Menu = new UI.Menu();

			_menuButtonTex = Global.Instance.Content.Load<Texture2D>("images/icons/hamburger");

			_font = Global.Instance.Content.Load<SpriteFont>("fonts/font14");

			_menuButton = new Sprites.Buttons(_menuButtonTex, _font, "", 50, 50);
			_menuButton.Position = new Vector2(Settings.Window.Width - 80, Settings.Window.Height - 650);
			_menuButton.Click += _menuButton_clicked;
		}

		public override void Update()
		{
			_menuButton.Update();
			Menu.Update();
		}

		public override void Draw()
		{
			_menuButton.Draw();
			Menu.Draw();
		}

		public void _menuButton_clicked(object sender, EventArgs args)
		{
			Global.Instance.GameData.Paused = true;
		}
	}
}
