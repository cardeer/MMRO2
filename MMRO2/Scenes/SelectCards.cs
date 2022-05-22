using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Scenes
{
    class SelectCards : Main.GameScene
    {
        private Texture2D _bgTex;
        private Texture2D _buttonTex;
        private Texture2D _card1Tex;
        private Texture2D _card2Tex;
        private Texture2D _card3Tex;

        private Sprites.Rectangle _background;

        private Sprites.Buttons _card1;
        private Sprites.Buttons _card2;
        private Sprites.Buttons _card3;

        private SpriteFont _font;

        private List<Sprites.Buttons> _buttonList;

        private int[,] _cards = Utils.Gameplay.RandomUpgradePerk();

        public SelectCards()
        {
            _buttonList = new List<Sprites.Buttons>();

            //_cards[0, 0] = 1;
            //_cards[0, 1] = 1;

            Texture2D[] cardTextures = new Texture2D[] {
                Global.Instance.Content.Load<Texture2D>($"images/cards/{_cards[0, 0]}_{_cards[0, 1]}"),
                Global.Instance.Content.Load<Texture2D>($"images/cards/{_cards[1, 0]}_{_cards[1, 1]}"),
                Global.Instance.Content.Load<Texture2D>($"images/cards/{_cards[2, 0]}_{_cards[2, 1]}"),
            };

            //Load Texture
            _bgTex = Global.Instance.Content.Load<Texture2D>("images/backgrounds/gacha");
            _buttonTex = Global.Instance.Content.Load<Texture2D>("images/ui/main_button");

            //Load Font
            _font = Global.Instance.Content.Load<SpriteFont>("fonts/font20");

            //Create Object
            _background = new Sprites.Rectangle(_bgTex, _bgTex.Width, _bgTex.Height);
            _background.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight);

            _card1 = new Sprites.Buttons(cardTextures[0], _font, "", cardTextures[0].Width / 3, cardTextures[0].Height / 3);
            _card1.Position = new Vector2(Settings.Window.HalfWidth - 300, Settings.Window.HalfHeight);

            _card2 = new Sprites.Buttons(cardTextures[1], _font, "", cardTextures[1].Width / 3, cardTextures[1].Height / 3);
            _card2.Position = new Vector2(Settings.Window.HalfWidth, Settings.Window.HalfHeight);

            _card3 = new Sprites.Buttons(cardTextures[2], _font, "", cardTextures[2].Width / 3, cardTextures[2].Height / 3);
            _card3.Position = new Vector2(Settings.Window.HalfWidth + 300, Settings.Window.HalfHeight);

            _buttonList.Add(_card1);
            _buttonList.Add(_card2);
            _buttonList.Add(_card3);

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

        private void ChangeScene(Enums.Perks perk)
        {
            Global.Instance.GameData.Reset();

            Global.Instance.GameData.Perks[perk]++;
            Global.Instance.GameData.UpdatePerks();

            Utils.Scene.Control.ChangeScene(Enums.Scenes.Playing);
        }

        public void _car1_clicked(object sender, EventArgs args)
        {
            ChangeScene((Enums.Perks)_cards[0, 0]);
        }

        public void _car2_clicked(object sender, EventArgs args)
        {
            ChangeScene((Enums.Perks)_cards[1, 0]);
        }

        public void _car3_clicked(object sender, EventArgs args)
        {
            ChangeScene((Enums.Perks)_cards[2, 0]);
        }
    }
}
