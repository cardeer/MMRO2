using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Scenes.UI
{
	class SelectCards: Main.SceneComponent
	{
		private Texture2D _bgTex;
		private Texture2D _buttonTex;

		private Sprites.Rectangle _background;

		private Sprites.Buttons _selectButton;

		private SpriteFont _font;

		public SelectCards()
		{
			//Load Texture
			_bgTex = Global.Instance.Content.Load <Texture2D>("images/backgrounds/gacha");
			_buttonTex = Global.Instance.Content.Load<Texture2D>("images/ui/main_button");

			//Load Font
			_font = Global.Instance.Content.Load<SpriteFont>("fonts/font20");


			//Create Object
			_background = new Sprites.Rectangle(_bgTex, _bgTex.Width, _bgTex.Height);
			_background.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight);


		}

		public override void Update()
		{
			
		}

		public override void Draw()
		{
			_background.Draw();
		}
	}
}
