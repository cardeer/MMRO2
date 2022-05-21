using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MMRO2.Controllers
{
    public class Animation
    {
        public Texture2D Texture;

        private int _partsX;

        public int FrameWidth;
        public int FrameHeight;
        public Vector2 FrameSize;

        public int Frames;

        public int CurrentFrame = 0;

        public int FrameX = 0;
        public int FrameY = 0;

        public float AnimationSpeed = 0.1f;
        private float _accumulatedSeconds = 0;

        public Animation(Texture2D texture, int partsX, int partsY = 1)
        {
            if (partsX <= 0 || partsY <= 0)
            {
                throw new Exception("partsX and partsY must be greater than zero");
            }

            Texture = texture;
            _partsX = partsX;

            FrameWidth = Texture.Width / partsX;
            FrameHeight = Texture.Height / partsY;
            FrameSize = new Vector2(FrameWidth, FrameHeight);

            Frames = partsX * partsY;
        }

        public void Update()
        {
            if (CurrentFrame > Frames - 1)
            {
                CurrentFrame = 0;
            }

            int x = CurrentFrame % _partsX;
            int y = CurrentFrame / _partsX;

            FrameX = x * FrameWidth;
            FrameY = y * FrameHeight;

            if (_accumulatedSeconds >= AnimationSpeed)
            {
                _accumulatedSeconds = 0;
                CurrentFrame++;
            }
            else
            {
                _accumulatedSeconds += (float)Global.Instance.GameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Reset()
        {
            _accumulatedSeconds = 0;
            CurrentFrame = 0;
        }
    }
}
