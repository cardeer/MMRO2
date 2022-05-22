using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Scenes
{
	class SelectCards: Main.GameScene
	{
		private Texture2D _bgTex;
		private Texture2D _buttonTex;
		private Texture2D _card1Tex;
		private Texture2D _card2Tex;
		private Texture2D _card3Tex;

		private Sprites.Rectangle _background;

		private Sprites.Buttons _selectButton;
		private Sprites.Buttons _card1;
		private Sprites.Buttons _card2;
		private Sprites.Buttons _card3;

		private SpriteFont _font;

		private List<Sprites.Buttons> _buttonList;

		public SelectCards()
		{
			_buttonList = new List<Sprites.Buttons>();


			//Load Texture
			_bgTex = Global.Instance.Content.Load <Texture2D>("images/backgrounds/gacha");
			_buttonTex = Global.Instance.Content.Load<Texture2D>("images/ui/main_button");

			//Load Font
			_font = Global.Instance.Content.Load<SpriteFont>("fonts/font20");

			//Create Object
			_background = new Sprites.Rectangle(_bgTex, _bgTex.Width, _bgTex.Height);
			_background.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight);

			_selectButton = new Sprites.Buttons(_buttonTex, _font, "SELECT", _buttonTex.Width / 3, _buttonTex.Height / 3);
			_selectButton.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.Height - 100);
			_selectButton.setTextColor(Color.White);

			_card1 = new Sprites.Buttons(_buttonTex, _font, "", _buttonTex.Width / 3, _buttonTex.Height / 3);
			_card1.Position = new Vector2(Settings.Window.HalfWidth - 300, Settings.Window.HalfHeight);

			_card2 = new Sprites.Buttons(_buttonTex, _font, "", _buttonTex.Width / 3, _buttonTex.Height / 3);
			_card2.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight);

			_card3 = new Sprites.Buttons(_buttonTex, _font, "", _buttonTex.Width / 3, _buttonTex.Height / 3);
			_card3.Position = new Vector2(Settings.Window.HalfWidth + 300, Settings.Window.HalfHeight);

			_buttonList.Add(_selectButton);
			_buttonList.Add(_card1);
			_buttonList.Add(_card2);
			_buttonList.Add(_card3);

			_selectButton.Click += _selectButton_clicked;
			_card1.Click += _car1_clicked;
			_card2.Click += _car2_clicked;
			_card3.Click += _car3_clicked;
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

			_background.Draw();

			foreach (var button in _buttonList)
			{
				button.Draw();
			}

			EndSprite();
		}

		public void _selectButton_clicked(object sender, EventArgs args)
		{

		}

		public void _car1_clicked(object sender, EventArgs args)
		{

		}

		public void _car2_clicked(object sender, EventArgs args)
		{

		}

		public void _car3_clicked(object sender, EventArgs args)
		{

		}
	}
}
