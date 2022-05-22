using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Rect = Microsoft.Xna.Framework.Rectangle;

namespace MMRO2.Sprites
{
    class Buttons : Main.Sprite
    {
        private int _width, _height;
        private String _text;
        private Color _textColor = Color.Black;
        private SpriteFont _font;
        private Color _color = Color.White;

        public EventHandler Click;

        private Vector2 _originScale;

        public bool Disabled = false;

        public Buttons(Texture2D texture, SpriteFont font, String text, int width, int height) : base(texture)
        {
            _width = width;
            _height = height;
            Origin = new Vector2(texture.Width / 2, texture.Height / 2);
            _originScale = Origin / new Vector2(texture.Width, texture.Height);

            _font = font;
            _text = text;
        }

        private Rect _rectangle
        {
            get { return new Rect((int)(Position.X - _width * _originScale.X), (int)(Position.Y - _height * _originScale.Y), _width, _height); }
        }

        private bool _isHovering
        {
            get
            {
                var mouseRectangle = new Rect(Global.Instance.CurrentMouseState.X, Global.Instance.CurrentMouseState.Y, 1, 1);
                return _rectangle.Intersects(mouseRectangle);
            }
        }

        public override void Update()
        {
            bool hovering = _isHovering;

            if (hovering)
            {
                if (Utils.Input.IsLeftMouseClicked())
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        public void setTexture(Texture2D texture)
        {
            base.Texture = texture;
        }

        public void setTextColor(Color color)
        {
            _textColor = color;
        }

        public override void Draw()
        {
            if (_isHovering || Disabled)
            {
                _color = Color.Gray;
            }
            else _color = Color.White;

            Global.Instance.SpriteBatch.Draw(
                Texture,
                Position,
                null,
                _color,
                Rotation,
                Origin,
                new Vector2(_width, _height) / TextureSize,
                SpriteEffects.None,
                0f
            );

            if (_text != null)
            {
                Vector2 size = _font.MeasureString(_text);
                Global.Instance.SpriteBatch.DrawString
                (
                    _font,
                    _text,
                    Position,
                    _textColor,
                    0f,
                    size / 2,
                    0.75f,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
